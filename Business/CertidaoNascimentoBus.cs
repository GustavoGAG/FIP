using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAcess;
using Ferramentas;


namespace Business
{
    /// <summary>
    /// Classe de Negócio Certidão de Nascimento
    /// </summary>
    public static class CertidaoNascimentoBus
    {

        private static CertidaoNascimento certidao = new CertidaoNascimento();

        /// <summary>
        /// Insere os dados relativos a Certidão de Nascimento
        /// </summary>
        /// <param name="numero">Número da Certidão de Nascimento</param>
        /// <param name="livro">Livro</param>
        /// <param name="folha">Folha</param>
        /// <param name="cartorio">Cartório em que a Certidão foi feita</param>
        /// <param name="dataCertidao">Data em que a Certidão foi feita</param>
        /// <param name="idCidade">Cidade em que a Certidão foi feita</param>
        /// <returns>Retorna um texto informando se a Certidão de Nascimento foi ou não inserida com sucesso</returns>
        public static int Inserir(string numero, string livro, string folha, string cartorio, DateTime dataCertidao, int idCidade)
        {
            certidao = new CertidaoNascimento()
                           {
                               Cartorio = cartorio,
                               DataCertidao = dataCertidao,
                               Numero = numero,
                               Livro = livro,
                               Folha = folha,
                               IdCidade = idCidade
                           };

            if (!certidao.ValidarExistencia(certidao))
                return certidao.Inserir(certidao);
            else
                throw new AlertaException("Ja existe uma pessoa Cadastrada com essa Certidão");
        }

        /// <summary>
        /// Pesquisa e exibe informações de uma Certidão de Nascimento presente no Banco de Dados
        /// </summary>
        /// <param name="idCertidao">id da Certidão a ser exibida</param>
        /// <returns>Retorna uma DataTable com os dados da Certidão de Nascimento</returns>
        public static DataTable Pesquisar(int idCertidao)
        {
            DataTable dTable = new DataTable();
            //Campos da tabela Certidao de Nascimento
            dTable.Columns.Add("idCertidao", typeof(int));
            dTable.Columns.Add("cartorio", typeof(string));
            dTable.Columns.Add("dataCertidao", typeof(DateTime));
            dTable.Columns.Add("folha", typeof(string));
            dTable.Columns.Add("idCidade", typeof(int));
            dTable.Columns.Add("idEstado", typeof(int));
            dTable.Columns.Add("livro", typeof(string));
            dTable.Columns.Add("numero", typeof(string));

            try
            {

             CertidaoNascimento   c = certidao.Pesquisar(idCertidao);

                DataRow dRow = dTable.NewRow();
                dRow["idCertidao"] = c.Id;
                dRow["cartorio"] = c.Cartorio;
                dRow["dataCertidao"] = c.DataCertidao;
                dRow["folha"] = c.Folha;
                dRow["idCidade"] = c.IdCidade;

                dRow["livro"] = c.Livro;
                dRow["numero"] = c.Numero;
                dTable.Rows.Add(dRow);

            }

            catch (Exception e)
            {

                DataRow dRow = dTable.NewRow();
                dRow["cartorio"] = e.Message;
                dTable.Rows.Add(dRow);
            }

            return dTable;
        }


        public static void Apagar(int idCertidao)
        {
            try
            {
                if (idCertidao > 0)
                {
                    certidao = new CertidaoNascimento();
                    certidao.Apagar(idCertidao);

                }


            }
            catch (Exception)
            {

                throw;
            }


        }




    }
}
