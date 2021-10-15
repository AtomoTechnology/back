using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyment.Persistence.Database.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.HasKey(u => u.ProductId);
            entityBuilder.Property(u => u.description).HasMaxLength(250);
            entityBuilder.Property(u => u.name).HasMaxLength(250);
        }
    }
}
