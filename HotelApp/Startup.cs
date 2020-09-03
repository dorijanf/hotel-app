using System;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelApp.API.Configuration;
using HotelApp.API.DbContexts;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace HotelApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
            }).AddFluentValidation();

            // Auto mapper
            services.AddAutoMapper(typeof(Startup));

            // Validation services
            services.AddTransient<IValidator<LoginUserDTO>, LoginUserDTOValidator>();
            services.AddTransient<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
            services.AddTransient<IValidator<RegisterHotelDTO>, RegisterHotelDTOValidator>();
            services.AddTransient<IValidator<ReservationDTO>, ReservationDTOValidator>();

            // Helpers
            services.AddScoped<ISort<Room>, Sort<Room>>();
            services.AddScoped<ISort<Reservation>, Sort<Reservation>>();
            
            // Repositories
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelStatusRepository, HotelStatusRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            // DB Context
            services.AddDbContextPool<HotelAppContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("HotelAppDb"));
            });

            // Identity
            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<HotelAppContext>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserResolverService>();

            // JWT settings
            var jwtSettings = new JwtSettings();
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.AddSingleton<JwtSettings>(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();
            });
        }
    }
}
