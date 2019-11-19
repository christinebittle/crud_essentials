<%@ Page Title="Update Teacher" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UpdateTeacher.aspx.cs" Inherits="HTTP5101_School_System.UpdateTeacher" %>
<asp:Content ID="newteacher" ContentPlaceHolderID="body" runat="server">
    <div id="teacher" runat="server">
        <div class="viewnav">
            <a class="left" href="ShowTeacher.aspx?teacherid=<%=Request.QueryString["teacherid"] %>">Cancel</a>
        </div>
        <h2>Update <span id="teacher_title_name" runat="server"></span> </h2>
        
        <div class="formrow">
            <label>First Name</label>
            <asp:TextBox runat="server" ID="teacher_fname"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Last Name</label>
            <asp:TextBox runat="server" ID="teacher_lname"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Employee Number</label>
            <asp:TextBox runat="server" ID="teacher_employee_number"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Salary</label>
            <asp:TextBox runat="server" ID="teacher_salary"></asp:TextBox>
        </div>
        <asp:Button Text="Update Teacher" runat="server" />
    </div>
</asp:Content>