<%@ Page Title="" Language="C#" MasterPageFile="~/Settings/SettingsPage.Master" AutoEventWireup="true"
    CodeBehind="Cidade_CDT.aspx.cs" Inherits="WebSite.Settings.Cidade_CDT" %>

<asp:Content ID="HeadBairro" ContentPlaceHolderID="HeadSettings" runat="server">
</asp:Content>
<asp:Content ID="BodyBairro" ContentPlaceHolderID="BodySettings" runat="server">

    <table>
        <caption>
            Gerenciar Cidades</caption>
        <tr>
            <td colspan="2">
                Estado:
                <asp:DropDownList ID="ddlEstado" runat="server" ToolTip="Escolha uma Cidade" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarListView" />
                <td>
                    <asp:Button ID="btNewCidade" runat="server" Text="Nova Cidade" OnClick="NewCidade" />
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblAlert" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
    <asp:ListView ID="lvCidade" EditIndex="-1" ItemPlaceholderID="Itens" runat="server"
        OnItemCommand="ItemComando" InsertItemPosition="none" OnItemDataBound="DataBound"
        OnItemCanceling="Canceling" OnItemDeleting="Deleting" OnItemEditing="Editing"
        OnItemInserting="Inserting" OnItemUpdating="Updating">
        <LayoutTemplate>
            <table runat="server">
                <caption>
                    Cidades Cadastrados
                </caption>
                <thead>
                    <tr>
                        <td visible="false" runat="server">
                            ID
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
                    <tr runat="server" id="itens">
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="3">
                            <asp:DataPager ID="PgListView" runat="server" PagedControlID="lvCidade" PageSize="20"
                                QueryStringField="pg">
                                <Fields>
                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td visible="false" runat="server">
                    <asp:Label ID="lblId" runat="server" Visible="false" />
                </td>
                <td>
                    <asp:Label ID="lblCidade" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btEditar" CommandName="Edit" runat="server" Text="Editar" />
                    <asp:Button ID="btApagar" CommandName="Delete" runat="server" Text="Apagar" />
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td visible="false" runat="server">
                    <asp:Label ID="lblId" runat="server" Visible="false" />
                </td>
                <td>
                    <asp:TextBox ID="txtCidade" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvTxtbairro" ControlToValidate="txtCidade" ErrorMessage="* Campo Obrigatorio"
                        runat="server" Display="Dynamic" SetFocusOnError="true" />
                </td>
                <td>
                    <asp:Button ID="Save" CommandName="Update" runat="server" Text="Salvar" />
                    <asp:Button ID="Cancel" CommandName="Cancel" runat="server" Text="Cancelar" />
                </td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td colspan="2">
                    Nome da Cidade:
                    <asp:TextBox ID="txtCidade" runat="server" ValidationGroup="vgCadastro" />
                    <asp:RequiredFieldValidator ID="rfvtxtBairro" runat="server" Display="Dynamic" ControlToValidate="txtCidade"
                        ErrorMessage="* Campo Requerido" SetFocusOnError="true" ValidationGroup="vgCadastro" />
                </td>
                <td>
                    <asp:Button ID="btCadastrar" ValidationGroup="vgCadastro" runat="server" CommandName="Insert"
                        Text="Cadastrar" />
                    <asp:Button ID="btCancel" ValidationGroup="vgCadastro" runat="server" CommandName="Cancel"
                        Text="Cancelar" />
                </td>
            </tr>
        </InsertItemTemplate>
        <EmptyItemTemplate>
        
        <tr>
        <td>
                     Escolha um Estado.
        </td>
        
        </tr>
        
        </EmptyItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td>
                                    Não há cidades cadastrada para esse Estado. <br />
                                    Escolha um Estado acima.
                </td>
            </tr>
        
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
