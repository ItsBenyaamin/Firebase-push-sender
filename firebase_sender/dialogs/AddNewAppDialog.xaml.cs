using firebase_sender.interfaces;
using firebase_sender.models;
using System.Windows;

namespace firebase_sender.Dialogs {
    /// <summary>
    /// Interaction logic for AddNewAppDialog.xaml
    /// </summary>
    public partial class AddNewAppDialog : Window {
        private AppsActionListener mListener;

        public AddNewAppDialog(AppsActionListener listener) {
            InitializeComponent();
            this.mListener = listener;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e) {
            if (AppNameTxt.Text != "" && FireBaseKeyTxt.Text != "") {
                AppsModel newApp = new AppsModel();
                newApp.appName = AppNameTxt.Text;
                newApp.firebaseKey = FireBaseKeyTxt.Text;
                mListener.onNewAppAdd(newApp);
                this.Close();
            }else {
                MessageBox.Show("Please fill the form :)", "Validation Error!");
            }
        }
    }
}
