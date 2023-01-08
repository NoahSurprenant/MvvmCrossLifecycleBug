using MvvmCross.ViewModels;

namespace MvvmCrossLifecycleBug.Core.Services.Interfaces
{
    public interface INavigationStackService
    {
        public IMvxViewModel GetViewModel(int index = default);
    }
}
