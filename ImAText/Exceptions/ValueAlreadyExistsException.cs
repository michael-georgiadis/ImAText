using System;
using System.Collections.Generic;
using System.Text;

namespace ImAText.Exceptions
{
    public class ValueAlreadyExistsException : Exception
    {
        public ValueAlreadyExistsException(string value)
            : base($"Charcater {value} already exists in RedValues")
        {

        }
    }
}
