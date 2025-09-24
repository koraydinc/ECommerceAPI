using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var appUser = new Domain.Entities.Identity.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = request.NameSurname,
                UserName = request.UserName,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, request.Password);

            CreateUserCommandResponse response = new CreateUserCommandResponse();

            if (result.Succeeded)
            {
                response.Succeeded = true;
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                        throw new UsernameAlreadyInUseException();
                    if (error.Code == "DuplicateEmail")
                        throw new EmailAlreadyInUseException();
                    response.Message += $"{error.Code} - {error.Description}";
                }
            }

            return response;
        }
    }
}
