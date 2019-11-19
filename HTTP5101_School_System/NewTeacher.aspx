<%@ Page Title="New Teacher" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="NewTeacher.aspx.cs" Inherits="HTTP5101_School_System.NewTeacher" %>
<asp:Content ID="newteacher" ContentPlaceHolderID="body" runat="server">
    <h2>New Teacher</h2>
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
        <asp:TextBox runat="server" ID="teacher_number"></asp:TextBox>
    </div>
    <div class="formrow">
        <label>Salary</label>
        <asp:TextBox runat="server" ID="teacher_salary"></asp:TextBox>
    </div>
    <asp:Button Text="Add Teacher" runat="server" />
</asp:Content>