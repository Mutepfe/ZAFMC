<%@ Page Title="MediaCentre" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MediaCentre.aspx.cs" Inherits="ZAFMC.MediaCentre" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
   <script type="text/javascript">
       //$('#News')

   </script>

    <div class="container">
        <div class="row">
            <%-- CRUD Controls --%>
            <div class="col-md-4">
                <asp:Panel ID="MediaOne" runat="server" BorderStyle="None" Height="700px" ScrollBars="Auto" ToolTip="Media details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon" Width="349px">

               
                   
                    <%-- ***************ACCORDIONS********** --%>

                    <%-- Panel NEWS --%>
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default panel-danger">
                           
                            <div class="panel-heading">
                                <a href="#ZIONNews" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title " title="News...">
                                        <span class="glyphicon glyphicon-home text-danger" id="News" >~News </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="ZIONNews" class="panel-collapse collapse">
                                <div class="panel-body" title="News Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
	                                    <%-- End Remove Bullets --%>
	                                    <li>
		                                    <asp:Label ID="NName" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="News Name"></asp:Label><br />
	                                    </li>

	                                    <li>
		                                    <asp:TextBox ID="NewsName" runat="server" placeholder="News Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
	                                    </li>
	                                    <li>
		                                    <asp:Label ID="NDate" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="News Date"></asp:Label><br />
	                                    </li>
	                                    <li>
		                                    <asp:TextBox ID="NewsDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
	                                    </li>
	                                    <li>
		                                    <asp:Label ID="NFile" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="News File"></asp:Label><br />
	                                    </li>
	                                    <li>
		                                    <asp:TextBox ID="NewsFile" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="1000" placeholder="News headlines.." TextMode="MultiLine" Width="245px"></asp:TextBox>
	                                    </li>
                                        
	                                    <li>
                                            <br />
		                                    <asp:FileUpload ID="NewsUpload" runat="server" style="border:0.5px solid maroon" Forecolor="Maroon" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Height="30px" Width="245px" ToolTip="Upload News...!!" />
	                                    </li>
                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
        <asp:LinkButton ID="MediaNewsSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveMediaNews_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaNwVw" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewMediaNews_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaNewsEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditMediaNews_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="MediaNewsRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshMediaNews_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaNewsDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteMediaNews_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaNewsReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- Panel SERMONS -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#ZIONSermon" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Sermons...">
                                        <span class="glyphicon glyphicon-road text-danger" >~Sermons </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="ZIONSermon" class="panel-collapse collapse">
                                <div class="panel-body" title="Sermon Details...">
                                    <ul class="text-danger" style="list-style-type: none;">

                                        <li>
                                            <asp:Label ID="Pr" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Preacher"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="Preacher" runat="server" placeholder="Preacher.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="ETN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Event Name"></asp:Label>
                                        </li>
                                        <li>
                                            <asp:TextBox ID="EventName" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Event.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="DP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Preached"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="DatePreached" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="MBV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Main Bible Verse"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="MainBibleVerse" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" placeholder="Bible Verse.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="SM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Sermon"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="SermonFile" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="130px" MaxLength="2000" placeholder="Sermon.." TextMode="MultiLine" Width="240px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <br />
                                            <asp:FileUpload ID="SermonUpload" runat="server" style="border:0.5px solid maroon" Forecolor="Maroon" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Height="30px" Width="245px" ToolTip="Upload Sermon!!" />
                                        </li>
                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
        <asp:LinkButton ID="MediaSermonSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveMediaSermon_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaSermView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewMediaSermon_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaSermonEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditMediaSermon_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="MediaSermonRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshMediaSermon_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaSermonDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteMediaSermon_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaSermonReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- Panel Videos and Music -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#ZIONVid" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Videos/Music...">
                                        <span class="glyphicon glyphicon-flag text-danger" >~Videos and Music </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="ZIONVid" class="panel-collapse collapse">
                                <div class="panel-body" title="Videos Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="VN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Video Name"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="VideoName" runat="server" placeholder="Clip Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="VO" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Video Occassion"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="VideoOccassion" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Occassion.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="VD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Video Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="VideoDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>

                                        <li>
                                            <asp:Label ID="VM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Videos and Music"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="VideoMusic" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="2000" placeholder="Music & Videos.." TextMode="MultiLine" Width="245px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <br />
                                            <asp:FileUpload ID="VidMus" runat="server" style="border:0.5px solid maroon" Forecolor="Maroon" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Height="30px" Width="245px" ToolTip="Upload Music / Video!!" />
                                        </li>

                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
        <asp:LinkButton ID="MediaVideoSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveMediaVideo_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaVideoVw" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewMediaVideo_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaVideoEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditMediaVideo_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="MediaVideoRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshMediaVideo_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaVideoDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteMediaVideo_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaVideoReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- PANEL TESTIMONIES -->

                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#ZIONTestimon" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Testimons...">
                                        <span class="glyphicon glyphicon-calendar text-danger">~Testimons </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="ZIONTestimon" class="panel-collapse collapse">
                                <%-- Remove Bullets --%>
                                <ul class="text-danger" style="list-style-type: none;">
                                    <%-- End Remove Bullets --%>
                                    <li>
                                        <asp:Label ID="Tst" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Testifier"></asp:Label><br />
                                    </li>

                                    <li>
                                        <asp:TextBox ID="Testifier" runat="server" placeholder="Testifier Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="TsDt" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Testimon Date"></asp:Label><br />
                                    </li>
                                    <li>
                                        <asp:TextBox ID="TestimonyDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="UpTs" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Testimony File"></asp:Label><br />
                                    </li>
                                    <li>
                                        <asp:TextBox ID="TestimonyFile" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="1000" placeholder="Testifier Testimony.." TextMode="MultiLine" Width="245px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <br />
                                        <asp:FileUpload ID="TestimonUpload" runat="server" style="border:0.5px solid maroon" Forecolor="Maroon" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Height="30px" Width="245px" ToolTip="Upload Testimony!!" />
                                    </li>

                                </ul>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="MediaTestimonySave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveMediaTestimony_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaTestimonyVw" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewMediaTestimony_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaTestimonyEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditMediaTestimony_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="MediaTestimonyRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshMediaTestimony_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaTestimonyDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteMediaTestimony_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="MediaTestimonyReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <%-- Panel Two --%>
            <div class="col-md-8">
                <asp:Panel ID="MediaTwo" runat="server" BorderStyle="Groove" Height="500px" ScrollBars="Auto" ToolTip="Media details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="MediaNews" runat="server" visible="false" Text="ZAFMC - News" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                   <asp:Label ID="MediaSermons" runat="server" visible="false"  Text="ZAFMC - Sermons" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                   <asp:Label ID="MediaVideos" runat="server" visible="false"  Text="ZAFMC - Videos and Music" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                   <asp:Label ID="MediaTestm" runat="server" visible="false"  Text="ZAFMC - Testimonies" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <%--  Grid View Control  --%>
                    <br />
                    <br />
                <asp:GridView ID="MediaList" runat="server" BackColor="White" BorderColor="#CC9966" ShowHeaderWhenEmpty="True" BorderStyle="None" BorderWidth="3px" CellPadding="4"  HorizontalAlign="Left" Width="609px" ToolTip="ZAFMC- Media...">
                     <%-- Insert Row Numbers --%>
                        <Columns>
		                        <asp:TemplateField HeaderText="No." ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Maroon" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Maroon">
			                        <ItemTemplate >
				                        <%# Container.DataItemIndex + 1 %>
				
			                        </ItemTemplate>
		                        </asp:TemplateField>
	                      </Columns>
                        <%-- General Format (Table) --%>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
		            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="false"   />
		            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
		            <RowStyle BackColor="White" ForeColor="#330099"   Width="150px"  Wrap="false"  BorderColor="Maroon" />
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
