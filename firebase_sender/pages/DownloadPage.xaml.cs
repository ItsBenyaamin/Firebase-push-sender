using firebase_sender.frames;
using firebase_sender.interfaces;
using firebase_sender.models;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender.pages {
    /// <summary>
    /// Interaction logic for DownloadPage.xaml
    /// </summary>
    public partial class DownloadPage : Page, PushSendListener {
        private Util util;

        public DownloadPage() {
            InitializeComponent();
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Hidden;
            util = new Util();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = MainPage.instance;
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Visible;
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) {
            if (package_value.Text != "" && link_value.Text != "") {
                MainPushModel mainModel = new MainPushModel();
                DownloadModel download = new DownloadModel();
                download.package = package_value.Text;
                download.link = link_value.Text;
                mainModel.data = download;
                util.PostRequest(JsonConvert.SerializeObject(mainModel), SendPushWindow.FireBaseKey, this);
            } else {
                MessageBox.Show("Please fill all of the fields", "Data Missed!");
            }
        }

        public void onPushSendedSuccessFully() {
            SendPushWindow.frame_instance.Content = MainPage.instance;
        }
    }
}
