using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace WebSite.Public
{
    public partial class Lista_Escola : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EstadoBus.TodosEstados(ddlEstado);
                CidadeBus.Pesquisar(int.Parse(ddlEstado.SelectedValue), ddlCidade);

            }
        }

        protected void PreencherListView(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                switch (ddl.ValidationGroup)
                {
                    case "Cidade":
                        lvEscola.DataSource = EscolaBus.Pesquisar(int.Parse(ddl.SelectedValue));
                        lvEscola.DataBind();
                        break;
                    case "Estado":
                        CidadeBus.Pesquisar(int.Parse(ddl.SelectedValue), ddlCidade);

                        break;

                }
            }
            catch { }
        }

   

        protected void IDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lblTipo = (Label)e.Item.FindControl("lblTipo");
                    HiddenField hfId = (HiddenField)e.Item.FindControl("hfId");
                    Label lblNome = (Label)e.Item.FindControl("lblNome");
                    Label lblTel = (Label)e.Item.FindControl("lblTel");
                    Label lblFax = (Label)e.Item.FindControl("lblFax");
                    Label lblEstado = (Label)e.Item.FindControl("lblEstado");
                    Label lblCidade = (Label)e.Item.FindControl("lblCidade");
                    Label lblBairro = (Label)e.Item.FindControl("lblBairro");
                    Label lblEndereco = (Label)e.Item.FindControl("lblEndereco");

                    hfId.Value = DataBinder.Eval(e.Item.DataItem, "idEscola").ToString();
                    lblTipo.Text = DataBinder.Eval(e.Item.DataItem, "TipoEscola").ToString();
                    lblNome.Text = DataBinder.Eval(e.Item.DataItem, "nome").ToString();
                    lblTel.Text = DataBinder.Eval(e.Item.DataItem, "telefone").ToString();
                    lblFax.Text = DataBinder.Eval(e.Item.DataItem, "fax").ToString();
                    lblEstado.Text = DataBinder.Eval(e.Item.DataItem, "estado").ToString();
                    lblCidade.Text = DataBinder.Eval(e.Item.DataItem, "cidade").ToString();
                    lblEndereco.Text = DataBinder.Eval(e.Item.DataItem, "bairro").ToString();
                    lblBairro.Text = DataBinder.Eval(e.Item.DataItem, "endereco").ToString();

                }
            }
            catch (Exception er)
            {
                lblAlert.Text = er.Source + " <br />" + er.Message;
                lblAlert.Visible = true;
            }
        }




    }
}