<%@ Page Title="" Language="C#"
    MasterPageFile="~/NestedSiteMaster.Master"
    EnableEventValidation="false"
    EnableViewState="false"
    AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs"
    Inherits="WebFormsApp._Profile" %>


<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/bootstrap_settings_page.css" rel="stylesheet" />
    <link href="Content/settings_profile.css" rel="stylesheet" />
    <link href="Content/profile.css" rel="stylesheet" />
</asp:Content>



<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">

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
                    <tr>
                        <td class="profiles">Gender: </td>
                        <td>
                            <asp:DropDownList
                                ID="ddlGender"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetGender"
                                DataTextField="Text"
                                DataValueField="Value" /></td>
                    </tr>
                    <tr>
                        <td class="profiles">Marital status: </td>
                        <td>
                            <asp:DropDownList
                                ID="ddlMarried"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetMarried"
                                DataTextField="Text"
                                DataValueField="Value" /></td>
                    </tr>
                    <tr>
                        <td class="profiles">Birthday: </td>
                        <td>
                            <asp:DropDownList
                                ID="ddlBirthDay"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetBirthDay"
                                DataTextField="Text"
                                DataValueField="Value"
                                onmouseover="getProfileBirthDays()"
                                onchange="updateUserBirthDay()" />

                            <asp:DropDownList
                                ID="ddlBirthMonth"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetBirthMonth"
                                DataTextField="Text"
                                DataValueField="Value"
                                onmouseover="getProfileBirthMonths()"
                                onchange="updateUserBirthMonth()" />

                            <asp:DropDownList
                                ID="ddlBirthYear"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetBirthYear"
                                DataTextField="Text"
                                DataValueField="Value"
                                onmouseover="getProfileBirthYears()"
                                onchange="updateUserBirthYear()" />

                        </td>
                    </tr>
                </table>


                <asp:UpdatePanel
                    runat="server"
                    ID="UpdatePanel1"
                    UpdateMode="Conditional">

                    <ContentTemplate>
                         <asp:ScriptManagerProxy ID="ScriptManagerProxy0" runat="server"></asp:ScriptManagerProxy>

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
                                <table>

                                    <br />
                                    <hr />
                                    <br />
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
                                                ItemType="Core.POCO.SelectListItem"
                                                SelectMethod="GetLanguages"
                                                SelectedValue='<%# Item.Language %>'
                                                DataTextField="Text"
                                                DataValueField="Value" />
                                        </td>
                                    </tr>


                                </table>

                                <br />
                                <hr />
                                <br />

                                <asp:Button
                                    ID="btnSave1"
                                    runat="server"
                                    Style="margin-left: 205px; width: 101px;"
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
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

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
                                            <asp:TextBox ID="txtScype" runat="server" Text='<%# BindItem.Skype %>' />
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
                                    ID="btnSave2"
                                    runat="server"
                                    Style="margin-left: 205px; width: 101px;"
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
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>

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
                                <table>
                                    <tr>
                                        <td class="profiles">Деятельность:
                                            <asp:HiddenField ID="hdnID" ClientIDMode="Static" runat="server" Value='<%# BindItem.ProfileID %>' />
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
                                <asp:Button ID="btnSave3"
                                    runat="server"
                                    Style="margin-left: 205px; width: 101px;"
                                    CommandName="Update"
                                    Text="Сохранить" />

                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div id="education" class="tab-pane fade tableSize">
              
                  <table>
                    <tr>
                        <td class="profiles">Страна:</td>
                        <td>
                            <asp:DropDownList
                                ID="ddlSchoolCountry"
                                ClientIDMode="Static"
                                runat="server"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetSchoolCountry"
                                CssClass="dropdownlist"
                                DataTextField="Text"
                                DataValueField="Value"
                                onchange="ddlCountryQueryForTowns()"
                                onmouseover="getCountries()"
                                onclick="ddlCountryQueryForTowns()" />
                        </td>
                    </tr>
                    <tr>
                        <td class="profiles">Город:</td>
                        <td>
                            <asp:DropDownList
                                ID="ddlSchoolTown"
                                ClientIDMode="Static"
                                runat="server"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetSchoolTown"
                                CssClass="dropdownlist"
                                DataTextField="Text"
                                DataValueField="Value" />
                        </td>
                    </tr>

                    <tr>
                        <td class="profiles">Школа:</td>
                        <td>
                            <asp:TextBox
                                ID="txtSchool"
                                ClientIDMode="Static"
                                runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="profiles">Год начала обучения:</td>
                        <td>
                            <asp:DropDownList
                                ID="ddlSchoolBeginningYear"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetSchoolStartYear"
                                DataTextField="Text"
                                DataValueField="Value"
                                onmouseover="getProfileSchoolStartYears()" />
                        </td>
                    </tr>
                    <tr>
                        <td class="profiles">Год окончания обучения:</td>
                        <td>
                            <asp:DropDownList
                                ID="ddlSchoolGraduationYear"
                                runat="server"
                                ClientIDMode="Static"
                                ItemType="Core.POCO.SelectListItem"
                                SelectMethod="GetSchoolFinishYear"
                                DataTextField="Text"
                                DataValueField="Value"
                                onmouseover="getProfileSchoolFinishYears()" />
                        </td>
                    </tr>
                </table>

                <br />
                <hr />
                <br />

                <asp:Button
                    ID="btnSave"
                    runat="server"
                    OnClientClick="updateEducationData();return false;"
                    Style="margin-left: 205px; width: 101px;"
                    Text="Сохранить" />

            </div>
        </div>
    </div>
</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/profile.js"></script>
</asp:Content>
