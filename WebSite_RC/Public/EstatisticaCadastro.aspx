<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="EstatisticaCadastro.aspx.cs" Inherits="WebSite.EstatisticaCadastro" %>

<asp:Content ID="EstatisticaCadastroHead" ContentPlaceHolderID="HeadMain" runat="server">
</asp:Content>
<asp:Content ID="EstatisticaCadastroBody" ContentPlaceHolderID="BodyMain" runat="server">
    <div runat="server" id="divAlert" visible="false" class="alerta">
    </div>
    <asp:ListView ID="lvEstatisticaEstado" runat="server" ItemPlaceholderID="item" EditIndex="-1"
        OnItemDataBound="PreencherListViewEstado">
        <LayoutTemplate>
            <table>
                <caption>
                    Pessoas Inscritas por Estado
                </caption>
                <thead>
                    <tr class="field">
                        <td>
                            Estado
                        </td>
                        <td>
                            Nº de Inscritos
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="item" runat="server" class="field">
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblEstado" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblQtd" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
