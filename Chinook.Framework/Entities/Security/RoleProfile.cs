using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Framework.Entities.Security
{
    /// <summary>
    /// RoleProfile is a Data Transfer Object summarizing information
    /// about a single security role on the site.
    /// </summary>
    public class RoleProfile
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<string> UserNames { get; set; }
    }
}
