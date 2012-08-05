using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ferramentas;

namespace DataAcess
{
    partial class CertidaoNascimento
    {

        private static int intRet = new int();

        //Criar. Todos os parâmetros serão passados para a inserção no BD
        public int Inserir(CertidaoNascimento certidao)
        {
            using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
            {
                fip.CertidaoNascimento.AddObject(certidao);
                fip.SaveChanges();

                return certidao.Id;
            }
        }

        //Os dados serão apagados a partir de um parâmetro passado (provavelmente id)
        public int Apagar(int idCertidao)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    var c = (from ce in fip.CertidaoNascimento
                             where ce.Id == idCertidao
                             select ce).FirstOrDefault();

                    fip.CertidaoNascimento.DeleteObject(c);
                    intRet += fip.SaveChanges();


                }
            }
            catch
            {
                throw;

            }
            return intRet;

        }

        /// <summary>
        /// Salva as alterações das informaçoes de uma Certidao
        /// </summary>
        /// <param name="certidao">Objeto a ser alerado ja com as novas informaçoes</param>
        /// <returns></returns>
        public int Editar(CertidaoNascimento certidao)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    CertidaoNascimento c = certidao.Pesquisar(certidao.Id);

                    c.IdCidade = certidao.IdCidade;

                    c.Numero = certidao.Numero;
                    c.Folha = certidao.Folha;
                    c.Cartorio = certidao.Cartorio;
                    c.DataCertidao = certidao.DataCertidao;
                    c.Livro = certidao.Livro;
                    intRet += fip.SaveChanges();

                }

            }
            catch
             (Exception e)
            {
                throw new ArgumentException(e.Message,e.Source,e.InnerException);

            }
            return intRet;

        }


        public bool ValidarExistencia(CertidaoNascimento certidao)
        {
            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    var lstCertidao = (from c in fip.CertidaoNascimento
                                       where ((c.Cartorio == certidao.Cartorio) && (c.Livro == certidao.Livro)
                                       && (c.Numero == Cartorio) && (c.Folha == certidao.Folha))
                                       select c).FirstOrDefault();

                    if(lstCertidao != null && lstCertidao.Id > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch
            {
                throw;
            }


        }

        /// <summary>Exibe dados da certidão de nascimento </summary>
        /// <param name="idCertidao">ID da Certidao de Nascimento</param>
        /// <returns>Lista IQueryble do tipo CertidaoNascimento</returns>
        public CertidaoNascimento Pesquisar(int idCertidao)
        {

            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    return (from c in fip.CertidaoNascimento
                            where c.Id == idCertidao
                            select c).FirstOrDefault();

                }
            }
            catch
            (Exception e)
            {
                throw new ArgumentException(e.Message,e.Source,e.InnerException);

            }


        }

    }
}
