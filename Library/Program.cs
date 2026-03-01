using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve connection string from appsettings.json
            var con = builder.Configuration.GetConnectionString("con");

            // Add services to the container.
            builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(con, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure() // Handle transient errors in the database
            ));

            // Add MVC with views
            builder.Services.AddControllersWithViews();

            // Enable session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
                options.Cookie.IsEssential = true; // Mark the session cookie as essential
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // Handle errors in production
                app.UseHsts(); // Use HSTS to enforce HTTPS in production
            }

            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
            app.UseStaticFiles(); // Serve static files

            app.UseRouting(); // Enable routing

            // Use session before authorization middleware
            app.UseSession(); // Enable session state

            app.UseAuthorization(); // Enable authorization checks

            // Map default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run(); // Run the application
        }
    }
}
