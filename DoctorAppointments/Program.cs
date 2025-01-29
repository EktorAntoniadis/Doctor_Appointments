using DoctorAppointments.Models;
using DoctorAppointments.Repository;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<DoctorAppointmentsDbContext>(options =>
            options.UseMySql("server=127.0.0.1;uid=root;database=doctorappointmentsdb",
            ServerVersion.AutoDetect("server=127.0.0.1;uid=root;database=doctorappointmentsdb")));

            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication("DoctorAppointmentsScheme")
                .AddCookie("DoctorAppointmentsScheme", options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/Error";
                });

            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DoctorAppointmentsDbContext>();

                var seeder = new DatabaseSeed(context); 
                seeder.Seed();
            }

            app.Run();
        }
    }
}