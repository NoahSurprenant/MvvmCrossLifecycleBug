using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace MvvmCrossLifecycleBug.Droid.Views.Main
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame)]
    public class ContactPageFragment : BaseFragment<ContactPageViewModel>
    {
        protected override int FragmentLayoutId => Resource.Layout.fragment_contact_page;
    }
}
