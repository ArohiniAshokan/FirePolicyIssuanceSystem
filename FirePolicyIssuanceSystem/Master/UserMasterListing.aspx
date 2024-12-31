<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="UserMasterListing.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.UserMasterListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function successAlert(message) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            });
        }
    </script>
    <div class="container mt-5">
        <h3 class="text-center  text-danger-emphasis">User Master Listing</h3>
        <div class="d-flex justify-content-between mt-3">
            <asp:Button ID="btnAddNewUser" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btnAddNewUser_Click" />
            <asp:Button ID="btnUmlBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnUmlBack_Click" CausesValidation="false" />
        </div>

        <div class="card small-card bg-primary-subtle">
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="txtFilterUserName" class="form-label">User Name</label>
                    <asp:TextBox ID="txtFilterUserName" runat="server" placeholder="Enter Username" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-primary btn-sm me-2" OnClick="btnApplyFilter_Click" />
                 <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm" OnClick="btnClearFilter_Click"/>
                </div>
            </div>
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="USER_ID"
            OnRowDeleting="GridView1_RowDeleting" CssClass="table table-striped table-bordered table-sm table-responsive table-hover">

            <Columns>
                <asp:BoundField DataField="USER_ID" HeaderText="User Id" ReadOnly="True" />
                <asp:BoundField DataField="USER_NAME" HeaderText="User Name" />
                <asp:BoundField DataField="USER_PASSWORD" HeaderText="Password" DataFormatString="****"/>
                <asp:BoundField DataField="USER_ACTIVE_YN" HeaderText="Active" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUserUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-sm btn-danger" CommandArgument='<%# Eval("USER_ID") %>' OnCommand="btnUserUpdate_Command" />
                        <asp:Button ID="btnUserDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-sm btn-dark" CommandArgument='<%# Eval("USER_ID") %>' OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
