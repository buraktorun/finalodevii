using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finalödevi
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UrunleriYukle();
        }

        private void UrunleriYukle()
        {
            dtgProducts.ItemsSource = _productDal.UrunleriGetir();
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            _productDal.Ekle(new product
            {
                UrunAd = txtUrunAd.Text,
                UrunFıyat = Convert.ToDecimal(txtUrunFıyat.Text),
                UrunMıktar = Convert.ToInt32(txtUrunMıktar.Text),
                Umarka = txtUrunMarka.Text
            });
            UrunleriYukle();
        }

        private void dtgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product product = new product();
            product = (product)dtgProducts.SelectedItem;
            grd1.DataContext = product;
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = string.Empty;
            txtUrunAd.Text = string.Empty;
            txtUrunFıyat.Text = string.Empty;
            txtUrunMıktar.Text = string.Empty;
            txtUrunMarka.Text = string.Empty;
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            _productDal.Guncelle(new product
            {
                Id = Convert.ToInt32(txtId.Text),
                UrunAd = txtUrunAd.Text,
                UrunFıyat = Convert.ToDecimal(txtUrunFıyat.Text),
                UrunMıktar = Convert.ToInt32(txtUrunMıktar.Text),
                Umarka = txtUrunMarka.Text
            });
            UrunleriYukle();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            _productDal.Sil(id);
            UrunleriYukle();
        }

        private void txtAra_SelectionChanged(object sender, RoutedEventArgs e)
        {
            dtgProducts.ItemsSource = _productDal.UrunleriGetir().Where(x => x.UrunAd.ToLower().Contains(txtAra.Text.ToLower())).ToList();
        }

        private void txtUrunMarka_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
