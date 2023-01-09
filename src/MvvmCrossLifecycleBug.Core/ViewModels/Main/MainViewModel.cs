using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;

namespace MvvmCrossLifecycleBug.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        private readonly IMvxNavigationService _navigationService;

        public MvxObservableCollection<Contact> Contacts = new MvxObservableCollection<Contact>();
        public MvxAsyncCommand<Contact> ContactSelectedCommand { get; set; }


        public MainViewModel(IContactService contactService, IMvxNavigationService navigationService, ILogger<MainViewModel> logger)
        {
            logger.LogInformation("MainViewModel constructed");
            _contactService = contactService;
            _navigationService = navigationService;
        }

        public override Task Initialize()
        {
            ContactSelectedCommand = new MvxAsyncCommand<Contact>(OnContactSelectCommandAsync);

            var contacts = _contactService.GetContacts().OrderBy(c => c.LastName.Value).ThenBy(c => c.FirstName.Value);

            Contacts.AddRange(contacts);

             return base.Initialize();
        }

        private async Task OnContactSelectCommandAsync(Contact model)
        {
            await _navigationService.Navigate<ContactPageViewModel, Contact>(model);
        }
    }
}
