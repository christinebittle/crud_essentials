<%@ Page Title="Show Class" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ShowClass.aspx.cs" Inherits="HTTP5101_School_System.ShowClass" %>
<asp:Content ID="student_view" ContentPlaceHolderID="body" runat="server">

    <!-- 
        Note: this page is intentionally styled poorly.
        Work with your group members to understand how it works and improve it!
    -->
    <div class="viewnav">
        <a class="left" href="ListClasses.aspx">Back to List</a>
        <a class="right" href="UpdateClass.aspx?classid=<%= Request.QueryString["classid"] %>">Edit</a>
    </div>
    <div id="course" runat="server">
        <h2>Details for <span id="class_title_coursecode" runat="server"></span></h2>
        Class Code: <span id="class_code" runat="server"></span><br />
        Class Name: <span id="class_name" runat="server"></span><br />
        Teacher: <span id="teacher_name" runat="server"></span><br />
        Start Date: <span id="class_start_date" runat="server"></span><br />
        End Date: <span id="class_finish_date" runat="server"></span><br />
    </div>
    <h3>Students Enrolled:</h3>
    <div class="_table">
        <div class="listitem">
            <div class="col3">Name</div>
            <div class="col3">Number</div>
            <div class="col3last">Action</div>
        </div>
    </div>
    <div id="class_students" runat="server">

    </div>
</asp:Content>
