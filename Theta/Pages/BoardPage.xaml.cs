using System;
using System.Collections.Generic;
using System.Diagnostics;
using Theta.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Chips;
using Xamarin.Forms.Xaml;

namespace Theta.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardPage : BasePage
    {
        BoardPageViewModel viewModel;

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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            try
            {

                viewModel = (BoardPageViewModel)BindingContext;

                if (viewModel != null)
                {
                    viewModel.UpdateFilterBox = () =>
                    {
                        flexLayoutFilterOptions?.Children.Clear();
                        foreach (var item in viewModel?.FilterOptions)
                        {
                            flexLayoutFilterOptions.Children.Add(new Chip()
                            {
                                Margin = 2,
                                VerticalOptions = LayoutOptions.Center,
                                BackgroundColor = Color.LightBlue,
                                Text = item.FullText,
                                Padding = new Thickness(6, 3),
                                CloseButtonImage = "https://path.to.image/close.png",
                                CloseCommand = viewModel.RemoveFilterOptionChipCommand,

                            });
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
