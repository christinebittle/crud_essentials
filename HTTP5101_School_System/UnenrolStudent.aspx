<%@ Page Title="Unenrol Student" MasterPageFile="~/Layout.Master" Language="C#" AutoEventWireup="true" CodeBehind="UnenrolStudent.aspx.cs" Inherits="HTTP5101_School_System.UnenrolStudent" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <h3>Are you sure you want to remove <span runat="server" id="studentname"></span> from <span id="classname" runat="server"></span>?</h3>
    <%/*
        This interface is a bit of a hybrid between. It acts both as the interface for
        - removing a student from a class
        - removing a class from a student
        which are essentially the same concept. 
        We can arrive at this page either in 
        - show class (where we see a list of students and a prompt to remove any particular one) or
        - show student (where we see a list of classes and a prompt to remove any particular one)
        
    */%>
    <div class="viewnav thin">
        <a class="left" href="ShowStudent.aspx?studentid=<%= Request.QueryString["studentid"] %>">No</a>
        <ASP:Button OnClick="Unenrol_Student" CssClass="right" Text="Yes" runat="server"/>   

    </div>
</asp:Content>
