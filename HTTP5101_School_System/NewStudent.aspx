<%@ Page Title="New Student" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="NewStudent.aspx.cs" Inherits="HTTP5101_School_System.NewStudent" %>
<asp:Content ID="newstudent" ContentPlaceHolderID="body" runat="server">
    <h2>New Student</h2>
    <div class="formrow">
        <label>First Name</label>
        <asp:TextBox runat="server" ID="student_fname"></asp:TextBox>
    </div>
    <div class="formrow">
        <label>Last Name</label>
        <asp:TextBox runat="server" ID="student_lname"></asp:TextBox>
    </div>
    <div class="formrow">
        <label>Student Number</label>
        <asp:TextBox runat="server" ID="student_number"></asp:TextBox>
    </div>

    <asp:Button OnClick="Add_Student" Text="Add Student" runat="server" />
</asp:Content>
