﻿//using InfinitTools.Commands;
//using System.Windows.Input;

//namespace InfinitTools.ViewModels
//{
//    public class MainWindowViewModel
//    {
//        private ICommand _openTimeTrackerCommand = null;
//        public ICommand OpenTimeTrackerCommand
//        {
//            get
//            {
//                return _openTimeTrackerCommand = _openTimeTrackerCommand ?? new CommandHandler(OnOpenTimeTrackerCommandHandler, true);
//            }
//        }

//        private ICommand _openFilenameCopierCommand = null;
//        public ICommand OpenFilenameCopierCommand
//        {
//            get
//            {
//                return _openFilenameCopierCommand = _openFilenameCopierCommand ?? new CommandHandler(OnOpenFilenameCopierCommandHandler, true);
//            }
//        }

//        private void OnOpenFilenameCopierCommandHandler()
//        {
//            FileNameCopier window = new FileNameCopier();
//            window.Show();
//        }

//        private void OnOpenTimeTrackerCommandHandler()
//        {
//            TimeTrackerWindow window = new TimeTrackerWindow();
//            window.Show();
//        }
//    }
//}
