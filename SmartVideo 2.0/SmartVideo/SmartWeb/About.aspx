<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SmartWeb.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="ListView1" runat="server" SelectMethod="SelectNews" ItemType="DTOLibrary.PostDTO" >
        <ItemTemplate>
        <div class="blog-post">
            <h2 class="blog-post-title"><%#: Item.Titre %></h2>
            <p class="blog-post-meta"><%# Item.Date.ToShortDateString() %></p>
            <p><%# Item.Contenu %></p>
        </div><!-- /.blog-post -->
        </ItemTemplate>
    </asp:ListView>
    
    
</asp:Content>
