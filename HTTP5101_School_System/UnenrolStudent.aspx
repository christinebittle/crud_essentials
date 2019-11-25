<%@ Page Title="Unenrol Student" MasterPageFile="~/Layout.Master" Language="C#" AutoEventWireup="true" CodeBehind="UnenrolStudent.aspx.cs" Inherits="HTTP5101_School_System.UnenrolStudent" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <h3>Are you sure you want to remove <span runat="server" id="studentname"></span> from <span id="classname" runat="server"></span>?</h3>
    <div class="viewnav thin">
        <a class="left" href="ShowStudent.aspx?studentid=<%= Request.QueryString["studentid"] %>">No</a>
        <ASP:Button OnClick="Unenrol_Student" CssClass="right" Text="Yes" runat="server"/>   

    </div>
</asp:Content>
