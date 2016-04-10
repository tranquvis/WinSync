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
            int result = await RunExecTaskAsync();
            _service.TasksRunning--;

            switch (result)
            {
                case 1:
                    MessageBox.Show("The directories to sync do not exist on this System.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 3:
                    MessageBox.Show("Unexpected Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        /// run synchronisation task async
        /// </summary>
        /// <returns>
        /// 0 - success <para />
        /// 1 - directory not found <para />
        /// 2 - sync canceled
        /// </returns>
        private Task<int> RunExecTaskAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    _service.ExecuteSync();
                    _si.SyncFinished();
                }
                catch (DirectoryNotFoundException)
                {
                    _si.SyncCancelled();
                    return 1;
                }
                catch (OperationCanceledException)
                {
                    _si.SyncCancelled();
                    return 2;
                }
                catch (Exception e)
                {
                    return 3;
                }

                return 0;
            });
        }

        public override int TasksRunning()
        {
            return _service.TasksRunning;
        }
    }
}
