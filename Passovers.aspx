<%@ Page Title="Passovers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Passovers.aspx.cs" Inherits="ZAFMC.Passovers" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <asp:Panel ID="PassoverOne" runat="server" BorderStyle="None" Height="800px" ScrollBars="Auto"  CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">

                    <%-- ********************ACCORDIONS****************************** --%>

                    <%-- Panel Passover --%>
                    <div class="panel-group" id="accordion">
                        <div class="panel  panel-danger">
                            <div class="panel-heading">
                                <a href="#PassoverPan" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Passovers...">
                                        <span class="glyphicon glyphicon-home text-danger">~Passovers </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="PassoverPan" class="panel-collapse collapse">
                                <div class="panel-body" title="Passover Details...">
                                    <ul class="text-danger " style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="PD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Passover Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="PassoverDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="PV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Passover Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="PassoverVenue" runat="server" placeholder="Venue.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="PL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Venue Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="VenueLeader" runat="server" placeholder="Leader.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="LNum" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="LeaderMobile" runat="server" placeholder="Mobile.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassoverSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassover_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassoverView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassover_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassoverEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassover_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassoverRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassover_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassoverDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassover_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassoverReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- Panel Memorial Services -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Memorial" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Memorials...">
                                        <span class="glyphicon glyphicon-road text-danger">~Memorial Services</span>
                                    </h2>
                                </a>
                            </div>
                            <div id="Memorial" class="panel-collapse collapse">
                                <div class="panel-body" title="Memorial Details...">
                                    <ul class="text-danger" style="list-style-type: none;">

                                        <li>
                                            <asp:Label ID="MD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Memorial Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="MemorialDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="DN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Deceased Name"></asp:Label>
                                        </li>
                                        <li>
                                            <asp:TextBox ID="DeceasedName" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Deceased.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="MV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Memorial Venue"></asp:Label>
                                        </li>
                                        <li>
                                            <asp:TextBox ID="MemorialVenue" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Venue.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="CL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Church Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="ChurchLeader" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="RN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Relative Number"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="RelativeNumber" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" placeholder="Relative.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                                <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassMemSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassMemorial_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassMemView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassMemorial_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassMemEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassMemorial_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassMemRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassMemorial_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassMemDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassMemorial_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassMemReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- Panel BigSunday -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#BigSunday" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Big Sundays...">
                                        <span class="glyphicon glyphicon-envelope text-danger">~Big Sundays </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="BigSunday" class="panel-collapse collapse">
                                <div class="panel-body" title="Big Sunday Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="BGD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Big Sunday Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="BigSundayDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="BGV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Big Sunday Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="BigSundayVenue" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Sunday.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="BL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Venue Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="BigVenueLeader" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                        <li>
                                            <asp:Label ID="BLM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="LeaderCell" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" placeholder="Mobile.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassBGSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassBG_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassBGSView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassBG_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassBGEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassBG_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassBGRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassBG_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassBGSDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassBG_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassBGReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <%-- Panel Graduation --%>
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Graduation" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Graduation...">
                                        <span class="glyphicon glyphicon-signal text-danger">~Graduation Ceremonies </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="Graduation" class="panel-collapse collapse">
                                <div class="panel-body" title="Graduation Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="GD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Ceremony Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="CeremonyDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Ceremony Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="CeremonyVenue" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Ceremony.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GNM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Graduate Name"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GraduateName" runat="server" placeholder="Name.." BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                        <li>
                                            <asp:Label ID="GMN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Graduate Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GraduateCell" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="15" placeholder="Cell.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="DP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Degree Programme"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="DegreeProgramme" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="15" placeholder="Degree.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="CLR" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Church Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="ChrchLeader" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="15" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="LCM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="LdrCell" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="15" placeholder="Mobile.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassGradSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassGraduation_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGradView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassGraduation_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGradEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassGraduation_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassGradRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassGraduation_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGradDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassGraduation_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGradReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>

                        <%-- Panel YOUTH --%>

                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Youth" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Youth...">
                                        <span class="glyphicon glyphicon-download  text-danger">~Youth Conferences</span>
                                    </h2>
                                </a>
                            </div>
                            <div id="Youth" class="panel-collapse collapse">
                                <div class="panel-body" title="Youth Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="YD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Youth Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="YouthConDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="YV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Youth Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="YouthConVenue" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Venue.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="YLR" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Youth Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="YouthConLeader" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="YDR" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Conference Duration"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="YouthConDuration" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Duration.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                               <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassYouthSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassYouth_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassYTHView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassYouth_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassYouthEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassYouth_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassYouthRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassYouth_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassYTHDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassYouth_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassYouthReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>

                        <%-- Panel Women --%>

                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Women" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Women...">
                                        <span class="glyphicon glyphicon-bookmark text-danger">~Women Conferences </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="Women" class="panel-collapse collapse">
                                <div class="panel-body" title="Women Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="WD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Women Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WomenDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="WV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Women Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WomenVenue" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Venue.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="WL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Women Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WomenLeader" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                        <li>
                                            <asp:Label ID="WCD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Conference Duration"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WomenDuration" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Duration.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassWomSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassWomen_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWomView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassWomen_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWomEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassWomen_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassWomRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassWomen_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWomDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassWomen_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWomReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <%-- Panel General Meetings --%>

                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#GM" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="General...">
                                        <span class="glyphicon glyphicon-question-sign text-danger">~General Meetings </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="GM" class="panel-collapse collapse">
                                <div class="panel-body" title="General Details...">
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <li>
                                            <asp:Label ID="GMD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="General-Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GeneralDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GDN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="General-Description"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GMDescription" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="30" placeholder="Description.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GMV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="General-Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GMVenue" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Venue.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                        <li>
                                            <asp:Label ID="GML" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="General-Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GMLeader" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GMDT" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="General-Duration"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GMDuration" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>

                                    </ul>
                                </div>
                                 <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassGMSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassGeneralMeeting_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGMView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassGeneralMeeting_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGMEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassGeneralMeeting_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassGMRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassGeneralMeeting_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGMDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassGeneralMeeting_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassGMReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                            </div>
                        </div>
                        <!-- PANEL Weddings -->

                        <div class="panel  panel-danger" >
                            <div class="panel-heading ">
                                <a href="#Wedding" data-toggle="collapse" data-parent="#accordion">
                                    <h2 class="panel-title" title="Weddings..." >
                                        <span class="glyphicon glyphicon-calendar text-danger" >~Weddings </span>
                                    </h2>
                                </a>
                            </div>
                            <div id="Wedding" class="panel-collapse collapse" >
                                <div class="panel-body" title="Wedding Details...">
                                    <%-- Remove Bullets --%>
                                    <ul class="text-danger" style="list-style-type: none;">
                                        <%-- End Remove Bullets --%>
                                        <li>
                                            <asp:Label ID="WEDD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Wedding Date"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WeddingDate" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" TextMode="Date" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="WEDV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Wedding Venue"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WeddingVenue" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Venue.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="BNS" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Bride Name & Surname"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="BrideName" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Bride Name.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="BCON" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Bride Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="BrideMobile" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Mobile.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GNS" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Groom Name & Surname"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GroomName" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Groom Name.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="GCN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Groom Mobile"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="GroomCell" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Cell.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="MO" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Marriage Officer"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="MarriageOfficer" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Officer.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="CLW" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Church Leader"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WedChrchLeader" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Leader.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="LCNM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Church Leader Cell"></asp:Label><br />
                                        </li>
                                        <li>
                                            <asp:TextBox ID="WeddLeadCell" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" placeholder="Mobile.." TextMode="SingleLine" Width="160px"></asp:TextBox>
                                        </li>
                                    </ul>
                                </div>
                                <%-- CRUD Controls --%>
                                <hr />
         <asp:LinkButton ID="PassWeddSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SavePassWedding_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWeddView" CssClass="text-danger" ToolTip="View data.." runat="server" OnClick="ViewPassWedding_Click"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWeddEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditPassWedding_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="PassWeddRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshPassWedding_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWeddDelete" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeletePassWedding_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="PassWeddReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server"><strong>Reset</strong></asp:LinkButton>

                                <br />
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>
             <%-- SECOND PANEL (Grid)--%>
            <div class="col-md-8">
                <asp:Panel ID="PassoverTwo" runat="server" BorderStyle="Groove" Height="800px" ScrollBars="Auto" ToolTip="Passover details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
               <%-- // Grid Caption Headings //--%>   
                <asp:Label ID="Pass" runat="server" visible="false" Text="List Of Passovers" CssClass="text-danger" Font-Bold="True" Font-Size="Small" ></asp:Label>
                <asp:Label ID="Memo" runat="server" visible="false" Text="List Of Memorial Services" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="BigSund" runat="server" visible="false" Text="List Of Big Sundays" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="Grad" runat="server" visible="false" Text="List Of Graduations" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="YTH" runat="server" visible="false" Text="List Of Youth Events" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="WomEv" runat="server" visible="false" Text="List Of Women Events" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="GenM" runat="server" visible="false" Text="List Of General Meetings" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                <asp:Label ID="Wedds" runat="server" visible="false" Text="List Of Weddings" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>

                    <br />
                <br />
                <asp:GridView ID="PassoverList" runat="server" BackColor="White" BorderColor="#CC9966" AutoGenerateColumns="true" BorderStyle="None" BorderWidth="3px"  ShowHeaderWhenEmpty="True" CellPadding="4"  HorizontalAlign="Left" Width="700px" ToolTip="ZAFMC- Passovers...">
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


    <hr />

</asp:Content>

