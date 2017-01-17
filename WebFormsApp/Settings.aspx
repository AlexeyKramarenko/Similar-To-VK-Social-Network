<%@ Page EnableViewState="false" Title="" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WebFormsApp.Settings" %>



<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/CustomControls/Chat.ascx" TagPrefix="uc1" TagName="Chat" %>




<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/bootstrap_settings_page.css" rel="stylesheet" />
    <link href="Content/settings_profile.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="innerContent" ContentPlaceHolderID="NestedPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">Общее</a></li>
            <li><a data-toggle="tab" href="#menu1">Приватность</a></li>
        </ul>

        <div class="tab-content">
            <div id="home" class="tab-pane fade in active" style="font-size: 14px; width: 500px; margin-top: 60px; margin-bottom: 40px; margin-left: auto; margin-right: auto;">

                <b>Изменить пароль</b>
                <hr />
                <br />

                <asp:UpdatePanel
                    ID="UpdatePanel3"
                    runat="server"
                    UpdateMode="Conditional">

                    <ContentTemplate>
                       
                        <asp:FormView
                            DefaultMode="Insert"
                            ID="FormView1"
                            ItemType="WebFormsApp.ViewModel.PasswordViewModel"
                            InsertMethod="UpdatePassword"
                            runat="server">

                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td class="settings_privacy_row1">Старый пароль:</td>
                                        <td>
                                            <asp:TextBox ID="txtOldPassword"
                                                TextMode="Password"
                                                runat="server"
                                                Text='<%# BindItem.OldPassword %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="settings_privacy_row1">Новый пароль:</td>
                                        <td>
                                            <asp:TextBox
                                                ID="txtNewPassword"
                                                TextMode="Password"
                                                runat="server"
                                                Text='<%# BindItem.NewPassword %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="settings_privacy_row1">Повторите пароль: </td>
                                        <td>
                                            <asp:TextBox
                                                ID="txtRepeatNewPassword"
                                                TextMode="Password"
                                                runat="server"
                                                Text='<%# BindItem.NewPasswordConfirm %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="settings_privacy_row1"></td>
                                        <td>
                                            <asp:Button
                                                ID="btnChangePsw"
                                                runat="server"
                                                CommandName="Insert"
                                                Width="124px"
                                                Text="Изменить пароль" />
                                    </tr>
                                </table>

                                <asp:ValidationSummary
                                    ID="valSummaryPassword"
                                    runat="server"
                                    ClientIDMode="Static"
                                    ShowModelStateErrors="true"
                                    DisplayMode="BulletList"
                                    EnableClientScript="true"
                                    ForeColor="Red" />

                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />


                <b>Адрес Вашей электронной почты</b>
                <hr />
                <br />
                <asp:UpdatePanel
                    ID="UpdatePanel2"
                    runat="server"
                    UpdateMode="Conditional">

                    <ContentTemplate>
                        
                        <asp:FormView
                            DefaultMode="Edit"
                            ID="FormView2"
                            ItemType="WebFormsApp.ViewModel.EmailViewModel"
                            SelectMethod="SelectEmail"
                            UpdateMethod="UpdateEmail"
                            runat="server">

                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td class="settings_privacy_row1">Электронный адрес:</td>
                                        <td>
                                            <asp:Label
                                                ID="lblTelNum"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text='<%# BindItem.OldEmail %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="settings_privacy_row1"></td>
                                        <td>
                                            <asp:TextBox
                                                ID="txtEmail"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text='<%# BindItem.NewEmail %>'
                                                AutoPostBack="false" />

                                            <asp:Button
                                                ID="btnChangeEmail"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text="Изменить электронный адрес"
                                                CommandName="Update" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ValidationSummary
                                    ID="valSummaryEmail"
                                    runat="server"
                                    ClientIDMode="Static"
                                    ShowModelStateErrors="true"
                                    DisplayMode="BulletList"
                                    EnableClientScript="true"
                                    ForeColor="Red" />
                            </EditItemTemplate>

                        </asp:FormView>

                    </ContentTemplate>

                </asp:UpdatePanel>



                <br />
                <b>Номер Вашего телефона</b>
                <hr />
                <br />

                <asp:UpdatePanel
                    ID="UpdatePanel1"
                    runat="server"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                       
                        <asp:FormView
                            DefaultMode="Edit"
                            ID="FormView3"
                            ItemType="WebFormsApp.ViewModel.PhoneNumberViewModel"
                            SelectMethod="SelectPhoneNumber"
                            UpdateMethod="UpdatePhoneNumber"
                            runat="server">

                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td class="settings_privacy_row1">Текущий номер:</td>
                                        <td>
                                            <asp:Label
                                                ID="lblTelNum"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text='<%# BindItem.PhoneNumber %>' />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="settings_privacy_row1"></td>
                                        <td>
                                            <asp:TextBox
                                                ID="txtInputTelNumber"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text='<%# BindItem.NewPhoneNumber %>'
                                                AutoPostBack="false" />

                                            <asp:Button
                                                ID="btnChangeTelNumber"
                                                ClientIDMode="Static"
                                                runat="server"
                                                Text="Изменить номер телефона"
                                                CommandName="Update" />
                                        </td>
                                    </tr>
                                </table>

                                <asp:ValidationSummary
                                    ID="valSummaryPhoneNumber"
                                    runat="server"
                                    ClientIDMode="Static"
                                    ShowModelStateErrors="true"
                                    DisplayMode="BulletList"
                                    EnableClientScript="true"
                                    ForeColor="Red" />
                            </EditItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </div>
            <div id="menu1" class="tab-pane fade" style="font-size: 14px; width: 500px; margin-top: 60px; margin-bottom: 40px; margin-left: auto; margin-right: auto;">

                <b>Моя страница</b>
                <hr />
                <br />
                <table>
                    <tr>
                        <td class="settings_privacy_row2">Кто видит основную информацию моей страницы</td>
                        <td>
                            <a class="privacy" id="privacy_1"></a>
                        </td>
                    </tr>
                </table>
                <br />


                <b>Записи на странице</b>
                <hr />
                <br />

                <asp:HiddenField ID="hdnProfileID" ClientIDMode="Static" Value="" runat="server" />


                <table>
                    <tr>
                        <td class="settings_privacy_row2">Кто видит записи на моей странице</td>
                        <td>
                            <a class="privacy" id="privacy_2"></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="settings_privacy_row2">Кто может оставлять записи на моей странице</td>
                        <td>
                            <a class="privacy" id="privacy_3"></a></td>
                    </tr>
                    <tr>
                        <td class="settings_privacy_row2">Кто видит комментарии к записям</td>
                        <td>
                            <a class="privacy" id="privacy_4"></a></td>
                    </tr>
                    <tr>
                        <td class="settings_privacy_row2">Кто может комментировать мои записи</td>
                        <td>
                            <a class="privacy" id="privacy_5"></a>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
</asp:Content>






<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <%: Scripts.Render("~/bundles/settings") %>
    <script>
        var vm = new SettingsController(new SettingsView(), new SettingsService());

        $('.privacy').click(function () {
            vm.getPrivacy(this);
        });        
    </script>
    <uc1:Chat runat="server" ID="Chat" />
</asp:Content>
