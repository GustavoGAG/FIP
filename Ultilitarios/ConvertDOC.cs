using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace Ultilitarios
{
    public class ConvertDOC
    {

        #region Propriedades
        private Word.Application objWord;
        private Word.Document doc;

        #endregion


        #region Construtor
        public ConvertDOC()
        {
            try
            {
                objWord = new Word.Application();
                objWord.Documents.Add(Type.Missing, Type.Missing, Type.Missing, true);
                objWord.DefaultSaveFormat = ".doc";
                doc = (Word.Document)objWord.Documents[1];
            }
            catch { }
        }

        #endregion


        #region Metodos

        public void Salvar()
        {
            try
            {
                #region  Define o Item de Cabecalho
                objWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageHeader;

                string logoPath = "C:\\Users\\Américo\\PeopleOrganizer\\Gratificacoes\\DocumentExport\\media\\logo.png ";

                Word.Shape logo = objWord.Selection.HeaderFooter.Shapes.AddPicture
                    (logoPath, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing
                    , Type.Missing, Type.Missing);

                logo.Select(Type.Missing);
                logo.Name = "logoHeader";
                logo.Left = (float)Word.WdShapePosition.wdShapeLeft;


                objWord.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument;
                #endregion
                   
                //objWord.Visible = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                Fechar();
            }

        }

        public void Fechar()
        {
            try
            {
                
                objWord.Quit(Type.Missing, Type.Missing, Type.Missing);


            }
            catch { throw; }

        }
        #endregion

    }
}
