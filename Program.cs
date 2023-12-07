using Ecommerce_Api;
using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Razorpay.Api;
using Serilog;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//serilog

string Logpath = builder.Configuration.GetSection("Logging:Logpath").Value;
var _logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(Logpath)
    .CreateLogger();
builder.Services.AddSerilog(_logger);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Daily_Pick", Version = "v1" });

    // Secure the Swagger JSON endpoint
    //c.OperationFilter<AuthorizeCheckOperationFilter>();
});
builder.Services.AddDbContext<EcommerceDailyPickContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Dbcon");

    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});

builder.Services.AddCors();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IloginRepository, LoginRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<ISupplierRepository,SupplierRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<Microsoft.Extensions.Logging.ILogger, DatabaseLogger>();
builder.Services.AddScoped<ExceptionLoggerService>();
builder.Services.AddScoped<DatabaseLogger>();

builder.Services.AddSingleton<RazorpayClient>(sp =>
{
    var apiKey = "rzp_test_1P09DzG08tmJ4d";
    var apiKeySecret = "fXZmmvCes4B8eGfqlLI2m5Ek";
    return new RazorpayClient(apiKey, apiKeySecret);
});
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


// Example, replace CartRepository with your actual implementation
builder.Services.AddSwaggerGen(c =>
{
    // ... (other configurations)
    c.OperationFilter<AddFileParamTypesOperationFilter>();
});





var app = builder.Build();

//mm
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAuthorized();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daily_Pick v1"));
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
