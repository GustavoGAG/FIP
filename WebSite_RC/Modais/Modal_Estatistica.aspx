<%@ Page Title="" Language="C#" MasterPageFile="~/Modal.Master" AutoEventWireup="true"
    CodeBehind="Modal_Estatistica.aspx.cs" Inherits="WebSite.Modal_Estatistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ModalHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModalBody" runat="server">
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
                    <p align="Left">
                        <asp:Label ID="lblQtd" runat="server" />
                    </p>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
