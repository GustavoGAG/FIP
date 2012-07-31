using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Ultilitarios
{
    public class ConvertXSL:FormatarTabela
    {

        #region Propriedades

        #region Privadas

        //Apicativo Excel
        private Excel.Application _excelApp;

        //Cria um arquivo
        private Excel.Workbook _arquivoExcel;

        //Cria uma Planilha
        private Excel.Worksheet _planilha;

        /// <summary>Nome de Cada Planilha da pasta de trabalho do Excel</summary>
        private List<string> _nomesPlanilhas;

        private int _qtdLinhas;
        #endregion

        #region Publicas

        protected Excel.Application excelApp
        {
            get
            {
                return _excelApp;
            }

        }

        public Excel.Workbook arquivoExcel
        {
            get
            {
                return _arquivoExcel;
            }


            set
            {
                _arquivoExcel = value;
            }
        }

        public List<string> NomesPlanilhas
        {
            get
            {
                return _nomesPlanilhas;
            }
            set
            {
                _nomesPlanilhas = value;
            }
        }

        public int QtdLinhas
        {
            get
            {
                return _qtdLinhas;
            }
            set
            {
                _qtdLinhas = value;
            }

        }

        public string NomeArquivo;


        #endregion

        #endregion


        /// <summary>Inicializa um objeto do Excel para ser utilizado nas operaçoes</summary>
        public ConvertXSL()
        {
            _excelApp = new Excel.Application()
            {
                DisplayAlerts = false
            };

            _excelApp.DefaultSaveFormat = Excel.XlFileFormat.xlExcel12;
            _excelApp.DefaultFilePath = "c:\\";
            _excelApp.DefaultWebOptions.OrganizeInFolder = true;
            _excelApp.DefaultWebOptions.RelyOnCSS = true;
            _excelApp.DefaultWebOptions.UpdateLinksOnSave = true;
            _excelApp.WarnOnFunctionNameConflict = true;

            listaTabelas = new List<DataTable>();
            NomeArquivo = "";
            _nomesPlanilhas = new List<string>();

            _arquivoExcel = (Excel.Workbook)_excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            //   _arquivoExcel.WebOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
            _arquivoExcel.KeepChangeHistory = false;
            _arquivoExcel.ConflictResolution = Excel.XlSaveConflictResolution.xlUserResolution;
            //_arquivoExcel.Name = "PO - Export";
            _planilha = (Excel.Worksheet)_arquivoExcel.Worksheets[1];

        }

        /// <summary>Converte um DataTable para um arquivo do Excel</summary>
        public void Salvar()
        {


            Excel.Range rangeAllCelulas = _planilha.UsedRange.Cells;

            try
            {
                int contatorPlanilha = 0;

                foreach(System.Data.DataTable tabela in listaTabelas)
                {
                    //Se não houver deixado o nome em branco escreve
                    if(this._nomesPlanilhas.Count > contatorPlanilha)
                        _planilha.Name = _nomesPlanilhas[contatorPlanilha];

                    if((_qtdLinhas > tabela.Rows.Count) || (_qtdLinhas == 0))
                        _qtdLinhas = tabela.Rows.Count;



                    #region Cabecalho



                    //Definindo a area de cabecalho
                    Excel.Range uColuna = _planilha.Cells[1,tabela.Columns.Count];
                    Excel.Range pColuna = _planilha.Cells[1,1];
                    Excel.Range cabecalho = _planilha.get_Range(pColuna,uColuna);

                    cabecalho.Merge(Type.Missing);

                    //Propriedades
                    cabecalho.Borders.Color = 2;
                    cabecalho.Cells.Interior.Color = 18.0;

                    //Centraliza o texto 
                    cabecalho.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    cabecalho.Cells.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    //Nome de Cabeçalho
                    cabecalho.Cells[1,1] = "Cabecalho";


                    #endregion

                    #region Titulo

                    int columns = 1;

                    foreach(DataColumn item in tabela.Columns)
                    {
                        Excel.Range cel = _planilha.Cells[2,columns];

                        _planilha.Cells[2,columns] = item.ColumnName;

                        //Define a cor da linha das colunas
                        cabecalho.Cells.Interior.Color = Excel.XlRgbColor.rgbAqua;

                        //Define o tamanho da letra
                        cel.Cells.Font.Size = 12.0;

                        //Faz com que as celulas se adaptem ao texto
                        cel.EntireColumn.AutoFit();

                        //Define o stilo de borda
                        cel.Borders.LineStyle = BorderStyle.FixedSingle;

                        //Define a cor da borda
                        cel.Borders.Color = Excel.XlRgbColor.rgbBlack;

                        //Pinta as linhas intercaladas com cores diferentes
                        cel.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;
                        columns++;
                    }

                    #endregion

                    #region  Linhas
                    int coluna = 1; //O index das colunas da planilha começa com 1 
                    int linha = 3; //O index das linhas da planilha começa com 1
                    foreach(DataRow dRow in tabela.Rows)
                    {

                        for(coluna = 0; coluna < tabela.Columns.Count; coluna++)
                        {

                            string texto = dRow[coluna].ToString();


                            _planilha.Cells[linha,coluna + 1] = texto;

                            //Defini a celula
                            Excel.Range rangeLinha = _planilha.Cells[linha,coluna + 1];

                            //Define o tamanho da letra
                            rangeLinha.Cells.Font.Size = 12.0;

                            //Faz com que as celulas se adaptem ao texto
                            rangeLinha.EntireColumn.AutoFit();

                            //Define o stilo de borda
                            rangeLinha.Borders.LineStyle = BorderStyle.FixedSingle;

                            //Define a cor da borda
                            rangeLinha.Borders.Color = Excel.XlRgbColor.rgbBlack;

                            //Pinta as linhas intercaladas com cores diferentes
                            if(linha % 2 == 0)
                                rangeLinha.Interior.Color = Excel.XlRgbColor.rgbSilver;
                            else
                                rangeLinha.Interior.Color = Excel.XlRgbColor.rgbPurple;
                        }

                        linha++;

                        //se index de linhas for maior que as linhas da tabela o laço de repetição é quebrado 
                        if((linha - 1) > _qtdLinhas)
                            break;

                    }
                    #endregion

                    contatorPlanilha++;
                    //Cria outra Planilha
                    if(contatorPlanilha < listaTabelas.Count)
                        this._planilha = (Excel.Worksheet)_arquivoExcel.Worksheets.Add(Type.Missing,Type.Missing,1,Type.Missing);

                }

                excelApp.SaveWorkspace(NomeArquivo);
            }
            catch
            {

                throw;


            }
            finally
            {
                Fechar();
            }

            return;
        }

        public List<DataTable> Ler(string LocalArquivo)
        {
            listaTabelas = new List<DataTable>();
            try
            {
                _arquivoExcel = (Excel.Workbook)_excelApp.Workbooks.Open(LocalArquivo,0,true,5,"","",true,Excel.XlPlatform.xlWindows,"\t",false,false,0,true,null,null);

                int arq = 1;
                foreach(var item in _arquivoExcel.Sheets)
                {
                    DataTable dTable = new DataTable();

                    _planilha = (Excel.Worksheet)_arquivoExcel.Sheets[arq];
                    int linha = 1;
                    foreach(Excel.Range linhaPlanilha in _planilha.UsedRange.Rows)
                    {

                        DataRow dRow = dTable.NewRow();
                        int colunaDrow = 0;
                        foreach(Excel.Range rColuna in linhaPlanilha.Cells)
                        {
                            if(linha == 1)
                            {
                                dTable.Columns.Add(rColuna.Text);
                                continue;
                            }
                            else
                                dRow[colunaDrow] = rColuna.Text;
                            colunaDrow++;
                        }
                        linha++;
                        if(linha > 1)
                            dTable.Rows.Add(dRow);


                    }
                    listaTabelas.Add(dTable);
                    arq++;
                    break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Fechar();
            }
            return listaTabelas;
        }

        public void Fechar()
        {

            _excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_arquivoExcel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_excelApp);
        }


    }


}

