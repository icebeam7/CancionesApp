using System;
using Xamarin.Forms;
using CancionesApp.Clases;

namespace CancionesApp.Paginas
{
	public partial class PaginaListaCanciones : ContentPage
	{
		public PaginaListaCanciones ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            actInd.IsRunning = true;
            lsvCanciones.ItemsSource = await App.AzureService.ObtenerCanciones();
            actInd.IsRunning = false;
        }

        private void lsvCanciones_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PaginaCancion pagina = new PaginaCancion();
                pagina.Cancion = e.SelectedItem as Canciones;
                Navigation.PushAsync(pagina);
            }
        }

        void btnNuevo_Click(object sender, EventArgs a)
        {
            PaginaCancion pagina = new PaginaCancion();
            pagina.Cancion = new Canciones() { Id = "" };
            Navigation.PushAsync(pagina);
        }
    }
}
