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
using EidSamples;

namespace SecureChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReadData data;

        public MainWindow()
        {
            InitializeComponent();
            data= new ReadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
           // MessageBox.Show(data.GetSurname());
           // MessageBox.Show(data.GetData("date_of_birth"));
            CryptoKeyGenerator keygen=new CryptoKeyGenerator();
            keygen.generateRSAKey();
            keygen.generateAESKey();
            keygen.Encrypt(encryptionTextBox.Text);



        }
    }
}
