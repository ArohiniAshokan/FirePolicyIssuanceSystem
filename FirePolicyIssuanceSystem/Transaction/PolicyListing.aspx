<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="PolicyListing.aspx.cs" Inherits="FirePolicyIssuanceSystem.Transaction.PolicyListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .small-card {
            padding: 1rem;
            max-width: 50%;
            margin: auto;
            margin-bottom: 25px;
        }

        .left-align {
            text-align: left;
        }

        .right-align {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }

        th a {
            text-decoration: none !important; 
            color: white !important; 
        }

    </style>
    <div class="p-5">
        <h3 class="text-center text-danger-emphasis">Fire Policy Listing</h3>
        <div class="d-flex justify-content-between mt-3">
            <asp:Button ID="btnAddNew" runat="server" Text="Add New Policy" CssClass="btn btn-success" OnClick="btnAddNew_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="false" />
        </div>
        <%--  --%>
        <div class="card small-card bg-primary-subtle">
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="txtFilterPolNo" class="form-label">Policy No</label>
                    <asp:TextBox ID="txtFilterPolNo" runat="server" placeholder="Enter Policy No" CssClass="form-control form-control-sm"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="ddlFilterProduct" class="form-label">Product</label>
                    <asp:DropDownList ID="ddlFilterProduct" runat="server" CssClass="form-select form-select-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select Product" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label for="ddlFilterStatus" class="form-label">Status</label>
                    <asp:DropDownList ID="ddlFilterStatus" runat="server" CssClass="form-select form-select-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select Status" Value="" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-2">
                <div class="col-md-4">
                    <label for="txtFilterIssueDate" class="form-label">Issue Date</label>
                    <asp:TextBox ID="txtFilterIssueDate" runat="server" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="txtFilterFromDate" class="form-label">From Date</label>
                    <asp:TextBox ID="txtFilterFromDate" runat="server" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label for="txtFilterToDate" class="form-label">To Date</label>
                    <asp:TextBox ID="txtFilterToDate" runat="server" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                </div>

            </div>
            <div class="text-center">
                <asp:Button ID="btnApplyFilters" runat="server" Text="Apply Filters" CssClass="btn btn-primary btn-sm me-2" OnClick="btnApplyFilters_Click" />
                <asp:Button ID="btnClearFilter" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm" OnClick="btnClearFilter_Click" />
            </div>
        </div>
        <%--  --%>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="POL_UID"
            CssClass="table table-striped table-bordered table-sm table-responsive table-hover" EmptyDataText="No Records Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" AllowSorting="true" OnSorting="GridView1_Sorting">

            <Columns>
                <%-- <asp:BoundField DataField="POL_UID" HeaderText="UID" ReadOnly="True" />--%>
                <asp:BoundField DataField="POL_NO" HeaderText="Policy No" ReadOnly="True" SortExpression="POL_NO" />
                <asp:BoundField DataField="PROD_CODE" HeaderText="Product" SortExpression="PROD_CODE" />
                <asp:BoundField DataField="POL_ISS_DT" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="POL_ISS_DT" />
                <asp:BoundField DataField="POL_FM_DT" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="POL_FM_DT" />
                <asp:BoundField DataField="POL_TO_DT" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="POL_TO_DT" />
                <asp:BoundField DataField="POL_ASSR_NAME" HeaderText="Assured Name" SortExpression="POL_ASSR_NAME" />
                <asp:BoundField DataField="POL_LC_SI" HeaderText="Sum Insured" HeaderStyle-CssClass="center-align" SortExpression="POL_LC_SI" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="POL_NET_LC_PREM" HeaderText="Premium" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" SortExpression="POL_NET_LC_PREM" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span>
                            <i class="fa fa-check-circle" style="color: green; display: none;" runat="server" id="checkIcon" title="Approved"></i>
                            <i class="fa fa-exclamation-circle" style="color: orange; display: none;" runat="server" id="exclamationIcon" title="Pending"></i>
                            <%--  <%# Eval("POL_APPR_STATUS") %>--%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnPolicyUpdate" runat="server" CssClass="btn btn-sm btn-danger" Text="View" CommandName="Update" CommandArgument='<%# Eval("POL_UID") %>' OnCommand="btnUpdate_Command"/>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
