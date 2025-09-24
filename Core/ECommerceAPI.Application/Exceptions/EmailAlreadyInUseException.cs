using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException() : base("Email adresi kullanılmaktadır!") { }
        public EmailAlreadyInUseException(string? message) : base(message) { }
        public EmailAlreadyInUseException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
