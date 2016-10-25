using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chinook.Framework.DAL; //for the ChinnokContext
using Chinook.Framework.Entities; //for chinook database Enities
using Chinook.Framework.Entities.ViewModels; //for DTOS/POCOs, etc.

namespace Chinook.Framework.BLL
{
    public class CustomerManager
    {
        public CustomerProfile GetProfile(int customerId)
        {
            // Access the database 
            using (var context = new ChinookContext())
            {
                var profile = from person in context.Customers//Add context for database access
                              where person.CustomerId == customerId
                              select new CustomerProfile()
                              {
                                  FirstName = person.FirstName,
                                  LastName = person.LastName,
                                  CompanyName = person.Company,
                                  StreetAddress = person.Address,
                                  City = person.City,
                                  State = person.State,
                                  Country = person.Country,
                                  PostalCode = person.PostalCode

                              };
                return profile.Single(); //add Single to change IQueryable to CustomerProfile 
                                           //and to make sure only one row CustomerProfile of is returned
            }
        }
    }
}
