﻿using Application.MediatR;
 
using Infrastructure.Contexts;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ClaenArch.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Domains.Data;
using Microsoft.Extensions.Configuration;

namespace ClaenArch.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationservices(this IServiceCollection services,
           IConfiguration config)
        {
            services.AddLogging(config => config.AddConsole());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRequestHandler<GetAllQuery<Activity>, IEnumerable<Activity>>, GetAllQueryHandler<Activity>>();
            services.AddScoped<IRequestHandler<GetAllQuery<TblUsers>, IEnumerable<TblUsers>>, GetAllQueryHandler<TblUsers>>();
             
            // Register our TokenService dependency
            services.AddScoped<TokenService, TokenService>();

            //services.AddMediatR(typeof(Application.MediatR.GetAllQueryHandler<>));
            services.AddMediatR(cng => cng.RegisterServicesFromAssemblies(typeof(Application.MediatR.GetAllQueryHandler<Activity>).Assembly));
            // services.AddMediatR(cng => cng.RegisterServicesFromAssemblies(typeof(Application.MediatR.GetAllQueryHandler<TblUser>).Assembly));



            //  
            services.Configure<JwtSettings>(config.GetSection("JwtTokenSettings"));

            //var jwtSettings = config.GetSection("JwtTokenSettings").Get<JwtSettings>();
           // services.AddSingleton(jwtSettings);

            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));







            // JWT Configuration
            // These will eventually be moved to a secrets file, but for alpha development appsettings is fine
            var validIssuer = config.GetValue<string>("JwtTokenSettings:ValidIssuer");
            var validAudience = config.GetValue<string>("JwtTokenSettings:ValidAudience");
            var symmetricSecurityKey = config.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin()// Replace with your frontend origin
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            return services;

        }
    }
}
