<%@ Page Title="Churches" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="ZAFMC.Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   
    <br />


    <%-- PANELS --%>
    <div class="container">
        <div class="row">
            <%-- First Panel --%>
            <div class="col-md-4">
                <asp:Panel ID="ReportsOne" runat="server" BorderStyle="None" Height="700px" ScrollBars="Auto" ToolTip="Reports details.." CssClass="form-control" BorderColor="Maroon">

                    <!-- ACCORDION -->
                    <div class="panel-group" id="CongRep">
                        <div class="panel  panel-danger">
                            <div class="panel-heading">
                                <!-- CONGREGATION REPORTS -->
                                <a href="#Congregation" data-toggle="collapse" data-parent="#CongRep">
                                    <h4 class="panel-title " title="Congregation...">
                                        <span class="glyphicon glyphicon-glass text-danger">~Congregation Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Congregation" class="panel-collapse collapse">
                                <div class="panel-body ">

                                    <%-- Men Reports(Inner Panel) --%>

                                    <div class="panel-group" id="MenRep">
                                        <div class="panel panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Men" data-toggle="collapse" data-parent="#MenRep">
                                                    <h4 class="panel-title" title="Men...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Mens' Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Men" class="panel-collapse collapse">
                                                <asp:LinkButton ID="MenReports" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalMenReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <%-- Women Reports(Inner Panel) --%>
                                    <div class="panel-group" id="WomRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#WOM" data-toggle="collapse" data-parent="#WomRep">
                                                    <h4 class="panel-title" title="Women...">
                                                        <span class="glyphicon glyphicon-grain   text-danger">Womens' Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="WOM" class="panel-collapse collapse">

                                                <asp:LinkButton ID="WomenRep" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <%-- Children Reports(Inner Panel) --%>
                                    <div class="panel-group" id="ChildRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#KID" data-toggle="collapse" data-parent="#ChildRep">
                                                    <h2 class="panel-title" title="Children...">
                                                        <span class="glyphicon glyphicon-grain   text-danger">Childrens' Reports </span>
                                                    </h2>
                                                </a>
                                            </div>
                                            <div id="KID" class="panel-collapse collapse">

                                                <asp:LinkButton ID="KidsReport" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                        <!-- CHURCHES REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#Churches" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Churches...">
                                        <span class="glyphicon glyphicon-download text-danger">~Churches Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Churches" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <%-- Province Reports(Inner Panel) --%>
                                    <div class="panel-group" id="ProvRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Province" data-toggle="collapse" data-parent="#ProvRep">
                                                    <h4 class="panel-title" title="Province...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Province Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Province" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ProvRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <%-- District Reports(Inner Panel) --%>
                                    <div class="panel-group" id="DistRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#District" data-toggle="collapse" data-parent="#DistRep">
                                                    <h4 class="panel-title" title="District...">
                                                        <span class="glyphicon glyphicon-grain text-danger">District Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="District" class="panel-collapse collapse">

                                                <asp:LinkButton ID="DistRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <%-- Zone Reports(Inner Panel) --%>
                                    <div class="panel-group" id="ZoneRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Zone" data-toggle="collapse" data-parent="#ZoneRep">
                                                    <h4 class="panel-title" title="Zone...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Zone Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Zone" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ZoneRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <%-- Section Reports(Inner Panel) --%>
                                    <div class="panel-group" id="SectRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Section" data-toggle="collapse" data-parent="#ZoneRep">
                                                    <h4 class="panel-title" title="Section...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Section Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Section" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SectRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- Priest Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PrstRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Priest" data-toggle="collapse" data-parent="#ZoneRep">
                                                    <h4 class="panel-title" title="Priests...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Priest Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Priest" class="panel-collapse collapse">

                                                <asp:LinkButton ID="PriestRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>

                                    <%-- Prophets Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PhtRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Prophet" data-toggle="collapse" data-parent="#PhtRep">
                                                    <h4 class="panel-title" title="Prophets...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Prophets Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Prophet" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ProphetRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Pastor Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PastRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Pastor" data-toggle="collapse" data-parent="#PastRep">
                                                    <h4 class="panel-title" title="Pastors...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Pastors Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Pastor" class="panel-collapse collapse">
                                                <asp:LinkButton ID="PastorRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Evangelist Reports(Inner Panel) --%>
                                    <div class="panel-group" id="EvRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Evangelist" data-toggle="collapse" data-parent="#EvRep">
                                                    <h4 class="panel-title" title="Evangelists...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Evangelist Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Evangelist" class="panel-collapse collapse">

                                                <asp:LinkButton ID="EvangRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Preachers Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PrRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Preachers" data-toggle="collapse" data-parent="#EvRep">
                                                    <h4 class="panel-title" title="Preachers...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Preachers Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Preachers" class="panel-collapse collapse">

                                                <asp:LinkButton ID="PreachRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Decons Reports(Inner Panel) --%>
                                    <div class="panel-group" id="DecRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Decon" data-toggle="collapse" data-parent="#DecRep">
                                                    <h4 class="panel-title" title="Decons...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Decons Reports </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Decon" class="panel-collapse collapse">
                                                <asp:LinkButton ID="DeconRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Other Members Reports(Inner Panel) --%>
                                    <div class="panel-group" id="OMRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Members" data-toggle="collapse" data-parent="#OMRep">
                                                    <h4 class="panel-title" title="Other...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Other Members </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Members" class="panel-collapse collapse">
                                                <asp:LinkButton ID="MembersRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <!-- TESTIMONY REPORTS -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Testimony" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Testimony...">
                                        <span class="glyphicon glyphicon-flag text-danger">~Testimony Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Testimony" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <asp:LinkButton ID="TestmRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <!-- SERMON REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#Sermon" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Sermon...">
                                        <span class="glyphicon glyphicon-inbox text-danger">~Sermon Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Sermon" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <asp:LinkButton ID="SermRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <!-- EVENTS REPORTS -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Events" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Events...">
                                        <span class="glyphicon glyphicon-euro text-danger">~Events Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Events" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <asp:LinkButton ID="EventRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                </div>
                            </div>
                        </div>
                        <!-- FINANCE REPORTS -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Finance" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Finance...">
                                        <span class="glyphicon glyphicon-heart text-danger">~Finance Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Finance" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <%-- Passover Donations Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PassDonRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#PassDon" data-toggle="collapse" data-parent="#PassDonRep">
                                                    <h4 class="panel-title" title="Donations...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Passover Donations </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="PassDon" class="panel-collapse collapse">
                                                <asp:LinkButton ID="PassRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Big Sunday Reports(Inner Panel) --%>
                                    <div class="panel-group" id="BSRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#BigSund" data-toggle="collapse" data-parent="#BSRep">
                                                    <h4 class="panel-title" title="Big Sundays...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Big Sundays </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="BigSund" class="panel-collapse collapse">
                                                <asp:LinkButton ID="BigSundRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Funerals Reports(Inner Panel) --%>
                                    <div class="panel-group" id="FunRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Funeral" data-toggle="collapse" data-parent="#FunRep">
                                                    <h4 class="panel-title" title="Funerals...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Funeral Contributions </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Funeral" class="panel-collapse collapse">

                                                <asp:LinkButton ID="FunRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Construction Reports(Inner Panel) --%>
                                    <div class="panel-group" id="ConstRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Construction" data-toggle="collapse" data-parent="#ConstRep">
                                                    <h4 class="panel-title" title="Constructions...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Construction Report </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Construction" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ConstRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- School Donation Reports(Inner Panel) --%>
                                    <div class="panel-group" id="SchRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#School" data-toggle="collapse" data-parent="#SchRep">
                                                    <h4 class="panel-title" title="School...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">School Donations </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="School" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SchoolRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Ophanage Donation Reports(Inner Panel) --%>
                                    <div class="panel-group" id="OphRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Ophanage" data-toggle="collapse" data-parent="#OphRep">
                                                    <h4 class="panel-title" title="Ophanage...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Ophanage Donations </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Ophanage" class="panel-collapse collapse">

                                                <asp:LinkButton ID="OphanRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Bishop Cost Reports(Inner Panel) --%>
                                    <div class="panel-group" id="BERep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Bishop" data-toggle="collapse" data-parent="#BERep">
                                                    <h4 class="panel-title" title="Bishop...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Bishop Expenses </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Bishop" class="panel-collapse collapse">
                                                <asp:LinkButton ID="BishRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <!-- PASSOVER REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#Passover" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Passovers...">
                                        <span class="glyphicon glyphicon-film text-danger">~Passover Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Passover" class="panel-collapse collapse">
                                <div class="panel-body ">

                                    <%-- Monthly Passovers Reports(Inner Panel) --%>
                                    <div class="panel-group" id="PvRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Pass" data-toggle="collapse" data-parent="#PvRep">
                                                    <h4 class="panel-title" title="Monthly...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Monthly Passovers </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Pass" class="panel-collapse collapse">

                                                <asp:LinkButton ID="MonthPassRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Memorial Services Reports(Inner Panel) --%>
                                    <div class="panel-group" id="MemRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Memorial" data-toggle="collapse" data-parent="#MemRep">
                                                    <h4 class="panel-title" title="Memorial...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Memorial Services </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Memorial" class="panel-collapse collapse">

                                                <asp:LinkButton ID="MemRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Big Sunday Services Reports(Inner Panel) --%>
                                    <div class="panel-group" id="BGSRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#BGS" data-toggle="collapse" data-parent="#BGSRep">
                                                    <h4 class="panel-title" title="Sunday Services...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Big Sunday Services </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="BGS" class="panel-collapse collapse">
                                                <asp:LinkButton ID="BGRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Weddings Reports(Inner Panel) --%>
                                    <div class="panel-group" id="WeddRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Wedding" data-toggle="collapse" data-parent="#WeddRep">
                                                    <h4 class="panel-title" title="Weddings...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Weddings</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Wedding" class="panel-collapse collapse">

                                                <asp:LinkButton ID="WedRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Graduation Reports(Inner Panel) --%>
                                    <div class="panel-group" id="GradRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Grad" data-toggle="collapse" data-parent="#GradRep">
                                                    <h4 class="panel-title" title="Graduations...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Graduation</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Grad" class="panel-collapse collapse">

                                                <asp:LinkButton ID="GrRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Women Reports(Inner Panel) --%>
                                    <div class="panel-group" id="WRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Women" data-toggle="collapse" data-parent="#WRep">
                                                    <h4 class="panel-title" title="Women...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Women Conferences</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Women" class="panel-collapse collapse">

                                                <asp:LinkButton ID="WomnRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- General Meetings Reports(Inner Panel) --%>
                                    <div class="panel-group" id="GenMRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Gen" data-toggle="collapse" data-parent="#GenMRep">
                                                    <h4 class="panel-title" title="General...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">General Meetings</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Gen" class="panel-collapse collapse">
                                                <asp:LinkButton ID="GenRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- PROJECTS REPORTS -->
                        <div class="panel  panel-danger">
                            <div class="panel-heading ">
                                <a href="#Projects" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Projects...">
                                        <span class="glyphicon glyphicon-ok text-danger">~Projects Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Projects" class="panel-collapse collapse">
                                <div class="panel-body ">

                                    <%-- Scheduled Projects Reports(Inner Panel) --%>
                                    <div class="panel-group" id="SchdRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Scheduled" data-toggle="collapse" data-parent="#SchdRep">
                                                    <h4 class="panel-title" title="Scheduled Projects...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Scheduled Projects</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Scheduled" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SchdRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Inprogress Projects Reports(Inner Panel) --%>
                                    <div class="panel-group" id="InPRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Inprogress" data-toggle="collapse" data-parent="#InPRep">
                                                    <h4 class="panel-title" title="Inprogress Projects...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Inprogress Projects</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Inprogress" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ProgRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>
                                    <%-- Completed Projects Reports(Inner Panel) --%>
                                    <div class="panel-group" id="CompRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Completed" data-toggle="collapse" data-parent="#CompRep">
                                                    <h4 class="panel-title" title="Completed Projects...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Completed Projects</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Completed" class="panel-collapse collapse">

                                                <asp:LinkButton ID="CompRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Abandoned Projects Reports(Inner Panel) --%>
                                    <div class="panel-group" id="AbdRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Abandoned" data-toggle="collapse" data-parent="#AbdRep">
                                                    <h4 class="panel-title" title="Abandoned Projects...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Abandoned Projects</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Abandoned" class="panel-collapse collapse">

                                                <asp:LinkButton ID="AbnRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <!-- ABOUT REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#About" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="About...">
                                        <span class="glyphicon glyphicon-remove text-danger">~About Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="About" class="panel-collapse collapse">
                                <div class="panel-body ">

                                    <%-- Leadership Reports(Inner Panel) --%>
                                    <div class="panel-group" id="LDRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Leadership" data-toggle="collapse" data-parent="#LDRep">
                                                    <h4 class="panel-title" title="Leadership...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Leadership (High Six)</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Leadership" class="panel-collapse collapse">

                                                <asp:LinkButton ID="LeadRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Administration Reports(Inner Panel) --%>
                                    <div class="panel-group" id="AdminRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Administration" data-toggle="collapse" data-parent="#AdminRep">
                                                    <h4 class="panel-title" title="Admin...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Administration</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Administration" class="panel-collapse collapse">

                                                <asp:LinkButton ID="AdmRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                    <%-- Deceased Reports(Inner Panel) --%>
                                    <div class="panel-group" id="DecsRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Deceased" data-toggle="collapse" data-parent="#DecsRep">
                                                    <h4 class="panel-title" title="Deceased...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">Deceased</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Deceased" class="panel-collapse collapse">

                                                <asp:LinkButton ID="DecRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>



                                </div>
                            </div>
                        </div>
                        <!-- CONTACT REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#Contact" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="Contacts...">
                                        <span class="glyphicon glyphicon-print text-danger">~Contact Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Contact" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <%-- Contacts Reports(Inner Panel) --%>
                                    <div class="panel-group" id="ContRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Contacts" data-toggle="collapse" data-parent="#ContRep">
                                                    <h4 class="panel-title" title="Contacts...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Contacts</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Contacts" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ContRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <!-- LOGIN REPORTS -->
                        <div class="panel  panel-danger ">
                            <div class="panel-heading ">
                                <a href="#LogIn" data-toggle="collapse" data-parent="#accordion">
                                    <h4 class="panel-title" title="LogIn...">
                                        <span class="glyphicon glyphicon-home text-danger">~LogIn Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="LogIn" class="panel-collapse collapse">
                                <div class="panel-body">

                                    <%-- System Admin Reports(Inner Panel) --%>
                                    <div class="panel-group" id="SARep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#SysAdm" data-toggle="collapse" data-parent="#SARep">
                                                    <h4 class="panel-title" title="System Admin...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">System Administrators</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="SysAdm" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SystAdminRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>



                                    <%-- Log Reports(Inner Panel) --%>
                                    <div class="panel-group" id="SysLogRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Sys" data-toggle="collapse" data-parent="#SysLogRep">
                                                    <h4 class="panel-title" title="System Log...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">System Log Report</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Sys" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SytAdmnRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>
            <%-- Second Panel --%>
            <div class="col-md-8">
                <asp:Panel ID="ReportsTwo" runat="server" BorderStyle="Groove" Height="700px" ScrollBars="Auto" ToolTip="Reports details.." CssClass="form-control" BorderColor="Maroon">
                </asp:Panel>

            </div>

        </div>
    </div>
    <br />
    <hr />
    <%-- Modal(Mens' Reports) --%>

    <div class="modal modal-wide  fade " id="ModalMenReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-lg modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalMensReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body " onload="ShowMenReport()">
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Mens' Report  </strong><strong></strong></h5>
                        <hr />
                        
                       
                        <rsweb:ReportViewer id="MenReportViewer" showprintbutton="false" runat="server" width="100%" height="800%" asyncrendering="true" zoommode="Percent" keepsessionalive="true" sizetoreportcontent="false">
                            <%--<ServerReport
                                ReportServerUrl="~/ZAFMCReports/Reports"
                                ReportPath="/Male.rdl"
                                DisplayName="ZAFMC - Mens' Report" />--%>
                            <LocalReport ReportPath="~/ZAFMCReports/Reports/Male.rdl" DisplayName="Men's Report" EnableExternalImages="true" > 
                                 
                            </LocalReport>
                        </rsweb:ReportViewer>


                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Womens' Reports) --%>
    <%-- Modal(Childrens' Reports) --%>
</asp:Content>
