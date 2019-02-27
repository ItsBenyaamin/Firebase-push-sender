using firebase_sender.models;

namespace firebase_sender.interfaces {
    public interface AppsActionListener {
        void onNewAppAdd(AppsModel app);
        void onAppRemoved(AppsModel app);
    }
}
