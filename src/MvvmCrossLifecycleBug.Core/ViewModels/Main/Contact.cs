using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.FieldBinding;

namespace MvvmCrossLifecycleBug.Core.ViewModels.Main
{
    public class Contact
    {
        public INC<string> FirstName = new NC<string>();
        public INC<string> LastName = new NC<string>();

        public INC<string> Phone = new NC<string>();
        public INC<string> Email = new NC<string>();

        public INC<string> City = new NC<string>();
        public INC<string> Country = new NC<string>();
    }
}
