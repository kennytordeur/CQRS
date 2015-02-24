using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Torken.CQRS.Interfaces
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }

        IEnumerable<ValidationResult> ValidationMessages { get; }

        void AddCommandResult(ICommandResult commandResult);

        void AddValidationResult(params ValidationResult[] validationResults);
    }
}