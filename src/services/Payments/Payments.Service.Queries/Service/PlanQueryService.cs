using Makaya.Resolver.Handlers;
using Payments.Common;
using Payments.Service.Queries.DTOs;
using Payments.Service.Queries.IService;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Service.Queries.Service
{
    public class PlanQueryService: IPlanQueryService
    {
        //private readonly ApplicationDbContext _context;
        public PlanQueryService(
            //ApplicationDbContext context
            )
        {
            //_context = context;
        }
        public async Task<List<PlanStripeDto>> GetAllAsync(int quantity)
        {
            try
            {    
                PriceListOptions options = new PriceListOptions
                {
                    Limit = quantity,
                    Active = true,
                };
                PriceService service = new PriceService();
                StripeList<Price> prices = service.List(options);

                return this.TransformToDto(prices);
                //return StripeCardPayment.GetAllProduct(count);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        private List<PlanStripeDto> TransformToDto(StripeList<Price> prices)
        {
            List <PlanStripeDto> dto= null;

            if (prices.Count() > 0)
            {
                dto = new List<PlanStripeDto>();
                foreach (var item in prices)
                {
                    dto.Add( new PlanStripeDto() { 
                     TypePlan = item.Metadata["Type Plan"],
                     idplanstripe = item.Id,
                     idProduct = item.ProductId,
                     Price = (int)item.UnitAmount / 100,
                     Description = item.Nickname,
                     PlanDate = item.Created,
                    });
                }
                return dto;
            }

            return null;
        }
    }
}
