using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using WSPhoto2.Android.Renderers;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;
using WSPhoto2.CustomControls;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMap_Droid))]
namespace WSPhoto2.Android.Renderers
{
    /// <summary>
    /// Renderer for the xamarin map.
    /// Enable user to get a position by taping on the map.
    /// </summary>
    public class ExtMap_Droid : MapRenderer, IOnMapReadyCallback
    {
        // We use a native google map for Android
        private GoogleMap _map;

        public void OnMapReady(GoogleMap googleMap)
        {
            InvokeOnMapReadyBaseClassHack(googleMap);

            googleMap.UiSettings.ZoomControlsEnabled = Map.HasZoomEnabled;
            googleMap.UiSettings.ZoomGesturesEnabled = Map.HasZoomEnabled;
            googleMap.UiSettings.ScrollGesturesEnabled = Map.HasScrollEnabled;
            googleMap.MyLocationEnabled = Map.IsShowingUser;

            _map = googleMap;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (_map != null)
                _map.MapClick -= googleMap_MapClick;

            if (Control != null)
                ((MapView)Control).GetMapAsync(this);
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((ExtMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }

        private void InvokeOnMapReadyBaseClassHack(GoogleMap googleMap)
        {
            System.Reflection.MethodInfo onMapReadyMethodInfo = null;

            Type baseType = typeof(MapRenderer);
            foreach (var currentMethod in baseType.GetMethods(System.Reflection.BindingFlags.NonPublic |
                                                             System.Reflection.BindingFlags.Instance |
                                                              System.Reflection.BindingFlags.DeclaredOnly))
            {

                if (currentMethod.IsFinal && currentMethod.IsPrivate)
                {
                    if (string.Equals(currentMethod.Name, "OnMapReady", StringComparison.Ordinal))
                    {
                        onMapReadyMethodInfo = currentMethod;

                        break;
                    }

                    if (currentMethod.Name.EndsWith(".OnMapReady", StringComparison.Ordinal))
                    {
                        onMapReadyMethodInfo = currentMethod;

                        break;
                    }
                }
            }

            if (onMapReadyMethodInfo != null)
            {
                onMapReadyMethodInfo.Invoke(this, new[] { googleMap });
            }
        }
    }
}