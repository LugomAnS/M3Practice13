using System;

namespace DataFormat
{
    public class ClientFormatException : Exception
    {
        public ClientFormatException(string ErrorMessage) : base(ErrorMessage)
        {

        }
    }
}
