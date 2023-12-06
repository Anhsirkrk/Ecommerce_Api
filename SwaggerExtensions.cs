namespace Ecommerce_Api
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
        //public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        //{
        //    return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        //}
    }
}
