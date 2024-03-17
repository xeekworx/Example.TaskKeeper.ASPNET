using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using TaskKeeper.Web.Models;
using Xunit;

namespace TaskKeeper.UnitTests;

public class UpdateTaskItemRequestTests
{
    [Fact]
    public void ValidateDueDate_Should_ReturnSuccess_WhenDueDateIsNull()
    {
        // Arrange
        var request = new UpdateTaskItemRequest();

        // Act
        var result = UpdateTaskItemRequest.ValidateDueDate(null, new ValidationContext(request));

        // Assert
        result.Should().Be(ValidationResult.Success);
    }

    [Fact]
    public void ValidateDueDate_Should_ReturnSuccess_WhenDueDateIsAfterToday()
    {
        // Arrange
        var request = new UpdateTaskItemRequest();
        var futureDate = DateTime.UtcNow.AddDays(1);

        // Act
        var result = UpdateTaskItemRequest.ValidateDueDate(futureDate, new ValidationContext(request));

        // Assert
        result.Should().Be(ValidationResult.Success);
    }

    [Fact]
    public void ValidateDueDate_Should_ReturnFailure_WhenDueDateIsBeforeToday()
    {
        // Arrange
        var request = new UpdateTaskItemRequest();
        var pastDate = DateTime.UtcNow.AddDays(-1);

        // Act
        var result = UpdateTaskItemRequest.ValidateDueDate(pastDate, new ValidationContext(request));

        // Assert
        result.Should().NotBe(ValidationResult.Success);
        result!.ErrorMessage.Should().Be("Due date must be after today.");
    }
}
