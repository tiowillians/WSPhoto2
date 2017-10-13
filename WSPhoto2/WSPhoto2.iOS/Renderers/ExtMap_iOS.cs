using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using WSPhoto2.CustomControls;
using WSPhoto2.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMap_iOS))]
namespace WSPhoto2.iOS.Renderers
{
    public class ExtMap_iOS : MapRenderer
    {
        private readonly UITapGestureRecognizer _tapRecogniser;

        public ExtMap_iOS()
        {
            _tapRecogniser = new UITapGestureRecognizer(OnTap)
            {
                NumberOfTapsRequired = 1,
                NumberOfTouchesRequired = 1
            };
        }

        private void OnTap(UITapGestureRecognizer recognizer)
        {
            var cgPoint = recognizer.LocationInView(Control);

            var location = ((MKMapView)Control).ConvertPoint(cgPoint, Control);

            ((ExtMap)Element).OnTap(new Position(location.Latitude, location.Longitude));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (Control != null)
                Control.RemoveGestureRecognizer(_tapRecogniser);

            base.OnElementChanged(e);

            if (Control != null)
                Control.AddGestureRecognizer(_tapRecogniser);
        }
    }
}
