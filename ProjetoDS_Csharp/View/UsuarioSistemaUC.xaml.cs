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

namespace ProjetoDS_Csharp.View
{
    /// <summary>
    /// Interaction logic for UsuarioSistemaUC.xaml
    /// </summary>
    public partial class UsuarioSistemaUC : UserControl
    {
        Model.wpf_dbEntities db = new Model.wpf_dbEntities();
        Model.UsuarioSistema user;

        public UsuarioSistemaUC()
        {
            InitializeComponent();
            DataContext = new ViewModel.UsuarioSistemaViewModel(this);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {           
            this.txtLogin.Focus();
        }

        private void CarrgarUsuarioSistema()
        {
            dataGrid.ItemsSource = db.UsuarioSistemas.ToList();
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
                this.txtLogin.Text = string.Empty;
                this.txtSenha.Password = string.Empty;
                this.cbxPerfil.SelectedIndex = -1;
                this.txtNomeUsuario.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha de Autenticação",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalvar_Alterar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user == null)
                {
                    user = new Model.UsuarioSistema();
                }
                user.Login = this.txtLogin.Text;
                user.Senha = this.txtSenha.Password;
                user.Perfil = this.cbxPerfil.SelectedValue.ToString();
                user.Nome = this.txtNomeUsuario.Text;
                db.UsuarioSistemas.Add(user);
                db.SaveChanges();
                this.btnNovo_Click(sender, e);
                this.CarrgarUsuarioSistema();
                MessageBox.Show("Usuário salvo com sucesso.", "Informação", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha de Autenticação",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
