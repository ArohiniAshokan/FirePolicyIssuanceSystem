<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="TariffMasterEntry.aspx.cs" Inherits="FirePolicyIssuanceSystem.Master.TariffMasterEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function successAlert(message, tmUid) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            })

                .then((result) => {
                    if (result.isConfirmed) {
                        window.location = "TariffMasterEntry.aspx?tmUid=" + tmUid + "&mode=U";
                    }
                });
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // Allow digits (0-9) and the decimal point
            if ((charCode >= 48 && charCode <= 57) || charCode === 46 || charCode === 8) {
                return true; // Allow
            }
            return false; // Block
        }

        function formatNumber(input) {
            let value = parseFloat(input.value);
            if (!isNaN(value)) {
                input.value = value.toFixed(2); // Format to two decimal places
            } else {
                input.value = ""; // Clear if not a valid number
            }
        }

    </script>

    <style>
        /* Hide the number input spinners in all browsers */
        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        input[type=number] {
            -moz-appearance: textfield; /* For Firefox */
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
    </style>
    <div class="m-5">
        <div class="d-flex justify-content-end mt-3">
            <asp:Button ID="btnTmeBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnTmeBack_Click" CausesValidation="false" />
        </div>
    <div class="form-container bg-primary-subtle">
        <h3 class="text-center  text-danger-emphasis">Tariff Master Entry Form</h3>


        <div class="container mt-4">

            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="txtTmUid">UID</label>
                        <asp:TextBox ID="txtTmUid" runat="server" CssClass="form-control right-align" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtTmUid_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblValidationMessage1" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvTmUid" runat="server" ControlToValidate="txtTmUid"
                            ErrorMessage="TM Uid Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="ddlRiskClassFm">Risk Class From</label>
                         <span style="color:red;">*</span>
                        <asp:DropDownList ID="ddlRiskClassFm" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRiskClassFm" runat="server" ControlToValidate="ddlRiskClassFm"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="ddlRiskClassTo">Risk Class To</label>
                        <span style="color:red;">*</span>
                        <asp:DropDownList ID="ddlRiskClassTo" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRiskClassTo" runat="server" ControlToValidate="ddlRiskClassTo"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="ddlRiskOccFm">Risk Occupancy From</label>
                        <span style="color:red;">*</span>
                        <asp:DropDownList ID="ddlRiskOccFm" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRiskOccFm" runat="server" ControlToValidate="ddlRiskOccFm"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="ddlRiskOccTo">Risk Occupancy To</label>
                        <span style="color:red;">*</span>
                        <asp:DropDownList ID="ddlRiskOccTo" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRiskOccTo" runat="server" ControlToValidate="ddlRiskOccTo"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="txtRiskSiFrom">Risk SI From</label>
                        <span style="color:red;">*</span>
                        <asp:TextBox ID="txtRiskSiFrom" runat="server" CssClass="form-control right-align" onkeypress="return isNumberKey(event)"
                            onblur="formatNumber(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRiskSiFm" runat="server" ControlToValidate="txtRiskSiFrom"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="txtRiskSiTo">Risk SI To</label>
                        <span style="color:red;">*</span>
                        <asp:TextBox ID="txtRiskSiTo" runat="server" CssClass="form-control right-align" onkeypress="return isNumberKey(event)"
                            onblur="formatNumber(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRiskSiTo" runat="server" ControlToValidate="txtRiskSiTo"
                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label for="txtRiskRate">Risk Rate</label>
                        <span style="color:red;">*</span>
                        <asp:TextBox ID="txtRiskRate" runat="server" CssClass="form-control right-align" onkeypress="return isNumberKey(event)"
                            onblur="formatNumber(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRiskRate" runat="server" ControlToValidate="txtRiskRate"
                            ErrorMessage="RiskRate Required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-center mt-4">
            <asp:Button ID="btnTmSave" runat="server" Text="Save" CssClass="btn btn-success me-3" OnClick="btnTmSave_Click" Visible="false" />
            <asp:Button ID="btnTmUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnTmUpdate_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Reset" CausesValidation="false" CssClass="btn btn-secondary me-3" OnClick="btnCancel_Click" Visible="false" />
            <asp:Button ID="btnTariffHistory" runat="server" Text="History" CommandName="ShowHistory" CssClass="btn btn-dark" CommandArgument='<%# Eval("TM_UID") %>' OnCommand="btnTariffHistory_Command" CausesValidation="false" />
        </div>
    </div>
        </div>
    <%-- <div class="text-center">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" DataKeyNames="TM_UID,TM_SRL" CssClass="table table-light table-bordered table-sm table-responsive table-hover">

            <Columns>
                <asp:BoundField DataField="TM_UID" HeaderText="TM Id" ReadOnly="True" />
                <asp:BoundField DataField="TM_SRL" HeaderText="TM Srl" ReadOnly="True" />
                <asp:BoundField DataField="TM_ACTION_TYPE" HeaderText="Action" />
                <asp:BoundField DataField="RISK_CLASS_FM" HeaderText="Class From" />
                <asp:BoundField DataField="RISK_CLASS_TO" HeaderText="Class To" />
                <asp:BoundField DataField="RISK_OCCP_FM" HeaderText="Occupancy From" />
                <asp:BoundField DataField="RISK_OCCP_TO" HeaderText="Occupancy To" />
                <asp:BoundField DataField="TM_RISK_SI_FM" HeaderText="SI From" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="TM_RISK_SI_TO" HeaderText="SI To" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="TM_RISK_RATE" HeaderText="Risk Rate" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
            </Columns>
        </asp:GridView>
    </div>--%>
</asp:Content>
