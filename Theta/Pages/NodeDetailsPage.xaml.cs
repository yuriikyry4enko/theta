using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Theta.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NodeDetailsPage : BasePage
    {
        public NodeDetailsPage()
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
