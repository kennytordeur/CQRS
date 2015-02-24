using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Torken.CQRS.Interfaces;

namespace Torken.CQRS.Core
{
    public class CommandResult : ICommandResult
    {
        private readonly List<ValidationResult> _validationMessages = new List<ValidationResult>();

        public bool IsSuccess
        {
            get { return !_validationMessages.Any(); }
        }

        public IEnumerable<ValidationResult> ValidationMessages
        {
            get { return _validationMessages; }
        }

        public void AddValidationResult(params ValidationResult[] validationResults)
        {
            _validationMessages.AddRange(validationResults);
        }

        public void AddCommandResult(ICommandResult commandResult)
        {
            if (null != commandResult)
            {
                _validationMessages.AddRange(commandResult.ValidationMessages.ToArray());
            }
        }
    }
}