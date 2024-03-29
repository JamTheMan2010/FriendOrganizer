﻿using FriendOrganizer.UI.Event;
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
        private IDetailViewModel _detailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IFriendDetailViewModel> friendDetailViewModeCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            _friendDetailViewModeCreator = friendDetailViewModeCreator;

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
               .Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            NavigationViewModel = navigationViewModel;
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ICommand CreateNewDetailCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set 
            { 
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShouwOkCancelDialog("You have made changes. Navigate away?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = _friendDetailViewModeCreator();
                    break;
            }

            await DetailViewModel.LoadAsync(args.Id);
        }
        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs()
                {
                    ViewModelName = viewModelType.Name
                });
        }


        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }
    }
}
