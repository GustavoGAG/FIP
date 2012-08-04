using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAcess;
using System.Web.UI.WebControls;
using Ultilitarios;

namespace Business
{
    /// <summary>
    /// Clase de Negócio Cidade
    /// </summary>
    public class CidadeBus
    {
        private static DataTable dTable = new DataTable();
        private static Cidade cidade = new Cidade();
        private static string retValue = String.Empty;

        /// <summary>
        /// Insere dados da Cidade no Banco de Dados
        /// </summary>
        /// <param name="idEstado">id do Estado a que a Cidade pertence</param>
        /// <param name="nomeCidade">Nome da Cidade que será inserida no Banco</param>
        /// <returns>Retorna um texto informando se a Cidade foi ou não inserida com sucesso</returns>
        public static string Inserir(int idEstado, string nomeCidade)
        {
            retValue = String.Empty;

            try
            {
                if (!cidade.ValidarExistencia(idEstado, nomeCidade))
                {
                    cidade = new Cidade() { IdEstado = idEstado, Nome = nomeCidade };

                    if (cidade != null)
                        if (cidade.Inserir(cidade) == 1)
                            retValue = "Cidade Cadastradas com sucesso";
                        else
                            retValue = "Erro ao Cadastrar";

                }

            }
            catch (Exception er)
            {
                AlertaException.EnviarEmailSuporte(er);
            }
            return retValue;

        }

        /// <summary>
        /// Editar os dados da Cidade
        /// </summary>
        /// <param name="idCidade">id da Cidade a ser alterada</param>
        /// <param name="nome">Nome da Cidade que será editada no Banco de Dados</param>
        /// <returns>Retorna um texto informando se a Cidade foi ou não alterada com sucesso</returns>
        public static string Editar(int idCidade, string nome)
        {

            try
            {
                if (!cidade.ValidarExistencia(nome, idCidade))
                {

                    if (cidade.Editar(idCidade, nome) == 1)
                        retValue = "Os dados foram alterados com Sucesso";
                    else
                        retValue = "Erro ao Editar no nome da cidade";
                }

            }
            catch (Exception er)
            {
                AlertaException.EnviarEmailSuporte(er);
            }
            return retValue;
        }

        /// <summary>
        /// Apagar uma Cidade cadastrada no Banco de Dados
        /// </summary>
        /// <param name="idCidade">id da Cidade que será apagada</param>
        /// <returns>Retorna um texto informando se a Cidade foi ou não apagada com sucesso</returns>
        public static string Apagar(int idCidade)
        {
            int ret = 0;
            try
            {
                cidade = new Cidade();
                if (idCidade != 0)
                {
                    if (cidade.ValidarExistencia(idCidade))
                    {
                        ret = cidade.Apagar(idCidade);
                        if (ret == 1)
                            retValue = "Item Apagado";
                        else if (ret == -1)
                            retValue = "Erro ao Apagar esta Cidade";
                        else
                            retValue = "Cidade não vai apagar";
                    }
                    else
                        retValue = "Esse Cidade não esta Cadastrada, por favor, recarregue a pagina.";

                }
            }
            catch (Exception er)
            {
                AlertaException.EnviarEmailSuporte(er);
            }
            return retValue;
        }


        #region Pesquisa para Preencher Objetos

        /// <summary>Retorna todas as cidade de um Estado</summary>
        /// <param name="idEstado">ID do Estado</param>
        /// <param name="ddlCidade">DropDownList que recebera as Informaçoes</param>
        /// <returns>DropDownList com as cidade de um Estado</returns>
        public static void Pesquisar(int idEstado, DropDownList ddlCidade)
        {
            ddlCidade.Items.Clear();

            ddlCidade.Items.Add(new ListItem
                                    {
                                        Text = "Selecione".ToUpper(), Value = "0"
                                    });
            foreach (Cidade c in cidade.Pesquisar(idEstado))
            {

                ddlCidade.Items.Add(new ListItem
                                        {
                                            Text = c.Nome.ToUpper(), Value = Convert.ToString(c.Id)
                                        });

            }
        }

        /// <summary>Retorna todas as cidade de um Estado</summary>
        /// <param name="idEstado">ID do Estado</param>
        /// <param name="ddlCidade">DropDownList que recebera as Informaçoes</param>
        /// <returns>DropDownList com as cidade de um Estado</returns>
        public static void Pesquisar(int idEstado, DropDownList ddlCidade,
            DropDownList ddlCidade1)
        {
            try
            {
                ddlCidade.Items.Clear();
                ddlCidade1.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Selecione".ToUpper();
                li.Value = "0";

                ddlCidade.Items.Add(li);
                ddlCidade1.Items.Add(li);
                foreach (Cidade c in cidade.Pesquisar(idEstado))
                {
                    li = new ListItem();
                    li.Text = c.Nome.ToUpper();
                    li.Value = Convert.ToString(c.Id);
                    ddlCidade.Items.Add(li);
                    ddlCidade1.Items.Add(li);
                }
            }
            catch  
            {
                throw  ;
                 
            }
            
        }

        /// <summary>
        /// Retorna todas as Cidades de um Estado
        /// </summary>
        /// <param name="idEstado">id do Estado a que as cidades pertencem</param>
        /// <returns>Retorna uma DataTable com as Cidades pesquisadas</returns>
        public static DataTable Pesquisar(int idEstado)
        {
            cidade = new Cidade();
            dTable = new DataTable();
            try
            {
                dTable.Columns.Add("id", typeof(string));
                dTable.Columns.Add("nomeCidade", typeof(string));

                foreach (Cidade c in cidade.Pesquisar(idEstado))
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["id"] = c.Id;
                    dRow["nomeCidade"] = c.Nome;
                    dTable.Rows.Add(dRow);

                }


            }
            catch (Exception ex)
            {

                DataRow dRow = dTable.NewRow();
                dRow["id"] = ex.GetHashCode();
                dRow["nomeCidade"] = ex.Message;
                dTable.Rows.Add(dRow);
            }
            return dTable;

        }

        #endregion



    }
}
