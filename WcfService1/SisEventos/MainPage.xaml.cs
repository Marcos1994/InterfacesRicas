using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using SisEventos.Resources;
using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using SisEventos.ServiceReference1;

namespace SisEventos
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Service1Client ws;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
			if(App.Phone != null && App.Phone.Length > 0) GoToIndex();
			ws = new Service1Client();
			ws.ContatoAdicionarCompleted += Ws_ContatoAdicionarCompleted;
        }

		private void Ws_ContatoAdicionarCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			GoToIndex();
		}

		private void button_Click(object sender, RoutedEventArgs e)
        {
			App.Phone = App.Nome = "";
            if (loginTxt.Text != string.Empty && usernameTxt.Text != string.Empty)
			{
				App.Phone = loginTxt.Text;
				App.Nome = usernameTxt.Text;

				ws.ContatoAdicionarAsync(loginTxt.Text, usernameTxt.Text, "uri");
			}
			else
				MessageBox.Show("Digite nome e numero");
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (App.Phone != null && App.Phone.Length > 0) GoToIndex();
		}

		private void GoToIndex()
		{
			NavigationService.Navigate(new Uri("/ParticipandoEventos.xaml", UriKind.Relative));
		}
    }
}