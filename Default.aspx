<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZAFMC._Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <div class="jumbotron" style="border: 1px solid maroon; height: 520px; width: 326px; margin-left: 303px;">
            <h4 class="text-danger"><strong>ZAFMC - Log In</strong></h4>
            <hr style="height: -60px" />
            <button class="btn btn-sm  btn-block text-capitalise" style="border: 2px solid #D44638" type="submit"><strong>Gmail</strong></button>
            <button class="btn btn-sm  btn-block text-capitalise " style="border: 2px solid #720e9e" type="submit"><strong>Yahoo !</strong></button>
            <hr style="height: -60px" />
            <%-- Passport ID --%>
            <asp:Label ID="PassportID" runat="server" Font-Size="Small" Text="ID Number / Passport" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-user"></i></span>
                <asp:TextBox ID="PassID" runat="server" placeholder="ID Number.." Height="30px" Width="158px" AutoCompleteType="Disabled" BorderStyle="Groove" MaxLength="11" ViewStateMode="Disabled" CssClass="form-control text-uppercase" ToolTip="ID Number / Passport !!" BorderColor="Maroon"></asp:TextBox>
            </div>
            <br />
            <%-- Password Field --%>
            <asp:Label ID="Pswd" runat="server" Font-Size="Small" Text="Password" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <br />
            <div class="input-group">
                <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-knight"></i></span>
                <asp:TextBox ID="Passwd" runat="server" type="password" placeholder="Password.." Height="30px" MaxLength="10" Width="158px" BorderStyle="Groove" ViewStateMode="Disabled" CssClass="form-control" ToolTip="Your Password!!" BorderColor="Maroon"></asp:TextBox>
            </div>
            <%-- Forgot Password --%>
            <%--<asp:Label ID="ForgotPasswd" runat="server" Style="margin-left: 105px" Font-Size="X-Small" Font-Italic="true" Font-Bold="true" CssClass="text-danger" Text=" <a href='RegisterUser.aspx' style= 'color:maroon' >(Forgot Password)</a>"></asp:Label>
            --%>
            <%--<asp:Label ID="Label2" runat="server" Style="margin-left: 105px" Font-Size="X-Small" Font-Italic="true" Font-Bold="true" CssClass="text-danger" Text=" <a href='RegisterUser.aspx' style= 'color:maroon' >(Forgot Password)</a>"></asp:Label>--%>
            <asp:HyperLink ID="ForgetPasswd" runat="server" CssClass="text-danger" Font-Size="X-Small" Font-Italic="true" Font-Bold="true" Style="margin-left: 105px" NavigateUrl="~/RegisterUser.aspx" Text="(Forgot Password)" Target="_blank"></asp:HyperLink>
            <br />
            <asp:Label ID="OTP" runat="server" Font-Bold="True" Font-Size="Small" Text="OTP" CssClass="text-danger"></asp:Label>
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="input-group">

                        <span class="input-group-addon" style="border-color: maroon; background-color: darkgrey;"><i class="glyphicon glyphicon-log-in"></i></span>
                        <asp:DropDownList ID="OTPList" runat="server" Font-Bold="True" AutoPostBack="true" OnSelectedIndexChanged="EmailedSMSed" Style="border: 0.5px solid maroon" Font-Size="Small" Height="30px" ToolTip="Receive through..." Width="85px" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="SMS" Value="SMS"></asp:ListItem>
                            <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="OTPNumber" runat="server" Style="border: 0.5px solid maroon" Font-Size="Small" placeholder="PIN.." Height="30px" MaxLength="6" ViewStateMode="Disabled" Width="68px" BorderStyle="Groove" ToolTip="Enter OTP!!" CssClass="form-control"></asp:TextBox>
                    </div>
                </ContentTemplate>
                <%-- <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="OTPList" EventName="SelectedIndexChanged" />
                </Triggers>--%>
            </asp:UpdatePanel>
            <hr />
            <%-- LogIn , Exit & NewUser Boxes --%>
            <asp:CheckBox ID="LogIn" runat="server" AutoPostBack="true" OnCheckedChanged="LogIn_CheckedChanged" CssClass="text-danger" Font-Size="Small" Text="LogIn  " ToolTip="Click to LogIn" Checked="False" />
            <asp:CheckBox ID="Exit" runat="server" AutoPostBack="true" OnCheckedChanged="Exit_CheckedChanged" CssClass="text-danger" Font-Size="Small" Text="Exit  " ToolTip="Close Application" Checked="False" />
            <asp:CheckBox ID="RegUser" runat="server" AutoPostBack="true" OnCheckedChanged="RegUser_CheckedChanged" CssClass="text-danger" Font-Size="Small" Text=" Register User" ToolTip="Create new user!!" Checked="False" />
            <hr />
            <asp:Label ID="CurDateTime" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="Small" OnLoad="CurDateTime_Load" Text="DateTime" CssClass="text-danger" ToolTip="Current LogIn Date &amp; Time!!"></asp:Label>
        </div>
        <%-- Dummy Labels to hold OTP Variable & Timer ~ NOT visible on GUI --%>
        <asp:Label ID="OTPToken" runat="server" Visible="false" Text="TokenOTP"></asp:Label>
        <asp:Label ID="TimerCount" runat="server" Visible="false" Text="CountTime"></asp:Label>
        <asp:Label ID="IdentityPassportDummy" runat="server" Visible="false" Text="IPD"></asp:Label>

    </div>

</asp:Content>
