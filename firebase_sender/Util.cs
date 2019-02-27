using firebase_sender.interfaces;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace firebase_sender {
    class Util {
        public void PostRequest(string content, string key, PushSendListener listener) {
            try {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                request.ContentType = "application/json";
                request.Headers.Add("Authorization:key=" + key);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                if (responseFromServer.Contains("message_id")) {
                    listener.onPushSendedSuccessFully();
                    MessageBox.Show("Push sended successfully :)", "Operation Successful");
                }
                Console.Write(responseFromServer);
                reader = null;
                response = null;
                dataStream = null;
            } catch (Exception eee) {
                MessageBox.Show("Something wrong! Push cannot be send.", "Operation Failed!");
                Console.Write(eee.Message);
            }
        }
    }
}
