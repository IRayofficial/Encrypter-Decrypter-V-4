using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Encoder_Decoder_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int[] PrivateKey;
        public string PublicKey;
        public string PartnerPublicKey;
        public string SharedKey;

        HellmanKeyExchange Exchanger;
        Encryption encrypt = new Encryption();
        public MainWindow()
        {
            InitializeComponent();

            Exchanger = new HellmanKeyExchange();
            PrivateKey = Exchanger.initPrivateKey(encrypt.AlphabetNew());
            PublicKey = Exchanger.generatePublicKey(PrivateKey, encrypt.AlphabetNew());
            PKey.Text = PublicKey;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PKey2.Text) || 
                PKey2.Text == "Key Missing or invalid" || 
                PKey2.Text.Length != Exchanger.KeyLenght)
            {
                PKey2.Text = "Key Missing or invalid";
            }
            else
            {
                PartnerPublicKey = PKey2.Text;
                SharedKey = Exchanger.getSharedKey(PartnerPublicKey, PrivateKey, encrypt.AlphabetNew());
                LoginWindow.Visibility = Visibility.Hidden;
                ChatWindow.Visibility = Visibility.Visible;
            }
        }

        private void EButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EncryptText.Text))
            {
                DecryptText.Text = new string(encrypt.encodeKey(EncryptText.Text, SharedKey));
                EncryptText.Text = "";
            }
        }

        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DecryptText.Text))
            {
                EncryptText.Text = new string(encrypt.decodeKey(DecryptText.Text, SharedKey));
                DecryptText.Text = "";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PartnerPublicKey = "";
            SharedKey = "";
            LoginWindow.Visibility = Visibility.Visible;
            ChatWindow.Visibility = Visibility.Hidden;
        }
    }
}