
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterUser.aspx.cs" Inherits="ZAFMC.RegisterUser" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
    
    <div class="jumbotron" style="border: 1px solid maroon; height: 530px; width: 405px; margin-left: 303px;">
        <h5 class="text-danger"><strong>ZAFMC - Create New User</strong></h5>
        <hr />
        <asp:Label ID="FName" runat="server" Font-Size="Small" Text="Firstname" Font-Bold="True" CssClass="text-danger"></asp:Label>
        <div class="input-group">
            <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-user"></i></span>
            <asp:TextBox ID="LogInFirstname" runat="server" placeholder="Firstname.." Height="30px" Width="144px" BorderStyle="Groove" MaxLength="20" ViewStateMode="Disabled" CssClass="form-control text-capitalize" ToolTip="Firstname !!" BorderColor="Maroon"></asp:TextBox>
        </div>
        <asp:Label ID="SName" runat="server" Font-Size="Small" Text="Surname" Font-Bold="True" CssClass="text-danger"></asp:Label>
        <div class="input-group">
            <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-signal"></i></span>
            <asp:TextBox ID="LogInSurname" runat="server" placeholder="Surname.." Height="30px" Width="144px" BorderStyle="Groove" MaxLength="20" ViewStateMode="Disabled" CssClass="form-control text-capitalize" ToolTip="Surname !!" BorderColor="Maroon"></asp:TextBox>
        </div>
        <asp:Label ID="PassID" runat="server" Font-Size="Small" Text="ID Number / Passport" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-road"></i></span>
                <asp:TextBox ID="PassportID" runat="server"  placeholder="ID Number.." Height="30px" Width="144px" BorderStyle="Groove" MaxLength="20" ViewStateMode="Disabled" CssClass="form-control text-uppercase" ToolTip="ID Number / Passport !!" BorderColor="Maroon"></asp:TextBox>
           </div>
         <asp:Label ID="Paswd" runat="server" Font-Size="Small" Text="Password" Font-Bold="True" CssClass="text-danger"></asp:Label>
            <div class="input-group">
                <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon; padding-top: 5px; padding-bottom: 5px;"><i class="glyphicon glyphicon-knight"></i></span>
                <asp:TextBox ID="PassWD" runat="server" type="password"  placeholder="Password..." Height="28px" MaxLength="20" Width="144px" BorderStyle="Groove" ViewStateMode="Disabled" CssClass="form-control" ToolTip="Your Password!!" BorderColor="Maroon"></asp:TextBox>
           </div>
        <asp:Label ID="MobNum" runat="server" Font-Size="Small" Text="Mobile" Font-Bold="True" CssClass="text-danger"></asp:Label>
        <div class="input-group">
            <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-phone"></i></span>
            <asp:TextBox ID="LogInMobile" runat="server" placeholder="Mobile.." Height="30px" Width="144px" BorderStyle="Groove" MaxLength="15" ViewStateMode="Disabled" CssClass="form-control text-capitalize" ToolTip="Mobile !!" BorderColor="Maroon"></asp:TextBox>
        </div>
        <asp:Label ID="LoMail" runat="server" Font-Size="Small" Text="Email" Font-Bold="True" CssClass="text-danger"></asp:Label>
        <div class="input-group">
            <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon; border-bottom-color: maroon;"><i class="glyphicon glyphicon-envelope"></i></span>
            <asp:TextBox ID="LogInEmail" runat="server" placeholder="Email.." Height="30px" Width="144px" BorderStyle="Groove" MaxLength="50" ViewStateMode="Disabled" CssClass="form-control text-capitalize" ToolTip="Email !!" BorderColor="Maroon"></asp:TextBox>
        </div>
        <asp:Label ID="RankPost" runat="server" Font-Bold="True" Font-Size="Small" Text="Church Position" CssClass="text-danger"></asp:Label>
        <div class="input-group">
                <span class="input-group-addon" style="background-color: darkgrey; border-left-color: maroon; border-top-color: maroon;  border-bottom-color: maroon; border-right-color: maroon"><i class="glyphicon glyphicon-education"></i></span>
                <asp:DropDownList ID="RankPosition" runat="server" Font-Bold="True" style="border:0.5px solid maroon"  Font-Size="Small" Height="30px" ToolTip="Select Position!!" Width="144px" ViewStateMode="Disabled" CssClass="form-control">
                    <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Bishop" Value="Bishop"></asp:ListItem>
                    <asp:ListItem Text="Vice-Bishop" Value="Vice-Bishop"></asp:ListItem>
                    <asp:ListItem Text="Snr. High Priest" Value="Snr. High Priest"></asp:ListItem>
                    <asp:ListItem Text="High Priest (1)" Value="High Priest (1)"></asp:ListItem>
                    <asp:ListItem Text="High Priest (2)" Value="High Priest (2)"> </asp:ListItem>
                    <asp:ListItem Text="High Priest (3)" Value="High Priest (3)"></asp:ListItem>
                    <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                    <asp:ListItem Text="Vice-Chairman" Value="Vice-Chairman"></asp:ListItem>
                    <asp:ListItem Text="Treasurer" Value="Treasurer"></asp:ListItem>
                    <asp:ListItem Text="Secretary" Value="Secretary"></asp:ListItem>
                    <asp:ListItem Text="Vice-Secretary" Value="Vice-Secretary"></asp:ListItem>
                    <asp:ListItem Text="Secretary General" Value="Secretary General"></asp:ListItem>
                 
                </asp:DropDownList>
                
            </div>
        <hr />
         <asp:LinkButton ID="UserSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveRegisterUser_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="UserView" CssClass="text-danger" ToolTip="View Data.." data-toggle="modal" data-target="#RegUSer" runat="server"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="UserEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="DeleteRegisterUser_Click" ><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="UserRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshRegisterUser_Click" ><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="UserDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteRegisterUser_Click" ><strong>Delete</strong></asp:LinkButton>
         |
         <asp:LinkButton ID="UserReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click" ><strong>Reset</strong></asp:LinkButton>

    </div>
 
    <%-- SQLDatasource to Bind data to Modal Form Grid View --%>
    <asp:SqlDataSource ID="ZAFMCRegisterUser" runat="server" ConnectionString="<%$ ConnectionStrings:ZionRegisterUser %>" SelectCommand="SELECT [LogInFirstname] as [Firstname],[LogInSurname] as [Surname],[LogInPassportID] as [LogIn Username],[LogInOTP] as [Generated OTP],[LogInMobileNumber] as [Mobile Number],[LogInEmailAddress] as [Email Address],[LogInChurchPosition] as [Church Position] FROM [dbo].[LogIn] ORDER BY [LogInFirstname] ASC"></asp:SqlDataSource>

    <%--MODAL FORM--%>
    <%-- JACOB JACOB JACOB JACOB JACOB --%>

          <div class="modal modal-wide  fade " id="RegUSer" tabindex="-1" role="dialog" aria-labelledby="RegUserTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-md modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="NewUserTitle"><span class="text-danger"><strong>ZAFMC - System Users</strong></span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div>
                            <%--RESIZE OR APPLY SCROLL BARS--%>
                            <div style="overflow-y: scroll; height: auto; width: auto; word-spacing:normal; word-wrap:normal">
                                <%--RESIZE OR APPLY SCROLL BARS--%>
                                <asp:GridView ID="UsersList" runat="server" AllowSorting="True" ToolTip="System Users..." CellPadding="4" ForeColor="#333333" GridLines="Both" DataSourceID="ZAFMCRegisterUser" AllowPaging="True" ShowHeaderWhenEmpty="True">
                                    <%-- Insert Row Numbers --%>
                                            <Columns>
		                                            <asp:TemplateField HeaderText="No." ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Maroon" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Maroon">
			                                            <ItemTemplate>
				                                            <%# Container.DataItemIndex + 1 %>
				
			                                            </ItemTemplate>
		                                            </asp:TemplateField>
	                                          </Columns>
                                    <%-- General Format (Table) --%>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
		                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="false" Height="25px" />
		                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
		                            <RowStyle BackColor="White" ForeColor="#330099"  height="20px" Width="150px" Wrap="false" BorderColor="Maroon"   />
		                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
		                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
		                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
		                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
		                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="CloseUserView" data-dismiss="modal" CssClass="text-danger" runat="server" ToolTip="Close System Users..." ><span class="glyphicon glyphicon-home "><strong> Close-Users</strong></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    <!--End Modal Form-->

</asp:Content>
