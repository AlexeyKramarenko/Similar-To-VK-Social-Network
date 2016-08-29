<%@ Page Title="" Language="C#" MasterPageFile="~/NestedSiteMaster.master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="WebFormsApp.Messages" %>


<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/bootstrap_settings_page.css" rel="stylesheet" />
    <link href="Content/settings_profile.css" rel="stylesheet" />
    <link href="Content/messages.css" rel="stylesheet" />
</asp:Content>



<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server"> 

    <div class="container">

        <ul class="nav nav-tabs">
            <li class="active" id="li_dialogs">
                <a data-toggle="tab" href="#dialogs">Dialogs</a>

            </li>
            <li id="li_lookDialog">
                <a data-toggle="tab" href="#menu1">Dialog viewer</a>
            </li>
        </ul>

        <div class="tab-content">

            <div id="dialogs" class="tab-pane fade in active" style="font-size: 14px; margin-top: 60px; margin-bottom: 40px; margin-left: auto; margin-right: auto;">

                <asp:ListView
                    ID="ListView1"
                    runat="server"
                    ItemType="WebFormsApp.ViewModel.MessagesViewModel"
                    SelectMethod="GetDialogsList">
                    <LayoutTemplate>
                        <asp:HiddenField ID="hdnActiveDialogID" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnMyCurrentInterlocutor" ClientIDMode="Static" runat="server" />
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>

                        <a
                            data-toggle="tab"
                            data-dialogid='<%# Item.DialogID %>'
                            data-interlocutoruserid='<%# Item.InterlocutorUserID %>'
                            href="#menu1"
                            onclick="goToDialog(this); getUsersInfoForDialogForm(this);"
                            class="linkToDialog">

                            <table
                                class="dialogs_row_t">

                                <tbody>
                                    <tr>
                                        <td class="dialogs_photo">
                                            <img src='<%# Item.InterlocutorAvatar %>' runat="server" width="50" height="50">
                                        </td>
                                        <td class="dialogs_info">
                                            <div class="dialogs_user wrapped" data-interlocutoruserid='<%# Item.InterlocutorUserID %>'><%# Item.InterlocutorUserName %></div>

                                            <div class="dialogs_date"><%# Item.CreateDate %></div>
                                        </td>
                                        <td class="dialogs_msg_contents">
                                            <div class="dialogs_msg_body  clear_fix" style="opacity: 1;">
                                                <img src='<%# Item.CurrentUserAvatar %>' runat="server" width="32" height="32">
                                                <div class="dialogs_comment"><%# Item.MessageText %></div>
                                            </div>
                                        </td>
                                        <td class="dialogs_unread_td">
                                            <div class="dialogs_unread unshown"></div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </a>
                    </ItemTemplate>
                </asp:ListView>
            </div>



            <div id="menu1" class="tab-pane fade dialogs" style="">
                <div class="myBox">
                    <table>
                        <tbody id="conversation">
                        </tbody>
                    </table>
                </div>

                <table id="write_form">
                </table>

            </div>
        </div>
    </div>

</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/sweetalert.min.js"></script>
    <script src="Scripts/signalRmessagesPage.js"></script>
    <script src="Scripts/messages.js"></script>
</asp:Content>
