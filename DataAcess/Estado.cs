using System.Collections.Generic;
using System.Linq;
using System.Data;
using Ultilitarios;

using System;
namespace DataAcess
{


    public partial class Estado
    {

        public List<Estado> ListarEstados()
        {
            try
            {
                
                string Conexao = Context.ObterStringConexaoWebConfig();
                using(Context fip = new Context(Conexao))
                {
                    IQueryable<Estado> estado = from e in fip.Estado
                                                select e;

                    List<Estado> est = estado.ToList();
         

                    return est;


                }
            }
            catch
            {
                throw;
            }

        }


        public int Atualizar()
        {

            try
            {

                using(Context fip = new Context(Context.ObterStringConexaoWebConfig()))
                {
                    List<DataTable> dt = new List<DataTable>();
                    ConvertXSL xls = new ConvertXSL();
                    dt = xls.Ler("C:\\Users\\GAG\\Desktop\\Estados.xls");


                    foreach(DataRow i in dt[0].Rows)
                    {
                        Estado estado = new Estado();
                        int id;
                        int.TryParse(i.ItemArray[0].ToString(),out id);
                        if(id > 0)
                        {
                            estado.Id = id;
                            estado.Nome = i.ItemArray[1].ToString();
                            estado.Sigla = i.ItemArray[2].ToString();
                            fip.Estado.AddObject(estado);
                        }
                    }
                    int linhasAfetadas = fip.SaveChanges();
                    int? tempo = fip.CommandTimeout;

                    return linhasAfetadas;
                }


            }
            catch
            {
                throw;
            }


        }

    }
}
