using Chinook.Framework.DAL;
using Chinook.Framework.DAL.Security; // for ApplicationUser
using Chinook.Framework.Entities.Security; // UserManager
using Microsoft.AspNet.Identity; // UserStore, AddWebMaster
using Microsoft.AspNet.Identity.EntityFramework; //ApplicationDbContext
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Chinook.Framework.BLL.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser> //CTRL + .
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext())) //CTRL + .
        {
        }

        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        /// <summary>
        /// Requires FirstName and LastName
        /// </summary>
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        /// <summary>
        /// Requires UserName
        /// </summary>
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.tba";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public void AddWebMaster()
        {
            if(!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };

                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }// end of AddWebMaster

        #region Standard CRUD Operations 
        [DataObjectMethod(DataObjectMethodType.Select, true)] //Oct18
        public List<UserProfile> ListAllUsers()
        {
            //Uses ApplicationDBContext "under the hood"
            var rm = new RoleManager();
            var result = from person in Users.ToList() // to grab data from db first
                         select new UserProfile() //Constructor
                         {
                             UserId = person.Id, //Asigning vlues to properties
                             UserName = person.UserName,
                             Email = person.Email,
                             EmailConfirmed = person.EmailConfirmed,
                             CustomerId = person.CustomerID,
                             EmployeeId = person.EmployeeID,
                             RoleMemberships = person.Roles.Select(r => rm.FindById(r.RoleId).Name)
                         }; //This is a Initalizaer List -- no first name/last name 

            //uses  chinnokcontext to get first/last names of users  //Oct 18
            using (var context = new ChinookContext())
            {
                foreach(var person in result)
                {
                    if(person.EmployeeId.HasValue)
                    {
                        var employee = context.Employees.Find(person.EmployeeId);
                        if(employee != null)
                        {
                            person.FirstName = employee.FirstName;
                            person.LastName = employee.LastName;
                        }

                     else if(person.CustomerId.HasValue)
                        {
                            var customer = context.Customers.Find(person.CustomerId);
                            if (customer != null)
                            {
                                person.FirstName = customer.FirstName;
                                person.LastName = customer.LastName;
                            }

                        }
                    }
                }
            }
            return result.ToList();
        } //end of ListAllUsers
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddUser(UserProfile userInfo)
        {
            //TODO:
            //convert the DTO data I receive into my Entity data for the ApplicationUser
            var userAcount = new ApplicationUser()
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email
            };

            //add the user account
            this.Create(userAcount, STR_DEFAULT_PASSWORD);

            //add this user to all the roles that were specified  in the UserProfile
            foreach (string roleName in userInfo.RoleMemberships)
            {
                this.AddToRole(userAcount.Id, roleName);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveUser(UserProfile userInfo)
        {
            //TODO:
        }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnregisteredUser> ListAllUnregisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                // Make an in-memory list of employees who have login accounts
                var registeredEmployees = (from emp in Users
                                           where emp.EmployeeID.HasValue
                                           select emp.EmployeeID).ToList();
                // Query employees who don't have login accounts.
                // Make it in-memory (.ToList()) for the next step of assigning usernames/emails
                var employees = (from emp in context.Employees
                                 where !registeredEmployees.Any(e => emp.EmployeeId == e)
                                 select new UnregisteredUser()
                                 {
                                     Id = emp.EmployeeId,
                                     FirstName = emp.FirstName,
                                     LastName = emp.LastName,
                                     UserType = UnregisteredUserType.Employee
                                 }).ToList();
                // Assign employee usernames and emails
                foreach (var person in employees)
                {
                    person.AssignedUserName = string.Format(STR_USERNAME_FORMAT, person.FirstName, person.LastName);
                    person.AssignedEmail = string.Format(STR_EMAIL_FORMAT, person.AssignedUserName);
                }

                // Make an in-memory list of customers who have login accounts
                var registeredCustomers = (from cust in Users
                                           where cust.CustomerID != null
                                           select cust.CustomerID).ToList();
                // Query customers who don't have login accounts.
                var customers = from cust in context.Customers
                                where !registeredCustomers.Any(c => cust.CustomerId == c)
                                select new UnregisteredUser()
                                {
                                    Id = cust.CustomerId,
                                    FirstName = cust.FirstName,
                                    LastName = cust.LastName,
                                    UserType = UnregisteredUserType.Customer
                                };

                // Merge and return the results
                return employees.Union(customers).ToList();
            }
        }

        public void RegisterUser(UnregisteredUser userInfo)
        {
            // string randomPassword = Guid.NewGuid().ToString().Replace("-", "");
            var userAccount = new ApplicationUser()
            {
                UserName = userInfo.AssignedUserName,
                Email = userInfo.AssignedEmail
            };
            switch (userInfo.UserType)
            {
                case UnregisteredUserType.Customer:
                    userAccount.CustomerID = userInfo.Id;
                    break;
                case UnregisteredUserType.Employee:
                    userAccount.EmployeeID = userInfo.Id;
                    break;
            }

            this.Create(userAccount, STR_DEFAULT_PASSWORD); // or randomPassword
            switch (userInfo.UserType)
            {
                case UnregisteredUserType.Employee:
                    this.AddToRole(userAccount.Id, SecurityRoles.Staff);
                    break;
                case UnregisteredUserType.Customer:
                    this.AddToRole(userAccount.Id, SecurityRoles.RegisteredUsers);
                    break;
            }
        }
    }
}
