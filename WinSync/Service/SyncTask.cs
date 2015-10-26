using WinSync.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncTask
    {
        readonly SyncInfo _si;
        AsyncSyncService _service;
        readonly Link _l;

        /// <summary>
        /// create SyncTask for running a synchronisation
        /// </summary>
        /// <param name="l"></param>
        public SyncTask(Link l)
        {
            _l = l;
            _si = l.SyncInfo;
        }

        /// <summary>
        /// execute sanchronisation async
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            _si.SyncStarted();
            _service = new AsyncSyncService(_si);
            try
            {
                await Task.Run(() => _service.ExecuteSync());
                _si.SyncFinished();
            }
            catch (AggregateException e)
            {
                if (e.InnerException.GetType() == typeof(OperationCanceledException))
                {
                    _si.SyncCancelled();
                }
                else if (e.InnerException.GetType() == typeof(DirectoryNotFoundException))
                {}
            }
            catch (OperationCanceledException) {
                _si.SyncCancelled();
            }
        }

        /// <summary>
        /// cancel synchronisation
        /// this might take some time until running task have finished
        /// </summary>
        public void Cancel()
        {
            _service.Cancel();
        }

        /// <summary>
        /// pause synchronisation
        /// this might take some time until running task have finished
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
    }
}
