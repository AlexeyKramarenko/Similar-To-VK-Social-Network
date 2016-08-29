using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebFormsApp
{
    [ToolboxData("<{0}:MessageComponent runat='server'></{0}:MessageComponent>")]
    public class MessageControl : CompositeControl
    {

        [DefaultValue(0)]
        public int RelationshipDefinitionID
        {
            get
            {
                return (int)ViewState["RelationshipDefinitionID"];
            }
            set
            {
                ViewState["RelationshipDefinitionID"] = value;
            }
        }

        //0
        private PlaceHolder send_message_placeHolder_0;
        PlaceHolder Send_message_panel_0
        {
            get
            {

                var input_container = new Panel { ClientIDMode = ClientIDMode.Static, CssClass = "input_container" };
                var fileUpload = new FileUpload { ClientIDMode = ClientIDMode.Static, ID = "inputfile" };
                fileUpload.Attributes.Add("onchange", "sendAvatar();return false;");
                input_container.Controls.Add(fileUpload);



                var newPhotos = new Panel { ClientIDMode = ClientIDMode.Static, ID = "addNewPhotos" };
                newPhotos.CssClass= "addNewPhotos";
                newPhotos.Attributes.Add("onclick", "upload()");
                newPhotos.Attributes.Add("onmouseover", "changeColor(this)");
                newPhotos.Attributes.Add("onmouseout", "changeColor(this)");
                newPhotos.Controls.Add(new Literal { Text = "Upload avatar" });

                send_message_placeHolder_0 = new PlaceHolder();
                send_message_placeHolder_0.Controls.Add(input_container);
                send_message_placeHolder_0.Controls.Add(newPhotos);

                return send_message_placeHolder_0;
            }
        }

        //1
        private Panel send_message_panel_1;
        Panel Send_message_panel_1
        {
            get
            {
                send_message_panel_1 = new Panel { ClientIDMode = ClientIDMode.Static, ID = "send_message" };

                var label = new HtmlGenericControl("label");
                label.Attributes.Add("for", "modal-1");
                label.Attributes.Add("class", "send_message_btn");
                label.InnerText = "Send message";
                label.EnableViewState = false;

                var div = new Panel();
                div.Controls.Add(new Literal { Text = "You are friend" });

                send_message_panel_1.Controls.Add(label);
                send_message_panel_1.Controls.Add(div);

                return send_message_panel_1;
            }
        }

        //2
        private Panel send_message_panel_2;
        Panel Send_message_panel_2
        {
            get
            {
                send_message_panel_2 = new Panel { ClientIDMode = ClientIDMode.Static, ID = "send_message" };

                var label = new HtmlGenericControl("label");
                label.Attributes.Add("for", "modal-1");
                label.Attributes.Add("class", "send_message_btn");
                label.InnerText = "Send message";
                label.EnableViewState = false;

                var div = new Panel();
                div.Controls.Add(new Literal { Text = "You are subscriber" });

                send_message_panel_2.Controls.Add(label);
                send_message_panel_2.Controls.Add(div);

                return send_message_panel_2;
            }
        }

        //3
        private Panel send_message_panel_3;
        Panel Send_message_panel_3
        {
            get
            {
                send_message_panel_3 = new Panel { ClientIDMode = ClientIDMode.Static, ID = "send_message" };

                var label = new HtmlGenericControl("label");
                label.Attributes.Add("for", "modal-1");
                label.Attributes.Add("class", "send_message_btn");
                label.InnerText = "Send message";
                label.EnableViewState = false;

                var btn = new Button();
                btn.ClientIDMode = ClientIDMode.Static;
                btn.ID = "AddToFriends";
                btn.CssClass = "send_message_btn";
                btn.Text = "Add to friends";
                btn.OnClientClick = "addToFriends();return false;";

                send_message_panel_3.Controls.Add(label);
                send_message_panel_3.Controls.Add(btn);

                return send_message_panel_3;
            }
        }



        protected override void CreateChildControls()
        {
            Controls.Clear();

        }
        protected override void Render(HtmlTextWriter writer)
        {
            RenderControl(RelationshipDefinitionID, writer);
        }


        private void RenderControl(int id, HtmlTextWriter writer)
        {
            switch (id)
            {
                case 0: //Посетитель - владелец этой страницы
                    Send_message_panel_0.RenderControl(writer);
                    break;

                case 1: //Посетитель - друг владельца этой страницы
                    Send_message_panel_1.RenderControl(writer);
                    break;

                case 2: //Посетитель - подписчик владельца этой страницы
                    Send_message_panel_2.RenderControl(writer);
                    break;

                default: //Посетитель - неизвестный владельцу этой страницы
                    Send_message_panel_3.RenderControl(writer);
                    break;
            }
        }


        /* Генерируемая разметка:
        
            string div0 = @"
                        <div class='input_container'>
                            <input type='file' id='inputfile' onchange='sendAvatar();return false;' />
                        </div>
                        <div id='addNewPhotos' class='addNewPhotos' onclick='upload()' onmouseover='changeColor(this)' onmouseout='changeColor(this)'>
                             Upload avatar
                        </div>";

            string div1 = @"
                        <div id='send_message'> 
                            <label for='modal-1' class='send_message_btn'>Send message</label>
                            <div>You are friend</div>
                        </div>";

            string div2 = @"
                        <div id='send_message'>
                            <label for='modal-1' class='send_message_btn'>Send message</label>
                            <div>You are subscriber</div>
                        </div>";

            string div3 = @"
                        <div id='send_message'>
                            <label for='modal-1' class='send_message_btn'>Send message</label>
                            <input 
                                type='button' 
                                id='AddToFriends'
                                class='send_message_btn' 
                                value='Add to friends' onclick='addToFriends();return false;'>
                        </div>";
        */
    }
}