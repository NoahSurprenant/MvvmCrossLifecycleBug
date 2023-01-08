using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Navigation;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android;
using MvvmCross.ViewModels;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;
using Xamarin.Essentials;

namespace MvvmCrossLifecycleBug.Droid.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMvxNavigationService _navigationService;
        private Dictionary<Type, int> _dict;
        private Type _navBarSelectedViewModelType;
        public Type NavBarSelectedViewModelType() { return _navBarSelectedViewModelType; }

        public MenuService(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            // It is important that this dictionary contains all VM types and nav bar item ids so the menu functions correctly
            _dict = new Dictionary<Type, int>
            {
                { typeof(MainViewModel), Resource.Id.nav_drawer_mainPage },
                { typeof(SecondViewModel), Resource.Id.nav_drawer_secondPage },
            };
        }

        private IMenu GetMenu()
        {
            var top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            var activity = top.Activity;

            var NavigationView = activity.FindViewById<NavigationView>(Resource.Id.navigation_view);
            if (NavigationView == null) //May not find it if called before the navigation view is created
                return null;
            var Menu = NavigationView.Menu;
            if (Menu == null)
                return null;

            return Menu;
        }

        public async Task OnNavigationItemSelectedAsync(int selectedItemId)
        {
            Type VM = null;

            VM = _dict.First(x => x.Value == selectedItemId).Key;

            if (VM != null)
            {
                await _navigationService.Navigate(VM);
            }
        }

        public void UpdateSelectedMenuItem(IMvxViewModel viewModelType)
        {
            UpdateSelectedMenuItem(viewModelType.GetType());
        }

        public void UpdateSelectedMenuItem(Type viewModelType)
        {
            int resourceId;
            var found = false;
            try
            {
                found = _dict.TryGetValue(viewModelType, out resourceId);
            }
            catch
            {
                return;
            }

            if (!found)
                return;

            // Store the selected VM type in case we recreate activity and need to set it for first time
            _navBarSelectedViewModelType = viewModelType;

            var Menu = GetMenu();
            if (Menu == null)
                return;

            Task.Run(async () =>
            {
                await MainThread.InvokeOnMainThreadAsync(() => Menu.FindItem(resourceId)?.SetChecked(true)).ConfigureAwait(false);
            });
        }
    }
}
