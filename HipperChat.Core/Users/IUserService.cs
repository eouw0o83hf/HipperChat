using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HipperChat.Core.Rooms;
using Newtonsoft.Json;

namespace HipperChat.Core.Users
{
    public interface IUserService
    {
        ICollection<User> GetUsers();
    }

    public class UserService : IUserService
    {
        private readonly string _apiKey;

        public UserService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public ICollection<User> GetUsers()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString("https://api.hipchat.com/v2/user?format=json&auth_token=" + _apiKey);
                var response = JsonConvert.DeserializeObject<GenericResult<User>>(json);
                return response.Items;
            }
        }
    }
}
