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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private static Model.UsuarioSistema userr = null;
        public MenuWindow(Model.UsuarioSistema user)
        {
            InitializeComponent();
            if (user != null)
            {
                userr = user;
                this.lalNomeUsuario.Content ="Usuário: " + user.Nome;
                if (user.Perfil == "O")
                {
                    menuCadastro.Visibility = Visibility.Collapsed;
                }
            }
   
        }

        public static Model.UsuarioSistema InstanceUsuario
        {
            get { return userr; }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lalMenuUsuarioSistema_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                contentControl.Content = null;
                UsuarioSistemaUC usc = new UsuarioSistemaUC();
             
                contentControl.Content = usc;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                contentControl.Content = null;
                ImportaArquivosUC usc = new ImportaArquivosUC();

                contentControl.Content = usc;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
