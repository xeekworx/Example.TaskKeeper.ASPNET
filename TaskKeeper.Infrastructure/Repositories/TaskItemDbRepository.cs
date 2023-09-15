using Microsoft.EntityFrameworkCore;
using TaskKeeper.Domain;
using TaskKeeper.Persistence.Data;

namespace TaskKeeper.Persistence.Repositories
{
    public class TaskItemDbRepository : ITaskItemRepository
    {
        private readonly TaskKeeperDbContext _context;

        public TaskItemDbRepository(TaskKeeperDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.TaskItems.CountAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<List<TaskItem>> SearchAsync(string? title = default, bool exactTitle = false, bool? isCompleted = default)
        {
            IQueryable<TaskItem> query = _context.TaskItems;

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(t => exactTitle
                    ? t.Title.Equals(title) 
                    : t.Title.Contains(title)
                );
            }

            if (isCompleted != default)
            {
                query = query.Where(t => t.IsCompleted == isCompleted);
            }

            return await query
                .ToListAsync();
        }

        public async Task AddAsync(TaskItem item)
        {
            await _context.TaskItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateByNameAsync(TaskItem taskItem)
        {
            var existingItem = await _context.TaskItems.
                SingleAsync(item => item.Title.Equals(taskItem.Title));

            return await UpdateAsync(existingItem);
        }

        public async Task<bool> UpdateAsync(TaskItem item)
        {
            var existingItem = await _context.TaskItems.FindAsync(item.Id);

            if (existingItem == null)
            {
                return false;
            }

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TaskItem?> DeleteAsync(Guid id)
        {
            var item = await _context.TaskItems.FindAsync(id);
            if (item != null)
            {
                _context.TaskItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<TaskItem?> CompleteTaskAsync(Guid id)
        {
            var item = await _context.TaskItems.FindAsync(id);
            if (item != null)
            {
                item.CompletionDate = DateTime.UtcNow;
                item.IsCompleted = true;
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<TaskItem?> ResetTaskAsync(Guid id)
        {
            var item = await _context.TaskItems.FindAsync(id);
            if (item != null)
            {
                item.CompletionDate = null;
                item.IsCompleted = false;
                await _context.SaveChangesAsync();
            }
            return item;
        }
    }
}
