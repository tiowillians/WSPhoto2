using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSPhoto2.Models;
using WSPhoto2.ViewModels;
using WSPhoto2.WebServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WSPhoto2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheImagem : ContentPage
    {
        DetalheViewModel viewModel;

        public DetalheImagem(ListaImagens pai, ImagensModel im)
        {
            InitializeComponent();
            viewModel = new DetalheViewModel(im);
            this.BindingContext = viewModel;

            ObtemImagemHD(pai, im);
        }

        public async Task ObtemImagemHD(ListaImagens pai, ImagensModel im)
        {
            if (im.ImagemHD == null)
            {
                object imgHD = await MyWebServices.GetPhotoAsync(
                                            (im.Width > im.Height ? im.Width : im.Height) / 2,
                                            im.Reference);
                if (imgHD != null)
                {
                    viewModel.Imagem = imgHD;
                    pai.AtualizaImagemHD(im.Reference, imgHD);
                    viewModel.InformaAlteracao("Imagem");
                }

                viewModel.IsBusy = false;
            }
        }
    }
}