using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DataAcess;
using System.Data;

namespace Business
{
    /// <summary>
    /// Classe de Negócio Bairro
    /// </summary>
    public static class BairroBus
    {

        private static Bairro bairro = new Bairro();
        private static DataTable dTable = new DataTable();
        private static string retValue = string.Empty;

        /// <summary>
        /// Insere os dados do Bairro no Banco de Dados
        /// </summary>
        /// <param name="nomeBairro">Nome do Bairro</param>
        /// <param name="idCidade">O id da Cidade a que o Bairro pertence</param>
        /// <returns>Retorna um texto informando se o Bairro foi ou não cadastrado</returns>
        public static string Inserir(string nomeBairro, int idCidade)
        {
            try
            {
                if (bairro.ValidarExistencia(idCidade, nomeBairro))
                {
                    bairro = new Bairro();
                    bairro.IdCidade = idCidade;
                    bairro.Nome = nomeBairro;

                    bairro.Inserir(bairro);
                        retValue = "Bairro Cadastrado com Sucesso";
                     
                }
                else
                    retValue = "O Bairro já esta Cadastrado";

            }
            catch (Exception ex) { retValue = ex.Message; }
            return retValue;
        }

        /// <summary>
        /// Apagar um Bairro
        /// </summary>
        /// <param name="idBairro">id do Bairro que será apagado</param>
        /// <returns>Retorna um texto informando se o Bairro foi ou não apagado</returns>
        public static string Apagar(int idBairro)
        {
            try
            {
                if (bairro.ValidarExistencia(idBairro) == true)
                {
                    if (bairro.Apagar(idBairro) > 0)
                        return "Item Apagado com Sucesso";
                    else
                        return "Erro ao Apagar, Tente Novamente";
                }
                else
                {
                    return "O ID do Bairro não existe.";
                }
            }
            catch (Exception ex) { return ex.Message; }

        }

        /// <summary>
        /// Edita as informações de um Bairro
        /// </summary>
        /// <param name="idBairro">id do Bairro a ser alterado</param>
        /// <param name="nomeBairro">Nome do Bairro que será alterado no Banco de Dados</param>
        /// <param name="idCidade">O id da Cidade a que o Bairro pertence, e que será alterado no Banco de Dados</param>
        /// <returns>Retorna um texto informando se o Bairro foi ou não alterado</returns>
        public static string Editar(int idBairro, string nomeBairro, int idCidade)
        {
            try
            {
                if (bairro.ValidarExistencia(nomeBairro))
                {
                    int intRet = bairro.Editar(idBairro, nomeBairro, idCidade);
                    if (intRet == 1)
                        return "Alterçoes salvas com Sucesso";
                    else
                        return "Erro ao Concluir as alterações, tente novamente";
                }
                else
                    return "Esse Bairro já Existe";
            }
            catch (Exception ex) { return ex.Message; }

        }


        /// <summary>Cria uma Tabela com todos os Bairros de uma Cidade</summary>
        /// <param name="idCidade">ID Cidade</param>
        /// <returns>Retorna uma DataTable com os dados da pesquisa</returns>
        public static DataTable Pesquisar(int idCidade)
        {
            dTable = new DataTable();
            dTable.Columns.Add("id", typeof(string));
            dTable.Columns.Add("nomeBairro", typeof(string));
            try
            {
                foreach (Bairro b in bairro.Pesquisar(idCidade))
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["id"] = b.Id.ToString();
                    dRow["nomeBairro"] = b.Nome;

                    dTable.Rows.Add(dRow);
                }


            }
            catch (Exception b)
            {
                DataRow dRow = dTable.NewRow();
                dRow["id"] = b.GetHashCode();
                dRow["nomeBairro"] = b.Message;
                dTable.Rows.Add(dRow);
            }
            return dTable;

        }

        /// <summary>Preenche uma DropDownList com uma lista de Bairros de uma Determinada Cidade</summary>
        /// <param name="ddlBairro">DropDownList que Recebera os Bairro</param>
        /// <param name="idCidade">ID da Cidade em que o Bairro se localiza</param>
        /// <returns>Retorna DropDownList Preenchida</returns>
        public static DropDownList Pesquisar(DropDownList ddlBairro, int idCidade)
        {

            try
            {
                ddlBairro.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Selecione".ToUpper();
                li.Value = "0";

                ddlBairro.Items.Add(li);
                foreach (Bairro b in bairro.Pesquisar(idCidade))
                {
                    li = new ListItem();
                    li.Text = b.Nome.ToUpper();
                    li.Value = b.IdCidade.ToString() ;
                    ddlBairro.Items.Add(li);

                }

            }

            catch
            {
                throw;
            }
            return ddlBairro;

        }

        /// <summary>Preenche uma DropDownList com uma lista de Bairros de uma Determinada Cidade</summary>
        /// <param name="ddlBairro">DropDownList que Recebera os Bairro</param>
        /// <param name="idCidade">ID da Cidade em que o Bairro se localiza</param>
        /// <returns>Retorna DropDownList Preenchida</returns>
        public static DropDownList Pesquisar(DropDownList ddlBairro, DropDownList ddlBairro1, int idCidade)
        {

            try
            {
                ddlBairro.Items.Clear();
                ddlBairro1.Items.Clear();
                ListItem li = new ListItem();
                li.Text = "Selecione";
                li.Value = "0";

                ddlBairro.Items.Add(li);
                ddlBairro1.Items.Add(li);
                foreach (Bairro b in bairro.Pesquisar(idCidade))
                {
                    li = new ListItem();
                    li.Text = b.Nome.ToUpper();
                    li.Value = b.IdCidade.ToString();
                    ddlBairro.Items.Add(li);
                    ddlBairro1.Items.Add(li);
                }

            }

            catch  
            {
                throw;
            }
            return ddlBairro  ;

        }

    }
}
