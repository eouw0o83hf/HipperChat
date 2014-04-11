using HipperChat.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Authentication
{
    public interface IAuthenticationService
    {
        Token GetToken(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public Token GetToken(string username, string password)
        {
            var response = HttpHelpers.PostObject("https://api.hipchat.com/v2/oauth/token", new AuthenticationRequest
            {
                Username = username,
                Password = password,
                GrantType = GrantTypeEnum.password,
                Scopes = new []
                {
                    Scopes.SendMessage,
                    Scopes.SendNotifications,
                    Scopes.ViewGroup
                }
            });
            return JsonConvert.DeserializeObject<Token>(response);
        }
    }
}
