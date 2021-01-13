using System;
using Xamarin.Forms;

namespace Theta.Pages
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
