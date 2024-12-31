<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="CodeMasterEntry.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.CodeMasterEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function successAlert(message, code, type) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            })

                .then((result) => {
                    if (result.isConfirmed) {
                        window.location = "CodeMasterEntry.aspx?code=" + code + "&type=" + type + "&mode=U";
                    }
                });
        }
    </script>
    <div class="m-5">
        <div class="d-flex justify-content-end mb-3">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>
    <div class="form-container bg-primary-subtle">

        <h3 class="text-center  text-danger-emphasis">Code Master Entry Form</h3>
       
        <div class="container mt-4">

            <div class="form-group row">
                <div class="col-sm-4">
                    <label for="txtCode" class="col-form-label">Code</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="12" CssClass="form-control" AutoPostBack="true" placeholder="Enter Code" OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblValidationMessage1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>


                <div class="col-sm-4">
                    <label for="txtType" class="col-form-label">Type</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtType" runat="server" MaxLength="12" CssClass="form-control" AutoPostBack="true" placeholder="Enter Type" OnTextChanged="txtType_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblValidationMessage2" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="txtType"
                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-4">
                    <label for="txtValue" class="col-form-label">Value</label>
                    <asp:TextBox ID="txtValue" runat="server" MaxLength="12" CssClass="form-control" placeholder="Enter Value"></asp:TextBox>
                </div>

            </div>

            <div class="form-group row">
                <div class="col-sm-4">
                    <label for="txtDescription" class="col-form-label">Description</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="240" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-4">
                    <label for="chkActive">Active</label><br />
                    <asp:CheckBox ID="chkActive" runat="server" Checked="true"/>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-center mt-4">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success me-3" OnClick="btnSave_Click" Visible="false" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnUpdate_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-secondary " OnClick="btnCancel_Click" Visible="false" />
        </div>
    </div>
</div>
</asp:Content>
