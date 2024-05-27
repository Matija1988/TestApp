using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Ninject;
//using Ninject.Extensions.Hosting;
//using Ninject.Web.Common;
using Project.MVC;
using Project.Service.Data;
using Project.Service;
using Microsoft.AspNetCore;


//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var hostConfiguration = new AspNetCoreHostConfiguration(args)
//                .UseWebHostBuilder(CreateWebHostBuilder(args))
//                .UseStartup<Startup>();

//        var host = new NinjectSelfHostBootstrapper(CreateKernel, hostConfiguration);
//        host.Start();
//    }

//    private static IWebHostBuilder CreateWebHostBuilder(string[] args)
//    {
//        return new DefaultWebHostConfiguration(args)
//            .ConfigureContentRoot()
//            .ConfigureAppSettings()
//            .ConfigureLogging()
//            .ConfigureAllowedHosts()
//            .ConfigureForwardedHeaders()
//            .ConfigureRouting()
//            .GetBuilder();
//    }

//    public static IKernel CreateKernel()
//    {
//        var settings = new NinjectSettings();
//        // Unfortunately, in .NET Core projects, referenced NuGet assemblies are not copied to the output directory
//        // in a normal build which means that the automatic extension loading does not work _reliably_ and it is
//        // much more reasonable to not rely on that and load everything explicitly.
//        settings.LoadExtensions = false;

//        var kernel = new AspNetCoreKernel(settings);

//        kernel.Load(typeof(AspNetCoreHostConfiguration).Assembly);

//        return kernel;
//    }
//}



var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Persistance(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();