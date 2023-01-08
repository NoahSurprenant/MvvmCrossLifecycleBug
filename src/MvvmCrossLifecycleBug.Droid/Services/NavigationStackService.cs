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
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Platforms.Android;
using MvvmCross.ViewModels;
using MvvmCross;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;

namespace MvvmCrossLifecycleBug.Droid.Services
{
    public class NavigationStackService : INavigationStackService
    {
        public IMvxViewModel GetViewModel(int index = default)
        {
            var activity = (MvxActivity)Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var fragmentManager = activity.SupportFragmentManager;

            var offset = (fragmentManager.BackStackEntryCount - 1) - index; //BackStackEntryCount is 1-based, but GetBackStackEntryAt is an array, and thus 0-based.
            if (offset < 0)
                return null;

            var backStackEntry = fragmentManager.GetBackStackEntryAt(offset);
            var fragment = activity.FindFragmentByTag<MvxFragment>(backStackEntry.Name);
            return fragment?.ViewModel;
        }
    }
}
