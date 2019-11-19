<%@ Page Title="Update Class" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UpdateClass.aspx.cs" Inherits="HTTP5101_School_System.UpdateClass" %>
<asp:Content ID="newclass" ContentPlaceHolderID="body" runat="server">
    <div id="course" runat="server">
        <div class="viewnav">
            <a class="left" href="ShowClass.aspx?classid=<%=Request.QueryString["classid"] %>">Cancel</a>
        </div>
        <h2>Update Class <span runat="server" id="class_title"></span></h2>
        
        <div class="formrow">
            <label>Class Name</label>
            <asp:TextBox runat="server" ID="class_name"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Class Code</label>
            <asp:TextBox runat="server" ID="class_code"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Start Date</label>
            <asp:TextBox runat="server" ID="class_start_date"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Finish Date</label>
            <asp:TextBox runat="server" ID="class_finish_date"></asp:TextBox>
        </div>
        <div class="formrow">
            <label>Teacher</label>
            <asp:DropDownList runat="server" ID="class_teacherid"></asp:DropDownList>
        </div>
        <asp:Button Text="Add Class" runat="server" />
    </div>
</asp:Content>