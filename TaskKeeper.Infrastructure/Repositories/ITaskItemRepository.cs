using TaskKeeper.Domain;

namespace TaskKeeper.Persistence.Repositories
{
    public interface ITaskItemRepository
    {
        Task AddAsync(TaskItem item);
        Task<TaskItem?> DeleteAsync(Guid id);
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<int> GetCountAsync();
        Task<List<TaskItem>> SearchAsync(string? title = null, bool exactTitle = false, bool? isCompleted = null);
        Task<bool> UpdateAsync(TaskItem item);
        Task<bool> UpdateByNameAsync(TaskItem taskItem);
        Task<TaskItem?> CompleteTaskAsync(Guid id);
        Task<TaskItem?> ResetTaskAsync(Guid id);
    }
}