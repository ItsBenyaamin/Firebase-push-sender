using firebase_sender.Dialogs;
using firebase_sender.interfaces;
using firebase_sender.models;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace firebase_sender {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, AppsActionListener {
        public static MainWindow instance;
        private List<AppsModel> userAppsList = new List<AppsModel>();
        private string configPath = "";
        private string path = "";



        public MainWindow() {
            InitializeComponent();
            instance = this;
            readConfigFile();
        }

        private void readConfigFile() {
            configPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/fire_config.txt";
            if (File.Exists(configPath)) {
                string config = read_from_text(configPath);
                if (isNotNull(config)) {
                    ConfigModel configData = deserializeConfig(config);
                    if (configData.dataFolder != "" || configData.dataFolder != null || configData.dataFolder != String.Empty) {
                        path = configData.dataFolder;
                    }
                }
            } else {
                ConfigModel model = new ConfigModel();
                model.dataFolder = "";
                write_to_text(configPath, serializeConfig(model));
                File.SetAttributes(configPath, FileAttributes.Hidden);
            }
            checkForFile();
        }

        private void checkForFile() {
            if (isNotNull(path)) {
                if (File.Exists(path))
                    getListofApps();
                else {
                    File.Create(path);
                }
            }
        }

        private void getListofApps() {
            string json = read_from_text(path);
            if (isNotNull(json))
                userAppsList = JsonConvert.DeserializeObject<List<AppsModel>>(json);

            foreach (AppsModel item in userAppsList)
                appsList.Items.Add(item.appName);

            checkForStuff();
        }

        private void addNewAppBtn_Click(object sender, RoutedEventArgs e) {
            new AddNewAppDialog(this).ShowDialog();
        }

        private void selectAppBtn_Click(object sender, RoutedEventArgs e) {
            if (appsList.SelectedItem != null) {
                foreach (AppsModel item in userAppsList) {
                    if (item.appName == appsList.SelectedItem.ToString()) {
                        new SendPushWindow(item.firebaseKey).Show();
                        Hide();
                        break;
                    }
                }
            }
        }

        private void removeAppBtn_Click(object sender, RoutedEventArgs e) {
            if (appsList.SelectedItem != null) {
                Console.WriteLine(appsList.SelectedItem.ToString());
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    foreach (AppsModel item in userAppsList) {
                        if (item.appName.Trim().Equals(appsList.SelectedItem.ToString().Trim())) {
                            this.onAppRemoved(item);
                            break;
                        }
                    }
                }
            }
        }

        public void onNewAppAdd(AppsModel app) {
            userAppsList.Add(app);
            appsList.Items.Add(app.appName);
            write_to_text(path, serializeAppsList());
            checkForStuff();
        }

        public void onAppRemoved(AppsModel app) {
            userAppsList.Remove(app);
            appsList.Items.Remove(app.appName);
            write_to_text(path, serializeAppsList());
            checkForStuff();
        }

        private void SelectDataAddressBtn_Click(object sender, RoutedEventArgs e) {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if(result == CommonFileDialogResult.Ok) {
                appsList.Items.Clear();
                userAppsList.Clear();
                ConfigModel configModel = new ConfigModel();
                configModel.dataFolder = dialog.FileName + "/fire_data.txt";
                path = configModel.dataFolder;
                write_to_text(configPath, serializeConfig(configModel));
                checkForFile();
            }else {
                if(isNotNull(path))
                    MessageBox.Show("You have to choose a Directory for save data!", "Directory Not Found!");
            }
            checkForStuff();
        }

        private void checkForStuff() {
            if (appsList.Items.Count == 0) {
                removeAppBtn.IsEnabled = false;
                selectAppBtn.IsEnabled = false;
            } else {
                removeAppBtn.IsEnabled = true;
                selectAppBtn.IsEnabled = true;
            }
            if (path == null || path == "" || path == String.Empty)
                addNewAppBtn.IsEnabled = false;
            else
                addNewAppBtn.IsEnabled = true;
        }

        private void write_to_text(string filePath, string content) {
            if (File.Exists(filePath)) 
                File.SetAttributes(filePath, FileAttributes.Normal);
            TextWriter tw = new StreamWriter(filePath, false);
            tw.Write(content);
            tw.Close();
            File.SetAttributes(filePath, FileAttributes.Hidden);
        }

        private string read_from_text(string filePath) {
            if (File.Exists(filePath)) {
                File.SetAttributes(filePath, FileAttributes.Normal);
                TextReader tr = new StreamReader(filePath);
                string content = tr.ReadToEnd();
                tr.Close();
                File.SetAttributes(filePath, FileAttributes.Hidden);
                return content;
            }
            return "";
        }

        private ConfigModel deserializeConfig(string configTXT) {
            return JsonConvert.DeserializeObject<ConfigModel>(configTXT);
        }

        private string serializeConfig(ConfigModel config) {
            return JsonConvert.SerializeObject(config);
        }

        private string serializeAppsList() {
            return JsonConvert.SerializeObject(userAppsList, Formatting.Indented);
        }

        private bool isNotNull(string val) {
            if (val == null || val == "" || val == String.Empty)
                return false;
            else
                return true;
        }
    }
}
