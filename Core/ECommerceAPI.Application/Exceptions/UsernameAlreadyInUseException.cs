using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class UsernameAlreadyInUseException : Exception
    {
        public UsernameAlreadyInUseException() : base("Kullanıcı adı kullanılmaktadır!") { }
        public UsernameAlreadyInUseException(string? message) : base(message) { }
        public UsernameAlreadyInUseException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
