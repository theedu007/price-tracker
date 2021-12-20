using Data.Collections;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Field(x => x.Id)
                .Type<StringType>();
            descriptor.Field(x => x.VendorId)
                .Type<StringType>();
            descriptor.Field(x => x.Name)
                .Type<StringType>();
            descriptor.Field(x => x.ProductId)
                .Type<StringType>();
            descriptor.Field(x => x.Category)
                .Type<StringType>();
            descriptor.Field(x => x.Url)
                .Type<StringType>();
        }
    }
}
