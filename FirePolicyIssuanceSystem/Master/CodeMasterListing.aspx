<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="CodeMasterListing.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.CodeMasterListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

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
        <h3 class="text-center text-danger-emphasis">Code Master Listing</h3>
        <div class="d-flex justify-content-between m-3">
            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>

        <div class="card small-card bg-primary-subtle">
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="ddlFilterCmType" class="form-label">CM Type</label>
                    <asp:DropDownList ID="ddlFilterCmType" runat="server" CssClass="form-select form-select-sm bg-white" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-primary btn-sm me-2" OnClick="btnApplyFilter_Click" />
                     <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm" OnClick="btnClearFilter_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="CM_CODE,CM_TYPE"
            OnRowDeleting="UsersGridView_RowDeleting" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered table-sm table-responsive table-hover table-striped">

            <Columns>
                <asp:BoundField DataField="CM_CODE" HeaderText="Code" ReadOnly="True" />
                <asp:BoundField DataField="CM_TYPE" HeaderText="Type" ReadOnly="True" />
                <asp:BoundField DataField="CM_VALUE" HeaderText="Value" />
                <asp:BoundField DataField="CM_DESC" HeaderText="Description" />
                <asp:BoundField DataField="CM_ACTIVE_YN" HeaderText="Active" />
                <asp:TemplateField HeaderText="Action" >
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-danger" CommandName="Update" CommandArgument='<%# Eval("CM_CODE") + ";" + Eval("CM_TYPE") %>' OnCommand="btnUpdate_Command" />
                        <asp:Button ID="btnDelete" runat="server"  CommandName="Delete" CssClass="btn btn-sm btn-dark" CommandArgument='<%# Eval("CM_CODE") + ";" + Eval("CM_TYPE") %>' OnClientClick="return confirm('Are you sure you want to delete ?');" Text="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
