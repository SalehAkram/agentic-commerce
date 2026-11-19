using AgenticCommerce.Infra;
using AgenticCommerce.Core.Behaviors;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Validators;
using AgenticCommerce.Api.ExceptionHandlers;
using AgenticCommerce.Core.Queries;
using AgenticCommerce.Core.Services;
using AgenticCommerce.Infra.Seeding;
using AgenticCommerce.Core.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<BasketResultMapper>();
builder.Services.AddTransient<IRequestValidator<GetBasketQuery>, GetBasketQueryValidator>();
builder.Services.AddTransient<IRequestValidator<GetProductsQuery>, GetProductsQueryValidator>();
builder.Services.AddTransient<IRequestValidator<GetProductByIdQuery>, GetProductByIdQueryValidator>();
builder.Services.AddTransient<IRequestValidator<AddBasketItemCommand>, AddBasketItemCommandValidator>();
builder.Services.AddTransient<IRequestValidator<ChangeBasketItemQuantityCommand>, ChangeBasketItemQuantityCommandValidator>();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<GetProductsQuery>();
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<RequestValidationExceptionHandler>();
builder.Services.AddExceptionHandler<DomainRuleExceptionHandler>();
builder.Services.AddExceptionHandler<ResourceNotFoundExceptionHandler>();
builder.Services.AddSqlServer(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        // Route IDs are integers; avoid a numeric/string union that Swagger UI cannot validate reliably.
        if (context.JsonTypeInfo.Type == typeof(int))
        {
            schema.Type = Microsoft.OpenApi.JsonSchemaType.Integer;
            schema.Pattern = null;
        }
        return Task.CompletedTask;
    }));

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<AgenticCommerceDbContext>();
    await ProductSeeder.SeedAsync(context);
    await CustomerSeeder.SeedAsync(context);

    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("../openapi/v1.json", "AgenticCommerce API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
