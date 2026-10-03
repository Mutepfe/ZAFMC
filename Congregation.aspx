<%@ Page Title="Congregation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Congregation.aspx.cs" Inherits="ZAFMC.Congregation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <%-- JQUERY Script to disable some Dropdown Items --%>
    <script type="text/javascript">
        $(function ()
        {

            //Province DropDown
            $("#<%=Province.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            $("#<%=Province.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
             //District DropDown
            $("#<%=District.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            $("#<%=District.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            //ZONE DropDown
            $("#<%=Zones.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            $("#<%=Zones.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            //Section DropDown
            $("#<%=Sect.ClientID%>> option[value=Zimbabwe]").attr("disabled", "disabled")
            $("#<%=Sect.ClientID%>> option[value=SouthAfrica]").attr("disabled", "disabled")
            //ViceLeader DropDown
            $("#<%=ViceLeader.ClientID%>> option[value=Priests]").attr("disabled", "disabled")
            $("#<%=ViceLeader.ClientID%>> option[value=Vafundisi]").attr("disabled", "disabled")
            $("#<%=ViceLeader.ClientID%>> option[value=VaVhangeri]").attr("disabled", "disabled")
        });
    </script>
    <div class="container">
        <div class="row">
            <%-- First Panel --%>
            <div class="col-md-4">
                <asp:Panel ID="DetailsOne" runat="server" BorderStyle="None" Height="680px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    &nbsp;<asp:Label ID="MemNo" runat="server" CssClass="text-danger" Font-Bold="True" Style="font-size: small" Text="Membership Number"></asp:Label>
                    <br />
                    <asp:TextBox ID="MembershipNumber" runat="server" ReadOnly="true" placeholder="Auto-generated.." MaxLength="20" Width="150px" Height="29px" style="border:0.5px solid maroon" CssClass="form-control text-uppercase" ToolTip="Generated automatically when the member is saved" BorderColor="Maroon"></asp:TextBox>
                    &nbsp;<asp:Label ID="Tit" runat="server" CssClass="text-danger" Font-Bold="True" Style="font-size: small" Text="Title"></asp:Label>
                    <asp:DropDownList ID="Titles" runat="server" style="border:0.5px solid maroon" AutoPostBack="False" CssClass="form-control" Height="29px" ToolTip="Salutation!!" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                        <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                        <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                        <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                        <asp:ListItem Text="Prof." Value="Prof."></asp:ListItem>
                        <asp:ListItem Text="Dr." Value="Dr."></asp:ListItem>
                        <asp:ListItem Text="Eng." Value="Eng."></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label ID="FName" runat="server" Font-Bold="True" Style="font-size: small" Text="Firstname" CssClass="text-danger"></asp:Label>
                    <br />
                    <asp:TextBox ID="Firstname" placeholder="Firstname.." runat="server" MaxLength="15" Width="150px" Height="29px" CausesValidation="True" CssClass="form-control text-capitalize" ToolTip="Firstname!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="SName" runat="server" Text="Surname" Font-Bold="True" Style="font-size: small" CssClass="text-danger"></asp:Label>
                    <asp:TextBox ID="Surname" runat="server" placeholder="Surname.." MaxLength="20" Width="150px" Height="29px" CausesValidation="True" CssClass="form-control text-capitalize" ToolTip="Surname!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="DB" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Of Birth"></asp:Label>
                    <br />
                    <asp:TextBox ID="DOB" runat="server"  style="border:0.5px solid maroon" CssClass="form-control" Font-Bold="True" Font-Size="Small" Height="29px" MaxLength="30" TextMode="Date" ToolTip="Birthdate!!" Width="150px"></asp:TextBox>
                    <br />
                    <%--Gender Checkboxes with Script above--%>
                    <div id="GenderType">
                    <asp:RadioButtonList ID="Gender" runat="server"  CssClass="text-danger" Font-Bold="True" Font-Size="Small" RepeatDirection="Horizontal" ToolTip="Select Gender!!" Width="150px">
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                    </asp:RadioButtonList>
                        </div>
                    <asp:Label ID="PaID" runat="server" Font-Bold="True" Style="font-size: small" Text="Passport / ID" CssClass="text-danger"></asp:Label>
                    <br />
                    <asp:TextBox ID="PassportID" runat="server" placeholder="ID Number.." CssClass="form-control text-uppercase" Height="29px" Width="150px" MaxLength="15" ToolTip="Identity!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="MSt" runat="server" Font-Bold="True" Font-Size="Small" Text="Marital Status" CssClass="text-danger"></asp:Label>
                    <asp:DropDownList ID="MaritalStatus" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Status!!">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                        <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                        <asp:ListItem Text="Divorced" Value="Divorced"></asp:ListItem>
                        <asp:ListItem Text="Widow" Value="Widow"></asp:ListItem>
                        <asp:ListItem Text="Widower" Value="Widower"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Profs" runat="server" Font-Bold="True" Font-Size="Small" Text="Profession" CssClass="text-danger"></asp:Label>
                    <asp:TextBox ID="Profession" runat="server" placeholder="Profession.." CssClass="form-control text-capitalize" Height="29px" Width="150px" MaxLength="15" ToolTip="Job!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="NK" runat="server" Font-Bold="True" Font-Size="Small" Text="Next Of Kin" CssClass="text-danger"></asp:Label>
                    <br />
                    <asp:TextBox ID="NextOfKin" runat="server" placeholder="Kin.." CssClass="form-control text-capitalize" Height="29px" Width="150px" ToolTip="Relative!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="KCon" runat="server" Font-Bold="True" Font-Size="Small" Text="Kin Contact" CssClass="text-danger"></asp:Label>
                    <asp:TextBox ID="KinContact" runat="server" placeholder="Cell / Mail.." CssClass="form-control" Height="29px" MaxLength="15" Width="150px" ToolTip="Kin Contact!!" BorderColor="Maroon"></asp:TextBox>
                    <asp:Label ID="CNum" runat="server" Font-Bold="True" Font-Size="Small" Text="Cell Number" CssClass="text-danger"></asp:Label>
                    <asp:TextBox ID="CellNumber" runat="server" placeholder="Mobile.." CssClass="form-control" Height="29px" MaxLength="15" Width="150px" ToolTip="Mobile!!" BorderColor="Maroon" TextMode="Phone"></asp:TextBox>
                    <asp:Label ID="Addss" runat="server" Font-Bold="True" Font-Size="Small" Text="Physical Address" CssClass="text-danger"></asp:Label>
                    <asp:TextBox ID="PhysAddress" runat="server" placeholder="Address.." Columns="5" CssClass="form-control" Height="70px" MaxLength="50" Rows="4" TextMode="MultiLine" Width="150px" ToolTip="Location!!" BorderColor="Maroon"></asp:TextBox>
                    <br />
                </asp:Panel>
            </div>
            <%-- Second Panel --%>
            <div class="col-md-4">
                <asp:Panel ID="DetailsTwo" runat="server" BorderStyle="None" Height="680px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="Mail" runat="server" Text="Email Address" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:TextBox ID="EmailAdd" runat="server" BorderColor="Maroon" placeholder="Email.." CssClass="form-control text-uppercase" Height="29px" MaxLength="40" ToolTip="Contact Email!!" Width="150px" TextMode="Email"></asp:TextBox>
                    <asp:Label ID="RP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Rank / Position"></asp:Label>
                    <asp:DropDownList ID="RankPosition" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" ToolTip="Church Position!!" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Bishop" Value="Bishop"></asp:ListItem>
                        <asp:ListItem Text="Vice-Bishop" Value="Vice-Bishop"></asp:ListItem>
                        <asp:ListItem Text="Snr. High Priest" Value="Snr. High Priest"></asp:ListItem>
                        <asp:ListItem Text="High Priest" Value="High Priest"></asp:ListItem>
                        <asp:ListItem Text="Priest" Value="Priest"></asp:ListItem>
                        <asp:ListItem Text="Muungamiri" Value="Muungamiri"></asp:ListItem>
                        <asp:ListItem Text="Mufundisi(Pastor)" Value="Mufundisi(Pastor)"></asp:ListItem>
                        <asp:ListItem Text="Muvhangeri(Evangelist)" Value="Muvhangeri(Evangelist)"></asp:ListItem>
                        <asp:ListItem Text="Muparidzi(Preacher)" Value="Muparidzi(Preacher)"></asp:ListItem>
                        <asp:ListItem Text="Gosa(Decon)" Value="Gosa(Decon)"></asp:ListItem>
                        <asp:ListItem Text="VeMweya(Prophet)" Value="VeMweya(Prophet)"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="DApp" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Appointed"></asp:Label>
                    <br />
                    <asp:TextBox ID="DateAppointed" runat="server" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" MaxLength="30" TextMode="Date" ToolTip="Appointed Date!!" Width="150px">Appointment</asp:TextBox>
                    <asp:Label ID="MP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Managerial Post"></asp:Label>
                    <asp:DropDownList ID="ManagerialPost" AutoPostBack="False" runat="server" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" ToolTip="Management!!" Width="150px">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Secretary General" Value="Secretary General"></asp:ListItem>
                        <asp:ListItem Text="Projects Development" Value="Projects Development"></asp:ListItem>
                        <asp:ListItem Text="Music, Praise & Worship" Value="Music, Praise & Worship"></asp:ListItem>
                        <asp:ListItem Text="Social Service" Value="Social Service"></asp:ListItem>
                        <asp:ListItem Text="Security" Value="Security"></asp:ListItem>
                        <asp:ListItem Text="ICT" Value="ICT"></asp:ListItem>
                        <asp:ListItem Text="Education Skills" Value="Education Skills"></asp:ListItem>
                        <asp:ListItem Text="Health" Value="Health"></asp:ListItem>
                        <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                        <asp:ListItem Text="Vice-Chairman" Value="Vice-Chairman"></asp:ListItem>
                        <asp:ListItem Text="Treasurer" Value="Treasurer"></asp:ListItem>
                        <asp:ListItem Text="Secretary" Value="Secretary"></asp:ListItem>
                        <asp:ListItem Text="Vice-Secretary" Value="Vice-Secretary"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="DElec" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Date Elected"></asp:Label>
                    <br />
                    <asp:TextBox ID="DateElected" runat="server" CssClass="form-control" style="border:0.5px solid maroon" Font-Bold="False" Font-Size="Small" Height="29px" MaxLength="30" TextMode="Date" ToolTip="Elected!!" Width="150px">Elected</asp:TextBox>
                    <asp:Label ID="Prov" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Province"></asp:Label>
                    <asp:DropDownList ID="Province" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Font-Bold="False" Font-Size="Small" Height="29px" Width="150px" ToolTip="Province!!">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                         <asp:ListItem class="text-danger">Zimbabwe</asp:ListItem>
                        <asp:ListItem Text="Harare" Value="Harare"></asp:ListItem>
                        <asp:ListItem Text="Bulawayo" Value="Bulawayo"></asp:ListItem>
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
                        <asp:ListItem Text="Limpopo" Value="Limpopo"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Dist" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="District"></asp:Label>
                    <asp:DropDownList ID="District" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="District!!">
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
                    <asp:Label ID="Zn" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Zone"></asp:Label>
                    <br />
                    <asp:DropDownList ID="Zones" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Zone Area!!">
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
                    <asp:Label ID="St" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Section"></asp:Label>
                    <asp:DropDownList ID="Sect" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Section!!">
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
                    <asp:Label ID="SL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Snr. Leader"></asp:Label>
                    <asp:DropDownList ID="SeniorLeader" AutoPostBack="False" runat="server" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Top Leader!!">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Bishop Ezra" Value="Bishop Ezra"></asp:ListItem>
                        <asp:ListItem Text="J. Muzangwa" Value="J. Muzangwa"></asp:ListItem>
                        <asp:ListItem Text="S. Mawisire" Value="S. Mawisire"></asp:ListItem>
                        <asp:ListItem Text="T. Hove" Value="T. Hove"></asp:ListItem>
                        <asp:ListItem Text="P. Penengwa" Value="P. Penengwa"></asp:ListItem>
                        <asp:ListItem Text="J. Chidanga" Value="J. Chidanga"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="VL" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Vice-Leader"></asp:Label>
                    <asp:DropDownList ID="ViceLeader" runat="server" AutoPostBack="False" style="border:0.5px solid maroon" CssClass="form-control" Height="29px" Width="150px" ToolTip="Vice Leader!!">
                        <asp:ListItem Text="Select.." Value="-1"></asp:ListItem>
                        <asp:ListItem class="text-danger">Priests</asp:ListItem>
                        <asp:ListItem  Text="Mr Ziyambi" Value="Mr Ziyambi"></asp:ListItem>
                        <asp:ListItem  Text="Mr Hlongwane" Value="Mr Hlongwane"></asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                        <asp:ListItem class="text-danger">Vafundisi</asp:ListItem>
                        <asp:ListItem Text="Mr Chiworese" Value="Mr Chiworese"></asp:ListItem>
                        <asp:ListItem Text="Mr Mbizvo" Value="Mr Mbizvo"></asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                        <asp:ListItem class="text-danger">VaVhangeri</asp:ListItem>
                        <asp:ListItem Text="Mr Makondo" Value="Mr Makondo"></asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                        <asp:ListItem>------</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <%-- Third Panel --%>
            <div class="col-md-4">
                <asp:Panel ID="DetailsThree" runat="server" BorderStyle="None" Height="850px" ScrollBars="Auto" ToolTip="Capture details.." CssClass="form-control" ViewStateMode="Disabled" BorderColor="Maroon">
                    <asp:Label ID="Pct" runat="server" Text="Individual Photo" CssClass="text-danger" Font-Bold="True" Font-Size="Small"></asp:Label>
                     <br />
                    <asp:ImageMap ID="PersonPhoto" runat="server" style="border:0.5px solid maroon" Height="198px" Width="300px" >
                    </asp:ImageMap>
                    <br />
                  
                    <br />
                    <asp:FileUpload ID="PhotoUpload" runat="server"  style="border:0.5px solid maroon" ForeColor="Maroon" BorderStyle="Groove" CssClass="form-control" Font-Bold="True" Font-Size="Small" Height="30px" Width="300px" />
                    <br />

                    <asp:Label ID="IDP" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Upload ID / Passport"></asp:Label>
                    <br />
                    <asp:Image ID="PassID" runat="server" BorderColor="Maroon" style="border:0.5px solid maroon" Height="157px" Width="300px" />
                    <br />
                    <br />
                    <asp:FileUpload ID="IDPass" runat="server" style="border:0.5px solid maroon" CssClass="form-control" ForeColor="Maroon"  Font-Bold="True" Font-Size="Small" Height="30px" Width="350px" ToolTip="Upload Identity!!" />
                    <br />
                    <asp:Label ID="FNGT" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Fingerprint"></asp:Label>
                    <br /> <br />
                    <asp:Image ID="FingerPrint" runat="server" BorderColor="Maroon" style="border:0.5px solid maroon"  Height="70px" Width="150px"  />
                    <br />
                    <br />
                    <asp:Image ID="Barcode" runat="server" BorderColor="Maroon" style="border:0.5px solid maroon"  Height="70px" Width="150px" />
                    <br /> 
                    <asp:CheckBox ID="GenerateBarcode" runat="server" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Generate Barcode" ToolTip="Unique Data Barcode!!" />
                    <br /><br />
                    <%-- CRUD Controls --%>
                    <hr />
        
                <asp:LinkButton ID="CongregationSave" CssClass="text-danger" ToolTip="Save data.." runat="server" OnClick="SaveCongregation_Click"><strong>Save</strong></asp:LinkButton>
                |
                 <asp:LinkButton ID="CongregaView" CssClass="text-danger" ToolTip="View Data.." data-toggle="modal" data-target="#CongregationModal" runat="server"><strong>View</strong></asp:LinkButton>
                |
                 <asp:LinkButton ID="CongregationEdit" CssClass="text-danger" ToolTip="Edit data.." runat="server" OnClick="EditCongregation_Click"><strong>Edit</strong></asp:LinkButton>
                |
                <asp:LinkButton ID="CongregationRefresh" CssClass="text-danger" ToolTip="Refresh data.." runat="server" OnClick="RefreshCongregation_Click"><strong>Refresh</strong></asp:LinkButton>
                |
                 <asp:LinkButton ID="CongregationDel" CssClass="text-danger" ToolTip="Delete data.." runat="server" OnClick="DeleteCongregation_Click"><strong>Delete</strong></asp:LinkButton>
                |
                 <asp:LinkButton ID="CongregationsReset" CssClass="text-danger" ToolTip="Reset Fields.." runat="server" OnClick="ResetPage_Click"><strong>Reset</strong></asp:LinkButton>
                    <br />
                    <br />
                    <br />
                    
                </asp:Panel>
            </div>
        </div>
        
    </div>
 <br />
    <%-- SQLDatasource to Bind data to Modal Form Grid View --%>
    <asp:SqlDataSource ID="ZAFMCCong" runat="server" ConnectionString="<%$ ConnectionStrings:ZionCongregation %>" SelectCommand="SELECT [CongMembershipNumber] as [Membership No],[CongTitle] as [Title],[CongName] as [Name],[CongSurname] as [Surname],(REPLACE(convert(nvarchar,[CongDOB],106),'','/')) as [D.O.B],[CongGender] as [Gender],[CongPassportID] as [Identity],[CongStatus] as [Status],[CongProfession] as [Profession],[CongKin] as [Kin],[CongKinContact] as [Kin Contact],[CongCell] as [Mobile],[CongAddress] as [Address],[CongEmail] as [Email],[CongPosition] as [Position],(REPLACE(convert(nvarchar,[CongDateAppointed],106),'','/')) as [Date Appointed],[CongManagerial] as [Managerial Post],(REPLACE(convert(nvarchar,[CongDateElected],106),'','/')) as [Date Elected],[CongProvince] as [Province],[CongDistrict] as [District],[CongZone] as [Zone],[CongSection] as [Section],[CongSnrLeader] as [Snr. Leader],[CongViceLeader] as [Vice-Leader],[CongPhoto] as [Photo],[CongPassID] as [Identity-Photo],[CongFingerprint] as [Fingerprint],[CongBarcode] as [Encrypted-Data] FROM [dbo].[Congregation] WHERE [CongMembershipNumber] LIKE @FIND OR [CongPassportID] LIKE @FIND ORDER BY [CongPassportID] ASC" OnSelecting="ZAFMCCong_Selecting">
        <SelectParameters>
            <asp:Parameter Name="FIND" Type="String" DefaultValue="%" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
     <br /> 
   
    <%--MODAL FORM(Congregation)--%>
    <%-- JACOB JACOB JACOB JACOB JACOB --%>

          <div class="modal modal-wide  fade " id="CongregationModal" tabindex="-1" role="dialog" aria-labelledby="CongModalTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-lg modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="CongTitle"><span class="text-danger"><strong>ZAFMC - Congregants List</strong></span></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div>
                            <%-- Search by Membership Number or Passport / ID --%>
                            <asp:Panel ID="SearchPanel" runat="server" DefaultButton="CongregationSearch" CssClass="form-inline">
                                <asp:Label ID="SearchLbl" runat="server" AssociatedControlID="SearchCongregant" CssClass="text-danger" Font-Bold="True" Font-Size="Small" Text="Search (Membership No. / ID)"></asp:Label>
                                <asp:TextBox ID="SearchCongregant" runat="server" MaxLength="20" placeholder="ZAFMC.. or ID.." CssClass="form-control text-uppercase" style="border:0.5px solid maroon" Width="180px" Height="29px" ToolTip="Search by Membership Number or Passport / ID!!"></asp:TextBox>
                                <asp:LinkButton ID="CongregationSearch" CssClass="text-danger" ToolTip="Search Congregants.." runat="server" OnClick="ViewCongregation_Click"><strong>Search</strong></asp:LinkButton>
                            </asp:Panel>
                            <br />
                            <%--RESIZE OR APPLY SCROLL BARS TO GRIDVIEW--%>
                            <div style="overflow-y: scroll; height: auto; width: auto; word-spacing:normal; word-wrap:normal">
                                <%--(Above)RESIZE OR APPLY SCROLL BARS TO GRIDVIEW--%>
                                <asp:GridView ID="CongregantsList" runat="server" AllowSorting="True" ToolTip="Congregation List..." CellPadding="4" ForeColor="#333333" GridLines="Both" DataSourceID="ZAFMCCong" AllowPaging="True" ShowHeaderWhenEmpty="True" OnPageIndexChanged="CongregantsList_Changed" OnSorted="CongregantsList_Changed" OnRowCommand="CongregantsList_RowCommand">
                                    <%-- Insert Row Number --%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Maroon" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Maroon">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- Loads the member into the form for editing --%>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Maroon" HeaderStyle-BackColor="White" HeaderStyle-ForeColor="Maroon">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LoadMember" runat="server" CommandName="LoadMember" CommandArgument='<%# Eval("Identity") %>' CssClass="text-danger" ToolTip="Load this member into the form for editing.."><span class="glyphicon glyphicon-edit" aria-hidden="true"></span> <strong>Select</strong></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
                        <asp:LinkButton ID="CloseCongregation" data-dismiss="modal" CssClass="text-danger" runat="server" ToolTip="Close Congregation List..." ><span class="glyphicon glyphicon-home "><strong> Close-Congregation</strong></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    <!--End Modal Form-->

<%-- VALIDATORS --%>
    <%--<asp:RegularExpressionValidator ID="LoadPhoto" 
        runat="server"
        ErrorMessage="Uploads (JPEG / GIF ) Only !!"
        ValidationExpression="^(([a-zA-Z:)|((\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|GIF)$"
       ControlToValidate="PhotoUpload">
     </asp:RegularExpressionValidator>--%>


</asp:Content>

