using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TicketingApi
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration; // The Configuration property is automatically set by the constructor

        public void ConfigureServices(IServiceCollection services)
        {
            // Adding controllers and OpenAPI support
            services.AddControllers();
            services.AddEndpointsApiExplorer(); // Enables OpenAPI support (Swagger)

            // Enable CORS with a named policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed exception page in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Custom error page for non-development environments
                app.UseHsts(); // Enforce HTTPS in production
            }

            // Apply CORS before other middleware
            app.UseCors("AllowAll");

            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
            app.UseStaticFiles(); // Serve static files from wwwroot

            app.UseRouting(); // Enable routing

            app.UseAuthorization(); // Enable authorization

            // Map controllers to their respective endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
