using BuisnessObject.Interface;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using BuisnessObject;
using CustomPipeline.Interface;
using CustomPipeline;

namespace Calculator
{
    public static class RegistorService
    {
        public static void RegisterContextsServices(this IServiceCollection services)
        {
            services.AddScoped<IInterfaceLog, InterfaceLog>();
            services.AddTransient<IBlob, Blob>();

            services.AddTransient<ICalculator, BuisnessObject.Calculator>();
            services.AddTransient<ITokenDao, TokenDao>();
            services.AddTransient<IToken, Token>();
            



        }

    }
}
