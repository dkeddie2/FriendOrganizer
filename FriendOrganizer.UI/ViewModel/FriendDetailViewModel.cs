namespace FriendOrganizer.UI.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
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
            this.eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

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

        public ICommand SaveCommand { get; }

        public async Task LoadAsync(int friendId)
        {
            var friend = await friendRepository.GetByIdAsync(friendId);
            Friend = new FriendWrapper(friend);

            Friend.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await friendRepository.SaveAsync();
            eventAggregator.GetEvent<AfterFriendSavedEvent>()
                .Publish(
                new AfterFriendSavedEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}",
                });
        }
    }
}
