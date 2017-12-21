using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ProjetoDS_Csharp.ViewModel
{
    public class UsuarioSistemaViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private Model.wpf_dbEntities db;
        private View.UsuarioSistemaUC _view;
        public UsuarioSistemaViewModel(View.UsuarioSistemaUC view)
        {
            db = new Model.wpf_dbEntities();
            this._view = view;
        }

        //Propriedades utilizadas na View             
        private ObservableCollection<Model.UsuarioSistema> _listaUsuarioSistema;

        public ObservableCollection<Model.UsuarioSistema> ListaUsuarioSistema
        {
            get { return _listaUsuarioSistema; }

            set     
            {                
                _listaUsuarioSistema = value;
                this.NotifyPropertyChanged("ListaUsuarioSistema");
            }
        }        
    }
}
