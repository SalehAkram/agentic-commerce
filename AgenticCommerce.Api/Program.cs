using AgenticCommerce.Infra;
using AgenticCommerce.Core.Behaviors;
using AgenticCommerce.Core.Contracts;
using AgenticCommerce.Core.Validators;
using AgenticCommerce.Api.ExceptionHandlers;
using AgenticCommerce.Core.Queries;
using AgenticCommerce.Infra.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IRequestValidator<GetProductsQuery>, GetProductsQueryValidator>();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<GetProductsQuery>();
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<RequestValidationExceptionHandler>();
builder.Services.AddSqlServer(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<AgenticCommerceDbContext>();
    await ProductSeeder.SeedAsync(context);

    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("../openapi/v1.json", "AgenticCommerce API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
