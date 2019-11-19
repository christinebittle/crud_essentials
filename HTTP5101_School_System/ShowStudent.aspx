<%@ Page Title="Show Student" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ShowStudent.aspx.cs" Inherits="HTTP5101_School_System.ShowStudent" %>
<asp:Content ID="student_view" ContentPlaceHolderID="body" runat="server">

    <!-- 
        Note: this page is intentionally styled poorly.
        Work with your group members to understand how it works and improve it!
    -->
    <div class="viewnav">
        <a class="left" href="ListStudents.aspx">Back To List</a>
        <a class="right" href="UpdateStudent.aspx?studentid=<%= Request.QueryString["studentid"] %>">Edit</a>
    </div>
    <div id="student" runat="server">
        <h2>Details for <span id="student_title_fname" runat="server"></span></h2>
        First Name: <span id="student_fname" runat="server"></span><br />
        Last Name: <span id="student_lname" runat="server"></span><br />
        Student Number: <span id="student_number" runat="server"></span><br />
        Enrolment Date: <span id="enrolment_date" runat="server"></span><br />
    </div>
    <h3>Classes Enrolled:</h3>
    <div class="_table">
        <div class="listitem">
            <div class="col4">Course Code</div>
            <div class="col4">Course Name</div>
            <div class="col4">Teacher</div>
            <div class="col4last">Action</div>
            <div class="clear"></div>
        </div>
    </div>
    <div id="student_classes" class="_table" runat="server">

    </div>
    <div class="_table">
        <div class="listitem">
            <div class="col3">Add Student to Class</div>
            <div class="col3"><asp:DropDownList ID="student_classid" runat="server"></asp:DropDownList></div>
            <div class="col3last"><asp:button runat="server" Text="Enrol"></asp:button></div>
        </div>
    </div>
</asp:Content>
