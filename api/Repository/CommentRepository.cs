using api.Data;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(ApplicationDbContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            var entity = await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (entityToDelete == null)
            {
                return false;
            }

            _context.Comments.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return _context.Comments.FirstOrDefault(x =>x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
