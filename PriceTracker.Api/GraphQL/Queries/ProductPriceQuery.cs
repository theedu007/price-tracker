using Data.Collections;
using GraphQL.Resolver;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Queries
{
    public class ProductPriceQuery : ObjectTypeExtension<ProductPrice>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductPrice> descriptor)
        {
            descriptor.Name("Query")
                .Field("GetAllPricesForProduct")
                .Argument("productId", a => a.Type<NonNullType<StringType>>())
                .ResolveWith<ProductPriceResolver>(x => x.GetAllPricesForProduct(default!));

            descriptor.Name("Query")
                .Field("GetPricesForProductBetweenDates")
                .Argument("productId", a => a.Type<NonNullType<StringType>>())
                .Argument("startDate", a => a.Type<NonNullType<DateTimeType>>())
                .Argument("endDate", a => a.Type<NonNullType<DateTimeType>>())
                .ResolveWith<ProductPriceResolver>(x => x.GetPricesForProductBetweenDates(default!, default!, default!));
        }
    }
}
