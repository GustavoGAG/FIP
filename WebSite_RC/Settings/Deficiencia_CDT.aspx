<%@ Page Title="" Language="C#" MasterPageFile="~/Settings/Settings.Master" AutoEventWireup="true"
    CodeBehind="Deficiencia_CDT.aspx.cs" Inherits="WebSite.Settings.Deficiencia_CDT" %>

<asp:Content ID="TituloDeficiencia_CDT" ContentPlaceHolderID="HeadSettings" runat="server">
</asp:Content>
<asp:Content ID="BodyDeficiencia_CDT" ContentPlaceHolderID="BodySettings" runat="server">
    <asp:ListView ID="lvDeficiencia" runat="server" InsertItemPosition="None" ItemPlaceholderID="ItemPlacehold"
        ConvertEmptyStringToNull="true" OnItemCanceling="Canceling" OnItemCommand="Command"
        OnItemDataBound="DataBound" OnItemDeleting="Deleting" OnItemEditing="Editing"
        OnItemInserting="Inserting" OnItemUpdating="Updating" OnItemInserted="Inserted">
		
        <LayoutTemplate>
            <table runat="server">
                <tr>
                    <td>
                        Gerenciar Deficiencias
                    </td>
                    <td>
                        <asp:Button ID="btCadastrar" runat="server" Text="Cadastrar Nova Deficiencia" OnClick="AbrirInsert" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblAlert" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table runat="server">
                            <thead>
                                <tr>
                                    <th>
                                        ID
                                    </th>
                                    <th>
                                        Deficiencia
                                    </th>
                                    <th>
                                        Ação
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr id="ItemPlacehold" runat="server"></tr>
                                    
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataPager ID="dpDeficiencia" runat="server" PagedControlID="lvDeficiencia" QueryStringField="pg">
                                            <Fields>
                                                <asp:NumericPagerField />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblID"    runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblDeficiencia" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btEditar" runat="server" CommandName="Edit" Text="Editar" />
                    <asp:Button ID="btApagar" runat="server" CommandName="Delete" Text="Apagar" />
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblID" runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:TextBox ID="txtDeficiencia" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btUpdate" runat="server" CommandName="Update" Text="Salvar" />
                    <asp:Button ID="btCancel" runat="server" CommandName="Cancel" Text="Cancelar" />
                </td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="txtDeficiencia" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btInsert" runat="server" CommandName="Insert" Text="Cadastrar" />
                    <asp:Button ID="btCancel" runat="server" CommandName="Cancel" Text="Cancelar" />
                </td>
            </tr>
        </InsertItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="lblEmpty" runat="server" Text="Não há itens cadastrados" />
                </td>
            </tr>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
