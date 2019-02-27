using firebase_sender.frames;
using System;
using System.Windows;
using System.Windows.Controls;

namespace firebase_sender {
    /// <summary>
    /// Interaction logic for SendPushWindow.xaml
    /// </summary>
    public partial class SendPushWindow : Window {
        public static Frame frame_instance;
        public static Button BackButtonInstance;
        public static string FireBaseKey;

        public SendPushWindow(string firebase_key) {
            InitializeComponent();

            frame_instance = PushMainFrame;
            BackButtonInstance = BackButton;

            FireBaseKey = firebase_key;

            PushMainFrame.Content = new MainPage();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            Close();
            MainWindow.instance.Show();
        }

        private void Window_Closed(object sender, EventArgs e) {
            MainWindow.instance.Show();
        }
    }
}
