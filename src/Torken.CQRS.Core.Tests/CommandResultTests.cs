using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace Torken.CQRS.Core.Tests
{
    [TestClass]
    public class CommandResultTests
    {
        [TestMethod]
        public void Success()
        {
            var commandResult = new CommandResult();

            commandResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void AddValidationResult()
        {
            var commandResult = new CommandResult();
            commandResult.AddValidationResult(new ValidationResult("Test"));
            commandResult.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public void AddCommandResult()
        {
            var originalCommandResult = new CommandResult();
            originalCommandResult.AddValidationResult(new ValidationResult("Test"));

            var commandResult = new CommandResult();
            commandResult.AddCommandResult(originalCommandResult);

            commandResult.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public void AddEmptyCommandResult()
        {
            var originalCommandResult = new CommandResult();

            var commandResult = new CommandResult();
            commandResult.AddCommandResult(originalCommandResult);

            commandResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void AddNullCommandResult()
        {
            var commandResult = new CommandResult();
            commandResult.AddCommandResult(null);

            commandResult.IsSuccess.Should().BeTrue();
        }
    }
}