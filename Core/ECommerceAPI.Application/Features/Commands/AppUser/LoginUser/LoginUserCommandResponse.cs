using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }

    public class LoginUserCommandErrorResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }


}
