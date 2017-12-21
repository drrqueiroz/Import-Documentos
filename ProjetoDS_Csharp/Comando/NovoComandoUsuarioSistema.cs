using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjetoDS_Csharp.Comando
{
    public class NovoComandoUsuarioSistema : ICommand
    {
        #region Fields

        
        private ViewModel.UsuarioSistemaViewModel m_ViewModel;

        #endregion

        #region Constructor

       /// <summary>
       /// 
       /// </summary>
       /// <param name="viewModel"></param>
        public NovoComandoUsuarioSistema(ViewModel.UsuarioSistemaViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// 
        /// </summary>
        public bool CanExecute(object parameter)
        {
                return true;
        }

        /// <summary>
        /// Actions to take when CanExecute() changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Inclui um novo clube ou altera um existente.
        /// </summary>
        public void Execute(object parameter)
        {
            m_ViewModel.Login = string.Empty;
            m_ViewModel.Senha = string.Empty;
            m_ViewModel.CodigoPerfil = string.Empty;
            m_ViewModel.Nome = string.Empty;
        }

        #endregion
    }
}
