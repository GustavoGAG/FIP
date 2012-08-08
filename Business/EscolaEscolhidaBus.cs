using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcess;

namespace Business
{
    public class EscolaEscolhidaBus
    {
        private static EscolaEscolhida escola;

        public static void Inserir(long idUsuario, int escolaOrigem, int escolaDestino, int idSerieEscolar)
        {
            escola = new EscolaEscolhida();
            try
            {
                escola.IdUsuario = idUsuario;
                escola.IdEscolaDestino = escolaDestino;
                escola.IdEscolaOrigem = escolaOrigem;
                escola.IdSerieAtual = idSerieEscolar;

                escola.Inserir(escola);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ApagarAsEscolasEscolhidasPeloUsuario(long idUsuario)
        {
            try
            {
                escola = new EscolaEscolhida();
                escola.Apagar(idUsuario);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
