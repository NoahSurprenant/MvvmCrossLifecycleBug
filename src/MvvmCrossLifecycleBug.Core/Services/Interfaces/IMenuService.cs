using System;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace MvvmCrossLifecycleBug.Core.Services.Interfaces
{
    public interface IMenuService
    {
        public Type NavBarSelectedViewModelType();
        public void UpdateSelectedMenuItem(Type viewModelType);
        public void UpdateSelectedMenuItem(IMvxViewModel viewModelType);
        public Task OnNavigationItemSelectedAsync(int selectedItemId);
    }
}
