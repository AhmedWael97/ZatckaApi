using Microsoft.AspNetCore.Mvc;

namespace ZatckaAPI.Exetsions
{
    public static class ServiceExentsion 
    {
        public static void ConfiguerCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });

    }
}
