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

                                    <%-- Congregation Reports(Inner Panel) --%>

                                    <div class="panel-group" id="CongregationRep">
                                        <div class="panel panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Cong" data-toggle="collapse" data-parent="#CongregationRep">
                                                    <h4 class="panel-title" title="Congregation...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">~Congregants </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Cong" class="panel-collapse collapse">

                                                <asp:LinkButton ID="ConReports" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalCongReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
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
                                        <span class="glyphicon glyphicon-tower text-danger">~Churches Reports </span>
                                    </h4>
                                </a>
                            </div>
                            <div id="Churches" class="panel-collapse collapse">
                                <div class="panel-body ">
                                    <%-- Churches Reports(Inner Panel) --%>
                                    <div class="panel-group" id="CHRep">
                                        <div class="panel  panel-danger">
                                            <div class="panel-heading ">
                                                <a href="#Church" data-toggle="collapse" data-parent="#CHRep">
                                                    <h4 class="panel-title" title="Churches...">
                                                        <span class="glyphicon glyphicon-grain  text-danger">~Churches</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Church" class="panel-collapse collapse">

                                                <asp:LinkButton ID="CHReports" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalChurchReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

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
                                    <asp:LinkButton ID="TestmRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalTestmReport"  Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
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
                                    <asp:LinkButton ID="SermRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalSermReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

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
                                    <asp:LinkButton ID="EventRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalEventsReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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
                                                    <h4 class="panel-title" title="Passovers...">
                                                        <span class="glyphicon glyphicon-grain text-danger">Passovers </span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Pass" class="panel-collapse collapse">

                                                <asp:LinkButton ID="MonthPassRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalPassoverReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>
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

                                                <asp:LinkButton ID="MemRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalMemorialReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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
                                                <asp:LinkButton ID="BGRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalBigSundayReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

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

                                                <asp:LinkButton ID="WedRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalWeddingReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

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

                                                <asp:LinkButton ID="GrRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalGraduationReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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

                                                <asp:LinkButton ID="WomnRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalWomenReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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
                                                <asp:LinkButton ID="GenRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalGenReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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
                                                        <span class="glyphicon glyphicon-grain text-danger">Projects</span>
                                                    </h4>
                                                </a>
                                            </div>
                                            <div id="Scheduled" class="panel-collapse collapse">

                                                <asp:LinkButton ID="SchdRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalProjectReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>

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

                                                <asp:LinkButton ID="LeadRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalLeadershipReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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

                                                <asp:LinkButton ID="AdmRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger"  data-toggle="modal" data-target="#ModalAdminReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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

                                                <asp:LinkButton ID="DecRpt" runat="server" BorderStyle="None" CssClass="form-control text-danger" data-toggle="modal" data-target="#ModalDeceasedReport" Font-Bold="True" Font-Size="Small" Width="180px" ToolTip="Report..">Report</asp:LinkButton>


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
    <%-- Modal(Congregation Reports) --%>
    <div class="modal modal-wide  fade" id="ModalCongReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalMensReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Congregation Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="CongReport" runat="server" Target="_blank">Congregants</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Church Reports) --%>
    <div class="modal modal-wide  fade" id="ModalChurchReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalChrchReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Church Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="CHRCReport" runat="server" Target="_blank">Churches</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Testimony Reports) --%>
    <div class="modal modal-wide  fade" id="ModalTestmReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalTestReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Testimony Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="TestmReport" runat="server" Target="_blank">Testimonies</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Sermon Reports) --%>
    <div class="modal modal-wide  fade" id="ModalSermReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalSmnReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Sermon Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="SMNReport" runat="server" Target="_blank">Sermon</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Events Reports) --%>
    <div class="modal modal-wide  fade" id="ModalEventsReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalEvtReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Events Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="EVTReport" runat="server" Target="_blank">Events</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Passover Reports) --%>
    <div class="modal modal-wide  fade" id="ModalPassoverReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalPassReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Passover Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="PassReports" runat="server" Target="_blank">Passovers</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Memorial Reports) --%>
    <div class="modal modal-wide  fade" id="ModalMemorialReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalMemReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Memorial Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="MemReport" runat="server" Target="_blank">Memorials</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Big Sundays Reports) --%>
    <div class="modal modal-wide  fade" id="ModalBigSundayReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalBGSReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Big Sundays Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="BigSundayReport" runat="server" Target="_blank">Big Sundays</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>


     <%-- Modal(Weddings Reports) --%>
    <div class="modal modal-wide  fade" id="ModalWeddingReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalWEDDReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Weddings Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="WeddReport" runat="server" Target="_blank">Weddings</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

     <%-- Modal(Graduation Reports) --%>
    <div class="modal modal-wide  fade" id="ModalGraduationReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalGradReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Graduation Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="GradReport" runat="server" Target="_blank">Graduation</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

 <%-- Modal(Women Reports) --%>
    <div class="modal modal-wide  fade" id="ModalWomenReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalWOMReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Women Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="WOMReport" runat="server" Target="_blank">Women</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(General Meetings Reports) --%>
    <div class="modal modal-wide  fade" id="ModalGenReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalGMReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - General Meetings Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="GMReports" runat="server" Target="_blank">General Meetings</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Projects Reports) --%>
    <div class="modal modal-wide  fade" id="ModalProjectReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalProjReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Projects Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="PROJREport" runat="server" Target="_blank">Projects</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Leadership Reports) --%>
    <div class="modal modal-wide  fade" id="ModalLeadershipReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLEADReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Leadership Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="LEADReport" runat="server" Target="_blank">Leadership</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%-- Modal(Administration Reports) --%>
    <div class="modal modal-wide  fade" id="ModalAdminReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalADMReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Administration Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="ADMReport" runat="server" Target="_blank">Administration</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

     <%-- Modal(Deceased Reports) --%>
    <div class="modal modal-wide  fade" id="ModalDeceasedReport" tabindex="-1" role="dialog" aria-labelledby="ReportTitle" aria-hidden="true">

        <div class="modal-dialog" role="document">
            <div class="modal-sm modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalDSDReport"><span class="text-danger"></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body ">
                    
                    <div>
                        <h5 class="text-danger"><strong>ZAFMC - Deceased Report  </strong></h5>
                        <hr />
                        <asp:HyperLink ID="DCDReport" runat="server" Target="_blank">Deceased</asp:HyperLink>

                        <hr />
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
