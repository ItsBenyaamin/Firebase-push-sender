using firebase_sender.frames;
using firebase_sender.interfaces;
using firebase_sender.models;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender.pages {
    /// <summary>
    /// Interaction logic for DialogPage.xaml
    /// </summary>
    public partial class DialogPage : Page, PushSendListener {
        private Util util;

        public DialogPage() {
            InitializeComponent();
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Hidden;
            util = new Util();
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) {
            if (image_link.Text != "" && title_txt.Text != "" && content_txt.Text != ""
                && button_txt.Text != "" && link_txt.Text != "") {
                MainPushModel mainModel = new MainPushModel();
                DialogModel dialog = new DialogModel();
                dialog.type = "dialog";
                dialog.buttontext = button_txt.Text;
                dialog.image = image_link.Text;
                dialog.title = title_txt.Text;
                dialog.uri = link_txt.Text;
                dialog.content = content_txt.Text;
                mainModel.data = dialog;
                util.PostRequest(JsonConvert.SerializeObject(mainModel), SendPushWindow.FireBaseKey, this);
            } else {
                MessageBox.Show("Please fill all of the fields", "Data Missed!");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = MainPage.instance;
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Visible;
        }

        public void onPushSendedSuccessFully() {
            SendPushWindow.frame_instance.Content = MainPage.instance;
        }
    }
}
