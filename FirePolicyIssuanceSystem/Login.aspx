<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FirePolicyIssuanceSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    
</head>

<body style="background:linear-gradient(#210202, #5590e2, #808080">
   
    <div class="d-flex justify-content-between m-5 p-5">
      
        <div>
            <img src="/Images/Smile.jpg" alt="Image" class="img-fluid" />
        </div>
        <div class="container d-flex justify-content-center">
            
            <div >
                <h1 class="text-white mt-0 mb-5">The New India Assurance Company Ltd</h1>
               
                <div class="d-flex justify-content-center mt-5" style="margin-top:15%!important">
                <div class="card mt-5 " style="max-width:60%; width: 100%; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);">

                    <div class="card-body">
                        <h4 class="text-center mb-5 text-primary">Login Here</h4>

                        <form id="form1" runat="server">
                            <asp:Label ID="lblLogout" runat="server" ForeColor="Red" CssClass="d-block text-center mb-3"></asp:Label>

                            <div class="mb-3">
                                <label for="txtUserName" class="form-label">User Name</label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter User Name"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <label for="txtPassword" class="form-label">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                            </div>

                            <div class="text-center">
                                <asp:Label ID="lblInvalidUser" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                        </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
