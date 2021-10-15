using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyment.Persistence.Database.Configuration
{
    public class PlanConfiguration
    {
        public PlanConfiguration(EntityTypeBuilder<Plan> entityBuilder)
        {
            entityBuilder.HasKey(u => u.PlanId);
            entityBuilder.Property(u => u.AccountId).IsRequired();
            entityBuilder.Property(u => u.idProduct).IsRequired();
            entityBuilder.Property(u => u.idplanstripe).IsRequired();
            entityBuilder.Property(u => u.Price).IsRequired();
            entityBuilder.Property(u => u.TypePlan).IsRequired().HasMaxLength(250);
            entityBuilder.Property(u => u.Description).HasMaxLength(250);
        }
    }
}
