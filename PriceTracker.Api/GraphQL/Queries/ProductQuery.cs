using Data.Collections;
using Data.DataAccessObjects.Interfaces;
using GraphQL.Resolver;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Queries
{
    public class ProductQuery : ObjectTypeExtension<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Name("Query")
                .Field("GetProducts")
                .ResolveWith<ProductResolver>(x => x.GetProducts());

            //Set default as non null because a not null validation is specified in graphql argument setup
            descriptor.Name("Query")
                .Field("GetProductById")
                .Argument("id", a => a.Type<NonNullType<StringType>>())
                .ResolveWith<ProductResolver>(x => x.GetProduct(default!));
        }
    }
}
