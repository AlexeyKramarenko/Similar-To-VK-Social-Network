<%@ Page EnableViewState="false" Title="" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="WebFormsApp.People" %>

<%@ MasterType VirtualPath="~/NestedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/CustomControls/Chat.ascx" TagPrefix="uc1" TagName="Chat" %>



<asp:Content ContentPlaceHolderID="Styles" runat="server">
    <link href="/Content/people_friends.css" rel="stylesheet" />
    <link href="/Content/friends.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="NestedPlaceHolder" runat="server">

    <asp:Panel ID="searchForm" data-userid="" ClientIDMode="Static" runat="server">
        <ul>
            <li>Страна :
            </li>
            <li>

                <select
                    id="ddlCountry"
                    name="D1"
                    class="auto-style1"
                    onchange="vm.setTownsToDefault();
                              vm.getTownsBySelectedCountry();
                              vm.getUsers()">
                </select>

                <br />
                <div id="Towns" runat="server">
                    Город :<br />

                    <select
                        id="ddlTown"
                        name="D1"
                        class="auto-style2"
                        onchange="vm.getUsers()">
                        <option value="0"></option>
                    </select>
                </div>

            </li>
            <li>Возраст :
            </li>
            <li>
                <select
                    id="ddlFrom"
                    name="D1"
                    class="auto-style3"
                    onchange="vm.updateAgeValue();
                              vm.getUsers()">
                </select>
                - 
                    <select
                        id="ddlTo"
                        name="D1"
                        class="auto-style4"
                        onchange="vm.getUsers()">
                    </select>

            </li>
            <li>Пол :
            </li>


            <li id="gender">
                <input onchange="vm.getUsers()" name="gender" type="radio" value="Any" checked="checked" />All<br />
                <input onchange="vm.getUsers()" name="gender" type="radio" value="Male" />Male<br />
                <input onchange="vm.getUsers()" name="gender" type="radio" value="Female" />Female<br />
            </li>

        </ul>
    </asp:Panel>

    <div id="friendsList">
        <table>
            <tbody id="users">
            </tbody>
        </table>
    </div>



    <link href="/Content/main_popup_window.css" rel="stylesheet" />
    <input class="modal-state" id="modal-1" type="checkbox" />
    <div class="modal">
        <label class="modal__bg" for="modal-1"></label>
        <div class="modal__inner">
            <label class="modal__close" for="modal-1"></label>
            <div>
                <span>Message</span>
                <textarea id="messageBody"></textarea>
                <input
                    type="button"
                    value="Send"
                    id="sendBtn"
                    onclick="vm.createNewDialog()" />
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server">    
    <script src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../../Scripts/modalPopLite.min.js"></script>
    <script src="../../Scripts/_base/promise.js"></script>
    <script src="../../Scripts/_base/ajax.js"></script>
    <script src="../../Scripts/_people/view.js"></script>
    <script src="../../Scripts/_people/service.js"></script>
    <script src="../../Scripts/_people/controller.js"></script>
    <script>
        var vm = new PeopleController(new PeopleView(), new PeopleService());        
    </script>
    <uc1:Chat runat="server" ID="Chat" />
</asp:Content>
