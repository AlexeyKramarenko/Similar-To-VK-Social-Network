<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebFormsApp.Registration" %>


<asp:Content ContentPlaceHolderID="_Styles" runat="server">
    <link href="Content/login.css" rel="stylesheet" />
    <link href="Content/registration.css" rel="stylesheet" />
    <link href="Content/index.css" rel="stylesheet" />

    <!-- Favicon and touch icons -->
    <link rel="shortcut icon" href="Content/assets/ico/favicon.png" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="Content/assets/ico/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="Content/assets/ico/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="Content/assets/ico/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="Content/assets/ico/apple-touch-icon-57-precomposed.png" />
</asp:Content>




<asp:Content ID="innerContent" ContentPlaceHolderID="PlaceHolder" runat="server">
    <div style="display:block;width: 650px; margin-left: auto; margin-right: auto;">

        <asp:FormView runat="server"
            InsertMethod="CreateUser"
            DefaultMode="Insert"
            RenderOuterTable="false"
            ItemType="WebFormsApp.ViewModel.RegistrationViewModel">

            <InsertItemTemplate>

                <fieldset style="display: block;">

                    <div class="form-top">
                        <div class="form-top-left">
                            <h3>Шаг 1  </h3>
                            <h4>Введите свое имя и фамилию</h4>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-user"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <div class="form-group">
                            <label class="sr-only" for="form-first-name">Ваше имя</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="FirstNameErrorMessage"
                                ModelStateKey="FirstName"
                                ForeColor="Blue"
                                runat="server" />



                            <asp:TextBox
                                ClientIDMode="Static"
                                runat="server"
                                CssClass="form-first-name form-control"
                                ID="FirstName" Text='<%# BindItem.FirstName  %>' />
                        </div>
                        <div class="form-group">

                            <label class="sr-only" for="form-last-name">Ваша фамилия</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage1"
                                ModelStateKey="LastName"
                                ForeColor="Blue"
                                runat="server" />



                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                CssClass="form-last-name form-control"
                                Text='<%# BindItem.LastName  %>'
                                ID="LastName" />

                        </div>
                        <button type="button" class="btn btn-next">Далее</button>
                    </div>
                </fieldset>

                <fieldset>
                    <div class="form-top">
                        <div class="form-top-left">
                            <h3>Шаг 2  </h3>
                            <h4>Данные аккаунта</h4>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-key"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <div class="form-group">
                            <label class="sr-only" for="form-email">Телефон</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage2"
                                ModelStateKey="PhoneNumber"
                                ForeColor="Blue"
                                runat="server" />


                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                CssClass="form-email form-control"
                                ValidationGroup="FormGroup"
                                Text='<%# BindItem.PhoneNumber %>'
                                ID="PhoneNumber" />
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-email">Email</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage3"
                                ModelStateKey="Email"
                                ForeColor="Blue"
                                runat="server" />


                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                ValidationGroup="FormGroup"
                                CssClass="form-email form-control"
                                ID="Email"
                                Text='<%# BindItem.Email %>' />
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-last-name">Имя пользователя</label><br />
                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage4"
                                ModelStateKey="UserName"
                                ForeColor="Blue"
                                runat="server" />


                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                ValidationGroup="FormGroup"
                                CssClass="form-last-name form-control"
                                ID="UserName"
                                Text='<%# BindItem.UserName %>' />
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-password">Пароль</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage5"
                                ModelStateKey="Password"
                                ForeColor="Blue"
                                runat="server" />


                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                TextMode="Password"
                                ValidationGroup="FormGroup"
                                CssClass="form-password form-control"
                                ID="Password"
                                Text='<%# BindItem.Password %>' />
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-repeat-password">Повторите пароль</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage6"
                                ModelStateKey="PasswordConfirm"
                                ForeColor="Blue"
                                runat="server" />


                            <asp:TextBox
                                runat="server"
                                ClientIDMode="Static"
                                TextMode="Password"
                                ValidationGroup="FormGroup"
                                CssClass="form-repeat-password form-control"
                                ID="PasswordConfirm"
                                Text='<%# BindItem.PasswordConfirm %>' />

                        </div>

                        <div class="form-group">
                            <label class="sr-only" for="form-repeat-password">Укажите Вашу дату рождения</label><br />

                            <select id="BirthDay" name="BirthDay">
                            </select>
                            <select id="BirthMonth" name="BirthMonth">
                            </select>
                            <select id="BirthYear" name="BirthYear">
                            </select>

                        </div>
                        <button type="button" class="btn btn-previous">Назад</button>
                        <button type="button" class="btn btn-next">Далее</button>
                    </div>
                </fieldset>

                <fieldset>
                    <div class="form-top">
                        <div class="form-top-left">
                            <h3>Шаг 3</h3>
                            <h4>Укажите Ваше место жительства</h4>
                        </div>
                    </div>
                    <div class="form-top-right">
                        <i class="fa fa-key"></i>
                    </div>

                    <div class="form-bottom">
                        <div class="form-group">
                            <label class="sr-only" for="form-facebook">Страна</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage9"
                                ModelStateKey="Country"
                                ForeColor="Blue"
                                runat="server" />



                            <select id="Country" class="dwellingPlace" name="Country">
                            </select>

                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-twitter">Город</label><br />

                            <asp:ModelErrorMessage
                                CssClass="modelErrorMessage"
                                ClientIDMode="Static"
                                ID="ModelErrorMessage10"
                                ModelStateKey="Town"
                                ForeColor="Blue"
                                runat="server" />


                            <select id="Town" class="dwellingPlace" name="Town">
                            </select>

                        </div>

                        <button type="button" class="btn btn-previous">Назад</button>
                        <button type="button" class="btn btn-next">Далее</button>
                    </div>
                </fieldset>

                <fieldset>
                    <div class="form-top">
                        <div class="form-top-left">
                            <h3>Шаг 4</h3>
                            <h4>Укажите Ваши личные данные</h4>
                        </div>
                    </div>
                    <div class="form-top-right">
                        <i class="fa fa-key"></i>
                    </div>

                    <div class="form-bottom">
                        <div class="form-group">
                            <label class="sr-only" for="form-facebook">Ваш пол</label><br />

                            <asp:DropDownList ID="Gender" runat="server"
                                SelectedValue='<%# BindItem.Gender %>'>
                                <asp:ListItem Value="man" Text="мужской" />
                                <asp:ListItem Value="woman" Text="женский" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="sr-only" for="form-twitter">Семейное положение</label><br />

                            <asp:DropDownList ID="Married" runat="server" SelectedValue='<%# BindItem.Married %>'>
                                <asp:ListItem Value="false" Text="не женат/не замужем" />
                                <asp:ListItem Value="true" Text="женат/замужем" />
                            </asp:DropDownList>
                        </div>

                        <button type="button" class="btn btn-previous">Назад</button>
                        <button type="button" class="btn btn-next">Далее</button>
                    </div>
                </fieldset>

                <fieldset>
                    <div class="form-top">
                        <div class="form-top-left">
                            <h3>Шаг 5</h3>
                            <h4>Завершение регистрации</h4>
                        </div>
                        <div class="form-top-right">
                            <i class="fa fa-twitter"></i>
                        </div>
                    </div>
                    <div class="form-bottom">
                        <div id="endRegistration">
                            <button type="button" class="btn btn-previous">Назад</button>
                            <br />
                            <br />
                            <asp:Button
                                CommandName="Insert"                                
                                ID="submitBtn"
                                runat="server"
                                Text="Зарегистрироваться!"
                                Height="45px" Width="150px" />

                        </div>
                    </div>
                </fieldset>


            </InsertItemTemplate>
        </asp:FormView>
    </div> 

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
</asp:Content>


 <asp:Content ContentPlaceHolderID="Scripts" runat="server">
       <!-- Javascript -->
    <script src="Content/assets/js/jquery-1.11.1.min.js"></script>
    <script src="Content/assets/js/jquery.backstretch.js"></script>
    <script src="Content/assets/js/scripts.js" type="text/javascript"></script>
    <!--[if lt IE 10]>
        <script src="assets/js/placeholder.js"></script>
    <![endif]-->
    <script src="Scripts/registration.js"></script> 

    <asp:Literal ID="literalForCallJS" runat="server"></asp:Literal>
 </asp:Content>