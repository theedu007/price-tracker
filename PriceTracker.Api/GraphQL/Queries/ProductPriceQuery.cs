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
                .Field("GetPriceForProduct")
                .Argument("productId", a => a.Type<NonNullType<StringType>>())
                .ResolveWith<ProductPriceResolver>(x => x.GetPriceForProduct(default!));
        }
    }
}
