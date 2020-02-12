﻿namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using FriendOrganizer.UI.Data.Lookups;
    using FriendOrganizer.UI.Event;
    using Prism.Events;

    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService friendLookupService;
        private IEventAggregator eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupService, IEventAggregator eventAggregator)
        {
            this.friendLookupService = friendLookupService;
            this.eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
            eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(AfterFriendDeleted);
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public async Task LoadAsync()
        {
            var lookup = await friendLookupService.GetFriendLookupAsync();
            Friends.Clear();

            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, eventAggregator));
            }
        }

        void AfterFriendSaved(AfterFriendSavedEventArgs obj)
        {
            var lookupItem = Friends.SingleOrDefault(l => l.Id == obj.Id);

            if (lookupItem == null)
            {
                Friends.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = obj.DisplayMember;
            }

            lookupItem.DisplayMember = obj.DisplayMember;
        }

        private void AfterFriendDeleted(int friendId)
        {
            var friend = Friends.SingleOrDefault(f => f.Id == friendId);

            if (friend != null)
            {
                Friends.Remove(friend);
            }
        }
    }
}
