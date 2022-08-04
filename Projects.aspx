<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="ZAFMC.Projects" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />

    <div class="container">
        <div class="row">

            <div class="col-md-4">
                <asp:Panel ID="ProjectsOne" runat="server" BorderStyle="None" Height="600px" ScrollBars="Auto" ToolTip="Project details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="PN" runat="server" Text="Project Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="ProjectName" runat="server" placeholder="Project Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                    <asp:Label ID="PS" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Project Start Date"></asp:Label>
                    <asp:TextBox ID="ProjectStartDate" runat="server" placeholder="Start Date.." BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                    <asp:Label ID="PD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Project Duration (Days)"></asp:Label>
                    <asp:TextBox ID="ProjectDuration" runat="server" placeholder="Project Duration.." BorderColor="Maroon" CssClass="form-control" Height="29px" MaxLength="500" Width="160px"></asp:TextBox>
                    <asp:Label ID="ECD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Expected Completion Date"></asp:Label>
                    <asp:TextBox ID="ExpectedDate" runat="server" placeholder="Expected Date.." CssClass="form-control" Font-Bold="False" Font-Size="Small" Height="29px" TextMode="Date" Width="160px" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="PL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Project Leader"></asp:Label>
                    <asp:TextBox ID="ProjectLeader" runat="server" placeholder="Project Leader.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" Width="160px"></asp:TextBox>
                    <asp:Label ID="LPt" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Position"></asp:Label>
                    <asp:TextBox ID="LeaderPosition" runat="server" placeholder="Leader Position.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                    <asp:Label ID="PSt" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Project Site"></asp:Label>
                    <asp:TextBox ID="ProjectSite" runat="server" placeholder="Project Site.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" Width="160px"></asp:TextBox>
                    <asp:Label ID="ECt" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Estimated Cost"></asp:Label>
                    <asp:TextBox ID="EstimatedCost" runat="server" placeholder="Estimated Cost.." CssClass="form-control text-capitalize" Font-Bold="False" Height="29px" Width="160px" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="CDt" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Completed Date"></asp:Label>
                    <asp:TextBox ID="CompletedDate" runat="server" BorderColor="Maroon" CssClass="form-control" placeholder="Completed Date.." Height="29px" OnTextChanged="CompletedDate_TextChanged" TextMode="Date" Width="160px"></asp:TextBox>
                    <br />
                    <br />
                    <%-- CRUD Controls --%>
                    <hr />                                      
         <asp:LinkButton ID="ProjectSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveProject_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ProjectView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewProject_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ProjectEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditProject_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="ProjectRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshProject_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ProjectDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteProject_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ProjectReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                </asp:Panel>
                
            </div>
        <div class="col-md-8">
            <asp:Panel ID="ProjectsTwo" runat="server" BorderStyle="Groove" Height="600px" ScrollBars="Auto" ToolTip="Project details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                <asp:Label ID="LSP" runat="server" Text="List Of Scheduled Projects" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <br />
                <br />
                
                <asp:GridView ID="ProjectsList" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None"  ShowHeaderWhenEmpty="True" BorderWidth="1px" CellPadding="4"  Width="707px">
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
		            <RowStyle BackColor="White" ForeColor="#330099"   Width="150px" Wrap="false" BorderColor="Maroon"   />
		            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
		            <SortedAscendingCellStyle BackColor="#FEFCEB" />
		            <SortedAscendingHeaderStyle BackColor="#AF0101" />
		            <SortedDescendingCellStyle BackColor="#F6F0C0" />
		            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
                <br />
            </asp:Panel>
        </div>
        <%-- END PAGINATION --%>
    </div>
    </div>
   <br />



</asp:Content>
