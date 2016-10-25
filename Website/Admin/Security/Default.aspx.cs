using Chinook.Framework.BLL.Security;
using Chinook.Framework.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Force all users who are not in the WebsiteAdmins role to go back to the home page.
        if (!User.IsInRole("WebsiteAdmins"))
            Response.Redirect("~/Default.aspx", true);
    }

    protected List<string> GetRoleNames()
    {
        var mgr = new RoleManager(); //dont forget the using statement at the top

        List<string> data = mgr.Roles.Select(r => r.Name).ToList();

        return data;
    }

    protected void UserListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        var addToRoles = new List<string>();
        var rolesCheckBoxList = e.Item.FindControl("RoleMemberships") as CheckBoxList; //this is a "safe cast"
        //                                         \  id of the    / 
        //                                         \   control    /  \safe casting = if it is the correct datatype. and not return a null
        //                                          \on the form /
        if (rolesCheckBoxList != null)
        {
            foreach(ListItem item in rolesCheckBoxList.Items)
            {
                if(item.Selected)
                {
                    addToRoles.Add(item.Value);
                }
            }
        }

        e.Values["RoleMemberships"] = addToRoles; // "RoleMemberships" is the property name of the UserProfile class
    }

    protected void UnregisteredUsersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        UnregisteredUsersGridView.SelectedIndex = e.NewSelectedIndex;
        GridViewRow row = UnregisteredUsersGridView.SelectedRow;
        if (row != null)
        {
            string userName = null, email = null;
            UnregisteredUserType userType;
            TextBox input;
            input = row.FindControl("GivenUserName") as TextBox;
            if (input != null)
                userName = input.Text;
            input = row.FindControl("GivenEmail") as TextBox;
            if (input != null)
                email = input.Text;
            userType = (UnregisteredUserType)Enum.Parse(typeof(UnregisteredUserType), row.Cells[1].Text);
            UnregisteredUser user = new UnregisteredUser()
            {
                Id = int.Parse(UnregisteredUsersGridView.SelectedDataKey.Value.ToString()),
                UserType = userType,
                FirstName = row.Cells[2].Text,
                LastName = row.Cells[3].Text,
                AssignedUserName = userName,
                AssignedEmail = email
            };

            UserManager manager = new UserManager();
            manager.RegisterUser(user);
            DataBind();
        }
    }
}