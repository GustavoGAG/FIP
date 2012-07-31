using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAcess;
using Ultilitarios;
using MySql.Data.MySqlClient;

namespace Business
{
    /// <summary>
    /// Classe de Negócios Usuário
    /// </summary>
    public class UsuarioBus
    {
        #region Propriedades

        private static Usuario usuario;


        #endregion



        /// <summary>
        /// Inserir dados de um Usuário no Banco de Dados
        /// </summary>
        /// <param name="nome">Nome do Usuário</param>
        /// <param name="cpf">CPF do Usuário</param>
        /// <param name="dataNascimento">Data de Nascimento do Usuário</param>
        /// <param name="sexo">Sexo do Usuário</param>
        /// <param name="identidade">Identidade do Usuário</param>
        /// <param name="orgaoEmissor">Órgão Emissor da Identidade do Usuário</param>
        /// <param name="endereco">Endereço do Usuário</param>
        /// <param name="cep">CEP do Usuário</param>
        /// <param name="idCertidao">id da Certidão do Usuário</param>
        /// <param name="idPesquisa">id do Formulário de Pesquisa preenchido pelo Usuário</param>
        /// <param name="idEscola">id da Escola do Usuário</param>
        /// <param name="idMotivoMatricula">id das informações referentes ao Motivo da Matrícula preenchido pelo Usuário</param>
        /// <param name="idCidade">id da Cidade do Usuário</param>
        /// <param name="idStatus">id do Status do Usuário</param>
        /// <param name="idBairro">id do Bairro do Usuário</param>
        /// <param name="idEstado">id do Estado do Usuário</param>
        /// <param name="idResponsavel">id do Responsável do Usuário</param>
        /// <param name="nomePai">Nome do Pai do Usuário</param>
        /// <param name="nomeMae">Nome da Mãe do Usuário</param>
        /// <returns>Retorna um texto informando se o Usuário foi ou não cadastrado com sucesso</returns>
        public static long Inserir(
            string nome,string cpf,
            DateTime dataNascimento,string sexo,
            string identidade,string orgaoEmissor,
            string endereco,string cep,
            int idCertidao,int idPesquisa,
             int idMotivoMatricula,
            int idCidade,int idStatus,
            int idBairro,int idEstado,
            long idResponsavel,string nomePai,
            string nomeMae)
        {
            try
            {
                usuario = new Usuario()
                {

                    #region Propriedades
                    Nome = nome,
                    Cpf = cpf,
                    DataNascimento = dataNascimento,
                    Sexo = sexo,
                    Identidade = identidade,
                    OrgaoEmissor = orgaoEmissor,
                    Endereco = endereco,
                    Cep = cep,
                    IdCertidao = idCertidao,
                    IdPesquisa = idPesquisa,
                    IdMotivoMatricula = idMotivoMatricula,
                    IdCidade = idCidade,
                    IdStatus = idStatus,
                    IdBairro = idBairro,
                    IdEstado = idEstado,
                    IdResponsavel = idResponsavel,
                    NomeMae = nomeMae,
                    NomePai = nomePai,
                    DataCadastro = DateTime.Now
                    #endregion
                };
                //Se o Usuario nao existir cadastro no banco
                if(!usuario.ValidarExistencia(usuario))
                    return usuario.Inserir();

                throw new AlertaException("Você já esta cadastrado");

            }
            #region Exception
            catch(MySqlException er)
            {
                throw new AlertaException(er.Message);

            }
            catch(UpdateException upEx)
            {
                throw new AlertaException(upEx.Message);
            }
            catch(Exception)
            {
                throw;
            }
            #endregion

        }


        public static void Apagar(long idUsuario)
        {
            try
            {
                if(idUsuario > 0)
                    usuario.Apagar(idUsuario);
            }
            catch
            {
                throw;
            }

        }


        #region Consultas

        public static DataTable Pesquisar(long id)
        {
            try
            {
                usuario = new Usuario();
                usuario.Pesquisar(id);
                return usuario.Imprimir();


            }

            catch
            {
                throw;
            }

        }

        public static DataTable Pesquisar(string cpf,DateTime nascimentoAluno)
        {
            try
            {
                if(cpf.Count() == 14 & nascimentoAluno > Convert.ToDateTime("01/01/1960"))
                {
                    usuario = new Usuario();
                    usuario.Pesquisar(cpf,nascimentoAluno);
                    return usuario.Imprimir();
                }
                else
                    throw new AlertaException("Verifique se o CPF e a data de Nascimento estao corretas");
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Estatistica()
        {
            DataTable dTable = new DataTable();
            try
            {

                dTable.Columns.Add("nome").DataType = Type.GetType("System.String");

                dTable.Columns.Add("qtd").DataType = Type.GetType("System.Int32");
                
                Estatistica estatistica = new DataAcess.Estatistica();


                var grupo = estatistica.AgruparPorEstado();

                foreach(var i in grupo)
                {
                    DataRow dr = dTable.NewRow();
                    dr["qtd"] = i.QtdUsuario;
                    dr["nome"] = i.Nome;
                    dTable.Rows.Add(dr);

                }
            }
            catch
            {
                throw;
            }
            return dTable;
        }
        #endregion

    }


}
