using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Mysql = MySql.Data.MySqlClient;
using System.Data;
namespace DataAcess
{
    partial class UsuDeficiencia
    {
        private static int intRet = new int();

        private static DataTable dTable;
        protected static DataTable Tabela
        {
            get
            {
                dTable = new DataTable();
                dTable.Columns.Add("idRelacao");

                dTable.Columns.Add("idDeficiencia");
                dTable.Columns.Add("nomeDeficiencia");

                dTable.Columns.Add("idUsuario");
                dTable.Columns.Add("nomeUsuario");
                return dTable;

            }
        }


        public int Inserir(long idUsuDeficiencia,List<int> idDeficiencia)
        {
            try
            {
                using(Context fip = new Context())
                {
                    UsuDeficiencia usuDeficiencia = new UsuDeficiencia();
                    foreach(int i in idDeficiencia)
                    {
                        usuDeficiencia = new UsuDeficiencia();
                        usuDeficiencia.IdDeficiencia = i;
                        usuDeficiencia.IdUsuario = idUsuDeficiencia;
                        fip.UsuDeficiencia.ApplyCurrentValues(usuDeficiencia);
                    }

                    fip.AcceptAllChanges();
                    intRet = fip.SaveChanges();

                }

            }
            catch
            {
                throw;
            }
            return intRet;
        }

        public int Inserir(DataTable tabelaItems)
        {
            /*  try
             {
                 using (Context fip = new Context())
                 {
                     tabelaItems.TableName = "UsuDeficiencia";
                     Mysql.MySqlConnection con =
                         new Mysql.MySqlConnection(fip.Connection.ConnectionString);
                     Mysql.MySqlBulkLoader mBulk = new Mysql.MySqlBulkLoader(con);
                     mBulk.TableName = "UsuDeficiencia";
                     mBulk.LineTerminator = ";";
                     mBulk.ConflictOption = Mysql.MySqlBulkLoaderConflictOption.Ignore;
                     int i = mBulk.Timeout;
                     intRet = mBulk.Load();


                     // fip.AcceptAllChanges();
                     //intRet = fip.SaveChanges();

                 }

             }
             catch { throw; }
           */
            return 1;

        }

        public int Apagar(int idDeficiencia,long idUsuario)
        {
            try
            {
                using(Context fip = new Context())
                {
                    IQueryable<UsuDeficiencia> lu = from ud in fip.UsuDeficiencia
                                                    where idDeficiencia == ud._IdDeficiencia && ud._IdUsuario == _IdUsuario
                                                    select ud;
                    foreach(UsuDeficiencia ud in lu)
                    {
                        fip.UsuDeficiencia.DeleteObject(ud);

                    }
                    intRet += fip.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
            return intRet;

        }

        public void Apagar(long idUsuario)
        {
            try
            {
                using (Context fip = new Context())
                {
                    IQueryable<UsuDeficiencia> lu = from ud in fip.UsuDeficiencia
                                                    where  ud._IdUsuario == _IdUsuario
                                                    select ud;
                    foreach (UsuDeficiencia ud in lu)
                    {
                        fip.UsuDeficiencia.DeleteObject(ud);

                    }
                    intRet = fip.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
             

        }


        #region Consultas

        public DataTable Imprimir(List<UsuDeficiencia> lu)
        {

            try
            {
                //  using (Context fip = new Context())

                //{

                dTable = new DataTable();
                dTable.TableName = "Deficiencia";
                foreach(var item in lu)
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["idRelacao"] = item.Id;
                    dRow["idDeficiencia"] = item.IdDeficiencia;
                    dRow["nomeDeficiencia"] = item.FK_DeficienciaDoUsuario.Nome;
                    dRow["idUsuario"] = item.FK_UsuarioDeficiente.Id;
                    dRow["nomeUsuario"] = item.FK_UsuarioDeficiente.Nome;
                    dTable.Rows.Add(dRow);
                }
                return dTable;
                //  }
            }
            catch
            {
                throw;
            }


        }



        #endregion

    }
}
