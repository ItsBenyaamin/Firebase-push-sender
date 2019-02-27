using firebase_sender.pages;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender.frames {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        public static Page instance;
        public MainPage() {
            InitializeComponent();
            instance = this;
        }

        private void dialog_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new DialogPage();
        }

        private void dialog_app_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new DialogAppPage();
        }

        private void download_hidden_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new DownloadPage();
        }

        private void Sorush_type_btnd_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new SingleFieldPage("soroush");
        }

        private void Twitter_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new SingleFieldPage("twitter");
        }

        private void view_join_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new ViewJoinPage();
        }

        private void popup_type_btn_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new SingleFieldPage("popup");
        }

        private void instagram_type_btnd_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new SingleFieldPage("instagram");
        }

        private void custom_json_btn_Copy_Click(object sender, RoutedEventArgs e) {
            
        }

        private void ussd_type_btn_Copy_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = new SingleFieldPage("ussd");
        }
    }
}
