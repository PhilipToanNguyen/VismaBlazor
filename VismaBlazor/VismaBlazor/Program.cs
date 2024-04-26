
using VismaBlazor.Components;
using VismaBlazor;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using VismaBlazor.AuthSync;

var builder = WebApplication.CreateBuilder(args);

/*Environment.SetEnvironmentVariable("Audience", builder.Configuration.GetSection("Auth0").GetSection("Audience").Value);
Environment.SetEnvironmentVariable("Domain", builder.Configuration.GetSection("Auth0").GetSection("Domain").Value);
Environment.SetEnvironmentVariable("ClientId", builder.Configuration.GetSection("Auth0").GetSection("ClientId").Value);
Environment.SetEnvironmentVariable("ClientSecret", builder.Configuration.GetSection("Auth0").GetSection("ClientSecret").Value);*/

Environment.GetEnvironmentVariable("Audience");
Environment.GetEnvironmentVariable("Domain");
Environment.GetEnvironmentVariable("ClientId");
Environment.GetEnvironmentVariable("ClientSecret");



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();



//Add services for authentication
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Domain"];
    options.ClientId = builder.Configuration["ClientId"];
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();


builder.Services.AddScoped<HttpClientPost>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapGet("/Account/Login", async (HttpContext context, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
    .WithRedirectUri(redirectUri)
    .Build();

    await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    Console.WriteLine("Login");
});

app.MapGet("/Account/Logout", async (HttpContext context, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
    .WithRedirectUri(redirectUri)
    .Build();

    await context.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    
    .AddInteractiveServerRenderMode();


app.Run();
