//using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Configuration;
using Microsoft.AspNetCore.Rewrite;
using LesBooks.IoC;
using LesBook.Monitoring;
using System.Threading;

namespace LesBooks.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Swagger.LesBooks",
                        Version = "v1"
                       
                    });
            });
            services.AddCors();
            services.Configure<HostOptions>(options =>
            {
                options.ShutdownTimeout = TimeSpan.FromSeconds(5); 
            });




        }
        public void Configure(WebApplication app, IWebHostEnvironment environment, IMonitoring monitoring)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Swagger.LesBooks.V1");
            });


            monitoring.InitMonitoting();

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.MapControllers();
        }
    }
}
