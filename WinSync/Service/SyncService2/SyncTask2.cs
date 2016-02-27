using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSync.Data;

namespace WinSync.Service
{
    class SyncTask2 : SyncTask
    {
        SyncService2 _service;

        public SyncTask2(SyncInfo si) : base(si) { }

        public override void Cancel()
        {
            _service.Cancel();
        }
        public override async void Execute()
        {
            _si.SyncStarted();
            _service = new SyncService2(_si);

            _service.TasksRunning++;
            try
            {
                await Task.Run(() => _service.ExecuteSync());
                _si.SyncFinished();
            }
            catch (DirectoryNotFoundException dnfe)
            {
                _si.SyncCancelled();
                MessageBox.Show(dnfe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OperationCanceledException)
            {
                _si.SyncCancelled();
            }
            _service.TasksRunning--;
        }

        public override int TasksRunning()
        {
            return _service.TasksRunning;
        }
    }
}
