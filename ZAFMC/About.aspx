<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ZAFMC.About" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="container">
        <div class="row">

            <div class="col-md-4">
                <asp:Panel ID="AboutOne" runat="server" BorderStyle="None" Height="700px" ScrollBars="Auto" ToolTip="About details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">

                    <%-- Accordions --%>

                    <div class="panel-group" id="accordion">
                        <div class="panel  panel-danger">
                            <div class="panel-heading" >
                                <!-- Leadership -->
                                <a href="#Leadership" data-toggle="collapse" data-parent="#accordion" >
                                    <h4 class="panel-title ">
                                        <span class="glyphicon glyphicon-apple text-danger">~Leadership </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Leadership" class="panel-collapse collapse" >
                                <div class="panel-body PAN" >
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="LAN" runat="server" Text="Leader Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="ALeadName" runat="server" placeholder="Leader Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="LASN" runat="server" Text="Leader Surname" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="ALeadSurnam" runat="server" placeholder="Leader Surname.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <asp:Label ID="LDOB" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Date Of Birth"></asp:Label>
                                        <asp:TextBox ID="LeaderDOB" runat="server" placeholder="DOB.." CssClass="form-control" Font-Size="Small" Height="29px" TextMode="Date" Width="150px" BorderColor="Maroon"></asp:TextBox>
                                        <li>
                                            <asp:Label ID="LID" runat="server" Text="Leader Identity" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="LeaderID" runat="server" placeholder="ID.." BorderColor="Maroon" CssClass="form-control text-uppercase" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <asp:Label ID="LRP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Rank / Position"></asp:Label>
                                        <li>
                                            <asp:DropDownList ID="LeaderRankPost" runat="server" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Post..!!">
                                                <asp:ListItem Text="High Six.." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Bishop" Value="Bishop"></asp:ListItem>
                                                <asp:ListItem Text="Vice-Bishop" Value="Vice-Bishop"></asp:ListItem>
                                                <asp:ListItem Text="Snr. High Priest" Value="Snr. High Priest"></asp:ListItem>
                                                <asp:ListItem Text="High Priest (1)" Value="High Priest (1)"></asp:ListItem>
                                                <asp:ListItem Text="High Priest (2)" Value="High Priest (2)"> </asp:ListItem>
                                                <asp:ListItem Text="High Priest (3)" Value="High Priest (3)"></asp:ListItem>
                                            </asp:DropDownList></li>
                                        <li>
                                            <asp:Label ID="LPct" runat="server" Text="Individual Photo" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:Image ID="LeaderIndPhoto" runat="server" style="border:0.5px solid maroon"  BorderColor="Maroon"  Height="150px" Width="186px" /></li>
                                        <li>
                                            <br />
                                        </li>
                                        <li>
                                            <asp:FileUpload ID="LeaderPhotoUpload" runat="server" style="border:0.5px solid maroon" ForeColor="Maroon"   CssClass="form-control" Font-Bold="True" Font-Size="Small" Height="30px" Width="190px" ToolTip="Upload Picture!!" /></li>

                                    </ul>
                                </div>
                                <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="SaveLeader" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveLeader_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ViewLeader" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewLeader_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EditLeader" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditLeader_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="RefreshLeader" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshLeader_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeleteLeader" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteLeader_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ResetLeader" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- Administration -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Administration" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title">
                                        <span class="glyphicon glyphicon-download text-danger">~Administration </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Administration" class="panel-collapse collapse">
                                <div class="panel-body PAN">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="ADMN" runat="server" Text="Admin Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="AdminName" runat="server" placeholder="Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="ADMSN" runat="server" Text="Admin Surname" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="AdminSurname" runat="server" placeholder="Surname.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="ADD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Appointed Date"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="AdminAppDate" runat="server" placeholder="Date.." CssClass="form-control" Font-Size="Small" Height="29px" TextMode="Date" Width="150px" BorderColor="Maroon"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="AIC" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Internation Committee"></asp:Label></li>
                                        <li>
                                            <asp:DropDownList ID="AdminIntCom" runat="server" style="border:0.5px solid maroon"  CssClass="form-control" Height="29px" Width="150px" ToolTip="Committee..!!">
                                                <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                                                <asp:ListItem Text="Vice-Chairman" Value="Vice-Chairman"></asp:ListItem>
                                                <asp:ListItem Text="Treasurer" Value="Treasurer"></asp:ListItem>
                                                <asp:ListItem Text="Secretary" Value="Secretary"></asp:ListItem>
                                                <asp:ListItem Text="Vice-Secretary" Value="Vice-Secretary"></asp:ListItem>
                                                <asp:ListItem Text="Committee Members" Value="Committee Members"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>

                                        <li>
                                            <asp:Label ID="AID" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="International Directors"></asp:Label></li>
                                        <li>
                                            <asp:DropDownList ID="AdminIntDir" runat="server" style="border:0.5px solid maroon"  CssClass="form-control" Height="29px" Width="150px" ToolTip="Directors..!!">
                                                <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Projects and Development" Value="Projects and Development"></asp:ListItem>
                                                <asp:ListItem Text="Music, Praise and Worship" Value="Music, Praise and Worship"></asp:ListItem>
                                                <asp:ListItem Text="Security" Value="Security"></asp:ListItem>
                                                <asp:ListItem Text="Education and Training" Value="Education and Training"></asp:ListItem>
                                                <asp:ListItem Text="Social Services" Value="Social Services"></asp:ListItem>
                                                <asp:ListItem Text="Health and Wellness" Value="Health and Wellness"></asp:ListItem>
                                                <asp:ListItem Text="ICT" Value="ICT"></asp:ListItem>
                                            </asp:DropDownList></li>

                                        <li>
                                            <asp:Label ID="SG" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Secretary General"></asp:Label></li>
                                        <li>
                                            <asp:DropDownList ID="SecretaryGeneral" runat="server" style="border:0.5px solid maroon"  CssClass="form-control" Height="29px" Width="150px" ToolTip="Secretary..!!">
                                                <asp:ListItem Text="Select.." Value="-1">Select..</asp:ListItem>
                                                <asp:ListItem Text="Secretary General" Value="Secretary General"></asp:ListItem>
                                            </asp:DropDownList></li>
                                    </ul>
                                </div>
                                <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="AdminSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveAdmin_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="AdminView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewAdmin_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="AdminEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditAdmin_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="AdminRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshAdmin_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="AdminDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteAdmin_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="AdminReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>

                        <!-- Deceased -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Deceased" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title">
                                        <span class="glyphicon glyphicon-road text-danger">~Deceased </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Deceased" class="panel-collapse collapse">
                                <div class="panel-body PAN">
                                    <ul class="text-danger" style="list-style-type: none;">

                                        <li>
                                            <asp:Label ID="DSN" runat="server" Text="Deceased Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedName" runat="server" placeholder="Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DSSN" runat="server" Text="Deceased Surname" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedSurname" runat="server" placeholder="Surname.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DSID" runat="server" Text="Deceased ID" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedID" runat="server" placeholder="ID.." BorderColor="Maroon" CssClass="form-control text-uppercase" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Of Death"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedDate" runat="server" placeholder="Date.." CssClass="form-control" Font-Size="Small" Height="29px" TextMode="Date" Width="150px" BorderColor="Maroon"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DP" runat="server" Text="Deceased Position" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedPosition" runat="server" placeholder="Position.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DBP" runat="server" Text="Burial Place" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="DeceasedBurialPlace" runat="server" placeholder="Burial.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="150px"></asp:TextBox></li>
                                        <li>
                                            <asp:Label ID="DMS" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Memorial Service Date"></asp:Label></li>
                                        <li>
                                            <asp:TextBox ID="MemorialService" runat="server" placeholder="Date.." CssClass="form-control" Font-Size="Small" Height="29px" TextMode="Date" Width="150px" BorderColor="Maroon"></asp:TextBox></li>

                                    </ul>
                                </div>
                                <%-- CRUD Controls --%>
                                <hr />
        <asp:LinkButton ID="DeceasedSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveDeceased_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeceasedView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewDeceased_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeceasedEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditDeceased_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="DeceasedRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshDeceased_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeceasedDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteDeceased_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="DeceasedReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>

                    </div>

                </asp:Panel>
            </div>
            
            <%-- SECOND COLUMN / PANEL --%>
            <div class="col-md-8">
                <asp:Panel ID="AboutTwo" runat="server" BorderStyle="Groove" Height="700px" ScrollBars="Auto" ToolTip="About details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">

                    <%-- GridView here --%>
                    <br />
                    <asp:Label ID="ZAFMCLeadership" runat="server"  Text="ZAFMC - Leadership" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Visible="false"></asp:Label>
                    <asp:Label ID="ZAFMCAdministration" runat="server"  Text="ZAFMC - Administration" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Visible="false"></asp:Label>
                    <asp:Label ID="ZAFMCDeceased" runat="server"  Text="ZAFMC - Deceased" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Visible="false"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="AboutZION" runat="server" BackColor="White" BorderColor="#CC9966"  ShowHeaderWhenEmpty="True" BorderStyle="None" BorderWidth="1px" CellPadding="4"  Width="699px" AllowSorting="False" OnLoad="AboutZION_Load">
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
		                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="false" Height="5px" />
		                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
		                <RowStyle BackColor="White" ForeColor="#330099"  height="5px" Width="150px" Wrap="false" BorderColor="Maroon"   />
		                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
		                <SortedAscendingCellStyle BackColor="#FEFCEB" />
		                <SortedAscendingHeaderStyle BackColor="#AF0101" />
		                <SortedDescendingCellStyle BackColor="#F6F0C0" />
		                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>
                    <br />
                    <br />
                </asp:Panel>
            </div>

        </div>
    </div>



</asp:Content>
