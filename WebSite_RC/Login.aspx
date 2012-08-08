<%@ Page Title="Login Administrativo - MI5" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSite.Login" Culture="pt-BR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyMain" runat="server">
    <asp:Login ID="formLogin" runat="server" 
    DestinationPageUrl="~/Settings/Index.aspx" DisplayRememberMe="False" 
    MembershipProvider="SqlProvider" PasswordLabelText="Senha:" 
    PasswordRequiredErrorMessage="A Senha é obrigatorio" TextLayout="TextOnTop" 
    UserNameLabelText="Nome de Usuario:" 
    UserNameRequiredErrorMessage="O nome de usuario é obrigatorio">
    </asp:Login>
</asp:Content>
