using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Ferramentas;

namespace WebSite.Settings
{
    public partial class Bairro_CDT:System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    int idEstado = 0;
                    int idCidade = 0;
                    if(Request.QueryString.Count > 0)
                    {
                        int.TryParse(Request.QueryString["cid"],out idCidade);
                        int.TryParse(Request.QueryString["est"],out idEstado);

                    }

                    EstadoBus.TodosEstados(ddlEstado);
                    CidadeBus.Pesquisar(idEstado,ddlCidade);

                    ddlCidade.Items.FindByValue(idCidade.ToString()).Selected = true;
                    ddlEstado.Items.FindByValue(idEstado.ToString()).Selected = true;


                    CarregarListView(idCidade);

                }
            }
            catch(AlertaException erro)
            {
                divAlerta.InnerHtml = erro.Alerta;
                divAlerta.Visible = true;

            }
            catch(Exception ex)
            {
                divAlerta.InnerHtml = AlertaException.EnviarEmailSuporte(ex);
                divAlerta.Visible = true;


            }

        }

        protected void NewBairro(object sender,EventArgs e)
        {
            lvBairro.EditIndex = -1;
            lvBairro.InsertItemPosition = InsertItemPosition.FirstItem;
            ((Button)sender).Visible = false;

        }

        protected void CarregarListView(int idCidade)
        {
            lvBairro.DataSource = BairroBus.Pesquisar(idCidade);
            lvBairro.DataBind();
        }

        #region DropDownList
        /// <summary>Envia o ID do Estado selecionado para URL</summary>
        protected void CarregarCidade(object sender,EventArgs e)
        {
            Response.Redirect("Bairro_CDT.aspx?est=" + ddlEstado.SelectedValue);

        }
        /// <summary>Envia o ID da Cidade selecionado para URL</summary>
        protected void CarregarListView(object sender,EventArgs e)
        {
            Response.Redirect(QueryString(ddlCidade.SelectedValue));
        }
        #endregion

        #region ListView Bairros

        /// <summary>Vincula os Objetos aos dados</summary>
        protected void DataBound(object sender,ListViewItemEventArgs e)
        {
            try
            {
                if(e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lblId = (Label)e.Item.FindControl("lblId");
                    lblId.Text = DataBinder.Eval(e.Item.DataItem,"id").ToString();

                    Label lblBairro = (Label)e.Item.FindControl("lblBairro");

                    Button btEditar = (Button)e.Item.FindControl("btEditar") ?? (Button)e.Item.FindControl("btSave");
                    Button btApagar = (Button)e.Item.FindControl("btApagar") ?? (Button)e.Item.FindControl("btCancel");

                     
                    lblBairro.Text =  DataBinder.Eval(e.Item.DataItem,"nomeBairro").ToString();
                    btEditar.CommandArgument = lblId.Text;
                    btApagar.CommandArgument = lblId.Text;

                }

            }
            catch(AlertaException erro)
            {
                divAlerta.InnerHtml = erro.Alerta;
                divAlerta.Visible = true;

            }
            catch(Exception ex)
            {
                divAlerta.InnerHtml = AlertaException.EnviarEmailSuporte(ex);
                divAlerta.Visible = true;


            }
        }


        protected void ItemComando(object sender,ListViewCommandEventArgs e)
        {
            try
            {

                switch(e.CommandName)
                {
                    #region UPDATE
                    case "Update":
                        {
                            TextBox txtBairro = (e.Item.FindControl("txtBairro")) as TextBox;
                            Label lblId = (e.Item.FindControl("lblId")) as Label;

                            int idCidade = 0;

                            if(!string.IsNullOrEmpty(Request.QueryString["cid"]))
                                int.TryParse(Request.QueryString["cid"],out idCidade);

                            if(!string.IsNullOrEmpty(txtBairro.Text))
                                divAlerta.InnerHtml = BairroBus.Editar(int.Parse(lblId.Text),txtBairro.Text,idCidade);

                            divAlerta.Visible = true;
                            break;
                        }
                    #endregion

                    #region DELETE
                    case "Delete":
                        {
                            Label lblId = (e.Item.FindControl("lblId")) as Label;
                            divAlerta.InnerHtml = BairroBus.Apagar(int.Parse(lblId.Text));
                            divAlerta.Visible = true;
                            break;
                        }
                    #endregion

                    #region INSERT
                    case "Insert":
                        {

                            TextBox txtBairro = (TextBox)e.Item.FindControl("txtBairro");
                            int idCidade = 0;
                            int.TryParse(Request.QueryString["cid"],out idCidade);
                            if(idCidade != 0)
                                divAlerta.InnerHtml = BairroBus.Inserir(txtBairro.Text,idCidade);
                            else
                                divAlerta.InnerHtml = "Selecione uma Cidade.";

                            divAlerta.Visible = true;
                            break;
                        }
                    #endregion

                }
            }
            catch(AlertaException erro)
            {
                divAlerta.InnerHtml = erro.Alerta;
                divAlerta.Visible = true;

            }
            catch(Exception ex)
            {
                divAlerta.InnerHtml = AlertaException.EnviarEmailSuporte(ex);
                divAlerta.Visible = true;


            }
        }

        #endregion

        private void FecharInsert()
        {
            lvBairro.InsertItemPosition = InsertItemPosition.None;
            btNewBairro.Visible = !btNewBairro.Visible;

        }

        protected void Editing(object sender,ListViewEditEventArgs e)
        {

            int idCidade = 0;
            int.TryParse(ddlCidade.SelectedValue,out idCidade);
            FecharInsert();
            lvBairro.EditIndex = e.NewEditIndex;
            CarregarListView(idCidade);

        }

        #region Operaçoes ListView
        protected void Inserting(object sender,ListViewInsertEventArgs e)
        {

        }

        protected void Canceling(object sender,ListViewCancelEventArgs e)
        {
            int idCidade = 0;
            int.TryParse(ddlCidade.SelectedValue,out idCidade);
            if(e.CancelMode == ListViewCancelMode.CancelingInsert)
                FecharInsert();
            else
                lvBairro.EditIndex = -1;
            CarregarListView(idCidade);
        }

        protected void Updating(object sender,ListViewUpdateEventArgs e)
        {

        }

        protected void Deleting(object sender,ListViewDeleteEventArgs e)
        {

        }
        #endregion

        protected string QueryString(string idCidade)
        {
            string query = string.Empty;
            try
            {

                NameValueCollection nvc = new NameValueCollection();
                foreach(string key in Request.QueryString.Keys)
                    nvc.Add(key,Request.QueryString[key]);

                nvc.Set("cid",idCidade);

                foreach(string key in nvc.Keys)
                    query += key + "=" + nvc[key] + "&";

                if(!string.IsNullOrEmpty(query))
                    query = "?" + query.TrimEnd('&');
                query = Request.Url.AbsolutePath + query;

            }
            catch(AlertaException erro)
            {
                divAlerta.InnerHtml = erro.Alerta;
                divAlerta.Visible = true;

            }
            catch(Exception ex)
            {
                divAlerta.InnerHtml = AlertaException.EnviarEmailSuporte(ex);
                divAlerta.Visible = true;


            }
            return query;



        }


    }
}