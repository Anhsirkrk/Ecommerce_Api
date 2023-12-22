using Ecommerce_Api;
using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Razorpay.Api;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this using statement
using Microsoft.IdentityModel.Tokens; // And this one too, for TokenValidationParameters


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region  serilog

string Logpath = builder.Configuration.GetSection("Logging:Logpath").Value;
var _logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Error)
    .Enrich.FromLogContext()
    .WriteTo.File(Logpath)
    .CreateLogger();
builder.Services.AddSerilog(_logger);
#endregion


#region another way for seirlog but not succeeded
//string logPath = builder.Configuration.GetSection("Logging:Logpath").Value;

//Ecommerce_Api.Model.CustomLog = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .Enrich.WithExceptionDetails()
//    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
//    .CreateLogger();

//builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
#endregion



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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IloginRepository, LoginRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<ISupplierRepository,SupplierRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<Microsoft.Extensions.Logging.ILogger, DatabaseLogger>();
builder.Services.AddScoped<ExceptionLoggerService>();
builder.Services.AddScoped<DatabaseLogger>();
//builder.Services.AddScoped<JwtToken>();

var key = Encoding.ASCII.GetBytes("fU4wp8OYb1id/FZe6RkbPiyzaSBOnJwfaoXqTDGw");

builder.Services.AddAuthentication(options=> 
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew= TimeSpan.Zero
        };
    });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy =>
//        policy.RequireRole("1"));
//    options.AddPolicy("Useronly", policy =>
//        policy.RequireRole("2"));
//});

builder.Services.AddSingleton<RazorpayClient>(sp =>
{
    var apiKey = "rzp_test_1P09DzG08tmJ4d";
    var apiKeySecret = "fXZmmvCes4B8eGfqlLI2m5Ek";
    return new RazorpayClient(apiKey, apiKeySecret);
});



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
  //app.UseSwaggerAuthorized();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Daily_Pick v1"));
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Inside Configure method
app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
