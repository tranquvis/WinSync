using WinSync.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Service
{
    public abstract class SyncTask
    {
        protected readonly SyncInfo _si;

        /// <summary>
        /// create SyncTask for running a synchronisation
        /// </summary>
        /// <param name="l"></param>
        public SyncTask(SyncInfo si)
        {
            _si = si;
        }

        /// <summary>
        /// execute sanchronisation
        /// </summary>
        /// <returns></returns>
        public abstract void Execute();

        /// <summary>
        /// cancel synchronisation
        /// this might take some time until all running task have finished
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// pause synchronisation
        /// this might take some time until all running task have finished
        /// </summary>
        public void Pause()
        {
            _si.Paused = true;
        }

        /// <summary>
        /// resume synchronisation after pause
        /// </summary>
        public void Resume()
        {
            _si.Paused = false;
        }

        public abstract int TasksRunning(); 
    }
}
