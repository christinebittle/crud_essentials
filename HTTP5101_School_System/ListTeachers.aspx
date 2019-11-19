<%@ Page Title="Teachers" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ListTeachers.aspx.cs" Inherits="HTTP5101_School_System.ListTeachers" %>


<asp:Content ID="teachers_list" ContentPlaceHolderID="body" runat="server">
    <h1>Teachers</h1>
    <div id="class_nav">
        <asp:label for="teacher_search" runat="server">Search:</asp:label>
        <asp:TextBox ID="teacher_search" runat="server"></asp:TextBox>
        <asp:Button runat="server" text="submit"/>
        <div id="sql_debugger" runat="server"></div>
        <%
        //todod: search by keyword
        //search by semester selection(?) -- calendar picker before/after? -- range?
        //order by fname lname asc desc
        %>
    </div>
    <a href="NewTeacher.aspx">New Teacher</a>
    <div class="_table" runat="server">
        <div class="listitem">
            <div class="col4">Name</div>
            <div class="col4">Employee Number</div>
            <div class="col4">Salary</div>
            <div class="col4last">Hire Date</div>
        </div>
        <div id="teachers_result" runat="server">

        </div>
    </div>
</asp:Content>
