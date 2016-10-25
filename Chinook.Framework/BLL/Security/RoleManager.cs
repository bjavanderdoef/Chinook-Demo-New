using Chinook.Framework.DAL.Security; // for ApplicationDbContext
using Chinook.Framework.Entities.Security;// for SecurityRoles
using Microsoft.AspNet.Identity; // for RoleManager, AddDefaultRoles
using Microsoft.AspNet.Identity.EntityFramework; //for IdentityRole
using System;
using System.Collections.Generic;
using System.ComponentModel; // for DataObject & other attributes
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Framework.BLL.Security
{
    [DataObject]
    public class RoleManager : RoleManager<IdentityRole> //CTRL + .
    {
        public RoleManager()
            : base(new RoleStore<IdentityRole>(new ApplicationDbContext())) //CTRL + .
        {

        }

        public void  AddDefaultRoles()
        {
            foreach (string roleName in SecurityRoles.DefualtSecurityRoles) //CTRL + .
            {
                //Check if it exists
                if (!Roles.Any(r => r.Name == roleName))
                    this.Create(new IdentityRole(roleName)); // if not, adds to IdentityRole
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RoleProfile> ListAllRoles()
        {
            var um = new UserManager();
            var result = from data in Roles.ToList() //Force the query of data first and then get the results in memory, allows you to use the c# Method FindByID
                         select new RoleProfile()
                         {
                             RoleId = data.Id,
                             RoleName = data.Name,
                             UserNames = data.Users.Select(u => um.FindById(u.UserId).UserName)
                         };
             return result.ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddRole(RoleProfile role)
        {
            if (!this.RoleExists(role.RoleName))
                this.Create(new IdentityRole(role.RoleName));
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveRole(RoleProfile role)
        {
            this.Delete(this.FindById(role.RoleId));
        }
    }
}
