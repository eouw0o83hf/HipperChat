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
        ICollection<User> GetUsers(string apiKey);
    }

    public class UserService : BaseService, IUserService
    {
        public ICollection<User> GetUsers(string apiKey)
        {
            var response = GetActionResult<GenericResult<User>>("user", apiKey);
            return response.Items;
        }
    }
}
