using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Framework.Entities.Security
{
    internal static class SecurityRoles // is internal: only visable(public) in this class library 
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string RegisteredUsers = "RegisteredUsers";
        public const string Staff = "Staff";

        public static List<string> DefualtSecurityRoles
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(WebsiteAdmins);
                value.Add(RegisteredUsers);
                value.Add(Staff);
                return value;
            }
        }
    }
}
