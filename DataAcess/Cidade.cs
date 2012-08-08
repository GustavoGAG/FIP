using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ferramentas;

namespace DataAcess
{
    partial class Cidade
    {
        private static int intRet = new int();



        public int Inserir(Cidade cidade)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    fip.Cidade.AddObject(cidade);
                    intRet = fip.SaveChanges();
                }

            }
            catch
           (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }
            return intRet;

        }

        public int Apagar(int idCidade)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    Cidade cidade = (from c in fip.Cidade
                                     where c.Id == idCidade
                                     select c).First();

                    fip.Cidade.DeleteObject(cidade);
                    intRet = fip.SaveChanges();
                }

            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }
            return intRet;

        }

        public int Editar(int idCidade,string nomeCidade)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    Cidade cid = (from c in fip.Cidade
                                  where c.Id == idCidade
                                  select c).First();

                    cid.Nome = nomeCidade;
                    //cid.OnNomeChanging(nomeCidade);
                    fip.Cidade.ApplyCurrentValues(cid);
                    intRet = fip.SaveChanges();

                }

            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }
            return intRet;

        }

        public int InserirXLS(string localArquivo)
        {
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {

                    ConvertXSL xls = new ConvertXSL();
                   xls.ListaTabelas = xls.Ler(localArquivo);

                   foreach(DataRow i in xls.ListaTabelas[0].Rows)
                    {

                        int id;
                        Cidade cid = new Cidade();
                        int.TryParse(i.ItemArray[0].ToString(),out id);
                        if(id > 0)
                        {
                            cid.Id = id;
                            cid.Nome = i.ItemArray[1].ToString();
                            cid.IdEstado = Convert.ToInt32(i.ItemArray[2].ToString());
                            fip.Cidade.AddObject(cid);
                        }

                    }
                    int linhasAfetadas = fip.SaveChanges();

                    return linhasAfetadas;
                }

            }
            catch
            {
                throw;
            }


        }
  
        #region Verificar Existencia

        public bool ValidarExistencia(string nomeCidade,int idCidade)
        {
            bool existe = false;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Cidade> cidade = from c in fip.Cidade
                                                where ((c.Id == idCidade) && (c.Nome == nomeCidade))
                                                select c;
                    if(cidade != null && cidade.Count() > 0)
                        existe = true;
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }



            return existe;
        }

        public bool ValidarExistencia(int idEstado,string nomeCidade)
        {
            bool existe = false;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Cidade> cidade = from c in fip.Cidade
                                                where ((c.IdEstado == idEstado) && (c.Nome == nomeCidade))
                                                select c;
                    if(cidade != null && cidade.Count() > 0)
                        existe = true;
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }



            return existe;
        }

        public bool ValidarExistencia(int idCidade)
        {
            bool existe = false;
            try
            {
                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    IQueryable<Cidade> cidade = from c in fip.Cidade
                                                where ((c.Id == idCidade))
                                                select c;
                    if(cidade != null && cidade.Count() > 0)
                        existe = true;
                }
            }
            catch
            (Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }



            return existe;
        }
        #endregion

        #region Consultas

        public List<Cidade> Pesquisar(int idCidade,string nomeCidade)
        {
            IQueryable<Cidade> cidade;

            try
            {
                using(Context fip = new Context())
                {
                    cidade = from c in fip.Cidade
                             where c.Id == idCidade && c.Nome == nomeCidade
                             select c;
                }
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }
            return cidade.ToList();

        }

        public List<Cidade> Pesquisar(string nomeCidade)
        {
            IQueryable<Cidade> cidade;

            try
            {
                using(Context fip = new Context())
                {
                    cidade = from c in fip.Cidade
                             where c.Nome == nomeCidade
                             select c;
                }
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }
            return cidade.ToList();

        }

        public List<Cidade> Pesquisar(int idEstado)
        {


            try
            {
                using(Context fip = new Context())
                {
                    IQueryable<Cidade> cidade = from c in fip.Cidade
                                                where c.IdEstado == idEstado
                                                orderby c.Nome ascending
                                                select c;

                    return cidade.ToList();
                }
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }



        }

        public List<Cidade> Pesquisar(string nomeCidade,int idEstado)
        {
            IQueryable<Cidade> cidade;

            try
            {
                using(Context fip = new Context())
                {
                    cidade = from c in fip.Cidade
                             where c.IdEstado == idEstado && c.Nome == nomeCidade
                             select c;
                    return cidade.ToList();
                }
            }
            catch(Exception e)
            {
                ArgumentException argExc = new ArgumentException(e.Message,e.Source,e.InnerException);
                throw argExc;
            }


        }
        #endregion

    }
}
