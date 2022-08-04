<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZAFMC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <script type="text/javascript">
       function LogInZAFMC()
       {
            alert('Theo Mutepfe!!! .... Verify LogIn Details');
       }
       //Function For Tooltip LogIn Arrow
       $(function ()
       {
           $("#SUB").tooltip();
           
       });
   </script>
        <div>
        <div class="jumbotron" style="border: 1px solid maroon; height: 480px; width: 350px; margin-left: 303px;">
            <h4 class="text-danger"><strong>ZAFMC - Log In</strong></h4>
            <hr />
            <button class="btn btn-sm  btn-block text-capitalise"   style="border:2px solid #D44638  " type="submit"><strong>Gmail</strong></button>
            <button class="btn btn-sm  btn-block text-capitalise "  style="border:2px solid #720e9e"  type="submit"><strong>Yahoo !</strong></button>
            <hr />
            <asp:Label ID="PassportID" runat="server" Font-Size="Small" Text="ID Number / Passport" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <i class="input-group-addon glyphicon glyphicon-user" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon"></i>
                <asp:TextBox ID="PassID" runat="server" placeholder="ID Number.." Height="30px" Width="182px" BorderStyle="Groove" MaxLength="20" ViewStateMode="Disabled" CssClass="form-control text-uppercase" ToolTip="ID Number / Passport !!" BorderColor="Maroon"></asp:TextBox>
            </div>
            <asp:Label ID="Pswd" runat="server" Font-Size="Small" Text="Password" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <i class="input-group-addon glyphicon glyphicon-knight" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon"></i>
                <asp:TextBox ID="Passwd" runat="server" type="password" placeholder="Password.." Height="30px" MaxLength="20" Width="182px" BorderStyle="Groove" ViewStateMode="Disabled" CssClass="form-control" ToolTip="Your Password!!" BorderColor="Maroon"></asp:TextBox>
            </div>
            <asp:Label ID="OTP" runat="server" Font-Bold="True" Font-Size="Small" Text="OTP" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <i class="input-group-addon glyphicon glyphicon-log-in" style="border-color: maroon; background-color: darkgrey; top: 1px; left: 0px; height: 7px;"></i>
                <asp:DropDownList ID="OTPList" runat="server" Font-Bold="True" style="border:0.5px solid maroon"  Font-Size="Small" Height="30px" ToolTip="Receive through..." Width="85px" ViewStateMode="Disabled" CssClass="form-control">
                    <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="SMS" Value="SMS"></asp:ListItem>
                    <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="OTPNumber" runat="server" type="password" style="border:0.5px solid maroon" placeholder="PIN.." Height="30px" MaxLength="4" ViewStateMode="Disabled" Width="59px" BorderStyle="Groove" ToolTip="Enter OTP!!" CssClass="form-control"></asp:TextBox>
           <%-- LogIn Arrow --%>
                <div class="input-group-append">
		             <i class="btn btn-success glyphicon glyphicon-arrow-right " id="SUB" title="Click, to LogIn..." style="background-color: maroon; top: 1px; left: -2px; height: 30px; width:39px;"; onclick="LogInZAFMC()"></i>
	            </div>
            </div>
            <hr />
            <asp:CheckBox ID="RegUser" runat="server"  CssClass="text-danger"  Font-Size="Small"  Text="<a  href='RegisterUser.aspx' visible='true' style='color:maroon'> Create a New User...</a>" ToolTip="Register LogIn Details!!" Checked="True" />
            <hr />
            <asp:Label ID="CurDateTime" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="Small" OnLoad="CurDateTime_Load" Text="DateTime" CssClass="text-danger" ToolTip="Current LogIn Date &amp; Time!!"></asp:Label>
        </div>
      
    </div>

</asp:Content>
