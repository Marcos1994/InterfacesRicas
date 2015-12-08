using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SisEventos.ServiceReference1;

namespace SisEventos
{
    public partial class ParticipandoEventos : PhoneApplicationPage
    {
        private Service1Client ws;

        public ParticipandoEventos()
        {
            InitializeComponent();
            ws = new Service1Client();
            ws.ContatoEventosCompleted += ContatoEventosCompleted;
			ws.ContatoEventosAsync(App.Phone);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ws.ContatoEventosAsync(App.Phone);
        }

        private void ContatoEventosCompleted(object sender, ContatoEventosCompletedEventArgs e)
		{
            listBox1.ItemsSource = e.Result;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CriarEvento.xaml", UriKind.Relative));
        }

		private void AbrirEvento(object sender, System.Windows.Input.GestureEventArgs e)
		{
			string id = ((TextBlock)((StackPanel)sender).FindName("ID")).Text;
			string uri = string.Format("/DescricaoEvento.xaml?id={0}", id);
			NavigationService.Navigate(new Uri(uri, UriKind.Relative));
		}
	}
}