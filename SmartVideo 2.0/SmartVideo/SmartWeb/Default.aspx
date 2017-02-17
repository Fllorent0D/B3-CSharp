<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmartWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>SmartVideotheque</h1>
        <h2>Films disponibles</h2>
    </div>

    <div class="row">
        <div>       
         <% foreach(DTOLibrary.FilmDTO Item in GetStock()) { %>
                    <div class="row">
                        <div class="col-md-4">
                            <img class="img img-responsive" src="//image.tmdb.org/t/p/w185/<%: Item.poster_path %>" alt="<%: Item.original_title %>'s poster">
                        </div>
                        <div class="col-md-8">  
                            <div class="row">
                                <h2><%: Item.titre %></h2>
                                <small><%: Item.runtime %> minutes</small>

                            </div>      
                            <div class="row">
                                <div class="col-md-3">
                                    <h3>Acteurs</h3>
                                    <ul>
                                        <% foreach(DTOLibrary.ActorDTO actor in Item.actors) { %>
                                            <li>
                                            <%: actor.name %>
                                            </li>
                                        <% } //foreach %>
                                        
                                    </ul> 
                                </div>
                                <div class="col-md-3">
                                        <h3>Réalisateurs</h3>
                                    <ul>
                                        <% foreach(DTOLibrary.RealisateurDTO actor in Item.realisateurs) { %>
                                            <li>
                                            <%: actor.Name %>
                                            </li>
                                        <% } //foreach %>
                                        
                                    </ul> 
                                </div>
                                <div class="col-md-3">
                                      <h3>Genres</h3>
                                    <ul>
                                        <% foreach(DTOLibrary.GenreDTO actor in Item.genres) { %>
                                            <li>
                                            <%: actor.Name %>
                                            </li>
                                        <% } //foreach %>
                                        
                                    </ul>   
                                </div>
                            </div>
                        </div>

                    </div>
            <hr />
             <% } //foreach %>
  

        </div>
    </div>

</asp:Content>
