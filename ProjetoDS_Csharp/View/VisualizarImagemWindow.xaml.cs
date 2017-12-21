using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProjetoDS_Csharp.View
{
    /// <summary>
    /// Interaction logic for visualizarImagemWindow.xaml
    /// </summary>
    public partial class visualizarImagemWindow : Window
    {
        private IList<string> localDocumento;
        private int index = 0;
        private int quantidadeRegistro;
        public visualizarImagemWindow(IList<string> localDocumento)
        {
            InitializeComponent();
            this.localDocumento = localDocumento;
            this.quantidadeRegistro = localDocumento.Count;
            imgDocumentos.Source = CarregandoImagem(localDocumento[index].ToString());
        }

        private BitmapImage CarregandoImagem(string value)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(value);
            bitmap.EndInit();
            return bitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEsquerda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                index -= 1;
                if (index >= 0)
                {
                    imgDocumentos.Source = CarregandoImagem(localDocumento[index].ToString());
                }
                else
                {
                    index = quantidadeRegistro - 1;
                    imgDocumentos.Source = CarregandoImagem(localDocumento[index].ToString());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDireita_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                index += 1;
                if (quantidadeRegistro > index)
                {                    
                    imgDocumentos.Source = CarregandoImagem(localDocumento[index].ToString());
                }
                else
                {
                    index = 0;
                    imgDocumentos.Source = CarregandoImagem(localDocumento[index].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
