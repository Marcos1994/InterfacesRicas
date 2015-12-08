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
using Microsoft.Phone.Tasks;

namespace SisEventos
{
	public partial class DescricaoEvento : PhoneApplicationPage
	{
		private Service1Client ws;
		private int idEvento;

		public DescricaoEvento()
		{
			InitializeComponent();
			ws = new Service1Client();
			ws.EventoAbrirCompleted += Ws_EventoAbrirCompleted;
		}

		private void Ws_EventoAbrirCompleted(object sender, EventoAbrirCompletedEventArgs e)
		{
			tituloEvento.Text = e.Result.nome;
			textBlock1.Text = e.Result.descricao;
			textBlock3.Text = "local do evento";
			listBox1.ItemsSource = e.Result.participantes;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			idEvento = Convert.ToInt32(NavigationContext.QueryString["id"]);
            ws.EventoAbrirAsync(idEvento);
		}

		private void EventoConvidar(object sender, RoutedEventArgs e)
		{
			string uri = string.Format("/ConvidarEvento.xaml?id={0}", idEvento);
			NavigationService.Navigate(new Uri(uri, UriKind.Relative));
		}

		private void RealizarChamada(object sender, System.Windows.Input.GestureEventArgs e)
		{
			try
			{
				if (listBox1.SelectedItem != null)
				{
					PhoneCallTask call = new PhoneCallTask();
					call.DisplayName = ((TextBlock)((StackPanel)sender).FindName("Nome")).Text;
					call.PhoneNumber = ((TextBlock)((StackPanel)sender).FindName("Numero")).Text;
					call.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Data + "\n\n" + ex.Message);
			}
		}
	}
}