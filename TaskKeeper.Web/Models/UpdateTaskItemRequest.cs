using System.ComponentModel.DataAnnotations;
using TaskKeeper.Domain;

namespace TaskKeeper.Web.Models
{
    public class UpdateTaskItemRequest
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    }
}
