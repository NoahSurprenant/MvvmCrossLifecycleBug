using System;
using System.Collections.Generic;
using System.Text;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;

namespace MvvmCrossLifecycleBug.Core.Services.Interfaces
{
    public interface IContactService
    {
        public IEnumerable<Contact> GetContacts();
    }
}
