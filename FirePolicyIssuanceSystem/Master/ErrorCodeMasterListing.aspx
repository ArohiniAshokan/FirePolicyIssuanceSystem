<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMasterListing.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.ErrorCodeMasterListing" %>

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
        <h3 class="text-center  text-danger-emphasis">Error Code Master Listing</h3>
        <div class="d-flex justify-content-between mt-3">
            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnCmlBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>

        <div class="card small-card bg-primary-subtle">
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="ddlErrType" class="form-label">Error Type</label>
                    <asp:DropDownList ID="ddlErrType" runat="server" CssClass="form-select form-select-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4 d-flex align-items-end">
                    <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" CssClass="btn btn-primary btn-sm me-2" OnClick="btnApplyFilter_Click" />
                <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm" OnClick="btnClearFilter_Click" />
                    </div>
            </div>
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ERR_CODE"
            OnRowDeleting="GridView1_RowDeleting" CssClass="table table-striped table-bordered table-sm table-responsive table-hover"  AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging">

            <Columns>
                <asp:BoundField DataField="ERR_CODE" HeaderText="Code" ReadOnly="True" />
                <asp:BoundField DataField="ERR_TYPE" HeaderText="Type" />
                <asp:BoundField DataField="ERR_DESC" HeaderText="Description" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-danger" CommandName="Update" CommandArgument='<%# Eval("ERR_CODE") %>' OnCommand="btnUpdate_Command" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-dark" CommandName="Delete" CommandArgument='<%# Eval("ERR_CODE") %>' OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
