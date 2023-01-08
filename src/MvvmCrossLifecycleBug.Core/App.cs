using System;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;
using MvvmCrossLifecycleBug.Core.Services.Interfaces;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;

namespace MvvmCrossLifecycleBug.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            try
            {
                CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

                var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                var menuService = Mvx.IoCProvider.Resolve<IMenuService>();
                var navigationStackService = Mvx.IoCProvider.Resolve<INavigationStackService>();

                navigationService.DidNavigate += (_, e) =>
                {
                    var VM = e.ViewModel;
                    menuService.UpdateSelectedMenuItem(VM);
                };

                navigationService.DidClose += (object sender, IMvxNavigateEventArgs e) =>
                {
                    var VM = navigationStackService.GetViewModel();
                    menuService.UpdateSelectedMenuItem(VM);
                };

                RegisterAppStart<MainViewModel>();
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }
    }
}
