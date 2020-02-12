namespace FriendOrganizer.UI.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FriendOrganizer.Model;
    using FriendOrganizer.UI.Data.Repositories;
    using FriendOrganizer.UI.Event;
    using FriendOrganizer.UI.Wrapper;
    using Prism.Commands;
    using Prism.Events;

    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository friendRepository;
        private IEventAggregator eventAggregator;
        private FriendWrapper friend;

        public FriendDetailViewModel(IFriendRepository friendRepository, IEventAggregator eventAggregator)
        {
            this.friendRepository = friendRepository;
            this.eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public FriendWrapper Friend
        {
            get { return friend; }
            private set
            {
                friend = value;
                OnPropertyChanged();
            }
        }

        private bool hasChanges;

        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        public ICommand SaveCommand { get; }

        public async Task LoadAsync(int? friendId)
        {

            var friend = friendId.HasValue
                ? await friendRepository.GetByIdAsync(friendId.Value)
                : CreateNewFriend();

            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += (s, e) =>
            {
                if (HasChanges == false)
                {
                    HasChanges = friendRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                // Little trick to trigger validation
                Friend.FirstName = "";
            }
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await friendRepository.SaveAsync();
            HasChanges = friendRepository.HasChanges();

            eventAggregator.GetEvent<AfterFriendSavedEvent>()
                .Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}",
                });
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            friendRepository.Add(friend);
            return friend;
        }
    }
}
