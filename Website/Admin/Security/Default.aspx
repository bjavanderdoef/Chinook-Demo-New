<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Security_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class ="row jumbotron">
        <h1>Site Administrator</h1>
    </div>

    <div class="row">
        <div class="col-md-12">
            <!--Nav Tabs -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#users" data-toggle="tab">Users</a></li>
                <li><a href="#roles" data-toggle="tab">Security Roles</a></li>
                <li><a href="#unregistered" data-toggle="tab">Unregistered</a></li>
            </ul>

            <div class="tab-content">
                <div class="tab-pane fade in active" id="users"> <%--Oct 18--%>
                    <asp:ListView ID="UserListView" runat="server" DataSourceID="UserProfileDataSource"
                        ItemType="Chinook.Framework.Entities.Security.UserProfile" DataKeyNames="UserId" InsertItemPosition="LastItem" OnItemInserting="UserListView_ItemInserting">
                                                                                                                            <%--or InsertTemplate wont show--%>
                        <LayoutTemplate>
                             <div class="row bg-info">
                                 <div class="col-md-2 h4">Action</div>
                                 <div class="col-md-2 h4">User Name</div>
                                 <div class="col-md-5 h4">Profile</div>
                                 <div class="col-md-3 h4">Roles</div>
                             </div>
                             <div runat="server" id="itemPlaceHolder"></div>
                        </LayoutTemplate>

                        <ItemTemplate>
                             <div class="row">
                                 <div class="col-md-2 h4"></div>
                                 <div class="col-md-2 h4"><%# Item.UserName %></div> <%--//ListView Item--%>
                                 <div class="col-md-5 h4">
                                     <%# Item.Email %>
                                     <%# Item.FirstName + " " + Item.LastName %>
                                 </div>
                                 <div class="col-md-3 h4">
                                     <asp:Repeater ID="RoleUserRepeater" runat="server" DataSource="<%# Item.RoleMemberships %>" 
                                         ItemType="System.String">
                                         <ItemTemplate>
                                             <%# Item %> <%--Repeator Item--%>
                                         </ItemTemplate>
                                         <SeparatorTemplate>, </SeparatorTemplate> <%--//Incase Multiple Roles--%>
                                     </asp:Repeater>
                                 </div>
                             </div>
                         </ItemTemplate>

                        <InsertItemTemplate>
                            <hr />
                            <div class="row">
                                <div class="col-md-2 h4">
                                    <asp:LinkButton runat="server" ID="InsertButton"
                                        CommandName="Insert" Text="Add User"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="CancelButton"
                                        CommandName="Cancel" Text="Clear"></asp:LinkButton>
                                </div>
                                 <div class="col-md-2 h4">
                                     <asp:TextBox runat="server" ID="UserNameTextBox"
                                         placeholder="User Name" Text="<%#BindItem.UserName %>"></asp:TextBox>
                                 </div> 
                                 <div class="col-md-5 h4">
                                     <asp:TextBox runat="server" ID="EmailTextBox"
                                         placeholder="Email Address"
                                         TextMode="Email" Text="<%#BindItem.Email %>"></asp:TextBox>
                                 </div>
                                 <div class="col-md-3 h4">
                                     
                                     <asp:CheckBoxList ID="RoleMemberships" runat="server" DataSource="<%# GetRoleNames() %>">

                                     </asp:CheckBoxList>
                                 </div>
                            </div>

                        </InsertItemTemplate>

                    </asp:ListView>


                    <asp:ObjectDataSource ID="UserProfileDataSource" runat="server"
                         DataObjectTypeName="Chinook.Framework.Entities.Security.UserProfile" 
                        DeleteMethod="RemoveUser" InsertMethod="AddUser" OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListAllUsers" TypeName="Chinook.Framework.BLL.Security.UserManager"></asp:ObjectDataSource>
                </div>

                 <div class="tab-pane fade" id="roles">

                     <asp:ListView ID="RoleListView" runat="server" 
                         DataSourceID="RoleDataSource" DataKeyNames="RoleId" 
                         ItemType="Chinook.Framework.Entities.Security.RoleProfile" InsertItemPosition="LastItem">
                         
                         <LayoutTemplate>
                             <div class="row bg-info">
                                 <div class="col-md-3 h4">Action</div>
                                 <div class="col-md-3 h4">Role</div>
                                 <div class="col-md-6 h4">Members</div>
                             </div>
                             <div runat="server" id="itemPlaceHolder"></div>
                         </LayoutTemplate>

                         <ItemTemplate>
                             <div class="row">
                                 <div class="col-md-3">
                                     <asp:LinkButton runat="server" ID="DeleteButton" CommandName="Delete" Text="Delete" />
                                                                                        <%--CommandName connects to methods in DataSource--%>
                                 </div>
                                 <div class="col-md-3">
                                     <%#Item.RoleName %>
                                 </div>
                                 <div class="col-md-6">
                                     <asp:Repeater ID="RoleUserRepeater" runat="server" ItemType="System.String" DataSource="<%#Item.UserNames %>">
                                         <ItemTemplate>
                                             <%# Item %>
                                         </ItemTemplate>
                                         <SeparatorTemplate>, </SeparatorTemplate>
                                     </asp:Repeater>
                                 </div>
                             </div>
                         </ItemTemplate>
                         <InsertItemTemplate>
                             <hr />
                             <div class="row">
                                 <div class="col-md-3">
                                     <asp:LinkButton runat="server" ID="InsertButton"
                                         CommandName="Insert" Text="AddRole" /> <br />
                                     <asp:LinkButton runat="server" ID="CancelButton"
                                         CommandName="Cancel" Text="Clear" />
                                 </div>
                                 <div class="col-md-3">
                                     <asp:TextBox runat="server" ID="RoleNameTextBox"
                                         placeholder="Role Name"
                                         Text="<%# BindItem.RoleName %>" />
                                 </div>
                             </div>
                         </InsertItemTemplate>

                     </asp:ListView>

                     <asp:ObjectDataSource ID="RoleDataSource" runat="server" 
                         DataObjectTypeName="Chinook.Framework.Entities.Security.RoleProfile" 
                         DeleteMethod="RemoveRole" 
                         InsertMethod="AddRole" OldValuesParameterFormatString="original_{0}" 
                         SelectMethod="ListAllRoles" TypeName="Chinook.Framework.BLL.Security.RoleManager"></asp:ObjectDataSource>
                 </div>

                 <div class="tab-pane fade" id="unregistered">
                     <asp:GridView ID="UnregisteredUsersGridView" runat="server" 
                                    CssClass="table table-hover" AutoGenerateColumns="False" 
                                    DataSourceID="UnregisteredUserDataSource"
                                    ItemType="Chinook.Framework.Entities.Security.UnregisteredUser" 
                                    AllowPaging="True" DataKeyNames="Id"
                                    OnSelectedIndexChanging="UnregisteredUsersGridView_SelectedIndexChanging">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                            <asp:BoundField DataField="UserType" HeaderText="User Type" SortExpression="UserType"></asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"></asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"></asp:BoundField>
                            <asp:TemplateField HeaderText="Assigned UserName" SortExpression="AssignedUserName">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("AssignedUserName") %>' ID="GivenUserName"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned Email" SortExpression="AssignedEmail">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("AssignedEmail") %>' ID="GivenEmail"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource runat="server" ID="UnregisteredUserDataSource" OldValuesParameterFormatString="original_{0}"
                         SelectMethod="ListAllUnregisteredUsers" TypeName="Chinook.Framework.BLL.Security.UserManager">
                    </asp:ObjectDataSource>
                 </div>
            </div>
        </div>
    </div>
</asp:Content>

