using Ecommerce_Api;
using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EcommercedemoContext>(options =>

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
builder.Services.AddScoped<ICartRepository, CartRepository>(); // Example, replace CartRepository with your actual implementation
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddSingleton<RazorpayClient>(sp =>
{
    var apiKey = "rzp_test_eU5IaExAj1oLTZ";
    var apiKeySecret = "8GwQc2yBqtylRod7VLon9m2w";
    return new RazorpayClient(apiKey, apiKeySecret);
});

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();

builder.Services.AddSwaggerGen(c =>
{
    // ... (other configurations)
    c.OperationFilter<AddFileParamTypesOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
