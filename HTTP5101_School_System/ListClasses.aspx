<%@ Page Title="Classes" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ListClasses.aspx.cs" Inherits="HTTP5101_School_System.ListClasses" %>

<asp:Content ID="classes_list" ContentPlaceHolderID="body" runat="server">
    <h1>Classes</h1>
    <div id="class_nav">
        <asp:label for="class_search" runat="server">Search:</asp:label>
        <asp:TextBox ID="class_search" runat="server"></asp:TextBox>
        <asp:Button runat="server" text="submit"/>
        <div id="sql_debugger" runat="server"></div>
        <%
        //todod: search by keyword
        //search by semester selection(?) -- calendar picker before/after? -- range?
        //order by fname lname asc desc
        %>
    </div>
    <a href="NewClass.aspx">Add New</a>
    <div class="_table" runat="server">
        <div class="listitem">
            <div class="col5">Class Code</div>
            <div class="col5">Class Name</div>
            <div class="col5">Teacher</div>
            <div class="col5">Start Date</div>
            <div class="col5last">Finish Date</div>
        </div>
        <div id="classes_result" runat="server">

        </div>
    </div>
</asp:Content>
