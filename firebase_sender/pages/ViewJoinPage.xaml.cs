using firebase_sender.frames;
using firebase_sender.interfaces;
using firebase_sender.models;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender.pages {
    /// <summary>
    /// Interaction logic for ViewJoinPage.xaml
    /// </summary>
    public partial class ViewJoinPage : Page, PushSendListener {
        private Util util;

        public ViewJoinPage() {
            InitializeComponent();
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Hidden;
            util = new Util();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = MainPage.instance;
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Visible;
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) {
            if (join_id.Text != "" || view_id.Text != "") {
                MainPushModel mainModel = new MainPushModel();
                ViewJoinModel viewjoin = new ViewJoinModel();
                viewjoin.join = join_id.Text;
                viewjoin.view = view_id.Text;
                mainModel.data = viewjoin;
                util.PostRequest(JsonConvert.SerializeObject(mainModel), SendPushWindow.FireBaseKey, this);
            } else {
                MessageBox.Show("Please fill one of the field", "Data Missed!");
            }
        }

        public void onPushSendedSuccessFully() {
            SendPushWindow.frame_instance.Content = MainPage.instance;
        }
    }
}
