<%@ Page Title="Churches" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Churches.aspx.cs" Inherits="ZAFMC.Churches" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <%-- JQUERY Script to disable some Dropdown Items --%>
    <script type="text/javascript">
        $(function ()
        {
            
            //Province DropDown
            $("#<%=ChurchProvince.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            $("#<%=ChurchProvince.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
             //District DropDown
            $("#<%=ChurchDistrict.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            $("#<%=ChurchDistrict.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            //ZONE DropDown
            $("#<%=ChurchZone.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            $("#<%=ChurchZone.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            //Section DropDown
            $("#<%=ChurchSection.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            $("#<%=ChurchSection.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
        });

    </script>
    
    <div class="container">
        <div class="row">
            <%-- FIRST PANEL --%>
            <div class="col-md-4">
                <asp:Panel ID="ChurchesOne" runat="server" BorderStyle="None" Height="600px" ScrollBars="Auto" ToolTip="Church details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="CN" runat="server" Text="Church Name" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="ChurchName" runat="server" placeholder="Name.." BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" MaxLength="20" ToolTip="Name!!" Width="150px"></asp:TextBox>
                    <asp:Label ID="CL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Church Leader"></asp:Label>
                    <asp:TextBox ID="ChurchLeader" runat="server" placeholder="Leader.."  BorderColor="Maroon" CssClass="form-control text-capitalize" Height="29px" Width="150px"></asp:TextBox>
                    <asp:Label ID="LCN" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Cell Number"></asp:Label>
                    <asp:TextBox ID="LeaderCell" runat="server" placeholder="Cell.."  BorderColor="Maroon" CssClass="form-control" Height="29px" Width="150px"></asp:TextBox>
                    <asp:Label ID="LEA" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Leader Email"></asp:Label>
                    <asp:TextBox ID="LeaderEmail" runat="server" placeholder="Email.."  BorderColor="Maroon" CssClass="form-control" Height="29px" Width="150px"></asp:TextBox>
                    <asp:Label ID="DOO" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Officially Opened"></asp:Label>
                    <asp:TextBox ID="DateOpened" runat="server" BorderColor="Maroon" CssClass="form-control" Height="29px" MaxLength="30" TextMode="Date" Width="150px"></asp:TextBox>
                    <asp:Label ID="NOM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Number Of Members"></asp:Label>
                    <asp:TextBox ID="NumberOfMembers" runat="server" placeholder="Total.."  BorderColor="Maroon" CssClass="form-control" Height="29px" MaxLength="4" Width="150px"></asp:TextBox>
                    <asp:Label ID="PRV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Province"></asp:Label>

                    <asp:DropDownList ID="ChurchProvince" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="29px" Width="150px">
                        <asp:ListItem Text="Select.." Value="Select.."></asp:ListItem>
                        <asp:ListItem class="text-danger">Zimbabwe</asp:ListItem>
                        <asp:ListItem Text="Bulawayo" Value="Bulawayo"></asp:ListItem>
                        <asp:ListItem Text="Harare" Value="Harare"></asp:ListItem>
                        <asp:ListItem Text="Manicaland" Value="Manicaland"></asp:ListItem>
                        <asp:ListItem Text="Mash. Central" Value="Mash. Central"></asp:ListItem>
                        <asp:ListItem Text="Mash. East" Value="Mash. East"></asp:ListItem>
                        <asp:ListItem Text="Mash. West" Value="Mash. West"></asp:ListItem>
                        <asp:ListItem Text="Masvingo" Value="Masvingo"></asp:ListItem>
                        <asp:ListItem Text="Matabeleland" Value="Matabeleland"></asp:ListItem>
                        <asp:ListItem Text="Midlands" Value="Midlands"></asp:ListItem>
                        <asp:ListItem class="text-danger">SouthAfrica</asp:ListItem>
                        <asp:ListItem Text="WesternCape" Value="WesternCape"></asp:ListItem>
                        <asp:ListItem Text="EasternCape" Value="EasternCape"></asp:ListItem>
                        <asp:ListItem Text="NorthenCape" Value="NorthenCape"></asp:ListItem>
                        <asp:ListItem Text="NorthWest" Value="NorthWest"></asp:ListItem>
                        <asp:ListItem Text="FreeState" Value="FreeState"></asp:ListItem>
                        <asp:ListItem Text="KZN" Value="KZN"></asp:ListItem>
                        <asp:ListItem Text="Gauteng" Value="Gauteng"></asp:ListItem>
                        <asp:ListItem Text="Limpopo" Value="Limpopo">Limpopo</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="DSTC" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="District"></asp:Label>
                    <asp:DropDownList ID="ChurchDistrict" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="29px" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem class="text-danger">Zimbabwe</asp:ListItem>
                        <asp:ListItem Text="Bulawayo" Value="Bulawayo"></asp:ListItem>
                        <asp:ListItem Text="Harare" Value="Harare"></asp:ListItem>
                        <asp:ListItem Text="Buhera" Value="Buhera"></asp:ListItem>
                        <asp:ListItem Text="Chimanimani" Value="Chimanimani"></asp:ListItem>
                        <asp:ListItem Text="Chipinge" Value="Chipinge"></asp:ListItem>
                        <asp:ListItem Text="Makoni" Value="Makoni"></asp:ListItem>
                        <asp:ListItem Text="Mutare" Value="Mutare"></asp:ListItem>
                        <asp:ListItem Text="Mutasa" Value="Mutasa"></asp:ListItem>
                        <asp:ListItem Text="Nyanga" Value="Nyanga"></asp:ListItem>
                        <asp:ListItem Text="Bindura" Value="Bindura"></asp:ListItem>
                        <asp:ListItem Text="Guruve" Value="Guruve"></asp:ListItem>
                        <asp:ListItem Text="Mazowe" Value="Mazowe"></asp:ListItem>
                        <asp:ListItem Text="Mbire" Value="Mbire"></asp:ListItem>
                        <asp:ListItem Text="Mt. Darwin" Value="Mt. Darwin"></asp:ListItem>
                        <asp:ListItem Text="Muzarabani" Value="Muzarabani"></asp:ListItem>
                        <asp:ListItem Text="Rushinga" Value="Rushinga"></asp:ListItem>
                        <asp:ListItem Text="Shamva" Value="Shamva"></asp:ListItem>
                        <asp:ListItem Text="Chikomba" Value="Chikomba"></asp:ListItem>
                        <asp:ListItem Text="Goromonzi" Value="Goromonzi"></asp:ListItem>
                        <asp:ListItem Text="Marondera" Value="Marondera"></asp:ListItem>
                        <asp:ListItem Text="Mudzi" Value="Mudzi"></asp:ListItem>
                        <asp:ListItem Text="Murehwa" Value="Murehwa"></asp:ListItem>
                        <asp:ListItem Text="Mutoko" Value="Mutoko"></asp:ListItem>
                        <asp:ListItem Text="Seke" Value="Seke"></asp:ListItem>
                        <asp:ListItem Text="Uzumba" Value="Uzumba"></asp:ListItem>
                        <asp:ListItem Text="Wedza" Value="Wedza"></asp:ListItem>
                        <asp:ListItem Text="Chegutu" Value="Chegutu"></asp:ListItem>
                        <asp:ListItem Text="Chinhoyi" Value="Chinhoyi"></asp:ListItem>
                        <asp:ListItem Text="Hurungwe" Value="Hurungwe"></asp:ListItem>
                        <asp:ListItem Text="Kariba" Value="Kariba"></asp:ListItem>
                        <asp:ListItem Text="Makonde" Value="Makonde"></asp:ListItem>
                        <asp:ListItem Text="Ngezi" Value="Ngezi"></asp:ListItem>
                        <asp:ListItem Text="Sanyati" Value="Sanyati"></asp:ListItem>
                        <asp:ListItem Text="Zvimba" Value="Zvimba"></asp:ListItem>
                        <asp:ListItem Text="Bikita" Value="Bikita"></asp:ListItem>
                        <asp:ListItem Text="Chiredzi" Value="Chiredzi"></asp:ListItem>
                        <asp:ListItem Text="Chivi" Value="Chivi"></asp:ListItem>
                        <asp:ListItem Text="Gutu" Value="Gutu"></asp:ListItem>
                        <asp:ListItem Text="Masvingo" Value="Masvingo"></asp:ListItem>
                        <asp:ListItem Text="Mwenezi" Value="Mwenezi"></asp:ListItem>
                        <asp:ListItem Text="Zaka" Value="Zaka"></asp:ListItem>
                        <asp:ListItem Text="Binga" Value="Binga"></asp:ListItem>
                        <asp:ListItem Text="Binga" Value="Binga"></asp:ListItem>
                        <asp:ListItem Text="Bubi" Value="Bubi"></asp:ListItem>
                        <asp:ListItem Text="Hwange" Value="Hwange"></asp:ListItem>
                        <asp:ListItem Text="Lupane" Value="Lupane"></asp:ListItem>
                        <asp:ListItem Text="Nkayi" Value="Nkayi"></asp:ListItem>
                        <asp:ListItem Text="Tsholotsho" Value="Tsholotsho"></asp:ListItem>
                        <asp:ListItem Text="Umguza" Value="Umguza"></asp:ListItem>
                        <asp:ListItem Text="BeitBridge" Value="BeitBridge"></asp:ListItem>
                        <asp:ListItem Text="Bulilima" Value="Bulilima"></asp:ListItem>
                        <asp:ListItem Text="Gwanda" Value="Gwanda"></asp:ListItem>
                        <asp:ListItem Text="Insiza" Value="Insiza"></asp:ListItem>
                        <asp:ListItem Text="Mangwe" Value="Mangwe"></asp:ListItem>
                        <asp:ListItem Text="Matobo" Value="Matobo"></asp:ListItem>
                        <asp:ListItem Text="Umzingwane" Value="Umzingwane"></asp:ListItem>
                        <asp:ListItem Text="Chirumhanzu" Value="Chirumhanzu"></asp:ListItem>
                        <asp:ListItem Text="Gokwe North" Value="Gokwe North"></asp:ListItem>
                        <asp:ListItem Text="Gokwe South" Value="Gokwe South"></asp:ListItem>
                        <asp:ListItem Text="Gweru" Value="Gweru"></asp:ListItem>
                        <asp:ListItem Text="Kwekwe" Value="Kwekwe"></asp:ListItem>
                        <asp:ListItem Text="Mberengwa" Value="Mberengwa"></asp:ListItem>
                        <asp:ListItem Text="Shurugwi" Value="Shurugwi"></asp:ListItem>
                        <asp:ListItem Text="Zvishavane" Value="Zvishavane"></asp:ListItem>
                        <asp:ListItem class="text-danger">SouthAfrica</asp:ListItem>
                        <asp:ListItem Text="Pretoria" Value="Pretoria"></asp:ListItem>
                        <asp:ListItem Text="JHBurg" Value="JHBurg"></asp:ListItem>
                        <asp:ListItem Text="Durban" Value="Durban"></asp:ListItem>
                        <asp:ListItem Text="CapeTown" Value="CapeTown"></asp:ListItem>
                        <asp:ListItem Text="PE" Value="PE"></asp:ListItem>
                        <asp:ListItem Text="Bloemfontein" Value="Bloemfontein"></asp:ListItem>
                        <asp:ListItem Text="Rustenburg" Value="Rustenburg"></asp:ListItem>
                        <asp:ListItem Text="Mafikeng" Value="Mafikeng"></asp:ListItem>
                        <asp:ListItem Text="Brits" Value="Brits"></asp:ListItem>
                         <asp:ListItem Text="Cullinan" Value="Cullinan"></asp:ListItem>
                        <asp:ListItem Text="Kameldrift" Value="Kameldrift"></asp:ListItem>
                        <asp:ListItem Text="Pietermaritzburg" Value="Pietermaritzburg"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Zns" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Zone"></asp:Label>
                    <asp:DropDownList ID="ChurchZone" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="29px" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem class="text-danger">Zimbabwe</asp:ListItem>
                        <asp:ListItem Text="Museva" Value="Museva"></asp:ListItem>
                        <asp:ListItem Text="Rusape" Value="Rusape"></asp:ListItem>
                        <asp:ListItem Text="Sese" Value="Sese"></asp:ListItem>
                        <asp:ListItem Text="New Canaan" Value="New Canaan"></asp:ListItem>
                        <asp:ListItem Text="Kuwadzana" Value="Kuwadzana"></asp:ListItem>
                        <asp:ListItem Text="Sunningdale" Value="Sunningdale"></asp:ListItem>
                        <asp:ListItem Text="Epworth" Value="Epworth"></asp:ListItem>
                        <asp:ListItem Text="Dangamvura" Value="Dangamvura"></asp:ListItem>
                        <asp:ListItem Text="Sakubva" Value="Sakubva"></asp:ListItem>
                        <asp:ListItem Text="Sharara" Value="Sharara"></asp:ListItem>
                        <asp:ListItem Text="Mvuma" Value="Mvuma"></asp:ListItem>
                        <asp:ListItem Text="Chivhu" Value="Chivhu"></asp:ListItem>
                        <asp:ListItem Text="Ngomahuru" Value="Ngomahuru"></asp:ListItem>
                        <asp:ListItem Text="Checheche" Value="Checheche"></asp:ListItem>
                        <asp:ListItem Text="Glen View" Value="Glen View"></asp:ListItem>
                        <asp:ListItem Text="Chitungwiza" Value="Chitungwiza"></asp:ListItem>
                        <asp:ListItem Text="Mkoba" Value="Mkoba"></asp:ListItem>
                        <asp:ListItem Text="Mzilikazi" Value="Mzilikazi"></asp:ListItem>
                        <asp:ListItem class="text-danger">SouthAfrica</asp:ListItem>
                        <asp:ListItem Text="Nellmapius" Value="Nellmapius"></asp:ListItem>
                        <asp:ListItem Text="Soshanguve" Value="Soshanguve"></asp:ListItem>
                        <asp:ListItem Text="Olievienhout" Value="Olievienehout"></asp:ListItem>
                        <asp:ListItem Text="Tembisa" Value="Tembisa"></asp:ListItem>
                        <asp:ListItem Text="Kathlehong" Value="Kathlehong"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Sectn" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Section"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ChurchSection" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Height="29px" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem class="text-danger">Zimbabwe</asp:ListItem>
                        <asp:ListItem Text="Mucheke" Value="Mucheke"></asp:ListItem>
                        <asp:ListItem Text="Rujeko" Value="Rujeko"></asp:ListItem>
                        <asp:ListItem Text="Eastvale" Value="Eastvale"></asp:ListItem>
                        <asp:ListItem Text="Vengere" Value="Vengere"></asp:ListItem>
                        <asp:ListItem Text="Matibe" Value="Matibe"></asp:ListItem>
                        <asp:ListItem Text="Vhembe View" Value="Vhembe View"></asp:ListItem>
                        <asp:ListItem Text="Senga" Value="Senga"></asp:ListItem>
                        <asp:ListItem Text="Riverside" Value="Riverside"></asp:ListItem>
                        <asp:ListItem Text="Ascot" Value="Ascot"></asp:ListItem>
                        <asp:ListItem Text="Barbour Fields" Value="Barbour Fields"></asp:ListItem>
                        <asp:ListItem Text="Jahunda" Value="Jahunda"></asp:ListItem>
                        <asp:ListItem Text="Kuhle" Value="Kuhle"></asp:ListItem>
                        <asp:ListItem class="text-danger">SouthAfrica</asp:ListItem>                        
                        <asp:ListItem Text="Nellmapius" Value="Nellmapius"></asp:ListItem>
                        <asp:ListItem Text="Soshanguve" Value="Soshanguve"></asp:ListItem>
                        <asp:ListItem Text="Olievienhout" Value="Olievienehout"></asp:ListItem>
                        <asp:ListItem Text="Tembisa" Value="Tembisa"></asp:ListItem>
                        <asp:ListItem Text="Kathlehong" Value="Kathlehong"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <%-- SECOND PANEL --%>
            <div class="col-md-4">
                <asp:Panel ID="ChurchesTwo" runat="server" BorderStyle="None" Height="600px" ScrollBars="Auto" ToolTip="Church details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="Pst" runat="server" Text="List Of Pastors (Vafundisi)" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="ChurchPastors" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="100" placeholder="Pastor's List" TextMode="MultiLine" Width="225px"></asp:TextBox>
                    <br />
                    <asp:Label ID="LOV" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="List Of Evangelist (Vavhangeri)"></asp:Label>
                    <asp:TextBox ID="ChurchEvangelist" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="100" placeholder="Evangelist's List" TextMode="MultiLine" Width="225px"></asp:TextBox>
                    <br />
                    <asp:Label ID="LOP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="List Of Preachers (Vaparidzi)"></asp:Label>
                    <asp:TextBox ID="ChurchPreacher" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="100" placeholder="Preacher's List" TextMode="MultiLine" Width="225px"></asp:TextBox>
                    <br />
                </asp:Panel>
            </div>
            <%-- THIRD PANEL --%>
            <div class="col-md-4">
                <asp:Panel ID="ChurchesThree" runat="server" BorderStyle="None" Height="605px" ScrollBars="Auto" ToolTip="Church details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="LOPts" runat="server" Text="List Of Prophets" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="ChurchProphet" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" MaxLength="100" placeholder="Prophet's List.." TextMode="MultiLine" Width="225px"></asp:TextBox>
                    <br />
                    <asp:Label ID="LOD" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="List Of Decons (Vana Gosa)"></asp:Label>
                    <asp:TextBox ID="ChurchDecon" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="135px" TextMode="MultiLine" placeholder="Decon's List" Width="225px"></asp:TextBox>
                    <br />
                    <asp:Label ID="OM" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Other Members"></asp:Label>
                    <asp:TextBox ID="ChurchMember" runat="server" BorderColor="Maroon" CssClass="form-control text-capitalize" Height="139px" MaxLength="100" placeholder="Other Members.." TextMode="MultiLine" Width="225px"></asp:TextBox>
                    <br />
                    <%-- CRUD Controls --%>
                   
                    <hr />
        <asp:LinkButton ID="ChurchSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveChurch_Click"><strong>Save</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ChurchesView" CssClass="text-danger" ToolTip="View Data.." data-toggle="modal" data-target="#CHModal" runat="server"><strong>View</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ChurchesEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditChurch_Click"><strong>Edit</strong></asp:LinkButton>
        |
        <asp:LinkButton ID="ChurchesRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshChurch_Click"><strong>Refresh</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ChurchessDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteChurch_Click"><strong>Delete</strong></asp:LinkButton>
        |
         <asp:LinkButton ID="ChurchesReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>
                   
                </asp:Panel>
                

            </div>
        </div>
    </div>
     <br />
    <%-- SQLDatasource to Bind data to Modal Form Grid View --%>
    <asp:SqlDataSource ID="ZAFMCView" runat="server" ConnectionString="<%$ ConnectionStrings:ZionChurch %>" SelectCommand="SELECT [ChrchName] as [Name],[ChrchLeader] as [Leader],[ChrchLeaderCell] as [Leader Cell],[ChrchLeaderEmail] as [Leader Email],(REPLACE(convert(nvarchar,[ChrchDateOpened],106),'','/')) as [Date Opened],[ChrchNumMembers] as [Total Members],[ChrchProvince] as [Province],[ChrchDistrict] as [District],[ChrchZone] as [Zone] ,[ChrchSection] as [Section],[ChrchPastor] as [Pastor List],[ChrchEvangelist] as [Evangelist List],[ChrchPreachers] as [Preachers List],[ChrchProphets] as [Prophets List],[ChrchDecons] as [Decons List],[ChrchMembers] as [Members List] FROM [dbo].[Churches] ORDER BY [ChrchName] ASC"></asp:SqlDataSource>
     <br /> 
   
    <%--MODAL FORM--%>
    <%-- JACOB JACOB JACOB JACOB JACOB --%>

          <div class="modal modal-wide  fade " id="CHModal" tabindex="-1" role="dialog" aria-labelledby="ChurchModalTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-lg modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CHURCHTitle"><span class="text-danger"><strong>ZAFMC - Official Churches</strong></span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div>
                            <%--RESIZE OR APPLY SCROLL BARS--%>
                            <div style="overflow-y: scroll; height: auto; width: auto; word-spacing:normal; word-wrap:normal">
                                <%--RESIZE OR APPLY SCROLL BARS--%>
                                <asp:GridView ID="CHURCHESLIST" runat="server" AllowSorting="True" ToolTip="Church Lists..." CellPadding="4" ForeColor="#333333" GridLines="Both" DataSourceID="ZAFMCView" AllowPaging="True" ShowHeaderWhenEmpty="True">
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
                        
                        <asp:LinkButton ID="CloseChurchView" data-dismiss="modal" CssClass="text-danger" runat="server" ToolTip="Close Churches List View..." ><span class="glyphicon glyphicon-home "><strong> Close-View</strong></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    <!--End Modal Form-->
</asp:Content>

