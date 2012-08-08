<%@ Page Title="" Language="C#" MasterPageFile="~/Settings/SettingsPage.Master" AutoEventWireup="true"
    CodeBehind="ConselhoTutelar_CDT.aspx.cs" Inherits="WebSite.Settings.ConselhoTutelar_CDT" %>

<asp:Content ID="HeadConselhoTutelar" ContentPlaceHolderID="HeadSettings" runat="server">
</asp:Content>
<asp:Content ID="BodyConselhoTutelar" ContentPlaceHolderID="BodySettings" runat="server">

    <table>
    <tr>
    <td>
    <asp:Label ID="lblRetValue" runat="server" Visible="false" />
    </td>
    </tr>
        <tr>
            <td>
            <asp:Label ID="lblNome" runat="server" Text="Nome:" />
            </td>
            <td>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtNome" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTelefone" runat="server" Text="Telefone:" />
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTelefone" />
            </td>
            <td>
                <asp:Label ID="lblFax" runat="server" Text="Telefone Seundario:" />
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtFax" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEndereco" runat="server" Text="Endereço:" />
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtEndereco" />
            </td>
            <td>
                <asp:Label ID="lblCep" runat="server" Text="CEP:" />
            </td>
            <td>
                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtCep" />
            </td>
        </tr>
        <tr>
 
            <td>
                <asp:DropDownList runat="server" ID="ddlEstado" AutoPostBack="true" OnSelectedIndexChanged="CarregarCidades"
                    ToolTip="Estado">
                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="rfvEstado" ControlToValidate="ddlEstado" InitialValue="0"
                    Display="Dynamic" runat="server" ErrorMessage="* Campo Obrigatorio" SetFocusOnError="true"
                    ValidationGroup="vgCadastro" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlCidade" AutoPostBack="true" OnSelectedIndexChanged="CarregarBairros"
                    ToolTip="Cidade">
                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="rfvCidade" ControlToValidate="ddlCidade" InitialValue="0"
                    Display="Dynamic" runat="server" ErrorMessage="* Campo Obrigatorio" SetFocusOnError="true"
                    ValidationGroup="vgCadastro" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlBairro" ToolTip="Cidade">
                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="rfvBairro" ControlToValidate="ddlBairro" InitialValue="0"
                    Display="Dynamic" runat="server" ErrorMessage="* Campo Obrigatorio" SetFocusOnError="true"
                    ValidationGroup="vgCadastro" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btCadastrar" runat="server" ValidationGroup="vgCadastro" Text="Cadastrar"
                    OnClick="Cadastrar" />
            </td>
        </tr>
    </table>
</asp:Content>
