﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.DTOs
{
    using System;
    using System.Collections.Generic;

    namespace ShoppingPortal.Core.Exceptions
    {
        public class ValidationException : Exception
        {
            public IDictionary<string, string[]> Errors { get; }

            public ValidationException() : base("One or more validation failures have occurred.")
            {
                Errors = new Dictionary<string, string[]>();
            }

            public ValidationException(IDictionary<string, string[]> errors) : this()
            {
                Errors = errors;
            }
        }
    }
}
