using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.ServiceModel;
using SisEventos.ServiceReference1;

namespace SisEventos
{
    public partial class CriarEvento : PhoneApplicationPage
    {
        private Service1Client ws;

        public CriarEvento()
        {
            InitializeComponent();
            ws = new Service1Client();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Não é possível utilizar mapas sem um token de acesso da WindowsPhoneStore (somente após publicar o app)

            //Geoposition geoposition = await new Geolocator().GetGeopositionAsync();
            //ws.EventoAdicionarAsync(textBox.Text, txtArea.Text, DateTime.Now, App.Phone, geoposition.Coordinate.Point.Position.Latitude, geoposition.Coordinate.Point.Position.Longitude);

            ws.EventoAdicionarAsync(textBox.Text, txtArea.Text, DateTime.Now, App.Phone, 1.0F, 1.0F);

            // Redireciona para a página de listagem de eventos em que o usuário está participando
            NavigationService.Navigate(new Uri("/ParticipandoEventos.xaml", UriKind.Relative));
        }
    }
}