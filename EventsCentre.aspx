<%@ Page Title="EventsCentre" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventsCentre.aspx.cs" Inherits="ZAFMC.EventsCentre" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="container">
        <div class="row">

            <div class="col-md-4">
                <asp:Panel ID="EventsOne" runat="server" BorderStyle="None" Height="520px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="EN" runat="server" Text="Event Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="EventName" runat="server" placeholder="Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" Width="160px"></asp:TextBox>
                    <asp:Label ID="ED" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Event Date"></asp:Label>
                    <asp:TextBox ID="EventDate" runat="server" placeholder="Date.." BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                    <asp:Label ID="EV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Event Venue"></asp:Label>
                    <asp:TextBox ID="EventVenue" runat="server" placeholder="Venue.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                    <asp:Label ID="ECP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Contact Person"></asp:Label>
                    <asp:TextBox ID="EventPerson" runat="server" placeholder="Contact.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                    <asp:Label ID="ECC" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Contact Cell"></asp:Label>
                    <asp:TextBox ID="EventCell" runat="server" placeholder="Mobile.." BorderColor="Maroon" CssClass="form-control" Height="29px" MaxLength="15" Width="160px"></asp:TextBox>
                    <asp:Label ID="ECE" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Contact Email"></asp:Label>
                    <asp:TextBox ID="EventEmail" runat="server" placeholder="Mail.." BorderColor="Maroon" CssClass="form-control text-uppercase" Height="29px" Width="160px"></asp:TextBox>
                    <asp:Label ID="EVD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Event Duration (Days)"></asp:Label>
                    <asp:TextBox ID="EventDuration" runat="server" placeholder="Timeframe.." BorderColor="Maroon" CssClass="form-control" Height="29px" Width="160px"></asp:TextBox>
                    <asp:Label ID="ES" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Event Status"></asp:Label>
                    <asp:DropDownList ID="EventStatus" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="32px" Width="160px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                        <asp:ListItem Text="Not Approved" Value="Not Approved"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <%-- CRUD Controls --%>
                    <hr />
         <asp:LinkButton ID="EventSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveEvents_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EventView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewEvents_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EventEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditEvents_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="EventRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshEvents_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EventDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteEvents_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="EventReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                </asp:Panel>
            </div>
               <%-- Second Panel --%>
        <div class="col-md-8">
            <asp:Panel ID="EventsTwo" runat="server" BorderStyle="Groove" Height="520px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                <asp:Label ID="SchEvn" runat="server" Text="List Of Scheduled Events" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="EventsList" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None"  ShowHeaderWhenEmpty="True" BorderWidth="3px" CellPadding="4" HorizontalAlign="Left" Width="609px" ToolTip="ZAFMC- Events...">
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
		            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="false" Height="25px"  />
		            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
		            <RowStyle BackColor="White" ForeColor="#330099"  height="5px" Width="150px" Wrap="false" BorderColor="Maroon"   />
		            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
		            <SortedAscendingCellStyle BackColor="#FEFCEB" />
		            <SortedAscendingHeaderStyle BackColor="#AF0101" />
		            <SortedDescendingCellStyle BackColor="#F6F0C0" />
		            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>
            </asp:Panel>
        </div>

    </div>
    </div>

    <br />

</asp:Content>

