using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Common
{
    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<string> errorMessages)
        {
            ValidationErrors = errorMessages;
        }

        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
