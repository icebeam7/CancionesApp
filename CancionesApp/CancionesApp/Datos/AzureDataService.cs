using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Linq;
using System.IO;
using CancionesApp.Clases;
using System.Collections.Generic;

namespace CancionesApp.Datos
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Canciones> tablaCanciones;

        bool isInitialized;

        public AzureDataService()
        {
            Initialize();
        }

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            SQLitePCL.Batteries.Init();
            MobileService = new MobileServiceClient("http://canciones-app.azurewebsites.net");

            var path = "syncstore-canciones-v01.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Canciones>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            tablaCanciones = MobileService.GetSyncTable<Canciones>();
            isInitialized = true;
        }

        public async Task<IEnumerable<Canciones>> ObtenerCanciones()
        {
            await Initialize();
            await SyncCanciones();
            return await tablaCanciones.ToListAsync();
        }

        public async Task<Canciones> ObtenerCancionID(string id)
        {
            await Initialize();
            await SyncCanciones();
            return (await tablaCanciones.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Canciones> AgregarCancion(string cancion, string artista)
        {
            try
            {
                await Initialize();

                var item = new Canciones
                {
                    NombreCancion = cancion,
                    NombreArtista = artista
                };

                await tablaCanciones.InsertAsync(item);
                await SyncCanciones();
                return item;
            }
            catch (Exception ex)
            {
                return new Canciones();
            }
        }

        public async Task<Canciones> ModificarCancion(string id, string cancion, string artista)
        {
            await Initialize();

            var item = await ObtenerCancionID(id);
            item.NombreCancion = cancion;
            item.NombreArtista= artista;

            await tablaCanciones.UpdateAsync(item);
            await SyncCanciones();
            return item;
        }

        public async Task EliminarCancion(string id)
        {
            await Initialize();

            var item = await ObtenerCancionID(id);
            await tablaCanciones.DeleteAsync(item);
            await SyncCanciones();
        }

        public async Task SyncCanciones()
        {
            try
            {
                await tablaCanciones.PullAsync("Canciones", tablaCanciones.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
