namespace WinSync.Service
{
    public abstract class ConflictInfo
    {
        public ConflictType Type { get; set; }

        /// <summary>
        /// context where the conflict occurred
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 1: if the conflict occured in directory 1 of link 
        /// 2: if in directory 2
        /// </summary>
        public int ConflictPath { get; set; }

        /// <summary>
        /// additional info about conflict
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// create ConflictInfo
        /// </summary>
        /// <param name="type"></param>
        /// <param name="conflictPath"></param>
        /// <param name="context"></param>
        /// <param name="message"></param>
        protected ConflictInfo(ConflictType type, int conflictPath, string context, string message)
        {
            Type = type;
            ConflictPath = conflictPath;
            Context = context;
            Message = message;
        }

        public abstract string GetAbsolutePath();
    }
}
