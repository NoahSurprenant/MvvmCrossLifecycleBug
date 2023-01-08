using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MvvmCrossLifecycleBug.Core.ViewModels.Main;

namespace MvvmCrossLifecycleBug.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainViewModel>();
        }
    }
}
