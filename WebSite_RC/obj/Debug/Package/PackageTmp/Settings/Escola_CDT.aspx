<%@ Page Title="" Language="C#" MasterPageFile="~/Settings/Settings.Master" AutoEventWireup="true"
    CodeBehind="Escola_CDT.aspx.cs" Inherits="WebSite.Settings.Escola_CDT" %>

<asp:Content ID="HeadEscolaCDT" ContentPlaceHolderID="HeadSettings" runat="server">
</asp:Content>
<asp:Content ID="BodyEscolaCDT" ContentPlaceHolderID="BodySettings" runat="server">

    <div>S
        <table>
            <caption>
                Cadastrar Escola
            </caption>
            <tr>
                <td colspan="4">
                    <asp:Label runat="server" ID="lblRetValue" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNome" runat="server" Text="Nome:" />
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtNome" runat="server" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNome" ControlToValidate="txtNome" Display="Dynamic"
                        runat="server" ErrorMessage="* Campo Obrigatorio" SetFocusOnError="true" ValidationGroup="vgCadastro" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTelefone" runat="server" Text="Telefone:" />
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtTelefone" runat="server" MaxLength="14" />
                </td>
                <td>
                    <ajax:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtTelefone" MaskType="Number"
                        Mask="(099)9999-9999" runat="server" ClearMaskOnLostFocus="false" ClipboardText="true"
                        ClipboardEnabled="true" />
                    <asp:RequiredFieldValidator ID="rfvTelefone" ControlToValidate="txtTelefone" Display="Dynamic"
                        runat="server" ErrorMessage="* Campo Obrigatorio" SetFocusOnError="true" ValidationGroup="vgCadastro" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFax" runat="server" Text="Fax:" />
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtFax" runat="server" MaxLength="14" />
                </td>
                <td>
                    <ajax:MaskedEditExtender ID="mskTelefone" TargetControlID="txtFax" MaskType="Number"
                        Mask="(099)9999-9999" runat="server" ClearMaskOnLostFocus="false" ClipboardText="true"
                        ClipboardEnabled="true" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoEscola" runat="server" Text="Tipo de Escola:" />
                </td>
                <td colspan="2">
                    <asp:DropDownList runat="server" ID="ddlTipoEscola" ToolTip="Em que nivel escola você esta">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvTipoEscola" ControlToValidate="ddlTipoEscola"
                        InitialValue="0" Display="Dynamic" runat="server" ErrorMessage="* Campo Obrigatorio"
                        SetFocusOnError="true" ValidationGroup="vgCadastro" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblLocalizacao" runat="server" Text="Localização" />
                </th>
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
    </div>
    <div>
        <table>
            <tr>
                <th>
                    <asp:Label ID="Label1" runat="server" Text="Escola:" />
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlEstadoFiltro" AutoPostBack="true" OnSelectedIndexChanged="CarregarCidades"
                        ToolTip="Estado">
                    </asp:DropDownList>
                   
                   
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCidadeFiltro" ToolTip="Editar Cidade" AutoPostBack="true"
                        OnSelectedIndexChanged="CarregarListView">
                    </asp:DropDownList>
                  
                    
                </td>
   
            </tr>
        </table>
    </div>
    <div>
        <asp:ListView ID="lvEscola" runat="server" DataKeyNames="id" EnablePersistedSelection="True"
            ItemPlaceholderID="Placeholder" onitemdatabound="ItemDataBound">
            <LayoutTemplate>
                <table>
                    <caption>
                        Gerenciar Escolas Cadastradas
                    </caption>
                    <thead>
                        <tr>
                            <td runat="server" visible="false">
                                ID
                            </td>
                            <td>
                                Nome
                            </td>
                            <td>
                                Telefone
                            </td>
                            <td>
                                Fax
                            </td>
                            <td>
                                Nivel
                            </td>
                            <td>
                                Estado
                            </td>
                            <td>
                                Cidade
                            </td>
                            <td>
                                Bairro
                            </td>
                            <td>
                                Ação
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="Placeholder">
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="8">
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td runat="server" id="tdID" visible="false">
                        <asp:Label ID="lblId" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblNome" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblTel" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:Label ID="txtFax" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoEscola" runat="server" Enabled="false" ToolTip="Grau escolar" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstado" runat="server" Enabled="false" ToolTip="Estado" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCidade" runat="server" Enabled="false" ToolTip="Cidade" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBairro" runat="server" Enabled="false" ToolTip="Bairro" />
                    </td>
                    <td>
                        <asp:Button ID="btEditar" CommandName="Edit" Text="Editar" ToolTip="Editar os Dados da Escola"
                            runat="server" />
                        <asp:Button ID="btApagar" CommandName="Delete" Text="Apagar" ToolTip="Apagar a Escola"
                            runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td id="Td1" runat="server" visible="false">
                        <asp:Label ID="lblId" runat="server" Visible="false" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtTel" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Enabled="false" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoEscola" runat="server" Enabled="false" ToolTip="Grau escolar" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstado" runat="server" Enabled="false" ToolTip="Estado" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCidade" runat="server" Enabled="false" ToolTip="Cidade" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBairro" runat="server" Enabled="false" ToolTip="Bairro" />
                    </td>
                    <td>
                        <asp:Button ID="btEditar" CommandName="Edit" Text="Editar" ToolTip="Editar os Dados da Escola"
                            runat="server" />
                        <asp:Button ID="btApagar" CommandName="Delete" Text="Apagar" ToolTip="Apagar a Escola"
                            runat="server" />
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
            <tr>
            <td>
            Sem informação no banco
            </td>
            </tr>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</asp:Content>
