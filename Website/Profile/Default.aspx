<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Profile_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1 class="page_header">Your Profile:</h1>
    <div class="row">
        <div class="col-md-12">
            <div>
                <h2>Profile</h2>
                <asp:Label ID="FirstName" runat="server" />
                <asp:Label ID="LastName" runat="server" />
                <asp:Label ID="CompanyName" runat="server" />
                <asp:Label ID="StreetAddress" runat="server" />
                <asp:Label ID="City" runat="server" />
                <asp:Label ID="State" runat="server" />
                <asp:Label ID="Country" runat="server" />
                <asp:Label ID="PostalCode" runat="server" />
            </div>
            <div>
                <h2>Contatct Details</h2>
                TODO: Email, phone, fax
            </div>
            <div>
                <h2>Purchase History</h2>
                TODO: Grid of all purchases
            </div>
        </div>
    </div>
</asp:Content>

