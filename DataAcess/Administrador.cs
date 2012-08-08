using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class Administrador
    {
        private static int intRet = new int();

        #region Insert, Update e Delete

        /// <summary>Adiciona uma novo Administrador no Banco</summary>
        /// <param name="adm">Novo Objeto Administrador preenchido</param>
        /// <returns>Retorna o ID do Administrador, -1 caso erro</returns>
        public int Inserir(Administrador adm)
        {
            try
            {
                using (Context fip = new Context())
                {
                    fip.Administrador.AddObject(adm);
                    fip.SaveChanges();
                    intRet = adm.Id ;

                }
                return intRet;
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.InnerException);
                throw argExc;
            }
            
        }

        /// <summary>Edita as informacoes do Objeto no Banco</summary>
        /// <param name="adm">Objeto Administrador</param>
        /// <returns>Retorna o numero de Administrador Alterado, -1 caso erro</returns>
        public int Editar(Administrador adm)
        {
            try
            {
                using (Context fip = new Context())
                {
                    foreach (Administrador a in PesquisarID(adm.Id ))
                    {
                        a.Telefone = adm.Telefone;
                        a.Celular = adm.Celular;
                        a.DataNascimento = adm.DataNascimento;

                        a.IdBairro = adm.IdBairro;
                        
                        a.Organizacao = adm.Organizacao;

                        a.Nome = adm.Nome;
                        a.Cpf = adm.Cpf;
                        a.Email = adm.Email;
                        intRet = fip.SaveChanges();

                    }

                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;

        }

        /// <summary>Edita o Login e Senha do Administrador</summary>
        /// <param name="idAdministador">ID do Administrador a ser Alterado</param>
        /// <param name="login">Login novo</param>
        /// <param name="senha">Senha nova</param>
        /// <returns>retorna 1 se sucesso e -1 se erro</returns>
        public int Editar(int idAdministador, string login, string senha)
        {
            try
            {
                using (Context fip = new Context())
                {
                    foreach (Administrador a in PesquisarID(idAdministador))
                    {
                        a.Login = login;
                        a.Senha = senha;
                        intRet = fip.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;
        }

        /// <summary>Desativa um Administrador do Sistema</summary>
        /// <param name="idAdministrador">ID do Administrador a ser Desabilitado</param>
        /// <param name="nomeLogado">Nome de quem Desabilitou</param>
        /// <returns>1 para Sucesso e -1 para Erro </returns>
        public int Apagar(int idAdministrador, string nomeLogado)
        {
            try
            {
                using (Context fip = new Context())
                {
                    foreach (Administrador a in PesquisarID(idAdministrador))
                    {
                        a.DataRemovido = DateTime.Now;
                        a.AutorRemoveu = nomeLogado;
                        a.Ativo = 0;
                        intRet = fip.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;
        }

        #endregion

        #region Consultas

        /// <summary>Obtem as informacoes de todos os Administrador</summary>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> Pesquisar()
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where a.Ativo == 1
                                                    select a;
                    return adm.ToList();

                }
            }
            catch 
            {
                 
                throw  ;
            }

        }


        #region Dados Pessoais UNIQUE
        /// <summary>Obtem as informacoes de um Administrador</summary>
        /// <param name="idAdministrador">ID do Administrador</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarID(int idAdministrador)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.Id  == idAdministrador) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch { return null; }

        }

        /// <summary>Obtem as informacoes de um Administrador</summary>
        /// <param name="cpf">CPF do Administrador</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarPorCpf(string cpf)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.Cpf == cpf) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

        }

        /// <summary>Obtem as informacoes de um Administrador</summary>
        /// <param name="email">Email do Administrador</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarPorEmail(string email)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.Email == email) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

        }

        /// <summary>Obtem as informacoes de um Administrador</summary>
        /// <param name="login">Login do Administrador no Sistema</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarPorLogin(string login)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.Login == login) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

        }
        #endregion

        #region Localidade

        /// <summary>Procura todos os Administradores de um Bairro</summary>
        /// <param name="idBairro">ID do Bairro</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarPorBairro(int idBairro)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.IdBairro == idBairro) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch 
            {
               
                throw  ;
            }

        }

     
        /// <summary>Procura todos os Administradores de um Estado</summary>
        /// <param name="idEstado">ID Estado</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public List<Administrador> PesquisarPorEstado(int idEstado)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.FK_Bairro.FK_Cidade.FK_Estado.Id == idEstado) && (a.Ativo == 1))
                                                    select a;
                    return adm.ToList();

                }
            }
            catch 
            {
                
                throw  ;
            }

        }

        /// <summary>Procura todos os Administradores de uma Organizao</summary>
        /// <param name="organizacao">Nome da Organizao</param>
        /// <returns>Retorna uma Interface IQueryble do Tipo Administrador</returns>
        public IQueryable<Administrador> PesquisarPorOrganizacao(string organizacao)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Administrador> adm = from a in fip.Administrador
                                                    where ((a.Organizacao == Organizacao) && (a.Ativo == 1))
                                                    select a;
                    return adm;

                }
            }
            catch { return null; }

        }

        #endregion

        #endregion

        #region Validar Existencia do Item

        /// <summary>Verifica se Existe um Administrador com esse ID</summary>
        /// <param name="idAdministrador">ID do Administrador</param>
        /// <returns>Retorna True caso exista e False se não</returns>
        public bool ValidarID(int idAdministrador)
        {
            Boolean boolRet = false;
            try
            {
                foreach (Administrador a in PesquisarID(idAdministrador))
                {
                    boolRet = true;
                }

            }
            catch { boolRet = false; }
            return boolRet;

        }

        /// <summary>Verifica se Existe um Administrador com esse Login</summary>
        /// <param name="login">Login do Administrador</param>
        /// <returns>Retorna True caso exista e False se não</returns>
        public bool ValidarLogin(string login)
        {
            Boolean boolRet = false;
            try
            {
                foreach (Administrador a in PesquisarPorLogin(login))
                {
                    boolRet = true;
                }

            }
            catch { boolRet = false; }
            return boolRet;

        }

        /// <summary>Verifica se Existe um Administrador com esse Email</summary>
        /// <param name="email">Email do Administrador</param>
        /// <returns>Retorna True caso exista e False se não</returns>
        public bool ValidarEmail(string email)
        {
            Boolean boolRet = false;
            try
            {
                foreach (Administrador a in PesquisarPorLogin(email))
                {
                    boolRet = true;
                }

            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return boolRet;

        }

        /// <summary>Verifica se Existe um Administrador com esse CPF</summary>
        /// <param name="cpf">CPF do Administrador</param>
        /// <returns>Retorna True caso exista e False se não</returns>
        public bool ValidarCpf(string cpf)
        {
            Boolean boolRet = false;
            try
            {
                foreach (Administrador a in PesquisarPorLogin(cpf))
                {
                    boolRet = true;
                }

            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return boolRet;

        }



        #endregion

    }
}
