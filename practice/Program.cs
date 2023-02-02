using Microsoft.EntityFrameworkCore;
using practice.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var connnectionString = builder.Configuration.GetConnectionString("SqlString");

builder.Services.AddDbContext<StackOverFlow2010Context>(options => options.UseSqlServer(connnectionString));
var app = builder.Build();
app.UseStaticFiles();
app.UseMvc(route =>
{
    route.MapRoute(
        name: "Default",
        template: "{controller}/{action}",
        defaults: new { controller = "Home", action = "Index" }
    );

    route.MapRoute(
        name: "r1",
        template: "{controller}/{action}",
        defaults: new { controller = "ReqOne", action = "Index" }
    );

    route.MapRoute(
        name: "r2",
        template: "{controller}/{action}",
        defaults: new { controller = "ReqTwo", action = "Index" }
    );

    route.MapRoute(
        name: "r3",
        template: "{controller}/{action}",
        defaults: new { controller = "ReqThree", action = "Index" }
    );
});

app.Run();
