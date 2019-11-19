<%@ Page Title="Show Teacher" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ShowTeacher.aspx.cs" Inherits="HTTP5101_School_System.ShowTeacher" %>
<asp:Content ID="student_view" ContentPlaceHolderID="body" runat="server">

    <!-- 
        Note: this page is intentionally styled poorly.
        Work with your group members to understand how it works and improve it!
    -->
    <div class="viewnav">
        <a class="left" href="ListTeachers.aspx">Back To List</a>
        <a class="right" href="UpdateTeacher.aspx?teacherid=<%=Request.QueryString["teacherid"] %>">Edit</a>
    </div>
    <div id="teacher" runat="server">
        <h2>Details for <span id="teacher_title_fname" runat="server"></span></h2>
        First Name: <span id="teacher_fname" runat="server"></span><br />
        Last Name: <span id="teacher_lname" runat="server"></span><br />
        Employee Number: <span id="teacher_employee_number" runat="server"></span><br />
        Hire Date: <span id="teacher_hire_date" runat="server"></span><br />
        Salary: <span id="teacher_salary" runat="server"></span><br />
    </div>
    <h3>Classes Enrolled:</h3>
    <div class="_table">
        <div class="listitem">
            <div class="col2">Course Code</div>
            <div class="col2last">Course Name</div>
           
        </div>
    </div>
    <div id="teacher_classes" runat="server">

    </div>
</asp:Content>
