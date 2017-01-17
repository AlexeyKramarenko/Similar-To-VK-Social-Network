<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statuses.ascx.cs" Inherits="WebFormsApp.CustomControls.Statuses" %>

<asp:ListView
    ID="ListView1"
    ClientIDMode="Static"
    DataKeyNames="ID"
    SelectMethod="GetStatuses"
    ItemType="Core.POCO.Status"
    OnItemCreated="ListView1_ItemCreated"
    runat="server">
    <LayoutTemplate>
        <div style="margin-right: -1px; border-right: 1px solid gray; padding-right: 10px; padding-left: 10px;">
            <ul id="postsList">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </ul>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <li>
            <div class="table_">
                <div class="table_row">
                    <div class="table_leftCell">

                        <asp:Image
                            ImageUrl='<%# Item.AvatarUrl %>'
                            ID="Image3"
                            CssClass="post_image"
                            runat="server"
                            Width="30px" />

                        <br />
                    </div>
                    <div class="table_rightCell">
                        <b><%# Item.UserName %></b>

                        <% if (CurrentUserIsOwnerOfCurrentPage)
                                Response.Write("<a class='delete' onclick='vm.deletePost(this)'>delete</a>");
                        %>
                        <br />
                        <%# Item.Post %>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdnStatusId" runat="server" Value="<%# Item.ID %>" />

            <%# Item.CreateDate %> |
            
            <% if (DisplayCommentLink == true)
                {
                    Response.Write("<a onclick='vm.createCommentsForm(this)'>Comment</a>");
                }
            %>

            <hr />

            <asp:PlaceHolder ID="CommentsPlaceHolder" runat="server"></asp:PlaceHolder>

            <div class="COMMENT" style="width: 300px;">
            </div>

            <div class="commentContainer"></div>
        </li>
    </ItemTemplate>
</asp:ListView>
