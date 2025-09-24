using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser? appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }

            if (appUser == null)
            {
                throw new NotFoundUserException();
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);

            if (signInResult.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserCommandSuccessResponse()
                {
                    Token = token
                };
            }
            else
            {
                throw new AuthenticationErrorException();
            }

        }
    }
}
