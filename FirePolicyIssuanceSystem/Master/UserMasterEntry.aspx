<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="UserMasterEntry.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.UserMasterEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function successAlert(message, userId) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            })

                .then((result) => {
                    if (result.isConfirmed) {
                        window.location = "UserMasterEntry.aspx?userId=" + userId + "&mode=U";
                    }
                });
        }
    </script>
    <div class="m-5">
        <div class="d-flex justify-content-end mt-3">
            <asp:Button ID="btnUmBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnUmBack_Click" CausesValidation="false" />
        </div>
    <div class="form-container bg-primary-subtle">
        <h3 class="text-center  text-danger-emphasis">User Master Entry Form</h3>


        <div class="container mt-4">

            <div class="form-group row">
                <div class="col-sm-3">
                    <label for="txtUserId" class="col-form-label">User ID</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtUserId" runat="server" MaxLength="12" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUserId_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblValidationMessage1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                        ErrorMessage="User Id Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>


                <div class="col-sm-3">
                    <label for="txtUserName" class="col-form-label">User Name</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>                  
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="User Name Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-3">
                    <label for="txtPassword" class="col-form-label">Password</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" MaxLength="30" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required"
                        CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    <%-- <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" 
                    ErrorMessage="Password must be at least 8 characters long and contain a number" CssClass="text-danger" ValidationExpression="^(?=.*\d).{8,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    --%>
                </div>

                <div class="col-sm-3">
                    <label for="chkActiveYn">Active</label><br />
                    <asp:CheckBox ID="chkActiveYn" runat="server" Checked="true"/>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-center mt-4">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success me-3" OnClick="btnSave_Click" Visible="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnUpdate_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-secondary" OnClick="btnCancel_Click" Visible="false" />
        </div>
    </div>
        </div>
</asp:Content>
