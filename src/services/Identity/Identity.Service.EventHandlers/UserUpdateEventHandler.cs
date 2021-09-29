using Identity.Domain;
using Identity.Service.EventHandlers.Commands;
using Makaya.Resolver.Enum;
using Makaya.Resolver.Eum;
using Makaya.Resolver.IExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.EventHandlers
{
    public class UserUpdateEventHandler :
         IRequestHandler<UserUpdateCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserUpdateEventHandler(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UserUpdateCommand notification, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(notification.Email))
                throw new ApiBusinessException(EnumCode.User.ToString(), "Email required", System.Net.HttpStatusCode.Ambiguous, "Http");

            if (String.IsNullOrEmpty(notification.Id))
                throw new ApiBusinessException(EnumCode.User.ToString(), "Id account required", System.Net.HttpStatusCode.Ambiguous, "Http");

            var entity = _userManager.Users.Where(u => u.Email == notification.Email && u.state == (int)StateEnum.Activeted).ToList();
            if (entity.Count ==  0)
                throw new ApiBusinessException(entity.FirstOrDefault().Id, "User does not exist", System.Net.HttpStatusCode.Ambiguous, "Http");

            var user = await _userManager.FindByIdAsync(notification.Id);
            if (user == null)
                throw new ApiBusinessException(EnumCode.User.ToString(), "User does not exist", System.Net.HttpStatusCode.Ambiguous, "Http");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, notification.Password);
        }
    }
}
