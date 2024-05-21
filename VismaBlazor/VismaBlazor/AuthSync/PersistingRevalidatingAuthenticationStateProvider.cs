using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace VismaBlazor.AuthSync
{
    //for sync til application med auth etter login Validere etter 60min
    public class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        
        private readonly IServiceScopeFactory _scope;
        private readonly PersistentComponentState _status;
        private readonly IdentityOptions _identityOptions;

        
        private readonly PersistingComponentStateSubscription _subscription;
        
        private Task<AuthenticationState>? _authStateTask;

        //Konstruktør for PRASP
        public PersistingRevalidatingAuthenticationStateProvider(ILoggerFactory loggerFactory, IServiceScopeFactory scope, PersistentComponentState state, IOptions<IdentityOptions> options) : base(loggerFactory)
        {
            _scope = scope;
            _status = state;
            _identityOptions = options.Value;

            AuthenticationStateChanged += OnAuthStateChange;
            _subscription = state.RegisterOnPersisting(onPersistingAsync, RenderMode.InteractiveWebAssembly );

        }

        //Velg timespan for revalidering.
        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(10);

        //metode for å validere stamp
        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authState, CancellationToken cancellationToken)
        {
            await using var scope = _scope.CreateAsyncScope();

            return ValiderSecurityStampAsync(authState.User);
       
        }

        //returnerer false vis bruker stamp ikke er tilstedet
        private bool ValiderSecurityStampAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated is false)
            {
                return false;
            }
            return true;
        }
    
        //Oppdaterer _authstatetask med ny bruker info
        private void OnAuthStateChange(Task<AuthenticationState> authStateTask)
        {
            _authStateTask = authStateTask;
            
        }

        //henter brukerinfo og lagrer i state
        private async Task onPersistingAsync()
        {
            if (_authStateTask is null)
            {
                throw new UnreachableException("Error: AuthState er null");
            }
            var authState = await _authStateTask;
            var principal = authState.User;
            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirstValue(_identityOptions.ClaimsIdentity.UserIdClaimType);
                var name = principal.FindFirstValue(_identityOptions.ClaimsIdentity.UserNameClaimType);
                var email = principal.FindFirstValue(_identityOptions.ClaimsIdentity.EmailClaimType);
                if (userId != null && name != null && email != null)
                {
                    _status.PersistAsJson(nameof(AuthBruker), new AuthBruker
                    {
                        BrukerId = userId,
                        BrukerNavn = name,
                        BrukerEpost = email,
                    });
                }
            }
        }
    }
}
//Kilder
//https://auth0.com/blog/auth0-authentication-blazor-web-apps/
//https://github.com/dotnet/aspnetcore/blob/main/src/ProjectTemplates/Web.ProjectTemplates/content/BlazorWeb-CSharp/BlazorWeb-CSharp/Components/Account/PersistingRevalidatingAuthenticationStateProvider.cs#L103
////////

