using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.Navigation;
using MvvmCross;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;
using Xamarin.Essentials;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace MvvmCrossLifecycleBug.Droid.Views.Main
{
    [Activity(
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class MainContainerActivity : BaseActivity<MainContainerViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        public Toolbar Toolbar { get; set; }
        public DrawerLayout DrawerLayout { get; set; }
        public ActionBarDrawerToggle ActionBarDrawerToggle { get; set; }
        public NavigationView NavigationView { get; set; }

        protected override int ActivityLayoutId => Resource.Layout.activity_main_container;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer);
            NavigationView = FindViewById<NavigationView>(Resource.Id.navigation_view);

            _selectedItemId = null;

            ActionBarDrawerToggle = new ActionBarDrawerToggle(this, DrawerLayout, Toolbar, Resource.String.nav_drawer_open, Resource.String.nav_drawer_close);
            ActionBarDrawerToggle.SyncState();

            DrawerLayout.DrawerClosed += DrawerLayout_DrawerClosed;
            NavigationView.SetNavigationItemSelectedListener(this);

            var menuService = Mvx.IoCProvider.Resolve<IMenuService>();
            menuService.UpdateSelectedMenuItem(menuService.NavBarSelectedViewModelType());
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            try
            {
                if (!menuItem.IsChecked)
                {
                    _selectedItemId = menuItem.ItemId;
                }

                // Close the drawer on the main thread so it animates properly
                Task.Run(async () =>
                {
                    await MainThread.InvokeOnMainThreadAsync(() => DrawerLayout.CloseDrawer((int)GravityFlags.Start, true));
                });
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Error in MainContainerActivity.OnNavigationItemSelected");
            }

            return false;
        }

        private int? _selectedItemId;

        private async void DrawerLayout_DrawerClosed(object sender, DrawerLayout.DrawerClosedEventArgs e)
        {
            try
            {
                if (_selectedItemId == null) return;

                var id = _selectedItemId.Value;
                _selectedItemId = null;

                var menuService = Mvx.IoCProvider.Resolve<IMenuService>();

                await Task.Run(async () =>
                {
                    await menuService.OnNavigationItemSelectedAsync(id);
                });
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Error in MainContainerActivity.DrawerLayout_DrawerClosed");
            }
        }
    }
}
