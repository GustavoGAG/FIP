using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO = DataAcess;
using System.Data.Odbc;
using System.Data;

namespace Business
{
    public static class UsuDeficienciaBus
    {

        private static DAO.UsuDeficiencia usuDeficiecia = new DAO.UsuDeficiencia();

        public static int inserir(List<int> idDeficiencia,int idUsuario)
        {
            try
            {
                DataTable dTable = new DataTable();
                dTable.Columns.Add("idDeficiencia");
                dTable.Columns.Add("idUsuario");

                foreach(int i in idDeficiencia)
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["idDeficiencia"] = i.ToString();
                    dRow["idUsuario"] = idUsuario.ToString();
                }


                return usuDeficiecia.Inserir(dTable);



            }
            catch
            {
                throw;
            }


        }

        public static void Inserir(long idUsuario,List<int> listaDefinciencia)
        {
            try
            {
                if(listaDefinciencia.Count > 0 && idUsuario != 0)
                {
                    usuDeficiecia = new DAO.UsuDeficiencia();
                    usuDeficiecia.Inserir(idUsuario,listaDefinciencia);

                }

            }
            catch(Exception)
            {

                throw;
            }
        }
        
        public static void ApagarAsDeficienciasDoUsuario(long idUsuario)
        {
            try
            {
                if (idUsuario > 0)
                {
                    usuDeficiecia = new DAO.UsuDeficiencia();
                    usuDeficiecia.Apagar(idUsuario);

                }
            }
            catch { throw; }

        }
 
    }
}
