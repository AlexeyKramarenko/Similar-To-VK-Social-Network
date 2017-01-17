<%@ Page EnableViewState="false" EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebFormsApp.Profile" %>

<%@ Register Src="~/CustomControls/Chat.ascx" TagPrefix="uc1" TagName="Chat" %>



<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/bootstrap_settings_page.css" rel="stylesheet" />
    <link href="Content/settings_profile.css" rel="stylesheet" />
    <link href="Content/profile.css" rel="stylesheet" />
    <style>
        .btnSave {
            margin-left: 190px;
        }
    </style>
</asp:Content>


<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container" style="">

        <ul class="nav nav-tabs">
            <li><a data-toggle="tab" id="main_" href="#main">Common data</a></li>
            <li><a data-toggle="tab" id="contacts_" href="#contacts">Contacts</a></li>
            <li><a data-toggle="tab" id="hobby_" href="#hobby">Interests</a></li>
            <li><a data-toggle="tab" id="education_" href="#education">Education</a></li>
        </ul>

        <div class="tab-content unvisible">
            <div id="main" class="tab-pane fade in active tableSize">

                <table>
                </table>


                <asp:UpdatePanel
                    runat="server"
                    ID="UpdatePanel1"
                    UpdateMode="Conditional">

                    <ContentTemplate>
                       
                        <asp:FormView
                            DefaultMode="Edit"
                            ID="FormView1"
                            DataKeyNames="ProfileID"
                            ItemType="WebFormsApp.ViewModel.MainViewModel"
                            SelectMethod="GetMainInfo"
                            UpdateMethod="SaveMainInfo"
                            runat="server"
                            RenderOuterTable="false">

                            <EditItemTemplate>
                                <asp:HiddenField ID="HiddenField1" Value='<%# BindItem.ProfileID %>' runat="server" />
                                <table>
                                    <tr>
                                        <td class="profiles">Gender: </td>
                                        <td>
                                            <asp:DropDownList
                                                ID="ddlGender"
                                                runat="server"
                                                ClientIDMode="Static"
                                                DataSource='<%# Item.GenderList %>'
                                                SelectedValue='<%# BindItem.Gender %>' /></td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Marital status: </td>
                                        <td>
                                            <asp:DropDownList
                                                ID="ddlMaritalStatus"
                                                runat="server"
                                                ClientIDMode="Static"
                                                DataSource='<%# Item.MaritalStatuses %>'
                                                SelectedValue='<%# BindItem.MaritalStatus %>' /></td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Birthday: </td>
                                        <td>
                                            <asp:DropDownList
                                                ID="ddlBirthDay"
                                                runat="server"
                                                ClientIDMode="Static"
                                                DataSource='<%# Item.BirthDays %>'
                                                SelectedValue='<%# BindItem.BirthDay %>' />

                                            <asp:DropDownList
                                                ID="ddlBirthMonth"
                                                runat="server"
                                                ClientIDMode="Static"
                                                DataSource='<%# Item.BirthMonths %>'
                                                SelectedValue='<%# BindItem.BirthMonth %>' />

                                            <asp:DropDownList
                                                ID="ddlBirthYear"
                                                runat="server"
                                                ClientIDMode="Static"
                                                DataSource='<%# Item.BirthYears %>'
                                                SelectedValue='<%# BindItem.BirthYear %>' />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">First name:<asp:HiddenField ID="hdnID" ClientIDMode="Static" runat="server" Value='<%# BindItem.ProfileID %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFirstName" Text='<%# BindItem.FirstName %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Last name:</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtLastName" Text='<%# BindItem.LastName %>' />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Home town: </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtHomeTown" Text='<%# BindItem.HomeTown %>' />
                                    </tr>
                                    <tr>
                                        <td class="profiles">Native language:</td>
                                        <td>
                                            <asp:DropDownList
                                                ID="ddlLanguage"
                                                runat="server"
                                                DataSource='<%# Item.Languages %>'
                                                SelectedValue='<%# BindItem.Language %>' />
                                        </td>
                                    </tr>


                                </table>

                                <br />
                                <hr />
                                <br />

                                <asp:Button
                                    CssClass="btnSave"
                                    runat="server"
                                    CommandName="Update"
                                    Text="Сохранить" />

                            </EditItemTemplate>
                        </asp:FormView>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <div id="contacts" class="tab-pane fade tableSize">

                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                       
                        <asp:FormView
                            DefaultMode="Edit"
                            ID="formViewContacts"
                            DataKeyNames="ProfileID"
                            ItemType="WebFormsApp.ViewModel.ContactsViewModel"
                            SelectMethod="GetContacts"
                            UpdateMethod="SaveContacts"
                            runat="server"
                            RenderOuterTable="false">

                            <EditItemTemplate>
                                <asp:HiddenField ID="HiddenField1" Value='<%# BindItem.ProfileID %>' runat="server" />
                                <table>
                                    <tr>
                                        <td class="profiles">Мобильный телефон:</td>
                                        <td>
                                            <asp:ModelErrorMessage
                                                CssClass="modelErrorMessage"
                                                ClientIDMode="Static"
                                                ID="PhoneNumberErrorMessage"
                                                ModelStateKey="PhoneNumber"
                                                ForeColor="Blue"
                                                runat="server" />

                                            <asp:TextBox ID="txtPhoneNumber" runat="server" Text='<%# BindItem.PhoneNumber %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Skype:</td>
                                        <td>
                                            <asp:TextBox ID="txtSkype" runat="server" Text='<%# BindItem.Skype %>' />
                                    </tr>
                                    <tr>
                                        <td class="profiles">Личный сайт:</td>
                                        <td>
                                            <asp:TextBox ID="txtWebSite" runat="server" Text='<%# BindItem.WebSite %>' />
                                    </tr>
                                </table>
                                <br />
                                <hr />
                                <br />
                                <asp:Button
                                    runat="server"
                                    CssClass="btnSave"
                                    CommandName="Update"
                                    Text="Сохранить" />

                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
            <div id="hobby" class="tab-pane fade tableSize">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                       
                        <asp:FormView
                            DefaultMode="Edit"
                            ID="formViewHobby"
                            DataKeyNames="ProfileID"
                            ItemType="WebFormsApp.ViewModel.InterestsViewModel"
                            SelectMethod="GetInterests"
                            UpdateMethod="SaveInterests"
                            runat="server"
                            RenderOuterTable="false">

                            <EditItemTemplate>
                                <asp:HiddenField ID="HiddenField1" Value='<%# BindItem.ProfileID %>' runat="server" />
                                <table>
                                    <tr>
                                        <td class="profiles">Деятельность:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtActivity" runat="server" Text='<%# BindItem.Activity %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Интересы:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInterests" runat="server" Text='<%# BindItem.Interests %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Любимая музыка:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMusic" runat="server" Text='<%# BindItem.Music %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Любимые фильмы:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFilms" runat="server" Text='<%# BindItem.Films %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Любимые книги:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBooks" runat="server" Text='<%# BindItem.Books %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Любимые игры:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGames" runat="server" Text='<%# BindItem.Games %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Любимые цитаты:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuotes" runat="server" Text='<%# BindItem.Quotes %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">О себе:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAboutMe" runat="server" Text='<%# BindItem.AboutMe %>' />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <hr />
                                <br />
                                <asp:Button
                                    runat="server"
                                    CssClass="btnSave"
                                    CommandName="Update"
                                    Text="Сохранить" />

                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>



            <div id="education" class="tab-pane fade tableSize">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <asp:FormView
                            ID="EducationInfoFormView"
                            DefaultMode="Edit"
                            RenderOuterTable="false"
                            SelectMethod="GetEducationInfo"
                            UpdateMethod="UpdateEducationInfo"
                            ItemType="WebFormsApp.ViewModel.EducationViewModel"
                            runat="server">

                            <EditItemTemplate>

                                <asp:HiddenField ID="HiddenField1" Value='<%# BindItem.ProfileID %>' runat="server" />
                                <table>
                                    <tr>
                                        <td class="profiles">Страна:</td>
                                        <td>
                                            <asp:DropDownList
                                                ID="SchoolCountry"
                                                DataSource='<%# Item.SchoolCountries %>'
                                                SelectedValue='<%# BindItem.SchoolCountry %>'
                                                ClientIDMode="Static"
                                                onchange="vm.getProfileSchoolTown()"
                                                runat="server">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Город:</td>
                                        <td>
                                            <select id="SchoolTown" name="SchoolTown">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Школа:</td>
                                        <td>
                                            <asp:TextBox
                                                ID="School"
                                                runat="server"
                                                ClientIDMode="Static"
                                                Text='<%# BindItem.School %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Год начала обучения:</td>
                                        <td>
                                            <asp:DropDownList
                                                ID="StartSchoolYear"
                                                DataSource='<%# Item.StartYears %>'
                                                SelectedValue='<%# BindItem.StartSchoolYear %>'
                                                ClientIDMode="Static"
                                                onchange="vm.updateFinishYears($('#FinishSchoolYear').val())"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="profiles">Год окончания обучения:</td>
                                        <td>
                                            <select id="FinishSchoolYear" name="FinishSchoolYear">
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <hr />
                                <br />

                                <asp:Button
                                    runat="server"
                                    CssClass="btnSave"
                                    CommandName="Update"
                                    Text="Save education info" />

                            </EditItemTemplate>
                        </asp:FormView>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <%: Scripts.Render("~/bundles/profile") %>
    <script>
        var vm = new ProfileController(new ProfileView(), new ProfileService());            
    </script>
    <uc1:Chat runat="server" ID="Chat" />
</asp:Content>
