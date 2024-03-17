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

        [CustomValidation(typeof(UpdateTaskItemRequest), nameof(ValidateDueDate))]
        public DateTime? DueDate { get; set; }

        public static ValidationResult? ValidateDueDate(DateTime? dueDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as UpdateTaskItemRequest;
            if (instance == null || dueDate == null) return ValidationResult.Success;

            if (dueDate <= DateTime.UtcNow)
            {
                return new ValidationResult("Due date must be after today.");
            }

            return ValidationResult.Success;
        }
    }
}
