<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserTemplate.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="FirePolicyIssuanceSystem.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
    <style>
        .card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

            .card:hover {
                transform: scale(1.05);
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
            }

        .hover-image {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

            .hover-image:hover {
                transform: scale(1.05);
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
            }

        h1 {
            transition: color 0.3s ease, transform 0.3s ease;
            font-family:Arial;
            font-weight:bolder;
        }

            h1:hover {
                color: #007bff;
                transform: scale(1.05);
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row justify-content-center">
            <div class="col-12 col-md-6 mb-5 mt-3">
                <h1 class="text-bg-light text-center text-primary p-2">FIRE POLICY ISSUANCE</h1>
                <img src="/Images/LoginPage.jpg" class="img-fluid rounded hover-image" alt="Dashboard Image">
            </div>
        </div>
        <div class="row justify-content-center">

            <div class="col-12 col-sm-6 col-md-3 mb-4">
                <div class="card shadow-sm">
                    <div class="card-header text-white bg-info d-flex align-items-center">
                        <i class="bi bi-file-earmark-check me-2" style="font-size: 1.5rem;"></i>
                        <h5 class="card-title mb-0">Total Policies</h5>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-center">
                            <span id="totalPoliciesCard" class="fw-bold fs-2">
                                <asp:Label ID="lblTotalPolicies" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 col-sm-6 col-md-3 mb-4">
                <div class="card shadow-sm ">
                    <div class="card-header text-white bg-primary d-flex align-items-center">
                        <i class="bi bi-check-circle me-2" style="font-size: 1.5rem;"></i>
                        <h5 class="card-title mb-0">Total Policies Approved</h5>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-center">

                            <span id="totalPolicies" class="fw-bold fs-2">
                                <asp:Label ID="lblTotalPoliciesApproved" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-12 col-sm-6 col-md-3 mb-4">
                <div class="card shadow-sm ">
                    <div class="card-header text-white bg-success d-flex align-items-center">
                        <i class="bi bi-shield-check me-2" style="font-size: 1.5rem;"></i>
                        <h6 class="card-title mb-0">Total Approved Sum Insured</h6>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-end">

                            <span id="totalSumInsured" class="fw-bold fs-2">AED
                                <asp:Label ID="lblTotalSumInsured" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-12 col-sm-6 col-md-3 mb-4">
                <div class="card shadow-sm">
                    <div class="card-header text-white bg-warning d-flex align-items-center">
                        <i class="bi bi-currency-dollar me-2" style="font-size: 1.5rem;"></i>
                        <h5 class="card-title mb-0">Total Approved Premium</h5>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-end">

                            <span id="totalPremium" class="fw-bold fs-2">AED
                                <asp:Label ID="lblTotalPremium" runat="server" Text="0"></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
