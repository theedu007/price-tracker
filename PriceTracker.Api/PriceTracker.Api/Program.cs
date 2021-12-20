using Data;
using Data.Client;
using Data.DataAccessObjects;
using Data.DataAccessObjects.Interfaces;
using GraphQL.Queries;
using GraphQL.Types;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Adding my local settings
builder.Configuration.AddJsonFile("appsettings.Local.json", true);

// Adding mongo mappings
DbClient.RegisterClassMap();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductDAO, ProductDAO>();
builder.Services.AddScoped<IProductPriceDAO, ProductPriceDAO>();
builder.Services.AddScoped<IDbClient>(x => {
    var settings = builder.Configuration.GetSection("PriceTrackerSettings").Get<PriceTrackerSettings>();
    return ActivatorUtilities.CreateInstance<DbClient>(x, settings);
});

//configure graphql
builder.Services.AddGraphQLServer()
    .AddQueryType(x => x.Name("Query"))
    .AddType<ProductType>()
    .AddTypeExtension<ProductQuery>()
    .AddType<ProductPriceType>()
    .AddTypeExtension<ProductPriceQuery>();


var app = builder.Build();    
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints
        .MapGraphQL("/api")
        .WithOptions(new GraphQLServerOptions
        {
            Tool = {
                Enable = builder.Environment.IsDevelopment()
            }
        });
});

app.Run();
