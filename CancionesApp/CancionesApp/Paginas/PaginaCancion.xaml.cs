using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CancionesApp.Clases;

namespace CancionesApp.Paginas
{
	public partial class PaginaCancion : ContentPage
	{
        public Canciones Cancion;

		public PaginaCancion ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Cancion != null)
                BindingContext = Cancion;
        }

        void btnGuardar_Click(object sender, EventArgs a)
        {
            string cancion = txtCancion.Text;
            string artista = txtArtista.Text;

            if (Cancion.Id == "")
                App.AzureService.AgregarCancion(cancion, artista);
            else
                App.AzureService.ModificarCancion(Cancion.Id, cancion, artista);

            Navigation.PopAsync();
        }

        void btnEliminar_Click(object sender, EventArgs a)
        {
            if (Cancion.Id != "")
            {
                App.AzureService.EliminarCancion(Cancion.Id);
                Navigation.PopAsync();
            }
        }
    }
}
