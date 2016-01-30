using System.Collections.Generic;

namespace WinSync.Service
{
    public interface ISyncListener
    {

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
        /// is called when a file conflict occurred
        /// </summary>
        /// <param name="fi">conflicted file information</param>
        void OnFileConflicted(MyFileInfo fi);

        /// <summary>
        /// is called when a directory conflict occurred
        /// </summary>
        /// <param name="di">conflicted directory information</param>
        void OnDirConflicted(MyDirInfo di);

        /// <summary>
        /// is called when a log message has been received
        /// </summary>
        /// <param name="message"></param>
        void OnLog(LogMessage message);
    }
}
