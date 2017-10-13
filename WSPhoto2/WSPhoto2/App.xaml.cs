using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WSPhoto2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // página inicial da aplicação (com barra de navegação)
            Views.PaginaInicial pagInicial = new Views.PaginaInicial();
            MainPage = new NavigationPage(pagInicial)
            {
                BarBackgroundColor = Color.DarkSeaGreen,
                BarTextColor = Color.White
            };

            NavigationPage.SetBackButtonTitle(MainPage, "");
            NavigationPage.SetHasBackButton(MainPage, true);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
