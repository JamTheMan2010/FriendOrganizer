using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IMessageDialogService _messageDialogService;
        private IEventAggregator _eventAggregator;
        private Func<IFriendDetailViewModel> _friendDetailViewModeCreator;
        private IFriendDetailViewModel _friendDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModeCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            _friendDetailViewModeCreator = friendDetailViewModeCreator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>()
                .Subscribe(OnOpenFriendDetailView);

            _eventAggregator.GetEvent<AfterFriendDeletedEvent>()
               .Subscribe(AfterFriendDeleted);

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ICommand CreateNewFriendCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set 
            { 
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenFriendDetailView(int? friendId)        
        {
            if( FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShouwOkCancelDialog("You have made changes. Navigate away?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = _friendDetailViewModeCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }


        private void AfterFriendDeleted(int friendId)
        {
            FriendDetailViewModel = null;
        }
    }
}
