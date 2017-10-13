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
    public partial class ListaImagens : ContentPage
    {
        ImagensViewModel viewModel;
        public ListaImagens(double lat, double lng, int raio)
        {
            InitializeComponent();
            viewModel = new ImagensViewModel();
            this.BindingContext = viewModel;

            MostraImagens(lat, lng, raio);
        }

        public async void MostraImagens(double lat, double lng, int raio)
        {
            // mostra indicador de processamento em segundo plano e limpa dados da última busca
            viewModel.IsBusy = true;
            viewModel.ListaVazia = false;
            viewModel.Imagens.Clear();

            // consumir WebService do Google Place Photos para obter imagens
            WSGooglePlacesResponse resp = await MyWebServices.GetSearchAsync(lat, lng, raio);
            if (resp == null)
            {
                viewModel.IsBusy = false;
                viewModel.ListaVazia = true;
                return;
            }

            // verifica se foi encontrado alguma imagem
            if (resp.Results.Count == 0)
            {
                viewModel.IsBusy = false;
                viewModel.ListaVazia = true;
                return;
            }

            // percorre resultados para montar lista de imagens
            ImagensModel imgModel;
            object img;
            foreach (Result r in resp.Results)
            {
                if (r.Photos != null && r.Photos.Count > 0)
                {
                    foreach (Photo foto in r.Photos)
                    {
                        img = await MyWebServices.GetPhotoAsync(250, foto.Photo_reference);
                        imgModel = new ImagensModel(r.Name, foto.Html_attributions, r.Vicinity,
                                                    r.Geometry.GeoLocation, img, foto.Width, foto.Height,
                                                    foto.Photo_reference);

                        viewModel.Imagens.Add(imgModel);
                    }
                }
            }

            // esconde indicador de processamento em segundo plano
            viewModel.IsBusy = false;

            // indica se lista de imagens está vazia
            viewModel.ListaVazia = (viewModel.Imagens.Count == 0);
        }

        public void AtualizaImagemHD(string reference, object imgHD)
        {
            foreach (ImagensModel im in viewModel.Imagens)
            {
                if (im.Reference.CompareTo(reference) == 0)
                {
                    im.ImagemHD = imgHD;
                    return;
                }
            }
        }

        async void Imagem_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // obtem item selecionado
            ImagensModel item = (ImagensModel)e.Item;
            if (item == null)
                return;

            // mostra janela com detalhes
            DetalheImagem detPage = new DetalheImagem(this, item);
            await Navigation.PushAsync(detPage);
        }
    }
}