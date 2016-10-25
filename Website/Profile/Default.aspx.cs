using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chinook.Framework.BLL; //

public partial class Profile_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            // Assuming the customer is logged in, we chould
            // get his customerID. But, for now, pretend
            // the id is one
            int custId = 2;
            CustomerManager manager = new CustomerManager();
            var theProfile = manager.GetProfile(custId);
            // Now, put the data in the controls on the page.

            FirstName.Text = theProfile.FirstName;
            LastName.Text = theProfile.LastName;
            CompanyName.Text = theProfile.CompanyName;
            StreetAddress.Text = theProfile.StreetAddress;
            City.Text = theProfile.City;
            State.Text = theProfile.State;
            Country.Text = theProfile.Country;
            PostalCode.Text = theProfile.PostalCode;
        }
    }
}