using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using Makaya.Resolver.Enum;
using Makaya.Resolver.Eum;
using Makaya.Resolver.IExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.EventHandlers
{
    public class UserCreateEventHandler :
         IRequestHandler<UserCreateCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCreateEventHandler(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UserCreateCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _userManager.Users.Where(u => u.Email == notification.Email).ToList();
                if (entity.Count > 0)
                {
                    var resul = await _userManager.AccessFailedAsync(entity.FirstOrDefault());
                    notification.Email = entity.FirstOrDefault().Email;
                    notification.Id = entity.FirstOrDefault().Id;
                    notification.message = "You already have an account for that email";
                    return resul;
                }
                var entry = new ApplicationUser
                {
                    FirstName = " ",
                    LastName = " ",
                    Email = notification.Email,
                    UserName = notification.Email,
                    state = (int)StateEnum.Activeted
                };
                notification.Password = "without2021";
                var result = await _userManager.CreateAsync(entry, notification.Password);
                if (!result.Succeeded)
                    throw new ApiBusinessException(result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description, System.Net.HttpStatusCode.BadRequest, "Http");

                notification.Id = entry.Id;
                notification.message = null;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
    }
}
