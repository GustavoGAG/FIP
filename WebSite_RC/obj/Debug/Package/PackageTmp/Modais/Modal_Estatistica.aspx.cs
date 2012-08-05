using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Ferramentas;

namespace WebSite
{
    public partial class Modal_Estatistica:System.Web.UI.Page
    {
        protected void Page_Load(object sender,EventArgs e)
        {

            try
            {
                if(!IsPostBack)
                {
                    PreencherListViewEstado();

                }
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;

            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;

            }

            #endregion

        }

        protected void PreencherListViewEstado()
        {
            try
            {
                lvEstatisticaEstado.DataSource = UsuarioBus.Estatistica();
                lvEstatisticaEstado.DataBind();
            }
            #region Exceçoes
            catch(AlertaException erro)
            {

                divAlert.InnerHtml = erro.Alerta;
                divAlert.Visible = true;

            }
            catch(Exception Error)
            {
                divAlert.InnerHtml = AlertaException.EnviarEmailSuporte(Error);
                divAlert.Visible = true;

            }

            #endregion
        }

        protected void PreencherListViewEstado(object sender,ListViewItemEventArgs e)
        {
            ListViewItem item = e.Item;
            object dados = e.Item.DataItem;
            try
            {

                Label lblEstado = (Label)item.FindControl("lblEstado"),
            lblQtd = (Label)item.FindControl("lblQtd");

                lblQtd.Text = DataBinder.Eval(dados,"qtd").ToString();

                lblEstado.Text = DataBinder.Eval(dados,"nome").ToString();

            }
            catch
            {
                throw;
            }
        }
   
    }
}