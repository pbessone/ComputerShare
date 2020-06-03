using System;

namespace ShareHistoryQueryApi.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}