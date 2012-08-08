<%@ Page Title="" Language="C#" MasterPageFile="~/Settings/SettingsPage.Master" AutoEventWireup="true"
    CodeBehind="Bairro_CDT.aspx.cs" Inherits="WebSite.Settings.Bairro_CDT" %>

<asp:Content ID="HeadBairro" ContentPlaceHolderID="HeadSettings" runat="server">
</asp:Content>
<asp:Content ID="BodyBairro" ContentPlaceHolderID="BodySettings" runat="server">

    <asp:HiddenField runat="server" ID="hfIdEstado" Visible="false" Value="0" />
    <asp:HiddenField runat="server" ID="hfIdCidade" Visible="false" Value="0" />
    <table>
        <caption>
            Gerenciar Bairros</caption>
        <tr>
            <td>
                Estado:
                <asp:DropDownList ID="ddlEstado" runat="server" ToolTip="Escolha uma Cidade" AutoPostBack="true"
                    OnSelectedIndexChanged="CarregarCidade" />
            </td>
            <td>
                Cidade:
                <asp:DropDownList ID="ddlCidade" runat="server" AutoPostBack="true" ToolTip="Escolha uma Cidade"
                    OnSelectedIndexChanged="CarregarListView" />
                <asp:Button ID="btNewBairro" runat="server" Text="Cadastrar Bairro" OnClick="NewBairro"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div ID="divAlerta" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
    <asp:ListView ID="lvBairro" EditIndex="-1" ItemPlaceholderID="Itens" runat="server"
        OnItemCommand="ItemComando" InsertItemPosition="None" OnItemDataBound="DataBound"
        OnItemCanceling="Canceling" OnItemDeleting="Deleting" OnItemEditing="Editing"
        OnItemInserting="Inserting" OnItemUpdating="Updating">
        <LayoutTemplate>
            <table runat="server">
                <caption>
                    Bairros Cadastrados
                </caption>
                <tr>
                    <td colspan="3">
         
                    </td>
                </tr>
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
                <tr runat="server" id="itens">
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:DataPager ID="PgListView" runat="server" PagedControlID="lvBairro" PageSize="20"
                            QueryStringField="pg">
                            <Fields>
                                <asp:NumericPagerField ButtonCount="10" ButtonType="Link" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td visible="false" runat="server">
                    <asp:Label ID="lblId" runat="server" Visible="false" />
                </td>
                <td>
                    <asp:Label ID="lblBairro" runat="server" />
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
                   
                    <asp:TextBox ID="txtBairro" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvTxtbairro" ControlToValidate="txtbairro" ErrorMessage="* Campo Obrigatorio"
                        runat="server" Display="Dynamic" SetFocusOnError="true" />
                </td>
                <td>
                    <asp:Button ID="btSave" CommandName="Update" runat="server" Text="Salvar" />
                    <asp:Button ID="btCancel" CommandName="Cancel" runat="server" Text="Cancelar" />
                </td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                
                <td colspan="2">
                    Nome do Bairro:
                    <asp:TextBox ID="txtBairro" runat="server" ValidationGroup="vgCadastro" />
                    <asp:RequiredFieldValidator ID="rfvtxtBairro" runat="server" Display="Dynamic" ControlToValidate="txtBairro"
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
    </asp:ListView>
</asp:Content>
