<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="TariffMasterHistory.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.TariffMasterHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <h3 class="text-center  text-danger-emphasis">Tariff Master History</h3>
        <div class="d-flex justify-content-end mt-3">
            <asp:Button ID="btnTmhBack" runat="server" Text="Back" CssClass="btn btn-secondary mb-3" OnClick="btnTmhBack_Click" CausesValidation="false" />
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="TM_UID,TM_SRL" EmptyDataText="No Records Found"
            CssClass="table table-striped table-bordered table-sm table-responsive table-hover"  AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging">


            <Columns>
                <asp:BoundField DataField="TM_UID" HeaderText="UId" ReadOnly="True" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" />
                <asp:BoundField DataField="TM_SRL" HeaderText="Srl" ReadOnly="True" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" />
                <asp:BoundField DataField="TM_ACTION_TYPE" HeaderText="Action" />
                <asp:BoundField DataField="RISK_CLASS_FM" HeaderText="Class From" />
                <asp:BoundField DataField="RISK_CLASS_TO" HeaderText="Class To" />
                <asp:BoundField DataField="RISK_OCCP_FM" HeaderText="Occupancy From" />
                <asp:BoundField DataField="RISK_OCCP_TO" HeaderText="Occupancy To" />
                <asp:BoundField DataField="TM_RISK_SI_FM" HeaderText="SI From" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="TM_RISK_SI_TO" HeaderText="SI To" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="TM_RISK_RATE" HeaderText="Risk Rate" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
