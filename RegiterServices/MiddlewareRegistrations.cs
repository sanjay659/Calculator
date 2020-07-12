using Microsoft.AspNetCore.Builder;
using System;

namespace RegiterServices
{
    public static class MiddlewareRegistrations
    {
        public static void UseGepMiddleware(this IApplicationBuilder app)
        {
            //CAUTION: Kindly do not change order of below middlewares if you don't know the effects.
            app.UseStaticFiles();
            app.UseGepSwagger();
            
            //
        }
        public static void UseGepSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API V1");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
