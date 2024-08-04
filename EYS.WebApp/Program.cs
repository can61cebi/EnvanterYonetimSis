using EYS.Plugins.EFCoreSqlServer;
using EYS.Plugins.InMemory;
using EYS.UseCases.Aksiyonlar;
using EYS.UseCases.Envanterler;
using EYS.UseCases.Envanterler.Interfaces;
using EYS.UseCases.PluginInterfaces;
using EYS.UseCases.Raporlar;
using EYS.UseCases.Urunler;
using EYS.WebApp.Components;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<EYSIcerik>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnvanterYonetim"));
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

if (builder.Environment.IsEnvironment("Testing"))
{
    //StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>(); // bir kere oluşturuluyor ve lazım olduğu zaman kullanılıyor.
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
    builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
    builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
}
else
{
    builder.Services.AddTransient<IInventoryRepository, EnvanterEFCoreRepository>(); // çağrıldığında oluşuyor kullanılıyor ve siliniyor.
    builder.Services.AddTransient<IProductRepository, UrunEFCoreRepository>();
    builder.Services.AddTransient<IInventoryTransactionRepository, InventoryTransactionEFCoreRepository>();
    builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionEFCoreRepository>();

}

builder.Services.AddTransient<IIsmeGoreEnvanterleriGoruntuleUseCase, IsmeGoreEnvanterleriGoruntuleUseCase>(); // çağrıldığında oluşuyor kullanılıyor ve siliniyor.
builder.Services.AddTransient<IEnvanterEkleUseCase, EnvanterEkleUseCase>();
builder.Services.AddTransient<IEnvanterDuzenleUseCase, EnvanterDuzenleUseCase>();
// builder.Services.AddScoped<IIsmeGoreEnvanterleriGoruntuleUseCase, IsmeGoreEnvanterleriGoruntuleUseCase>(); // çağrıldığında oluşuyor ve kalmaya devam ediyor.
builder.Services.AddTransient<IIDyeGoreEnvanterGoruntuleUseCase, IDyeGoreEnvanterGoruntuleUseCase>();
builder.Services.AddTransient<IEnvanterSilUseCase, EnvanterSilUseCase>();

builder.Services.AddTransient<IIsmeGoreUrunleriGoruntuleUseCase, IsmeGoreUrunleriGoruntuleUseCase>();
builder.Services.AddTransient<IUrunSilUseCase, UrunSilUseCase>();
builder.Services.AddTransient<IUrunEkleUseCase, UrunEkleUseCase>();
builder.Services.AddTransient<IUrunDuzenleUseCase, UrunDuzenleUseCase>();
builder.Services.AddTransient<IIDyeGoreUrunGoruntuleUseCase, IDyeGoreUrunGoruntuleUseCase>();

builder.Services.AddTransient<IEnvanterSatinAlUseCase, EnvanterSatinAlUseCase>();
builder.Services.AddTransient<IUrunUretUseCase, UrunUretUseCase>();
builder.Services.AddTransient<IUrunSatUseCase, UrunSatUseCase>();

builder.Services.AddTransient<IEnvanterAramaIslemleriUseCase, EnvanterAramaIslemleriUseCase>();
builder.Services.AddTransient<IUrunAramaIslemleriUseCase, UrunAramaIslemleriUseCase>();

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

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
