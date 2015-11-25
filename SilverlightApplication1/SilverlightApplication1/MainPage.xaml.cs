using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1
{
	public partial class MainPage : UserControl
	{
		List<Sale> dataContext = new List<Sale>();
		public MainPage()
		{
			InitializeComponent();
			dataContext = new List<Sale>
			{
				new Sale {SalesPerson="Marcos",CompanyName="Contoso",SalesAmount=15000.0,SalesForecast=25000.0 },
				new Sale {SalesPerson="Marcos",CompanyName="Magie's Travels",SalesAmount=30000.0,SalesForecast=50000.0 },
				new Sale {SalesPerson="André",CompanyName="Joe's Tires",SalesAmount=50000.0,SalesForecast=70000.0 },
				new Sale {SalesPerson="Marcos",CompanyName="World Wide Traders",SalesAmount=75000.0,SalesForecast=55000.0 },
				new Sale {SalesPerson="André",CompanyName="Iono",SalesAmount=10000.0,SalesForecast=500.0 },
				new Sale {SalesPerson="Marcos",CompanyName="Nokes",SalesAmount=25000.0 },
				new Sale {SalesPerson="Medeiros",CompanyName="Bob's Repair",SalesAmount=15000.0},
				new Sale {SalesPerson="Marcos",CompanyName="Tara's Plumbing",SalesAmount=15000.0},
				new Sale {SalesPerson="André",CompanyName="Smitty Funs",SalesAmount=16000.0,SalesForecast=25000.0 },
				new Sale {SalesPerson="André",CompanyName="Granpeeda",SalesAmount=45000.0,SalesForecast=65000.0 },
				new Sale {SalesPerson="Marcos",CompanyName="Tyo",SalesAmount=10000.0,SalesForecast=500.0 },
				new Sale {SalesPerson="André",CompanyName="Weeebo",SalesAmount=11000.0,SalesForecast=15000.0 },
				new Sale {SalesPerson="André",CompanyName="Vic's Bagels",SalesAmount=15000.0,SalesForecast=1700.0 },
				new Sale {SalesPerson="Medeiros",CompanyName="Wrenchmaniacs",SalesAmount=10000.0,SalesForecast=500.0 },
				new Sale {SalesPerson="Medeiros",CompanyName="Fran Fixers",SalesAmount=10000.0,SalesForecast=500.0 },
			};
		}

		private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
		{
			PagedCollectionView pcv = new PagedCollectionView(dataContext);
			pcv.PageSize = 10;
			this.DataContext = pcv;
			this.searchSalesData.ItemsSource = dataContext.Select(a => a.SalesPerson);

			double minSalesAmout = dataContext.Min(a => a.SalesAmount);
			double maxSalesAmount = dataContext.Max(a => a.SalesAmount);
			double salesAmountDelta = maxSalesAmount - minSalesAmout;

			double minFontSize = 10.0;
			double maxFontSize = 30.0;

			double fontSizeDelta = maxFontSize - minFontSize;

			for (int i = 0; i < dataContext.Count; i++)
			{
				TextBlock textBlock = new TextBlock
				{
					Text = dataContext[i].CompanyName,
					Foreground = i % 2 == 0 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Blue),
					Margin = new Thickness(2),
					FontSize = minFontSize + dataContext[i].SalesAmount * fontSizeDelta / salesAmountDelta
				};
				this.wrapPanelSales.Children.Add(textBlock);
			}
		}

		private void searchSalesData_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string salesPerson = (sender as AutoCompleteBox).SelectedItem as string;
			if (salesPerson != null)
			{
				this.dgSales.ItemsSource = from s in dataContext where s.SalesPerson == salesPerson select s;
			}
			else
			{
				this.dgSales.ItemsSource = dataContext;
			}
		}

		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (this.dgSales != null)
			{
				this.dgSales.ItemsSource = from s in dataContext where s.SalesAmount >= this.sliderSalesAmount.Value && s.SalesForecast >= this.sliderSalesForecast.Value select s;
			}
		}
	}
}
