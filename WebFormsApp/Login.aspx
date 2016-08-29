<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebFormsApp.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_Styles" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder" runat="server">
    
    <div id="nav_content_footer" >
        <nav>
            <asp:FormView
                DefaultMode="Insert"
                InsertMethod="LoginUser"
                ItemType="WebFormsApp.ViewModel.LoginViewModel"
                DataKeyNames="ID"
                runat="server"
                RenderOuterTable="false">

                <InsertItemTemplate>
                    <ul>
                        <li>Username</li>

                        <li>
                            <asp:TextBox
                                ID="txtUserName"
                                runat="server"
                                ClientIDMode="Static"
                                Text='<%# BindItem.UserName %>'
                                Width="125px" /></li>

                        <li>Password</li>

                        <li>
                            <asp:TextBox
                                ID="txtPassword"
                                TextMode="Password"
                                ClientIDMode="Static"
                                Text='<%# BindItem.Password %>'
                                runat="server"
                                Width="125px" /></li>

                        <li>
                            <asp:Button
                                ID="btnInsert"
                                CommandName="Insert"
                                OnClientClick="setStorageData()"
                                runat="server"
                                Text="Войти"
                                Width="125px" />

                        </li>
                        <li><a href="RecoverPassword.aspx">Forgot password?</a></li>
                    </ul>
                </InsertItemTemplate>
            </asp:FormView>
             
            </nav>
             <br />
    
    <asp:ValidationSummary
        ID="valSummary"
        runat="server"
        ClientIDMode="Static"
        HeaderText="Исправьте следующие ошибки:"
        ShowModelStateErrors="true"
        DisplayMode="BulletList"
        EnableClientScript="true"
        ForeColor="Red" />

        
    </div> 
</asp:Content>



<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script src="Scripts/login.js"></script>
</asp:Content>