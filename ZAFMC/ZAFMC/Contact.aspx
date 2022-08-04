<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ZAFMC.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
  <%-- Page Design --%>
    <div class="container">
        <div class="row">
            <%-- First Panel --%>
            <div class="col-md-4">
                <asp:Panel ID="ContactsOne" runat="server" BorderStyle="None" Height="500px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="NS" runat="server" Text="Name and Surname" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label><br />
                    <asp:DropDownList ID="NameSurname" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="32px" Width="170px" ToolTip="High Six(6) Only..!!">
                        <asp:ListItem Text="High Six.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Bishop Ezra Shoko" Value="Bishop Ezra Shoko"  > </asp:ListItem>
                        <asp:ListItem Text="Mr.J.Muzangwa" Value="Mr.J.Muzangwa"></asp:ListItem>
                        <asp:ListItem Text="Mr.TKS.Mawisire" Value="Mr.TKS.Mawisire"></asp:ListItem>
                        <asp:ListItem Text="Mr.J.Matongo" Value="Mr.J.Matongo"></asp:ListItem>
                        <asp:ListItem Text="Mr.P.Penengwa" Value="Mr.P.Penengwa"></asp:ListItem>
                        <asp:ListItem Text="Mr.J.Chidanga" Value="Mr.J.Chidanga"> </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="RP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Rank / Position"></asp:Label><br />
                    <asp:DropDownList ID="RankPost" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="32px" Width="170px" ToolTip="Post..!!">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Bishop" Value="Bishop"></asp:ListItem>
                        <asp:ListItem Text="Vice-Bishop" Value="Vice-Bishop"></asp:ListItem>
                        <asp:ListItem Text="Senior High Priest" Value="Senior High Priest"></asp:ListItem>
                        <asp:ListItem Text="High Priest (1)" Value="High Priest (1)"></asp:ListItem>
                        <asp:ListItem Text="High Priest (2)" Value="High Priest (2)"> </asp:ListItem>
                        <asp:ListItem Text="High Priest (3)" Value="High Priest (3)"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="MN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Mobile Number"></asp:Label><br />
                    <asp:TextBox ID="MobileNumber" runat="server" placeholder="Cell.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="15" Width="170px"></asp:TextBox>
                    <asp:Label ID="EA" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Email Address"></asp:Label><br />
                    <asp:TextBox ID="EmailAddress" runat="server" placeholder="Mail.." CssClass="form-control " Font-Bold="False" Font-Size="Small" Height="29px" TextMode="SingleLine" Width="170px" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="PA" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Physical Address"></asp:Label><br />
                    <asp:TextBox ID="PhysicalAddress" runat="server" placeholder="Home Address.." BorderColor="Maroon" CssClass="form-control text-capitalize" TextMode="MultiLine" Height="100px" MaxLength="80" Width="170px" ></asp:TextBox><br />
                <%-- CRUD Controls --%>
            <hr />
        <asp:LinkButton ID="SaveCont" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveCont_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ViewCont" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewCont_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EditCont" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditContact_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="EditRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="EditRefresh_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeleteCont" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteContact_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="Resets" type="reset" value="reset"   CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="Resets_Click" ><strong>Reset</strong></asp:LinkButton>
                    <br />
                </asp:Panel>
            </div>
           <%-- Second Panel --%>
            <div class="col-md-8">
                <asp:Panel ID="ContactsTwo" runat="server" BorderStyle="Groove" Height="480px" ScrollBars="Auto" ToolTip="High Six.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="HSx" runat="server"  Text=" ZAFMC - High Six (6) Contacts" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="HighSix" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True"  Width="699px" AllowSorting="False" OnLoad="HighSix_Load" OnUnload="HighSix_Unload" ToolTip="High Six Only..." >
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
		                <RowStyle BackColor="White" ForeColor="#330099"  height="5px" Width="150px" Wrap="false" BorderColor="Maroon"   />
		                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
		                <SortedAscendingCellStyle BackColor="#FEFCEB" />
		                <SortedAscendingHeaderStyle BackColor="#AF0101" />
		                <SortedDescendingCellStyle BackColor="#F6F0C0" />
		                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>
                    <br /><br /><br /><br /><br /><br />
                    
                    
                  
                                   
                   
                    <address class="text-danger"><strong>
                        ZAFMC HQ <br />
                        P.O. Box 4, <br />
                        Chivi, <br />
                        Masvingo, <br />
                        Zimbabwe. <br />
                        +263(773)659 180 <br />
                        <asp:HyperLink ID="Mail" runat="server" CssClass="text-info" ToolTip="Email us.." NavigateUrl="mailto::info@zafmc.co.zw">info@zafmc.co.zw</asp:HyperLink><br />
                        <asp:HyperLink ID="Website" runat="server" NavigateUrl="https://www.zafmc.org.zw" Target="_blank" CssClass="text-info" ToolTip="ZAFMC Website...">www.zafmc.org.zw</asp:HyperLink><br />
                        <asp:LinkButton ID="SystemInfo" runat="server" data-toggle="modal" data-target="#SysInfo" CssClass="text-danger" ToolTip="System Information.." > System Info</asp:LinkButton>    
                    </strong>
                    </address>
                  </asp:Panel>
            </div>
        </div>
    </div>
     <%--MODAL FORM USER ACCOUNT-INFO (LOGIN)--%>
        <%-- JACOB JACOB JACOB JACOB JACOB --%>
        <div class="modal modal-wide  fade " id="SysInfo" tabindex="-1" role="dialog" aria-labelledby="SysInfoTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-md modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div>
                            <h5 class="text-danger"><strong>System Information</strong></h5>
                            <hr />
                                <asp:Label ID="WinUser" runat="server" Font-Size="Small" Text="Windows Username" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="WinUsername" runat="server" Font-Size="Small" Text="Username" Font-Bold="True" ></asp:Label><br />
                                
                                <asp:Label ID="UDom" runat="server" Font-Size="Small" Text="User Domain Name" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="UserDomainName" runat="server" Font-Size="Small" Text="Domain" Font-Bold="True" ></asp:Label><br />
                              
                                <asp:Label ID="MachNa" runat="server" Font-Size="Small" Text="Machine Name" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="MachineName" runat="server" Font-Size="Small" Text="Machine" Font-Bold="True" ></asp:Label><br />
                               
                                <asp:Label ID="WinVers" runat="server" Font-Size="Small" Text="Windows Version" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="WindowsVersion" runat="server" Font-Size="Small" Text="OS" Font-Bold="True" ></asp:Label><br />

                                <asp:Label ID="IPAdd" runat="server" Font-Size="Small" Text="IP Address" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="IPAddres" runat="server" Font-Size="Small" Text="IPA" Font-Bold="True" ></asp:Label><br />

                                <asp:Label ID="MA" runat="server" Font-Size="Small" Text="MacAddress" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="MacAddress" runat="server" Font-Size="Small" Text="Mac" Font-Bold="True" ></asp:Label><br />

                                <asp:Label ID="BOS" runat="server" Font-Size="Small" Text=" Is 64 Bit OS ?" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="BitOS" runat="server" Font-Size="Small" Text="Bits" Font-Bold="True" ></asp:Label><br />
                               
                                <asp:Label ID="FPath" runat="server" Font-Size="Small" Text="Folder Path" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="FolderPath" runat="server" Font-Size="Small" Text="Path" Font-Bold="True" ></asp:Label><br />
                            
								 <asp:Label ID="CDir" runat="server" Font-Size="Small" Text="Current Directory" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="CurrentDirectory" runat="server" Font-Size="Small" Text="Direc" Font-Bold="True" ></asp:Label><br />

                                 <asp:Label ID="LD" runat="server" Font-Size="Small" Text="Logical Drive" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="LogicalDirectory" runat="server" Font-Size="Small" Text="Logical" Font-Bold="True" ></asp:Label><br />

                                <asp:Label ID="SysD" runat="server" Font-Size="Small" Text="System Directory" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="SystemDirectory" runat="server" Font-Size="Small" Text="Sys" Font-Bold="True" ></asp:Label><br />

                                 <asp:Label ID="CPUCo" runat="server" Font-Size="Small" Text="CPU Count" Font-Bold="True" CssClass="text-danger"></asp:Label>::
                                <asp:Label ID="CPUCount" runat="server" Font-Size="Small" Text="CPU" Font-Bold="True" ></asp:Label><br />

                                  <asp:Label ID="Ver" runat="server" Font-Size="Small" Text="Version" Font-Bold="True" CssClass="text-danger"></asp:Label> ::
                                <asp:Label ID="Vers" runat="server" Font-Size="Small" Text="Verss" Font-Bold="True" ></asp:Label><br />
                            <hr />
							<asp:LinkButton ID="SystInfo" data-dismiss="modal" CssClass="text-danger" runat="server" ToolTip="Close System-Info.."><span class="glyphicon glyphicon-home"><strong> Close</strong></span> </asp:LinkButton>
							</div>
							</div>
							</div>
							</div>
                </div>
							
</asp:Content>
