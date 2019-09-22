using InfinitTools.Commands;
using InfinitTools.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfinitTools.ViewModels
{
    public class MainWindowViewModel
    {
        private ICommand _openTimeTrackerCommand = null;
        public ICommand OpenTimeTrackerCommand
        {
            get
            {
                return _openTimeTrackerCommand = _openTimeTrackerCommand ?? new CommandHandler(OnOpenTimeTrackerCommandHandler, true);
            }
        }

        private ICommand _openFilenameCopierCommand = null;
        public ICommand OpenFilenameCopierCommand
        {
            get
            {
                return _openFilenameCopierCommand = _openFilenameCopierCommand ?? new CommandHandler(OnOpenFilenameCopierCommandHandler, true);
            }
        }

        private void OnOpenFilenameCopierCommandHandler()
        {
            FileNameCopier window = new FileNameCopier();
            window.Show();
        }

        private void OnOpenTimeTrackerCommandHandler()
        {
            TimeTrackerWindow window = new TimeTrackerWindow();
            window.Show();
        }
    }
}
