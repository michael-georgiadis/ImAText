using System;
using System.Collections.Generic;
using System.Text;

namespace ImAText.Exceptions
{
    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException(int redValue)
            : base($"Red Value {redValue} already exists in RedValues")
        {

        }
    }
}
