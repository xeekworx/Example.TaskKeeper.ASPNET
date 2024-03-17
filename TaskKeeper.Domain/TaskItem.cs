using System.ComponentModel.DataAnnotations;

namespace TaskKeeper.Domain
{
    public class TaskItem
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime? DueDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletionDate { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    }
}