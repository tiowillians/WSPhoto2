using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using WSPhoto2.CustomControls;
using WSPhoto2.UWP.Renderers;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMap_UWP))]
namespace WSPhoto2.UWP.Renderers
{
    public class ExtMap_UWP : MapRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                MapControl map = Control as MapControl;
                map.MapTapped -= OnMapTapped;
            }

            if (e.NewElement != null)
            {
                MapControl map = Control as MapControl;
                map.MapTapped += OnMapTapped;
            }
        }

        //private void OnMapTapped(object sender, TappedRoutedEventArgs args)
        private void OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            ((ExtMap)Element).OnTap(new Position(args.Location.Position.Latitude, args.Location.Position.Longitude));
        }
    }
}
