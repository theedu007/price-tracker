using Data.Collections;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Types
{
    public class ProductPriceType : ObjectType<ProductPrice>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductPrice> descriptor)
        {
            descriptor.Field(x => x.Id)
                .Type<StringType>();
            descriptor.Field(x => x.Price)
                .Type<DecimalType>();
            descriptor.Field(x => x.Date)
                .Type<DateTimeType>();
            descriptor.Field(x => x.ProductId)
                .Type<StringType>();
        }
    }
}
