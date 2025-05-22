using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IApplicationDbContext _context;

        public CommentService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _context.Comments
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .ToListAsync();

            return comments.Select(MapToDto);
        }

        public async Task<CommentDto> AddCommentAsync(Guid postId, Guid authorId, string text)
        {
            var comment = new Comment
            {
                PostId = postId,
                AuthorId = authorId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(default);

            return MapToDto(comment);
        }

        private CommentDto MapToDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                Author = new UserDto
                {
                    Id = comment.Author.Id,
                    Username = comment.Author.Username,
                    FullName = comment.Author.FullName,
                    Email = comment.Author.Email,
                    Role = comment.Author.Role.ToString()
                }
            };
        }
    }
}
