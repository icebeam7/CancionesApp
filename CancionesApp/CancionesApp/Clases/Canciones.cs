using System;
using System.Collections.Generic;
using System.Text;

namespace CancionesApp.Clases
{
    public class Canciones
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string NombreCancion { get; set; }
        public string NombreArtista { get; set; }
    }
}
