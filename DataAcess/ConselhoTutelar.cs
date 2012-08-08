using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcess
{
    partial class ConselhoTutelar
    {
        /// <summary>Variavel de Retorno de valores inteiro</summary>
        private static int intRet = new int();


        public int Inserir(ConselhoTutelar conselho)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.ConselhoTutelar.AddObject(conselho);
                    intRet += fip.SaveChanges();
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;

        }

        public int Editar(ConselhoTutelar conselho)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (ConselhoTutelar b in Pesquisar(conselho.Id))
                    {
                        b.Nome = conselho.Nome;
                        b.Telefone = conselho.Telefone;
                        
                        b.IdBairro = conselho.IdBairro;
                        b.Fax = conselho.Fax;
                        b.Cep = conselho.Cep;
                        b.Endereco = conselho.Endereco;
                        intRet += fip.SaveChanges();
                    }
                }
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            return intRet;
        }

        public int Apagar(int idConselho)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    foreach (ConselhoTutelar c in Pesquisar(idConselho))
                    {

                        fip.ConselhoTutelar.DeleteObject(c);
                        intRet += fip.SaveChanges();
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

        public List<ConselhoTutelar> Pesquisar()
        {
            IQueryable<ConselhoTutelar> conselho;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    conselho = from c in fip.ConselhoTutelar
                               select c;return conselho.ToList();
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ConselhoTutelar> PesquisarConselho()
        {
            IQueryable<ConselhoTutelar> conselho;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    conselho = from c in fip.ConselhoTutelar
                               //where c.Tipo == "1"
                               select c;
                }
            }
            catch { return null; }
            return conselho.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ConselhoTutelar> PesquisarCre()
        {
            IQueryable<ConselhoTutelar> cre;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    cre = from c in fip.ConselhoTutelar
                          //where c.Tipo == "2"
                          select c;
                }
            }
            catch { return null; }
            return cre.ToList();
        }

        public List<ConselhoTutelar> Pesquisar(int idConselho)
        {
            IQueryable<ConselhoTutelar> conselho;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    conselho = from c in fip.ConselhoTutelar
                               where c.Id  == idConselho
                               select c;
                    if (conselho.Count() <= 0)
                        return null;return conselho.ToList();
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }
            
        }

        /// <summary>Verifica se Existe algum Conselho Tutelar</summary>
        /// <param name="nome">Nome do Conselho Tutelar</param>
        /// <param name="telefone">Telefone de contato</param>
        /// <param name="endereco">Endereco da de localização</param>
        /// <returns>Retorna True Caso Exista valor</returns>
        public bool ValidarExistencia(string nome, string telefone, string endereco)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<ConselhoTutelar> conselho = from c in fip.ConselhoTutelar
                                                           where ((c.Nome == nome) || (c.Telefone == telefone) || (Endereco == endereco))
                                                           select c;
                   //Se não hover valor false
                    if (conselho.Count() <= 0)
                        return false;
                    else
                        //Se houver alguma linha retora True
                        return true;
                }
            }
            catch (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message, e.Source, e.InnerException);
                throw argExc;
            }

        }






    }
}
