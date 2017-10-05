using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSPhoto2.Models;

namespace WSPhoto2.ViewModels
{
    public class ImagensViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ImagensModel> Imagens { get; set; }

        private bool _isBusy = false;
        private bool _listaVazia = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public void InformaAlteracao(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                InformaAlteracao("IsBusy");
            }
        }

        public bool ListaVazia
        {
            get { return _listaVazia; }
            set
            {
                if (_listaVazia == value)
                    return;

                _listaVazia = value;
                InformaAlteracao("ListaVazia");
            }
        }

        public ImagensViewModel()
        {
            ListaVazia = false;
            IsBusy = true;

            // Imagens não pode ser null
            Imagens = new ObservableCollection<ImagensModel>();
        }
    }
}
