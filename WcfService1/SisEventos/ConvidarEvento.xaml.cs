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
	public partial class ConvidarEvento : PhoneApplicationPage
	{
		private Service1Client ws;
		private int idEvento;

		public ConvidarEvento()
		{
			InitializeComponent();
			ws = new Service1Client();
			ws.EventoAbrirCompleted += Ws_EventoAbrirCompleted;
			ws.EventoConvidarParticipanteCompleted += Ws_EventoConvidarParticipanteCompleted;
		}

		private void Ws_EventoConvidarParticipanteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			string uri = string.Format("/DescricaoEvento.xaml?id={0}", idEvento);
			NavigationService.Navigate(new Uri(uri, UriKind.Relative));
		}

		private void Ws_EventoAbrirCompleted(object sender, EventoAbrirCompletedEventArgs e)
		{
			titulo.Text = e.Result.nome;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			idEvento = Convert.ToInt32(NavigationContext.QueryString["id"]);
			ws.EventoAbrirAsync(idEvento);
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			ws.EventoConvidarParticipanteAsync(idEvento, numeroConvidado.Text);
		}
	}
}