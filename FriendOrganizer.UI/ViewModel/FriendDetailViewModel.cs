﻿namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FriendOrganizer.Model;
    using FriendOrganizer.UI.Data;
    using FriendOrganizer.UI.Event;
    using Prism.Events;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendDataService dataService;
        private IEventAggregator eventAggregator;
        private Friend friend;

        public FriendDetailViewModel(IFriendDataService dataService, IEventAggregator eventAggregator)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }

        public async Task LoadAsync(int friendId)
        {
            Friend = await dataService.GetByIdAsync(friendId);
        }

        public Friend Friend
        {
            get { return friend; }
            private set
            {
                friend = value;
                OnPropertyChanged();
            }
        }
    }
}
