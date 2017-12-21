using DRR.Framework.Util;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetoDS_Csharp.View
{
    /// <summary>
    /// Interaction logic for ImportaArquivosUC.xaml
    /// </summary>
    public partial class ImportaArquivosUC : UserControl
    {
        // Cria o OpenFileDialog
        Microsoft.Win32.OpenFileDialog openFileDialog;


        Model.wpf_dbEntities db = new Model.wpf_dbEntities();
        Model.ImportaDocumento doc;

        public ImportaArquivosUC()
        {
            InitializeComponent();
            dtpRegistroCadastro.Text = DateTime.Now.ToShortDateString();
            CarrgarImportDocumentos();
        }

        private void CarrgarImportDocumentos()
        {
            dataGrid.ItemsSource = db.ImportaDocumentos.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pIdentificador"></param>
        private void ProcessandoDocumentos(string pIdentificador)
        {
            try
            {
                // Criar a pasta para salvar documentos
                String source = System.Environment.CurrentDirectory.ToString();
                DRR.Framework.Util.FileDirectoryOptions.Create(source + "\\Envios", TypeFileDirectory.Directory, false);
                DRR.Framework.Util.FileDirectoryOptions.Create(source + "\\Envios" + "\\" + DateTime.Now.ToString("yyyyMMdd"), TypeFileDirectory.Directory, false);
                
                string file = source + "\\Envios" + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\"+DateTime.Now.ToString("yyyyMMdd") + pIdentificador;
                // Compactando os documentos
                CompactandoDocumento(file + ".zip");

                // Gerando o json
                Formatting formatacao = Formatting.Indented;
                string jsonSerializado = JsonConvert.SerializeObject(doc, formatacao);
                System.IO.File.WriteAllText(file + ".json", jsonSerializado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// http://netcoders.com.br/manipulacao-de-arquivos-zip-com-sharpziplib/
        /// </summary>
        /// <param name="souceZip"></param>
        private void CompactandoDocumento(string souceZip)
        {
            try
            {
                using (ZipFile arquivoZip =
                    ZipFile.Create(souceZip))
                {
                    arquivoZip.BeginUpdate();

                    foreach (string item in openFileDialog.FileNames)
                    {
                        arquivoZip.Add(item, item.Substring(item.LastIndexOf("\\") + 1));
                    }

                    arquivoZip.CommitUpdate();
                    arquivoZip.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelecionarArquivos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Image files (*.bmp, *.jpeg, *.gif, *.png, *.tiff) | *.bmp; *.jpeg; *.gif; *.png; *.tiff";
                openFileDialog.Multiselect = true;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Selecionar Documentos";
                if (openFileDialog.ShowDialog() == true)
                {
                    string caminho = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVizualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (openFileDialog.FileNames != null && openFileDialog.FileNames.Length != 0)
                {
                    var fw = new visualizarImagemWindow(openFileDialog.FileNames);
                    //fw.DataContext = funcionario;
                    fw.ShowDialog();
                }
                else
                {
                    throw new Exception("Necessário selecionar imagens para vizualizar.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtNumeroDocumento.Text = string.Empty;
                this.txtNomeDocumento.Text = string.Empty;
                this.chkArquivoMorto.IsChecked = false;
                this.dtpRegistroCadastro.Text = DateTime.Now.ToShortDateString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Alterar_Click(object sender, RoutedEventArgs e)
        {
            using (DbContextTransaction tran = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    if (doc == null)
                    {
                        doc = new Model.ImportaDocumento();
                    }
                    doc.NumeroDocumento = Convert.ToInt64(this.txtNumeroDocumento.Text);
                    doc.Nome = this.txtNomeDocumento.Text;
                    if (this.chkArquivoMorto.IsChecked == true)
                    {
                        doc.ArquivoMortoSN = true;
                    }
                    else
                    {
                        doc.ArquivoMortoSN = false;
                    }

                    doc.DataRegistro = Convert.ToDateTime(this.dtpRegistroCadastro.Text);
                    doc.UsuarioSistemaId = View.MenuWindow.InstanceUsuario.Id;


                    db.ImportaDocumentos.Add(doc);
                    db.SaveChanges();
                    this.ProcessandoDocumentos(doc.Id.ToString());

                    this.btnNovo_Click(sender, e);                    
                    MessageBox.Show("Documento(s) salvo(s) com sucesso.", "Informação",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    tran.Commit();
                    this.CarrgarImportDocumentos();
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                finally
                {
                    tran.Dispose();
                }
            }

        }
    }
}
