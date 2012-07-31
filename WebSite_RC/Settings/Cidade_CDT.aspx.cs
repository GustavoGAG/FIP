using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace WebSite.Settings
{
    public partial class Cidade_CDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                EstadoBus.TodosEstados(ddlEstado);

                if (!string.IsNullOrEmpty(Request.QueryString["est"]))
                    ddlEstado.Items.FindByValue(Request.QueryString["est"]).Selected = true;




                CarregarListView();

            }

        }

        protected void NewCidade(object sender, EventArgs e)
        {
            lvCidade.EditIndex = -1;
            lvCidade.InsertItemPosition = InsertItemPosition.FirstItem;
            ((Button)sender).Visible = false;


        }

        protected void CarregarListView()
        {
            int idEstado = 0;
            int.TryParse(Request.QueryString["est"], out idEstado);
            if (idEstado == 0)
                idEstado = 68;
            lvCidade.DataSource = CidadeBus.Pesquisar(idEstado);
            lvCidade.DataBind();
        }

        #region DropDownList


        protected void CarregarListView(object sender, EventArgs e)
        {
            Response.Redirect("Cidade_CDT.aspx?est=" + ddlEstado.SelectedValue);
        }
        #endregion

        #region ListView Cidade

        /// <summary>Vincula os Objetos aos dados</summary>
        protected void DataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lblId = (Label)e.Item.FindControl("lblId");
                    lblId.Text = DataBinder.Eval(e.Item.DataItem, "id").ToString();

                    Label lblCidade = (Label)e.Item.FindControl("lblCidade");
                    lblCidade.Text = DataBinder.Eval(e.Item.DataItem, "nomeCidade").ToString();

                    Button btEditar = (Button)e.Item.FindControl("btEditar");
                    Button btApagar = (Button)e.Item.FindControl("btApagar");
                    btEditar.CommandArgument = lblId.Text;
                    btApagar.CommandArgument = lblId.Text;


                }
           
            }
            catch { }
        }


        protected void ItemComando(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                switch (e.CommandName)
                {
                    #region UPDATE
                    case "Update":
                        {
                            TextBox txtCidade = (e.Item.FindControl("txtCidade")) as TextBox;
                            Label lblId = (e.Item.FindControl("lblId")) as Label;

                            int idCidade = 0;

                            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                                int.TryParse(Request.QueryString["cid"], out idCidade);

                            if (!string.IsNullOrEmpty(txtCidade.Text))
                                lblAlert.Text = CidadeBus.Editar(int.Parse(lblId.Text), txtCidade.Text);

                            lblAlert.Visible = true;

                            CarregarListView();
                            break;
                        }
                    #endregion

                    #region DELETE
                    case "Delete":
                        {
                            Label lblId = (e.Item.FindControl("lblId")) as Label;
                            lblAlert.Text = CidadeBus.Apagar(int.Parse(lblId.Text));
                            lblAlert.Visible = true;
                            CarregarListView();

                            break;
                        }
                    #endregion

                    #region INSERT
                    case "Insert":
                        {

                            TextBox txtCidade = (TextBox)e.Item.FindControl("txtCidade");
                            int idCidade = 0;
                            int.TryParse(Request.QueryString["est"], out idCidade);
                            if (idCidade != 0)
                                lblAlert.Text = CidadeBus.Inserir(idCidade, txtCidade.Text);
                            else
                                lblAlert.Text = "Selecione uma Cidade.";

                            lblAlert.Visible = true;

                            CarregarListView();
                            break;
                        }
                    #endregion

                }
            }
            catch { }
        }

        #endregion


        protected void FecharInsert()
        {
            lvCidade.InsertItemPosition = InsertItemPosition.None;
            Button btNewCidade = (Button)lvCidade.FindControl("btNewCidade");
            //NewBairro.Visible = false;

        }

        protected void Editing(object sender, ListViewEditEventArgs e)
        {
            FecharInsert();
            lvCidade.EditIndex = e.NewEditIndex;
            CarregarListView();

        }

        #region Operaçoes ListView
        protected void Inserting(object sender, ListViewInsertEventArgs e)
        {

        }

        protected void Canceling(object sender, ListViewCancelEventArgs e)
        {
            if (e.CancelMode == ListViewCancelMode.CancelingInsert)
                FecharInsert();
            else
                lvCidade.EditIndex = -1;
            CarregarListView();
        }

        protected void Updating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void Deleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion


    }
}