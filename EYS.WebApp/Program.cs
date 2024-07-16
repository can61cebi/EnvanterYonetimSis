using EYS.Plugins.InMemory;
using EYS.UseCases.Envanterler;
using EYS.UseCases.Envanterler.Interfaces;
using EYS.UseCases.PluginInterfaces;
using EYS.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>(); // bir kere oluşturuluyor ve lazım olduğu zaman kullanılıyor.

builder.Services.AddTransient<IIsmeGoreEnvanterleriGoruntuleUseCase, IsmeGoreEnvanterleriGoruntuleUseCase>(); // çağrıldığında oluşuyor kullanılıyor ve siliniyor.

builder.Services.AddTransient<IEnvanterEkleUseCase, EnvanterEkleUseCase>();

builder.Services.AddTransient<IEnvanterDuzenleUseCase, EnvanterDuzenleUseCase>();
// builder.Services.AddScoped<IIsmeGoreEnvanterleriGoruntuleUseCase, IsmeGoreEnvanterleriGoruntuleUseCase>(); // çağrıldığında oluşuyor ve kalmaya devam ediyor.

builder.Services.AddTransient<IIDyeGoreEnvanterGoruntuleUseCase, IDyeGoreEnvanterGoruntuleUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
