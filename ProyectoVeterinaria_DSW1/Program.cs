using ProyectoVeterinaria_DSW1.DAO;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsuario, UsuarioDAO>();
builder.Services.AddScoped<IDueno, DuenoDAO>();
builder.Services.AddScoped<IVeterinario, VeterinarioDAO>();
builder.Services.AddScoped<IMascota, MascotaDAO>();
builder.Services.AddScoped<IEstadoCita, EstadoCitaDAO>();
builder.Services.AddScoped<IHistorial, HistorialDAO>();
builder.Services.AddScoped<ICita, CitaDAO>();
builder.Services.AddScoped<IAgenda, AgendaDAO>();
builder.Services.AddScoped<AgendaService>();
builder.Services.AddScoped<HistorialService>();
builder.Services.AddScoped<EstadoCitaService>();
builder.Services.AddScoped<DuenoService>();
builder.Services.AddScoped<MascotaService>();
builder.Services.AddScoped<VeterinarioService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<ProyectoVeterinaria_DSW1.Services.CitaService>();
builder.Services.AddScoped<ProyectoVeterinaria_DSW1.Services.VeterinarioService>();

//sesion
builder.Services.AddDistributedMemoryCache(); //guarda sesion en ram
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); //30 inactivo borra sesion
});

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();


