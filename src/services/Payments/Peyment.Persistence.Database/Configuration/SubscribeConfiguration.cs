using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyment.Persistence.Database.Configuration
{
    public class SubscribeConfiguration
    {
        public SubscribeConfiguration(EntityTypeBuilder<Subscribe> entityBuilder)
        {
            entityBuilder.HasIndex(u => u.SubscribeId);
            entityBuilder.Property(u => u.AccountId).IsRequired();
            entityBuilder.Property(u => u.PlanPriceSID).IsRequired();
            entityBuilder.Property(u => u.CustomerID).IsRequired();
            entityBuilder.Property(u => u.SubscribeStripeId).IsRequired();
            entityBuilder.Property(u => u.idCardStripe).IsRequired();
        }
    }
}
