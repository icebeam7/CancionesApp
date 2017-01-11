using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using CancionesApp.Datos;
using CancionesApp.Paginas;

namespace CancionesApp
{
	public class App : Application
	{
        public static AzureDataService AzureService = new AzureDataService();

        public App ()
		{
            SQLitePCL.Batteries.Init();
            MainPage = new NavigationPage(new PaginaListaCanciones());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
