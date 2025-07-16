using static System.Net.WebRequestMethods;

namespace EmpsManagement.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container  --> ConfiguraServices

            builder.Services.AddControllersWithViews(); 

            #endregion

            var app = builder.Build();


            #region  Configure the HTTP request pipeline.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                // This middleware adds the Strict-Transport-Security header to responses, which tells browsers to only access the site using HTTPS.
                // make sure all requests are under protocol HTTPS
                // This is important for security, especially in production environments. --> if any request is not under HTTPS, it will be redirected to HTTPS.
                
                app.UseHsts(); // Http Strict Transport Security
            }

            app.UseHttpsRedirection();   // Redirect HTTP requests to HTTPS
            app.UseStaticFiles();        // Serve static files (like CSS, JS, images) from wwwroot
            app.UseRouting();   // Enable routing capabilities / find matched routes for incoming requests


            app.UseAuthorization(); // Enable authorization middleware to enforce security policies

            app.MapStaticAssets();  // Map static assets to be served from the wwwroot folder

            app.MapControllerRoute(   // Define the default route for controllers
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); 

            #endregion

            app.Run();
        }
    }
}
