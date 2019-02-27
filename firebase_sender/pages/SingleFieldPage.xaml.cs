using firebase_sender.frames;
using firebase_sender.interfaces;
using firebase_sender.models;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender.pages {
    /// <summary>
    /// Interaction logic for SingleFieldPage.xaml
    /// </summary>
    public partial class SingleFieldPage : Page, PushSendListener{
        public string type;
        private Util util;


        public SingleFieldPage(string type) {
            InitializeComponent();
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Hidden;
            util = new Util();
            this.type = type;
            switch (type) {
                case "popup":
                    field_label.Content = "Enter Popup Link :";
                    break;
                case "twitter":
                    field_label.Content = "Enter Twitter ID :";
                    break;
                case "instagram":
                    field_label.Content = "Enter Instagram ID :";
                    break;
                case "soroush":
                    field_label.Content = "Enter Soroush ID :";
                    break;
                case "ussd":
                    field_label.Content = "Enter USSD Number :";
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            SendPushWindow.frame_instance.Content = MainPage.instance;
            SendPushWindow.BackButtonInstance.Visibility = Visibility.Visible;
        }

        private void send_btn_Click(object sender, RoutedEventArgs e) {
            if (field_value.Text != "") {
                MainPushModel mainModel = new MainPushModel();
                switch (type) {
                    case "popup":
                        PopupModel popup = new PopupModel();
                        popup.uri = field_value.Text;
                        mainModel.data = popup;
                        break;
                    case "twitter":
                    case "instagram":
                    case "soroush":
                    case "ussd":
                        SingleValueObject obj = new SingleValueObject();
                        obj.type = type;
                        obj.value = field_value.Text;
                        mainModel.data = obj;
                        break;
                }

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
