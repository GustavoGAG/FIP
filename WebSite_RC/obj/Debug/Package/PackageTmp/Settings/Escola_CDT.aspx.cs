using System;
using System.Web.UI.WebControls;
using Business;
namespace WebSite.Settings
{
    public partial class Escola_CDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreencherDDL();

            }

        }

        #region Cadastro
        protected void PreencherDDL()
        {
            EstadoBus.TodosEstados(ddlEstado, ddlEstadoFiltro);
            CidadeBus.Pesquisar(0, ddlCidade, ddlCidadeFiltro);
            BairroBus.Pesquisar(ddlBairro, 0);
            TipoEscolaBus.Pesquisar(ddlTipoEscola);
        }

        protected void CarregarCidades(object sender, EventArgs e)
        {

            DropDownList ddlSender = ((DropDownList)sender);
            int idEstado = 0;
            int.TryParse(ddlSender.SelectedValue, out idEstado);
            if (sender.Equals(ddlEstado))
                CidadeBus.Pesquisar(idEstado, ddlCidade);
            else
                CidadeBus.Pesquisar(idEstado, ddlCidadeFiltro);
        }

        protected void CarregarBairros(object sender, EventArgs e)
        {
            DropDownList ddlSender = ((DropDownList)sender);
            int idBairro = 0;
            int.TryParse(ddlSender.SelectedValue, out idBairro);

            BairroBus.Pesquisar(ddlBairro, idBairro);

        }

        protected void Cadastrar(object sender, EventArgs e)
        {
            string nome = txtNome.Text,
                telefone = txtTelefone.Text,
                fax = txtFax.Text;

            int idTipoEscola = int.Parse(ddlTipoEscola.SelectedValue),
              idEstado = int.Parse(ddlEstado.SelectedValue),
              idCidade = int.Parse(ddlCidade.SelectedValue),
              idBairro = int.Parse(ddlBairro.SelectedValue)
                ;

            lblRetValue.Text = EscolaBus.Inserir(nome, telefone, fax, idTipoEscola, idEstado, idCidade, idBairro);
            lblRetValue.Visible = true;

        }
        #endregion

        #region ListView
        protected void CarregarListView(int idCidade)
        {
            lvEscola.DataSource = EscolaBus.Pesquisar(idCidade);
            lvEscola.DataBind();
        }

        protected void CarregarListView(object sender, EventArgs e)
        {
            CarregarListView(int.Parse(ddlCidadeFiltro.SelectedValue));
        }


        #endregion

        protected void ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            { 
            Label lblId = (Label)e.Item.FindControl("lblId");

                           Label lblNome = (Label)e.Item.FindControl("lblNome");
                           Label lblTel = (Label)e.Item.FindControl("lblTel");
                           Label txtFax = (Label)e.Item.FindControl("txtFax");

            
            } 
        }



    }
}