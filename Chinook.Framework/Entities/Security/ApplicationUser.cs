using Microsoft.AspNet.Identity.EntityFramework;// for Identity User

namespace Chinook.Framework.Entities.Security
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser // used ctrl + .
    {
        // These custom properties will be used to asssociate a login user 
        //   with either an Employee or a Customer in the database
        //*Notice that the data type matches the datatype in the "related"
        //  tables, but that its also nullable.
        // Also note that there is NO forign key constraint on our database
        public int? EmployeeID { get; set; }
        public int? CustomerID { get; set; }
    }
}
