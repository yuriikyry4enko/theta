using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Theta.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardPage : BasePage
    {
        public BoardPage()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
