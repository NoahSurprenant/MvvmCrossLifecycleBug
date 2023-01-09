using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MvvmCrossLifecycleBug.Core.ViewModels.Main
{
    public class ContactPageViewModel : BaseViewModel<Contact>
    {
        public Contact Contact;
        public override void Prepare(Contact parameter)
        {
            Contact = parameter;
        }

        public ContactPageViewModel(ILogger<ContactPageViewModel> logger)
        {
            logger.LogInformation("ContactPageViewModel constructed");
        }
    }
}
