<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/NestedSiteMaster.master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="WebFormsApp.People" %>

<%@ MasterType VirtualPath="~/NestedSiteMaster.master" %>

<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="Content/people_friends.css" rel="stylesheet" />
    <link href="Content/friends.css" rel="stylesheet" />
</asp:Content>


<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">

    <asp:Panel ID="searchForm" data-userid="" ClientIDMode="Static" runat="server">
        <ul>
            <li>Страна :
            </li>
            <li>

                <select id="ddlCountry" name="D1" class="auto-style1">
                </select>

                <br />
                <div id="Towns" runat="server">
                    Город :<br />

                    <select id="ddlTown" name="D1" class="auto-style2">
                        <option value="0"></option>
                    </select>
                </div>

            </li>
            <li>Возраст :
            </li>
            <li>
                <select id="ddlFrom" name="D1" class="auto-style3">
                </select>
                - 
                    <select id="ddlTo" name="D1" class="auto-style4">
                    </select>

            </li>
            <li>Пол :
            </li>


            <li style="margin-left: 0px">
                <input name="gender" type="radio" value="Any" checked="checked" />All<br />
                <input name="gender" type="radio" value="Man" />Man<br />
                <input name="gender" type="radio" value="Woman" />Woman<br />
            </li>

        </ul>
    </asp:Panel>

    <div id="friendsList">
        <table>
            <tbody id="users">
            </tbody>
        </table>
    </div>



    <link href="Content/main_popup_window.css" rel="stylesheet" />
    <input class="modal-state" id="modal-1" type="checkbox" />
    <div class="modal">
        <label class="modal__bg" for="modal-1"></label>
        <div class="modal__inner">
            <label class="modal__close" for="modal-1"></label>
            <div>
                <span>Message</span>
                <textarea id="messageBody"></textarea>
                <input type="button" id="sendBtn" onclick="createNewDialog()" value="Send" />
            </div>
        </div>
    </div>

</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="/Scripts/jquery-1.7.1.min.js"></script>
    <script src="/Scripts/modalPopLite.min.js"></script>
    <script src="/Scripts/shared.js"></script>
    <script src="/Scripts/people.js"></script>

    <script type="text/javascript" src="https://code.jquery.com/jquery-latest.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/sweetalert.min.js"></script>
</asp:Content>
