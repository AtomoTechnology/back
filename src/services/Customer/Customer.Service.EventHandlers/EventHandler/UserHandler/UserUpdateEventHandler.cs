using Customer.Domain;
using Customer.Service.EventHandlers.Commands.User;
using Customers.Persistence.DataBase;
using Makaya.Resolver.Enum;
using Makaya.Resolver.Eum;
using Makaya.Resolver.IExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.EventHandlers.EventHandler.UserHandler
{
    public class UserUpdateEventHandler :
        INotificationHandler<UserUpdateCommand>
    {
        private readonly ApplicationDbContext _context;
        public UserUpdateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UserUpdateCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                var entity =  _context.Users.Where(x => x.UserId == notification.UserId && x.state == (Int32)StateEnum.Activeted).Include("Location").ToList();
                if (entity.Count > 0)
                    throw new ApiBusinessException(EnumCode.Province.ToString(), "User does not exist", System.Net.HttpStatusCode.NotFound, "Http");

                if (notification.AccountId == 0 || notification.AccountId == null)
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Id account user required", System.Net.HttpStatusCode.NotFound, "Http");
                if (String.IsNullOrEmpty(notification.Firstname))
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Firstname required", System.Net.HttpStatusCode.NotFound, "Http");
                if (String.IsNullOrEmpty(notification.Lastname))
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Lastname required", System.Net.HttpStatusCode.NotFound, "Http");
                if (notification.Location == null)
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Location user required", System.Net.HttpStatusCode.NotFound, "Http");
                if (notification.Location.CountryId == 0 || notification.Location.CountryId == null)
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Country required", System.Net.HttpStatusCode.NotFound, "Http");
                if (notification.Location.ProvinceId == 0 || notification.Location.ProvinceId == null)
                    throw new ApiBusinessException(EnumCode.User.ToString(), "Province required", System.Net.HttpStatusCode.NotFound, "Http");
                if (notification.Location.CityId == 0 || notification.Location.CityId == null)
                    throw new ApiBusinessException(EnumCode.User.ToString(), "City required", System.Net.HttpStatusCode.NotFound, "Http");

                
                entity.FirstOrDefault().Location.CityId = notification.Location.CityId;
                entity.FirstOrDefault().Location.ProvinceId = notification.Location.ProvinceId;
                entity.FirstOrDefault().Location.CountryId = notification.Location.CountryId;
                entity.FirstOrDefault().Location.UpdatedAt = DateTime.Now;
                entity.FirstOrDefault().Firstname = notification.Firstname;
                entity.FirstOrDefault().Lastname = notification.Lastname;
                entity.FirstOrDefault().EmailAddress = notification.EmailAddress;
                entity.FirstOrDefault().Gender = notification.Gender;
                entity.FirstOrDefault().Address = notification.Address;
                entity.FirstOrDefault().NumberAddress = notification.NumberAddress;
                entity.FirstOrDefault().DateOfBirth = notification.DateOfBirth;
                entity.FirstOrDefault().UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
