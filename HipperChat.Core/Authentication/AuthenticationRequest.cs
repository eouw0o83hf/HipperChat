using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipperChat.Core.Authentication
{
    public class AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public GrantTypeEnum? GrantType { get; set; }

        [JsonProperty("grant_type")]
        public string GrantTypeString { get { return GrantType.Value.ToString().ToLowerInvariant(); } }

        /// <summary>
        /// Only for authorization_code grant type
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Only for authorization_code grant type
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonIgnore]
        public ICollection<string> Scopes { get; set; }

        [JsonProperty("scopes")]
        public string ScopesString { get { return string.Join(" ", Scopes); } }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public enum GrantTypeEnum
    {
        authorization_code, 
        refresh_token, 
        password, 
        client_credentials, 
        personal, 
        room_notification
    }

    public static class Scopes
    {
        /// <summary>
        /// Perform group administrative tasks
        /// </summary>
        public const string AdminGroup = "admin_group";

        /// <summary>
        /// Perform room administrative tasks
        /// </summary>
        public const string AdminRoom = "admin_room";

        /// <summary>
        /// Create, update, and remove rooms
        /// </summary>
        public const string ManageRooms = "manage_rooms";

        /// <summary>
        /// Send private one-on-one messages
        /// </summary>
        public const string SendMessage = "send_message";

        /// <summary>
        /// Send room notifications
        /// </summary>
        public const string SendNotifications = "send_notification";

        /// <summary>
        /// View users, rooms, and other group information
        /// </summary>
        public const string ViewGroup = "view_group";
    }
}
