using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Theta.Pages.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPopupPage : PopupPage
    {
        public MenuPopupPage()
        {
            InitializeComponent();

            var animation = new Animation();

            var textFieldTranslate = new Animation(v => greyBoxView.TranslationX = v, 300, 0);
            var textFieldChangeWidth = new Animation(v => greyBoxView.WidthRequest = v, 0, 300);
            var textFieldChangeOpacity = new Animation(v => greyBoxView.Opacity = v, 0, 1);
            
            animation.Add(0, 1, textFieldTranslate);
            animation.Add(0, 1, textFieldChangeWidth);
            animation.Add(0, 0.2, textFieldChangeOpacity);

            animation.Commit(this, "Slide", 16, 220, Easing.Linear);
        }

        //void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        //{

        //    var animation = new Animation();

        //    var textFieldTranslate = new Animation(v => greyBoxView.TranslationX = v, 0, 300);
        //    var textFieldChangeWidth = new Animation(v => greyBoxView.WidthRequest = v, 300, 0);
        //    //var textFieldChangeOpacity = new Animation(v => greyBoxView.Opacity = v, 1, 0);

        //    animation.Add(0, 1, textFieldTranslate);
        //    animation.Add(0, 1, textFieldChangeWidth);
        //    //animation.Add(0, 0.2, textFieldChangeOpacity);

        //    animation.Commit(this, "Slide", 16, 200, Easing.Linear);



        //}
    }
}
