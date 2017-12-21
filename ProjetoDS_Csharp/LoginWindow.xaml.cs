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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetoDS_Csharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            this.txtLogin.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.wpf_dbEntities db = new Model.wpf_dbEntities();
                Model.UsuarioSistema user = db.UsuarioSistemas.Where(x => x.Login == this.txtLogin.Text && x.Senha == this.txtSenha.Password).SingleOrDefault();
                if (user != null)
                {
                    var fw = new View.MenuWindow(user);
                    fw.Show();
                    this.Close();
                }
                else
                {
                    this.txtLogin.Text = string.Empty;
                    this.txtSenha.Password = string.Empty;
                    MessageBox.Show("Usuário ou senha esta incorreto.", "Falha de Autenticação", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha de Autenticação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha de Autenticação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }
    }
}
