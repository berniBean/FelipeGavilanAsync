using FelipeGavilan.Codigo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FelipeGavilan
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private HttpClient httpClient;
        public Form1()
        {
            InitializeComponent();
            loadingGif.Visible = false;
            apiURL = "http://localhost:30958/tarjetas";
            httpClient = new HttpClient();
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {

            
          await  new ProgressTarjeta(apiURL, psProgreso).btnIniciar_click(loadingGif);
        }


    }
}
