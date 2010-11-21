﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Web.Models.LoginModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Trackyt.net | Login</title>
    <link href="../../Content/public-login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="container">
        <div id="content">
        <h1 class="left">Log In</h1>
        <span id="else" class="left"> or <%: Html.ActionLink("Register", "Index", "Registration") %></span>
        <div class="clear"></div>
        <p>
        If you are already our customer, please log in. If you are new, please proceed and create new account
        </p>
        <% using (Html.BeginForm("Login", "Login", FormMethod.Post, new { id = "login_form" }))
           {%>
        <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.")%>
        <fieldset>
            <div id="email-container">
                <label for="email">Email</label>
                <%: Html.TextBoxFor(m => m.Email)%>
            </div>
            <div id="password-container">
                <label for="password">Password</label>
                <%: Html.TextBoxFor(m => m.Password, new { type = "password" })%>
            </div>
            <p>
                <input id="submit-button" type="submit" value="Login »" />
            </p>
        </fieldset>
        <% } %>
        </div>
    </div>
</body>
</html>
