using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Common.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllPostsAsync();
        Task<BlogPostDto?> GetPostByIdAsync(Guid id);
        Task<BlogPostDto> CreatePostAsync(Guid authorId, string title, string content);
    }
}
