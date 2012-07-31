using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace WebSite.Settings
{
    public partial class Deficiencia_CDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarListView();
            }
        }


        protected void CarregarListView()
        {
            lvDeficiencia.DataSource = DeficienciaBus.Pesquisar();
            lvDeficiencia.DataBind();

        }

        protected void AbrirInsert(object sender, EventArgs e)
        {
            lvDeficiencia.EditIndex = -1;
            lvDeficiencia.InsertItemPosition = InsertItemPosition.FirstItem;
            ((Button)sender).Visible = false;
            CarregarListView();


        }

        protected void FecharInsert()
        {
            lvDeficiencia.EditIndex = -1;
            lvDeficiencia.InsertItemPosition = InsertItemPosition.None;
            Button btCadastrar = (Button)lvDeficiencia.FindControl("btCadastrar");
            btCadastrar.Visible = true;
        }

        protected void FecharEdit()
        {

            lvDeficiencia.EditIndex = -1;
        }

        protected void Command(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    #region INSERT
                    case "Insert":
                        {
                            TextBox txtDeficiencia = (TextBox)e.Item.FindControl("txtDeficiencia");
                            string nomeDeficiencia = txtDeficiencia.Text;

                            Label lblAlert = (Label)lvDeficiencia.FindControl("lblAlert");

                            if (!string.IsNullOrEmpty(nomeDeficiencia))
                                lblAlert.Text = DeficienciaBus.Inserir(nomeDeficiencia);

                            break;
                        }
                    #endregion

                    #region UPDATE
                    case "Update":
                        {

                            Label lblID = (Label)e.Item.FindControl("lblID");

                            TextBox txtDeficiencia = (TextBox)e.Item.FindControl("txtDeficiencia");
                            Label lblAlert = (Label)lvDeficiencia.FindControl("lblAlert");
                            int idDeficiencia = 0;
                            int.TryParse(lblID.Text, out idDeficiencia);
                            string nomeDeficiencia = txtDeficiencia.Text;

                            if (idDeficiencia != 0)
                                lblAlert.Text = DeficienciaBus.Editar(idDeficiencia, nomeDeficiencia);



                            break;
                        }
                    #endregion

                    #region DELETE

                    case "Delete":
                        {

                            Label lblID = (Label)e.Item.FindControl("lblID");


                            Label lblAlert = (Label)lvDeficiencia.FindControl("lblAlert");
                            int idDeficiencia = 0;
                            int.TryParse(lblID.Text, out idDeficiencia);

                            lblAlert.Text = DeficienciaBus.Apagar(idDeficiencia);



                            break;
                        }
                    #endregion
                }
            }
            catch
            { }
        }

        protected void DataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblID = (Label)e.Item.FindControl("lblID");
                lblID.Text = DataBinder.Eval(e.Item.DataItem, "id").ToString();

                Label lblDeficiencia = (Label)e.Item.FindControl("lblDeficiencia");
                lblID.Text = DataBinder.Eval(e.Item.DataItem, "deficiencia").ToString();


            }
        }

        #region ListView
        protected void Canceling(object sender, ListViewCancelEventArgs e)
        {
            if (e.CancelMode == ListViewCancelMode.CancelingInsert)
            { FecharInsert(); }
            else if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            { FecharEdit(); }


        }

        protected void Deleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void Editing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Inserting(object sender, ListViewInsertEventArgs e)
        {
            CarregarListView();
        }

        protected void Updating(object sender, ListViewUpdateEventArgs e)
        {

        }
        #endregion

        protected void Inserted(object sender, ListViewInsertedEventArgs e)
        {
            FecharInsert();
            CarregarListView();
        }
    }
}