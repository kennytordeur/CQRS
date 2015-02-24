using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using Torken.CQRS.Interfaces;

namespace Torken.CQRS.Core.Tests
{
    [TestClass]
    public class CommandDispatcherTests
    {
        public class TestCommand : ICommand
        {
            public Guid Id
            {
                get;
                protected set;
            }
        }

        [TestMethod]
        public void CommandHandlerNotFound()
        {
            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();

            var commandDispatcher = new CommandDispatcher(serviceProviderMoq.Object);

            Action action = () => commandDispatcher.Send(new TestCommand());

            action.ShouldThrow<Exceptions.CommandHandlerNotFound>();
        }

        [TestMethod]
        public void CommandHandlerSuccess()
        {
            var testCommand = new TestCommand();

            var commandHandlerMoq = new Moq.Mock<ICommandHandler<TestCommand>>();
            commandHandlerMoq.Setup(c => c.Execute(testCommand)).Returns(new CommandResult());

            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandHandler<TestCommand>))).Returns(commandHandlerMoq.Object);

            var commandDispatcher = new CommandDispatcher(serviceProviderMoq.Object);

            var commandResult = commandDispatcher.Send(testCommand);

            commandResult.Should().NotBeNull();
            commandResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void CommandHandlerFailure()
        {
            var testCommand = new TestCommand();
            var returnedCommandResult = new CommandResult();
            returnedCommandResult.AddValidationResult(new ValidationResult("Test"));

            var commandHandlerMoq = new Moq.Mock<ICommandHandler<TestCommand>>();
            commandHandlerMoq.Setup(c => c.Execute(testCommand)).Returns(returnedCommandResult);

            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandHandler<TestCommand>))).Returns(commandHandlerMoq.Object);

            var commandDispatcher = new CommandDispatcher(serviceProviderMoq.Object);

            var commandResult = commandDispatcher.Send(testCommand);

            commandResult.Should().NotBeNull();
            commandResult.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public void CommandValidatorFailure()
        {
            var testCommand = new TestCommand();
            var returnedCommandResult = new CommandResult();
            returnedCommandResult.AddValidationResult(new ValidationResult("Test"));

            var commandValidatorMoq = new Moq.Mock<ICommandValidator<TestCommand>>();
            commandValidatorMoq.Setup(c => c.Validate(testCommand)).Returns(returnedCommandResult);

            var commandHandlerMoq = new Moq.Mock<ICommandHandler<TestCommand>>();
            commandHandlerMoq.Setup(c => c.Execute(testCommand)).Returns(new CommandResult());

            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandHandler<TestCommand>))).Returns(commandHandlerMoq.Object);
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandValidator<TestCommand>))).Returns(commandValidatorMoq.Object);

            var commandDispatcher = new CommandDispatcher(serviceProviderMoq.Object);
            var commandResult = commandDispatcher.Send(testCommand);

            commandResult.Should().NotBeNull();
            commandResult.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public void CommandValidatorSuccess()
        {
            var testCommand = new TestCommand();

            var commandValidatorMoq = new Moq.Mock<ICommandValidator<TestCommand>>();
            commandValidatorMoq.Setup(c => c.Validate(testCommand)).Returns(new CommandResult());

            var commandHandlerMoq = new Moq.Mock<ICommandHandler<TestCommand>>();
            commandHandlerMoq.Setup(c => c.Execute(testCommand)).Returns(new CommandResult());

            var serviceProviderMoq = new Moq.Mock<IServiceProvider>();
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandHandler<TestCommand>))).Returns(commandHandlerMoq.Object);
            serviceProviderMoq.Setup(s => s.GetService(typeof(ICommandValidator<TestCommand>))).Returns(commandValidatorMoq.Object);

            var commandDispatcher = new CommandDispatcher(serviceProviderMoq.Object);
            var commandResult = commandDispatcher.Send(testCommand);

            commandResult.Should().NotBeNull();
            commandResult.IsSuccess.Should().BeTrue();
        }
    }
}