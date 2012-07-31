using System;
using System.Data;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DataAcess;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Estado
    /// </summary>
    public class EstadoBus
    {

        private static Estado estado = new Estado();

        #region Consultas

        /// <summary>Exibe todos os Estados Cadastrados</summary>
        /// <param name="ddlEstado">DropDownList que Sera prenchida</param>
        /// <returns>Retorna a DropDownList preenchida</returns>
        public static void TodosEstados(DropDownList ddlEstado)
        {
            try
            {

                ddlEstado.Items.Clear();

                ListItem li = new ListItem();
                li.Text = "Selecione".ToUpper();
                li.Value = "0";

                ddlEstado.Items.Add(li);
                foreach(Estado c in estado.ListarEstados())
                {
                    li = new ListItem();
                    li.Text = c.Nome.ToUpper();
                    li.Value = c.Id.ToString();
                    ddlEstado.Items.Add(li);

                }

            }
            catch(Exception er)
            {
                ListItem li = new ListItem();
                li.Text = er.Message;
                li.Value = "0";
                ddlEstado.Items.Add(li);
            }


        }

        /// <summary>Exibe todos os Estados Cadastrados</summary>
        /// <param name="ddlEstado1">DropDownList que Sera prenchida</param>
        public static void TodosEstados(DropDownList ddlEstado1,DropDownList ddlEstado2)
        {
            try
            {
                ddlEstado1.Items.Clear();
                ddlEstado2.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Selecione".ToUpper();
                li.Value = "0";

                ddlEstado1.Items.Add(li);
                ddlEstado2.Items.Add(li);
                foreach(Estado c in estado.ListarEstados())
                {
                    li = new ListItem();
                    li.Text = c.Nome.ToUpper();
                    li.Value = c.Id.ToString();
                    ddlEstado1.Items.Add(li);
                    ddlEstado2.Items.Add(li);
                }
            }
            catch(Exception er)
            {
                ListItem li = new ListItem();
                li.Text = er.Message;
                li.Value = "0";
                ddlEstado1.Items.Add(li);
                ddlEstado2.Items.Add(li);

            }

        }

        /// <summary>Exibe todos os Estados Cadastrados</summary>
        /// <param name="ddlEstado1">DropDownList que Sera prenchida</param>
        public static void TodosEstados(DropDownList ddlEstado1,DropDownList ddlEstado2,DropDownList ddlEstado3)
        {
            try
            {

                ddlEstado1.Items.Clear();
                ddlEstado2.Items.Clear();
                ddlEstado3.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Selecione".ToUpper();
                li.Value = "0";

                ddlEstado1.Items.Add(li);
                ddlEstado2.Items.Add(li);
                ddlEstado3.Items.Add(li);
                foreach(Estado c in estado.ListarEstados())
                {
                    li = new ListItem();
                    li.Text = c.Nome.ToUpper();
                    li.Value = c.Id.ToString();
                    ddlEstado1.Items.Add(li);
                    ddlEstado2.Items.Add(li);
                    ddlEstado3.Items.Add(li);
                }
            }
            catch(Exception er)
            {
                ListItem li = new ListItem();
                li.Text = er.Message;
                li.Value = "0";
                ddlEstado1.Items.Add(li);
                ddlEstado2.Items.Add(li);
                ddlEstado3.Items.Add(li);
            }

        }


        #endregion


    }



}