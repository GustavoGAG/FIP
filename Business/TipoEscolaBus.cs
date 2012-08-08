
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAcess;
using System.Web.UI.WebControls;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Tipo de Escola
    /// </summary>
    public static class TipoEscolaBus
    {

        private static TipoEscola tipoEscola = new TipoEscola();

        /// <summary>
        /// Pesquisar informações dos Tipos de Escola cadastrados
        /// </summary>
        /// <param name="ddlTipoEscola">Retorna uma DropDownList com os dados dos Tipos de Escola</param>
        public static void Pesquisar(DropDownList ddlTipoEscola)
        {
            try
            {
                ListItem li = new ListItem();
                li.Value = "0";
                li.Text = "Selecione".ToUpper();
                ddlTipoEscola.Items.Add(li);

                foreach (TipoEscola t in tipoEscola.Pesquisar())
                {
                    li = new ListItem();
                    li.Text = t.Nome.ToUpper();
                    li.Value = t.Id.ToString();
                    ddlTipoEscola.Items.Add(li);

                }
            }
            catch (Exception er)
            {
                ArgumentException arg = new ArgumentException(er.Message, er.Source, er.InnerException);
                throw arg;
            }

        }



    }
}
