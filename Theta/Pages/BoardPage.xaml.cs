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

        Style buttonStyle = new Style(typeof(ImageButton))
        {
            Setters =
            {
                new Setter
                {
                    Property = ImageButton.SourceProperty,
                    Value = Color.FromRgb(0, 77, 64)
                },
            },
        };

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
                            var chip = new Chip()
                            {
                                BindingContext = item,
                                Margin = 2,
                                VerticalOptions = LayoutOptions.Center,
                                BackgroundColor = Color.LightBlue,
                                Text = item.FullText,
                                Padding = new Thickness(6, 3),
                                CloseButtonImage = "https://image.flaticon.com/icons/png/128/61/61155.png",
                                CloseCommand = viewModel.RemoveFilterOptionChipCommand,
                               
                            };

                            chip.CloseCommandParameter = chip.BindingContext;

                            flexLayoutFilterOptions.Children.Add(chip); 
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
