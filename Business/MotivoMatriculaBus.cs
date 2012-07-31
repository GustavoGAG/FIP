using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;
using System.Web.UI.WebControls;
namespace Business
{
    /// <summary>
    /// Classe de Negócio Motivo da Matrícula
    /// </summary>
    public class MotivoMatriculaBus
    {
        private static MotivoMatricula motivo = new MotivoMatricula();

        /// <summary>
        /// Pesquisar por informações referentes ao Motivo da Matrícula
        /// </summary>
        /// <param name="ddl">DropDownList com os dados do Motivo da Matrícula</param>
        public static void Pesquisar(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "SELECIONE";
                li.Text = "0";
                foreach (var i in motivo.Pesquisar())
                {
                    li = new ListItem();
                    li.Value = i.Id.ToString();
                    li.Text = i.Motivo.ToUpper();
                    ddl.Items.Add(li);

                }
                
            }
            catch
            { throw; }


        }

    }
}
