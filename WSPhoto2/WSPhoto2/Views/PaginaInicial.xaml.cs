using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WSPhoto2.Views
{
    public partial class PaginaInicial : ContentPage
    {
        public PaginaInicial()
        {
            InitializeComponent();

            // mostrar Goiânia/Brasil no mapa
            Position pos = new Position(-16.6794417, -49.2676241);
            mapVisualizacao.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(50)));
        }

        private async void MapVisualizacao_Tapped(object sender, CustomControls.MapTapEventArgs e)
        {
            // mostra janela com lista das imagens
            ListaImagens listaPage = new ListaImagens(e.Position.Latitude, e.Position.Longitude, (int)raioSlider.Value);
            await Navigation.PushAsync(listaPage);
        }

        private void RaioSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }
    }
}
