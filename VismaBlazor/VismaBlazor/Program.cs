
using VismaBlazor.Components;
using VismaBlazor;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using VismaBlazor.AuthSync;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// variebler for å hente config fra appsettings.json
/*
Environment.SetEnvironmentVariable("Audience", builder.Configuration.GetSection("Auth0").GetSection("Audience").Value);
Environment.SetEnvironmentVariable("Domain", builder.Configuration.GetSection("Auth0").GetSection("Domain").Value);
Environment.SetEnvironmentVariable("ClientId", builder.Configuration.GetSection("Auth0").GetSection("ClientId").Value);
Environment.SetEnvironmentVariable("ClientSecret", builder.Configuration.GetSection("Auth0").GetSection("ClientSecret").Value);
*/

//variebler for å hente config fra heroku
var audience = Environment.GetEnvironmentVariable("Audience");
var domain = Environment.GetEnvironmentVariable("Domain");
var clientid = Environment.GetEnvironmentVariable("ClientId");
var clientsecret = Environment.GetEnvironmentVariable("ClientSecret");



//legger til services for razor components og interactive components siden dette er server side app
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();



//legger til auth0 webapp authentication (med config fra appsettings.json eller config fra heroku i hosted versjon)
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = domain;
    options.ClientId = clientid;

    //options.Domain = builder.Configuration["Auth0:Domain"];
    //options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddScoped<HttpClientPost>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
//flytt denne til under logout vis ikke funker
app.UseHttpsRedirection();

//standard error handling som kom stock med template
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

//bruker login sender login request til auth0 bygger login authentication properties
app.MapGet("/account/login", async (HttpContext context, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
    .WithRedirectUri(redirectUri)
    .Build();

    await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    Console.WriteLine("Login");
});
//bruker logout sender logout request til auth0 bygger logout authentication properties og logger ut (funker på hosted versjon men refresher ikke siden som på localhost)
app.MapGet("/account/logout", async (HttpContext context, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
    .WithRedirectUri(redirectUri)
    .Build();

    await context.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    return new RedirectResult(redirectUri);
});


app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedProto });

//setter opp routing for razor components og interactive components
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode();

//kjører appen
app.Run();
