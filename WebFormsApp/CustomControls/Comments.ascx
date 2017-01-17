<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="WebFormsApp.CustomControls.Comments" %>


<asp:ListView
    runat="server"
    ID="commentsListView"
    ClientIDMode="Static"
    SelectMethod="GetCommentsByStatusID"
     
    ItemType="Core.POCO.Comment">

    <LayoutTemplate>

        <a class="showDetails" 
           onclick="vm.scrollToCommentsForm(this);
                    vm.showAllComments(this)">
            Show all comments</a>

        <div class="COMMENT" style="width: 300px;">           
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </div>
    </LayoutTemplate>

    <ItemTemplate>

        <div class="hideInfo" style="position: relative">
            <div class="avatar">

                <b><%# Item.UserName %></b>

                <% if (CurrentUserIsOwnerOfCurrentPage)
                        Response.Write("<a class='delete' onclick='vm.deleteComment(this)'>delete</a>");
                %>
                <asp:HiddenField ID="cmnt" runat="server" Value="<%# Item.ID %>" />
                <br />
            </div>
            <div class="commentCell">
                <%# Item.CommentText %>
            </div>
        </div>

    </ItemTemplate>

</asp:ListView>
