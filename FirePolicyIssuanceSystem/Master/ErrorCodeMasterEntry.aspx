<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMasterEntry.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.ErrorCodeMasterEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function successAlert(message, code) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            })

                .then((result) => {
                    if (result.isConfirmed) {
                        window.location = "ErrorCodeMasterEntry.aspx?code=" + code + "&mode=U";
                    }
                });
        }
    </script>
    <div class="m-5">
        <div class="d-flex justify-content-end mt-3">
            <asp:Button ID="btnCmeBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>
    <div class="form-container bg-primary-subtle">
        <h3 class="text-center  text-danger-emphasis">Error Code Master Entry Form</h3>

        <div class="container mt-4">

            <div class="form-group row">
                <div class="col-sm-4">
                    <label for="txtErrCode" class="col-form-label">Code</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtErrCode" runat="server" MaxLength="12" CssClass="form-control" AutoPostBack="true" placeholder="Enter Code" OnTextChanged="txtErrCode_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblValidationMessage1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvErrCode" runat="server" ControlToValidate="txtErrCode"
                        ErrorMessage="Error Code Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-4">
                    <label for="ddlErrType" class="col-form-label">Error Type</label>
                     <span style="color:red;">*</span>
                    <asp:DropDownList ID="ddlErrType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvErrType" runat="server" ControlToValidate="ddlErrType"
                        ErrorMessage="Error Type Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-4">
                    <label for="txtErrDescription" class="col-form-label">Description</label>
                     <span style="color:red;">*</span>
                    <asp:TextBox ID="txtErrDescription" runat="server" MaxLength="240" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvErrDescription" runat="server" ControlToValidate="txtErrDescription"
                        ErrorMessage="Description Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
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
