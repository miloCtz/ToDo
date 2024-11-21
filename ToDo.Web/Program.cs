using Microsoft.EntityFrameworkCore;
using Scrutor;
using ToDo.Data;
using ToDo.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();


builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                ToDo.Data.AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ToDo.Application.AssemblyReference.Assembly));

builder.Services.AddDbContext<DatabaseContext>(
    (sp, optionsBuilder) =>
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnetionString"));
    });

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
