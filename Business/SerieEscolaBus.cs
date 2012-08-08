using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;
using System.Data;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Série escolar
    /// </summary>
    public class SerieEscolaBus
    {
        private SerieEscola serieEscola = new SerieEscola();

        /// <summary>
        /// Pesquisar informações sobre as Séries Escolares cadastradas
        /// </summary>
        /// <returns>Retorna uma DataTabele com os dados das Séries Escolares
        /// </returns>
        public DataTable Pesquisar()
        {
            DataTable dTable = new DataTable();

            dTable.Columns.Add("id", typeof(int));
            dTable.Columns.Add("serie", typeof(string));
            try
            {
                foreach (var se in serieEscola.Pesqusiar())
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["id"] = se.Id;
                    dRow["serie"] = se.Nome;
                    dTable.Rows.Add(dRow);
                }

            }
            catch (Exception er)
            {
                DataRow dRow = dTable.NewRow();
                dRow["id"] = er.Message;
                dRow["serie"] = er.Source;
                dTable.Rows.Add(dRow);
            }
            return dTable;
        }

    }
}
