using EmpsManagement.BLL.Services;
using EmpsManagement.BLL.Services.Attachment;
using EmpsManagement.BLL.Services.Employee;
using EmpsManagement.DAL.Data.Contexts;
using EmpsManagement.DAL.Models.Identity;
using EmpsManagement.DAL.Repositories.Classes;
using EmpsManagement.DAL.Repositories.Interfaces;
using EmpsManagement.DAL.Unit_Of_Work;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EmpsManagement.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container  --> ConfiguraServices

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // This filter automatically validates the antiforgery token for all POST requests to prevent CSRF attacks.

                var policy = new AuthorizationPolicyBuilder()
                               .RequireAuthenticatedUser()
                               .Build();

                options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
                options.Filters.Add(new AuthorizeFilter(policy));


            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>                   // options --> will be passed to the ApplicationDbContext constructor as parameter to configure the DbContext ( to set up the database connection and other configurations )
            {
                // Configure the DbContext to use SQL Server with a connection string
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();

            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>() // Add Identity services for user and role management
                .AddEntityFrameworkStores<ApplicationDbContext>() // Use the ApplicationDbContext for storing user and role data
                .AddDefaultTokenProviders(); // Add default token providers for password reset, email confirmation, etc.

        

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options =>
                            {
                                 options.LoginPath = "/Account/Login"; // or your login path
                                 options.AccessDeniedPath = "/Account/AccessDenied";
                            });

            builder.Services.AddAuthorization();

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
            else
            {
                app.UseDeveloperExceptionPage(); // Show detailed error pages in development mode
            }

            app.UseHttpsRedirection();   // Redirect HTTP requests to HTTPS
            app.UseStaticFiles();        // Serve static files (like CSS, JS, images) from wwwroot
            app.UseRouting();   // Enable routing capabilities / find matched routes for incoming requests

            app.UseAuthentication(); // Enable authentication middleware to handle user sign-in and sign-out
            app.UseAuthorization(); // Enable authorization middleware to enforce security policies

            app.MapStaticAssets();  // Map static assets to be served from the wwwroot folder

            app.MapControllerRoute(   // Define the default route for controllers
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            #endregion

            app.Run();
        }
    }
}
