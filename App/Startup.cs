using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App
{
    public class Startup
    {
        #region Properties
        public IConfiguration _configuration { get; }
        public Utils _utils { get; set; }
        #endregion

        #region Constructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _utils = new Utils(_configuration);
        }
        #endregion

        #region Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Fail/Report-Develop");
            }
            else
            {
                app.UseExceptionHandler("/Fail/Report");
                app.UseHsts();
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Views")),
                RequestPath = "/Views",
                EnableDefaultFiles = true
            });

            app.UseCors(builder => builder
                .WithOrigins(_configuration["AllowedOrigin"])
                .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ApiKeyMiddleware>(_configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InjectManagers(services);

            AddJwtAuthentication(services);

            services.AddControllers();
        }
        #endregion

        #region Inyection
        private static void InjectClass<T>(IServiceCollection services, object inyection) where T : class
        {
            services.AddSingleton<Utils>();

            services.AddScoped(x => ActivatorUtilities.CreateInstance<T>(x, inyection));
        }

        public IServiceCollection InjectManagers(IServiceCollection services)
        {
            string connection = Utils.ConnectionString(_configuration, "connection");

            return services;
        }
        #endregion

        #region Auth
        public IServiceCollection AddJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     //ValidIssuer = Configuration["Jwt:issuer"], REVISAR DEBIDO A ERROR DURANTE PRUEBAS
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"])),
                     ClockSkew = TimeSpan.Zero
                 });

            return services;
        }
        #endregion
    }
}