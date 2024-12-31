<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="PolicyEntry.aspx.cs" Inherits="FirePolicyIssuanceSystem.Transaction.PolicyEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.14.0/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>


    <script>
        //function validateBothGroups() {
        //    var isValidGroup1 = Page_ClientValidate('vg1');
        //    var isValidGroup2 = Page_ClientValidate('vg2');

        //    return isValidGroup1 && isValidGroup2;
        //}
        $(document).on('keydown', '.numonly', function (e) {
            var charCode = (e.which) ? e.which : event.keyCode;
            if (charCode !== 8 && charCode !== 0 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        });

        function openRiskModal() {
            var myModal = new bootstrap.Modal(document.getElementById('addRiskModal'));
            myModal.show();
        }

        function showError(message) {
            Swal.fire({
                icon: 'error',
                position: 'center',
                text: message,
                confirmButtonText: 'OK'
            });
        }

        function showSuccess(message) {
            Swal.fire({
                icon: 'success',
                position: 'center',
                text: message,
                confirmButtonText: 'OK'
            });
        }

        function successAlert(message, PolUid) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: true
            })

                .then((result) => {
                    if (result.isConfirmed) {
                        window.location = "PolicyEntry.aspx?PolUid=" + PolUid;
                    }
                });
        }

        function clearModalFields() {
            document.getElementById('<%= ddlRiskClass.ClientID %>').value = '';
            document.getElementById('<%= ddlOccType.ClientID %>').value = '';
            document.getElementById('<%= ddlConstrType.ClientID %>').value = '';
            document.getElementById('<%= txtRiskLocation.ClientID %>').value = '';
            document.getElementById('<%= txtRiskDescription.ClientID %>').value = '';
            document.getElementById('<%= txtRiskFcSi.ClientID %>').value = '';
            document.getElementById('<%= txtRiskLcSi.ClientID %>').value = '';
            document.getElementById('<%= txtRiskPremRate.ClientID %>').value = '';
            document.getElementById('<%= txtRiskFcPrem.ClientID %>').value = '';
            document.getElementById('<%= txtRiskLcPrem.ClientID %>').value = '';

            $('#<%= ddlRiskClass.ClientID %>').focus();
        }

        $(document).ready(function () {
            $('#<%= txtFromDate.ClientID %>').on('blur', function () {
                var dateInput = $(this).val();

                var selectedDate = new Date(dateInput);
                var today = new Date();
                today.setHours(0, 0, 0, 0);

                if (selectedDate < today) {

                    $.ajax({
                        type: 'POST',
                        url: 'PolicyEntry.aspx/GetErrorMessage',
                        data: '{}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            var message = response.d;
                            Swal.fire({
                                icon: 'error',
                                title: 'Invalid Date',
                                text: message
                            });
                            $('#<%= txtFromDate.ClientID %>').val("");
                            $('#<%= txtToDate.ClientID %>').val("");
                        }
                    });
                    return;
                }

                var oneYearLater = new Date(selectedDate);
                oneYearLater.setFullYear(oneYearLater.getFullYear() + 1);
                oneYearLater.setDate(oneYearLater.getDate() - 1);
                $('#<%= txtToDate.ClientID %>').val(oneYearLater.toISOString().split('T')[0]);
            });



            $('#<%= txtAssrDob.ClientID %>').on('blur', function () {
                var dobInput = $(this).val();

                if (!dobInput) return;

                var dob = new Date(dobInput);
                var today = new Date();
                today.setHours(0, 0, 0, 0);


                var age = today.getFullYear() - dob.getFullYear();
                if (dob > today.setFullYear(today.getFullYear() - age)) age--;


                if (age < 18 || age > 65) {

                    $.ajax({
                        type: 'POST',
                        url: 'PolicyEntry.aspx/GetErrorMessage2',
                        data: '{}',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            var message = response.d;
                            Swal.fire({
                                icon: 'error',
                                title: 'Invalid Age',
                                text: message
                            });
                            $('#<%= txtAssrDob.ClientID %>').val("");
                        }
                    });
                    return;
                }


            });
        });

    </script>

    <script type="text/javascript">
        function formatInput(element) {
            let value = parseFloat(element.value);
            if (!isNaN(value)) {
                element.value = value.toFixed(2);
            }

            var textBox = document.getElementById('<%= txtRiskFcSi.ClientID %>');
            if (textBox) {
                __doPostBack(textBox.name, '');  
            }
        }

        function openReport() {
            var polUid = '<%= Request.QueryString["PolUid"] %>';
            window.open('/Report/PrintView.aspx?PolUid=' + polUid, '_blank', 'width=800,height=600');
            return false;
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

        .right-align {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <br />
        <h3 class="text-center text-danger-emphasis">Fire Policy Entry Form</h3>

        <div class="d-flex justify-content-end me-5">
            <asp:Button ID="btnPeBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnPeBack_Click" CausesValidation="false" />
        </div>

        <div class="d-flex justify-content-center m-2">
            <asp:Button ID="btnPrint" runat="server" Text="Generate Report" CssClass="btn btn-warning me-3" Visible="false" CausesValidation="false" OnClientClick="if(confirm('Do you want to print the report?')) { openReport(); } return false;" />
            <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="btn btn-warning me-3" Visible="false" OnClick="btnCopy_Click" CausesValidation="false" OnClientClick="return confirm('Do you want to copy the policy?');" />
        </div>

        <%-- <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>
        <div class="card container mb-2 bg-primary-subtle">
            <!-- Personal Information Card -->
            <%--  <div class="card-header"></div>--%>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtPolicyNo">Policy No</label>

                            <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddlProduct">Product Code</label>
                            <span style="color: red;">*</span>
                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Value="">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" ControlToValidate="ddlProduct"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtPolicyIssueDate">Policy Issue Date</label>
                            <asp:TextBox ID="txtPolicyIssueDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtFromDate">From Date</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtToDate">To Date</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtAssrName">Assured Name</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtAssrName" runat="server" MaxLength="240" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAssrName" runat="server" ControlToValidate="txtAssrName"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtAssrAddress">Assured Address</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtAssrAddress" runat="server" MaxLength="240" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAssrAddress" runat="server" ControlToValidate="txtAssrAddress"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtAssrEmail">Assured Email</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtAssrEmail" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revAssrEmail" runat="server" ControlToValidate="txtAssrEmail"
                                ErrorMessage="Invalid email format" CssClass="text-danger"
                                ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvAssrEmail" runat="server" ControlToValidate="txtAssrEmail"
                                ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>

                <%--     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>--%>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <label for="ddlAssrType">Assured Type </label>
                                    <span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlAssrType" runat="server" CssClass="form-control dropdown" AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="ddlAssrType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAssrType" runat="server" ControlToValidate="ddlAssrType"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>



                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <label for="ddlAssrOccupation">Assured Occupation</label>
                                    <span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlAssrOccupation" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="">-- Select --</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAssrOccupation" runat="server" ControlToValidate="ddlAssrOccupation"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <label for="txtAssrMobile">Assured Mobile</label>
                                    <asp:TextBox ID="txtAssrMobile" runat="server" MaxLength="10" TextMode="SingleLine" CssClass="form-control numonly"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revassrmobile" runat="server" ControlToValidate="txtassrmobile"
                                        ErrorMessage="invalid mobile number format" CssClass="text-danger" ValidationExpression="^\d{10}$"
                                        Display="dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvAssrMobile" runat="server" ControlToValidate="txtAssrMobile"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <label for="txtNationalID">Assured Civil ID</label>
                                    <asp:TextBox ID="txtNationalID" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNationalId" runat="server" ControlToValidate="txtNationalID"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">

                            <label for="txtAssrDob">Assured Date of Birth</label>
                            <asp:TextBox ID="txtAssrDob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:RequiredFieldValidator ID="rfvAssrDob" runat="server" ControlToValidate="txtAssrDob"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <label for="chkMultiRisk">Multi-Risk</label><br />
                                    <asp:CheckBox ID="chkMultiRisk" runat="server" OnCheckedChanged="chkMultiRisk_CheckedChanged" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <!-- Currency and Premium Information Card -->
        <div class="card container mb-2 bg-primary-subtle">
            <%--   <div class="card-header"></div>--%>
            <div class="card-body">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="ddlSiCurrency">SI Currency</label>
                                    <span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlSiCurrency" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnTextChanged="ddlSiCurrency_TextChanged" AutoPostBack="true">
                                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSiCurrency" runat="server" ControlToValidate="ddlSiCurrency"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtSiCurrRate">SI Currency Rate</label>
                                    <asp:TextBox ID="txtSiCurrRate" runat="server" ReadOnly="true" AutoPostBack="true" CssClass="form-control right-align"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="ddlPremCurrency">Premium Currency</label>
                                    <span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlPremCurrency" runat="server" CssClass="form-control" OnTextChanged="ddlPremCurrency_TextChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPremCurrency" runat="server" ControlToValidate="ddlPremCurrency"
                                        ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtPremCurrRate">Premium Currency Rate</label>
                                    <asp:TextBox ID="txtPremCurrRate" runat="server" ReadOnly="true" AutoPostBack="true" CssClass="form-control right-align"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtFCSI">FC SI</label>
                            <asp:TextBox ID="txtFCSI" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtLCSI">LC SI</label>
                            <asp:TextBox ID="txtLCSI" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtGrossFCPremium">Gross FC Premium</label>
                            <asp:TextBox ID="txtGrossFCPremium" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row">

                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtGrossLCPremium">Gross LC Premium</label>
                            <asp:TextBox ID="txtGrossLCPremium" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtVATFC">VAT FC Amount</label>
                            <asp:TextBox ID="txtVATFC" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtVATLC">VAT LC Amount</label>
                            <asp:TextBox ID="txtVATLC" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtNetFC">Net FC Premium</label>
                            <asp:TextBox ID="txtNetFC" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtNetLC">Net LC Premium</label>
                            <asp:TextBox ID="txtNetLC" runat="server" ReadOnly="true" CssClass="form-control right-align"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-center mt-4">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary me-3" OnClick="btnSave_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Clear" CausesValidation="false" CssClass="btn btn-secondary me-3" OnClick="btnCancel_Click" Visible="false" />
            <asp:Button ID="btnUpdatePolicy" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnUpdatePolicy_Click" Visible="false" />
            <asp:Button ID="btnAddRisk" runat="server" Text="Add Risk" CssClass="btn btn-primary" Visible="false" OnClientClick="clearModalFields(); $('#addRiskModal').modal('show'); return false;" OnClick="btnAddRisk_Click" />
        </div>
    </div>
    <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
    <!-- Add Risk Modal -->
    <div class="modal fade" id="addRiskModal" tabindex="-1" role="dialog" aria-labelledby="addRiskModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header d-flex justify-content-between">
                    <h5 class="modal-title" id="addRiskModalLabel">Risk Details</h5>

                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <%-- <asp:UpdatePanel ID="up2" runat="server">
                    <ContentTemplate>--%>
                <div class="modal-body">
                    <asp:UpdatePanel ID="up2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskClass">Risk Class</label>
                                         <span style="color:red;">*</span>
                                        <asp:DropDownList ID="ddlRiskClass" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged ="ddlRiskClass_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRiskClass" runat="server" ControlToValidate="ddlRiskClass"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskOccType">Occupation Type</label>
                                         <span style="color:red;">*</span>
                                        <asp:DropDownList ID="ddlOccType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOccType_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvOccType" runat="server" ControlToValidate="ddlOccType"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label for="riskConstrType">Construction Type</label>
                                         <span style="color:red;">*</span>
                                        <asp:DropDownList ID="ddlConstrType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvConstrType" runat="server" ControlToValidate="ddlConstrType"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskLocation">Location</label>
                                         <span style="color:red;">*</span>
                                        <asp:TextBox ID="txtRiskLocation" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtRiskLocation"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label for="riskDesc">Description</label>
                                         <span style="color:red;">*</span>
                                        <asp:TextBox ID="txtRiskDescription" runat="server" CssClass="form-control" MaxLength="240"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtRiskDescription"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskFcSi">FC SI</label>
                                         <span style="color:red;">*</span>
                                        <asp:TextBox ID="txtRiskFcSi" runat="server" TextMode="Number" onblur="formatInput(this)" CssClass="form-control right-align" OnTextChanged="txtRiskFcSi_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRiskFcSi" runat="server" ControlToValidate="txtRiskFcSi"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label for="riskLcSi">LC SI</label>
                                        <asp:TextBox ID="txtRiskLcSi" runat="server" TextMode="Number" Enabled="false" CssClass="form-control right-align"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRiskLcSi" runat="server" ControlToValidate="txtRiskLcSi"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label for="riskPremRate">Premium Rate</label>
                                        <asp:TextBox ID="txtRiskPremRate" runat="server" Enabled="false" CssClass="form-control right-align"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPremRate" runat="server" ControlToValidate="txtRiskPremRate"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskFcPrem">FC Premium</label>
                                        <asp:TextBox ID="txtRiskFcPrem" runat="server" Enabled="false" CssClass="form-control right-align"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRiskFcPrem" runat="server" ControlToValidate="txtRiskFcPrem"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="riskLcPrem">LC Premium</label>
                                        <asp:TextBox ID="txtRiskLcPrem" runat="server" Enabled="false" CssClass="form-control right-align"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRiskLcPrem" runat="server" ControlToValidate="txtRiskLcPrem"
                                            ErrorMessage="Required field" CssClass="text-danger" Display="Dynamic" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%-- <div class="col-md-4">
                                    <br />
                                    <asp:Button ID="btnCalculatePR" runat="server" Text="Calculate" ValidationGroup="vg1" CssClass="btn btn-primary" OnClick="btnCalculatePR_Click" />
                                </div>--%>
                            </div>
                            <%-- <div class="text-center">
                                <asp:Label ID="lblCalcMessage" runat="server" CssClass="text-danger" />
                            </div>--%>
                            <%--<div class="text-center">
                                <br />
                                <asp:Button ID="btnCancelRisk" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelRisk_Click"  OnClientClick="clearModalFields(); $('#addRiskModal').modal('show'); return false;"  CausesValidation="false" />
                            </div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer d-flex justify-content-center">
                    <asp:Button ID="btnCancelRisk" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClientClick="clearModalFields(); $('#addRiskModal').modal('show'); return false;" CausesValidation="false" />
                    <asp:Button ID="btnSaveRisk" runat="server" Text="Save" CssClass="btn btn-primary me-3" OnClick="btnSaveRisk_Click" ValidationGroup="vg1" />
                    <asp:Button ID="btnUpdateRisk" runat="server" Text="Update" CssClass="btn btn-primary me-3" OnClick="btnUpdateRisk_Click" CommandArgument="0" ValidationGroup="vg1" Visible="false" />
                </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>

    <div class="p-5">
        <br />
        <asp:GridView ID="GridViewRisk" runat="server" AutoGenerateColumns="false" DataKeyNames="RISK_UID" OnRowDeleting="GridView1_RowDeleting"
            CssClass="table table-striped table-bordered table-sm table-responsive table-hover">

            <Columns>
                <asp:BoundField DataField="RISK_ID" HeaderText="RISK ID" ReadOnly="True" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" />
                <asp:BoundField DataField="R_CLASS" HeaderText="Risk Class" />
                <asp:BoundField DataField="R_OCCUP" HeaderText="Occupancy Type" />
                <asp:BoundField DataField="R_CONSTR" HeaderText="Construction Type" />
                <asp:BoundField DataField="RISK_FC_SI" HeaderText="FC SI" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="RISK_LC_SI" HeaderText="LC SI" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="RISK_PREM_RATE" HeaderText="Premium Rate" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="RISK_FC_PREM" HeaderText="FC Premium" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="RISK_LC_PREM" HeaderText="LC Premium" HeaderStyle-CssClass="center-align" ItemStyle-CssClass="right-align" DataFormatString="{0:F2}" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnRiskUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-danger" CommandArgument='<%# Eval("RISK_UID") %>' OnClick="btnRiskUpdate_Click" />
                        <asp:Button ID="btnRiskDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-dark" CommandName="Delete" CommandArgument='<%# Eval("RISK_UID") %>' OnClientClick="return confirm('Are you sure you want to delete this risk?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="text-center">
            <br />
            <asp:Button ID="btnApproval" runat="server" CssClass="btn btn-success" Text="Approve" OnClientClick="return confirm('Do you want to approve the policy?');" OnClick="btnApproval_Click" Visible="false" />
        </div>
    </div>
</asp:Content>
