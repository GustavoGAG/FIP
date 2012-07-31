<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Contato.aspx.cs" Inherits="WebSite.Contato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyMain" runat="server">
    <div runat="server" id="divAlert" visible="false" class="alert">
    </div>
    <section class="caixa1">
        <section class="form">
            <div class="formstyle1">
                <div class="field">
                    <p>
                        <asp:Label ID="Label1" runat="server"> 
                <h3>Sua Mensagem é muito importante.</h3> </asp:Label>
                    </p>
                </div>
                <div class="field">
                    <asp:Label ID="lblNome" runat="server">Nome:</asp:Label>
                    <asp:TextBox ID="txtNome" requerid="Campo Obrigatorio" MaxLength="100" class="textfield"
                        runat="server" AutoCompleteType="DisplayName" ToolTip="Digite seu nome" />
                </div>
                <div class="field">
                    <asp:Label ID="lblEmail" runat="server">Email:</asp:Label>
                    <asp:TextBox ID="txtEmail" MaxLength="100" class="textfield" runat="server" AutoCompleteType="DisplayName"
                        ToolTip="Digite seu email para entrarmos em contato com você" />
                </div>
                <div class="field">
                    <asp:Label ID="lblTel" runat="server">Telefone:</asp:Label>
                    <asp:TextBox ID="txtTel" MaxLength="14" class="textfield" runat="server" AutoCompleteType="DisplayName"
                        ToolTip="Digite seu telefone para entrarmos em contato com você" />
                </div>
                <div class="field">
                    <asp:Label ID="lblAssunto" runat="server">Assunto:</asp:Label><br />
                    <asp:TextBox ID="txtAssunto" requerid="Campo Obrigatorio" MaxLength="20" class="textfield"
                        runat="server" AutoCompleteType="None" Rows="10" ToolTip="Digite o assunto  referente a mensagem" />
                </div>
                <div class="field">
                    <asp:Label ID="lblMensagem" runat="server">Mensagem:</asp:Label><br />
                    <asp:TextBox ID="txtMensagem" requerid="Campo Obrigatorio" MaxLength="255" class="text-area"
                        runat="server" AutoCompleteType="None" TextMode="MultiLine" Rows="10" ToolTip="Digite sua mensagem" />
                </div>
                <div class="buttton">
                    <asp:Button ID="btEnviarMensagem" runat="server" Text="Enviar" class="button" OnClick="EnviarMensagem" />
                </div>
            </div>
        </section>
    </section>
</asp:Content>
