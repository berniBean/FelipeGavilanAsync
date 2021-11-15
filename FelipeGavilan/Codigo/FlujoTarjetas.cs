using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FelipeGavilan.Codigo
{
    public class FlujoTarjetas
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public FlujoTarjetas(string apiUrl)
        {
            this.apiURL = apiUrl;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_click(PictureBox loadingGif)
        {
            loadingGif.Visible = true;

            var tarjetas = await ObtenerTarjetasDeCredito(1000);
            var sttopwatch = new Stopwatch();
            sttopwatch.Start();

            try
            {
                await ProcesarTarjetas(tarjetas);
            }
            catch (HttpRequestException ex)
            {

                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operación finalizada en {sttopwatch.ElapsedMilliseconds / 1000.0} segundos");

            loadingGif.Visible = false;

        }

        private async Task ProcesarTarjetas(List<string> tarjetas)
        {
            using var semaforo = new SemaphoreSlim(100);
            var tareas = new List<Task<HttpResponseMessage>>();

            tareas = tarjetas.Select(async tarjeta => {
                var json = JsonConvert.SerializeObject(tarjeta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await semaforo.WaitAsync();
                try
                {
                    return await httpClient.PostAsync(apiURL, content);
                }
                finally
                {

                    semaforo.Release();
                }

            }).ToList();



           var respuestas = await Task.WhenAll(tareas);

            var tarjetasRechazadas = new List<string>();

            foreach (var item in respuestas)
            {
                var contenido = await item.Content.ReadAsStringAsync();
                var respuestaTarjeta = JsonConvert.DeserializeObject<respuestaTarjetas>(contenido);

                if (!respuestaTarjeta.Aprobada)
                {
                    tarjetasRechazadas.Add(respuestaTarjeta.Tarjeta);
                }
            }

            foreach (var item in tarjetasRechazadas)
            {
                Console.WriteLine(item);
            }
        }

        private async Task<List<string>> ObtenerTarjetasDeCredito(int cantidadDeTarjetas)
        {
            var tarjetas = new List<string>();
            return await Task.Run(() =>
            {
                for (int i = 0; i < cantidadDeTarjetas; i++)
                {
                    tarjetas.Add(i.ToString().PadLeft(16, '0'));
                }

                return tarjetas;
            });



        }

    }
}
