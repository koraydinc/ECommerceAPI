using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                //Yetkiler belirlenecek.
            }
            else
            {
                throw new NotFoundUserException("Kullanıcı şifresi hatalı!");
            }

                return new LoginUserCommandResponse();
        }
    }
}
