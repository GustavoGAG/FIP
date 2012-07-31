using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultilitarios;

namespace DataAcess
{
    public class Estatistica
    {

        #region Propriedades

        private string nome;
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        private int qtdUsuario;
        public int QtdUsuario
        {
            get
            {
                return qtdUsuario;
            }
            set
            {
                qtdUsuario = value;
            }
        }
        #endregion

        #region Contrutor
        public Estatistica()
        {
            this.nome = "";
            this.qtdUsuario = 0;
        }

        public Estatistica(string nome,int qtdUsuario)
        {
            this.nome = nome;
            this.qtdUsuario = qtdUsuario;
        }
        #endregion



        public List<Estatistica> AgruparPorEstado()
        {
            string conexao = Context.ObterStringConexaoWebConfig();
            try
            {
              
                using(Context fip = new Context(conexao))
                {

                    var grupo = (from u in fip.Usuario
                                 group u by
                                     u.FK_EstadoDoUsuario.Nome
                                     into g
                                     select new Estatistica()
                                     {
                                         nome = g.Key,
                                         qtdUsuario = g.Count()

                                     }).ToList();

                    return grupo;

                }

            }
            catch
            {
                throw;
            }

        }
    }
}
