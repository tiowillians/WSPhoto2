using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WSPhoto2.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // mostrar Goiânia/Brasil no mapa
            Position pos = new Position(-16.6794417, -49.2676241);
            mapVisualizacao.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(50)));
        }

        private async void mapVisualizacao_Tapped(object sender, CustomControls.MapTapEventArgs e)
        {
            // mostra janela com lista das imagens
            ListaImagens listaPage = new ListaImagens(e.Position.Latitude, e.Position.Longitude);
            await Navigation.PushAsync(listaPage);
        }
    }
}
