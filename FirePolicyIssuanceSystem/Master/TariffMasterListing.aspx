<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="TariffMasterListing.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.TariffMasterListing" %>

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
    <style>
        .left-align {
            text-align: left;
        }

        .right-align {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }
    </style>

    <div class="m-5">
        <h3 class="text-center  text-danger-emphasis">Tariff Master Listing</h3>
        <div class="d-flex justify-content-between mt-3">
            <asp:Button ID="btnAddNewTariff" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btnAddNewTariff_Click" />
            <asp:Button ID="btnTmlBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnTmlBack_Click" CausesValidation="false" />
        </div>

        <div class="card small-card bg-primary-subtle">
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="ddlFilterRiskClass" class="form-label">Risk Class</label>
                    <asp:DropDownList ID="ddlFilterRiskClass" runat="server" CssClass="form-select form-select-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="ddlFilterRiskOccup" class="form-label">Risk Occupancy</label>
                    <asp:DropDownList ID="ddlFilterRiskOccup" runat="server" CssClass="form-select form-select-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="txtFilterSI" class="form-label">Sum Insured</label>
                    <asp:TextBox ID="txtFilterSI" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>
            </div>
            <div class="text-center">
                <asp:Button ID="btnApplyFilters" runat="server" Text="Apply Filters" CssClass="btn btn-primary btn-sm me-2" OnClick="btnApplyFilters_Click" />
             <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm" OnClick="btnClearFilter_Click" />
            </div>
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="TM_UID" EmptyDataText="No Records Found"
            OnRowDeleting="GridView1_RowDeleting" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-striped table-bordered table-sm table-responsive table-hover">

            <Columns>
                <asp:BoundField DataField="TM_UID" HeaderText="UId" ReadOnly="True" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" />
                <asp:BoundField DataField="RISK_CLASS_FM" HeaderText="Class From" HeaderStyle-CssClass="left-align" ItemStyle-CssClass="left-align" />
                <asp:BoundField DataField="RISK_CLASS_TO" HeaderText="Class To" HeaderStyle-CssClass="left-align" ItemStyle-CssClass="left-align" />
                <asp:BoundField DataField="RISK_OCCP_FM" HeaderText="Occupancy From" HeaderStyle-CssClass="left-align" ItemStyle-CssClass="left-align" />
                <asp:BoundField DataField="RISK_OCCP_TO" HeaderText="Occupancy To" HeaderStyle-CssClass="left-align" ItemStyle-CssClass="left-align" />
                <asp:BoundField DataField="TM_RISK_SI_FM" HeaderText="SI From" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="TM_RISK_SI_TO" HeaderText="SI To" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="TM_RISK_RATE" HeaderText="Risk Rate" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnTariffUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-primary" CommandName="Update" CommandArgument='<%# Eval("TM_UID") %>' OnCommand="btnTariffUpdate_Command" />
                        <asp:Button ID="btnTariffDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" CommandName="Delete" CommandArgument='<%# Eval("TM_UID") %>' OnClientClick="return confirm('Are you sure you want to delete this tariff?');" />
                           <asp:Button ID="btnHistory" runat="server" Text="History" CommandName="ShowHistory" CssClass="btn btn-sm btn-dark" CommandArgument='<%# Eval("TM_UID") %>' OnCommand="btnHistory_Command"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
