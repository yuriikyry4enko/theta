using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Theta.Interfaces;
using Xamarin.Forms;

namespace Theta.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        OidcClient _client;
        LoginResult _result;

        Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());

        public AuthorizationService()
        {
            var browser = DependencyService.Get<IBrowser>();

            var options = new OidcClientOptions
            {
                Authority = "https://vps428517.ovh.net:8443/auth/realms/FirstRealm/",
                ClientId = "xamarin-test-client",
                ClientSecret = "EHIVDTPbkefj1869563cmgow",
                //RedirectUri = "tethaxamarinclient:/callback",
                RedirectUri = "tethaxamarinclient",
                Scope = "openid stefans-test-backend-access",
                Browser = browser,

                ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
            };

            options.BackchannelHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true };

            _client = new OidcClient(options);
            _apiClient.Value.BaseAddress = new Uri("https://vps428517.ovh.net:8443/auth/realms/FirstRealm/");
        }

        public async Task Login()
        {
            _result = await _client.LoginAsync(new LoginRequest());

            if (_result.IsError)
            {
                return;
            }

            var sb = new StringBuilder(128);
            foreach (var claim in _result.User.Claims)
            {
                sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
            }

            sb.AppendFormat("\n{0}: {1}\n", "refresh token", _result?.RefreshToken ?? "none");
            sb.AppendFormat("\n{0}: {1}\n", "access token", _result.AccessToken);

            _apiClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _result?.AccessToken ?? "");
        }
    }
}

