using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSPhoto2.Models;
using WSPhoto2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WSPhoto2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheImagem : ContentPage
    {
        DetalheViewModel viewModel;

        public DetalheImagem(ImagensModel img)
        {
            InitializeComponent();
            viewModel = new DetalheViewModel(img);
            this.BindingContext = viewModel;
        }
    }
}