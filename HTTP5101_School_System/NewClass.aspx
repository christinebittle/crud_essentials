<%@ Page Title="New Class" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="NewClass.aspx.cs" Inherits="HTTP5101_School_System.NewClass" %>
<asp:Content ID="newclass" ContentPlaceHolderID="body" runat="server">
    <h2>New Class</h2>
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
        <asp:TextBox runat="server" ID="start_date"></asp:TextBox>
    </div>
    <div class="formrow">
        <label>Finish Date</label>
        <asp:TextBox runat="server" ID="finish_date"></asp:TextBox>
    </div>
    <div class="formrow">
        <label>Teacher</label>
        <asp:DropDownList runat="server" ID="class_teacherid"></asp:DropDownList>
    </div>
    <asp:Button Text="Add Class" runat="server" />
</asp:Content>