using WinSync.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Service
{
    public class AsyncSyncTask : SyncTask
    {
        private AsyncSyncService _service;

        public AsyncSyncTask(SyncInfo si) : base(si) { }
        
        public override async void Execute()
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
            catch(DirectoryNotFoundException dnfe)
            {
                _si.SyncCancelled();
                MessageBox.Show(dnfe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _service.TaskEnded(_service.MainTaskId);
        }
        
        public override void Cancel()
        {
            _service.Cancel();
        }

        public override int TasksRunning()
        {
            return _service == null ? 0 : _service.TasksRunning;
        }
    }
}
