using Library.API.Configurations;
using Library.API.Middleware;
using Library.Application.Extensions;
using Library.Application.Interfaces;
using Library.Application.Mappings;
using Library.Application.Services;
using Library.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services);

// Build the application
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<ILibraryDbInitializer>();
    initializer.Initialize();
}

// Configure the HTTP request pipeline
ConfigureMiddleware(app);

app.Run();

// Method to configure services
void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
    }).ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureOptions<SwaggerConfigService>();

    services.RegisterDbContext();

    services.AddAutoMapper(typeof(MappingProfile));
    services.AddScoped<IBookService, BookService>();

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
}

// Method to configure middleware
void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<ApiKeyAuthMiddleware>();
    app.UseAuthorization();
    app.MapControllers();
}
