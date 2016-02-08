using System.Collections.Generic;

namespace WinSync.Service
{
    public interface ISyncListener
    {

        /*
        void OnFileFound(MyFileInfo fi);

        void OnDirFound(MyDirInfo di);

        /// <summary>
        /// is called when a detect task of a file started
        /// </summary>
        void OnDetectingFileStarted(MyFileInfo fi);
        
        /// <summary>
        /// is called when a detect task of a file finished and a change has been detected
        /// </summary>
        /// <param name="fi">detected file information</param>
        void OnFileChangeDetected(MyFileInfo fi);
        
        /// <summary>
        /// is called when a file has been copied or deleted
        /// </summary>
        /// <param name="fi">synced file information</param>
        void OnFileSynced(MyFileInfo fi);

        /// <summary>
        /// is called when a directory has been created or removed
        /// </summary>
        /// <param name="di">synced directory information</param>
        void OnDirSynced(MyDirInfo di);

        /// <summary>
        /// is called when a directory or file conflict occurred
        /// </summary>
        /// <param name="ci">conflict information</param>
        void OnConflicted(ConflictInfo ci);
        */

        /// <summary>
        /// is called when the sync state of a directory or file changed
        /// </summary>
        /// <param name="sei">sync info of the element</param>
        void OnSyncElementStateChanged(SyncElementInfo sei);
        
        /// <summary>
        /// is called when a log message has been received
        /// </summary>
        /// <param name="message"></param>
        void OnLog(LogMessage message);
    }
}
