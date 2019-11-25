<%@ Page Title="Update Student" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UpdateStudent.aspx.cs" Inherits="HTTP5101_School_System.UpdateStudent" %>
<asp:Content ID="updatestudent" ContentPlaceHolderID="body" runat="server">
    <div id="student" runat="server">
        <div class="viewnav">
            <a class="left" href="ShowStudent.aspx?studentid=<%=Request.QueryString["studentid"] %>">Cancel</a>
        </div>
        <h2>Updating Student <span id="student_title" runat="server"></span></h2>
        
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

        <asp:Button Text="Update Student" OnClick="Update_Student" runat="server" />
    </div>
</asp:Content>
