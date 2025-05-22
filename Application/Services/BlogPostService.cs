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
    public class BlogPostService : IBlogPostService
    {
        private readonly IApplicationDbContext _context;

        public BlogPostService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllPostsAsync()
        {
            var posts = await _context.BlogPosts
                .Include(p => p.Author)
                .ToListAsync();

            return posts.Select(MapToDto);
        }

        public async Task<BlogPostDto?> GetPostByIdAsync(Guid id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);

            return post == null ? null : MapToDto(post);
        }

        public async Task<BlogPostDto> CreatePostAsync(Guid authorId, string title, string content)
        {
            var post = new BlogPost
            {
                AuthorId = authorId,
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync(default);

            return MapToDto(post);
        }

        private BlogPostDto MapToDto(BlogPost post)
        {
            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Author = new UserDto
                {
                    Id = post.Author.Id,
                    Username = post.Author.Username,
                    FullName = post.Author.FullName,
                    Email = post.Author.Email,
                    Role = post.Author.Role.ToString()
                }
            };
        }
    }
}
