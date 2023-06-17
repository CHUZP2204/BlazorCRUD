
using BlazorCRUD.Server.Servicios;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using BlazorCRUD.Server.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.WebHost.UseWebRoot("wwwroot");

builder.Services.AddDbContext<PruebablazorContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));


//Directorio
//Console.WriteLine($"ContentRooth PATH:{ builder.Environment.ContentRootPath}");

//Servicios a implementar
//
//Usuarios Servicio Conexion
builder.Services.AddScoped<IUsuario, GestionUsuarios>();

//Vehiculos Servicio conexion   
builder.Services.AddScoped<IVehiculo, GestionVehiculos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseBlazorFrameworkFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
