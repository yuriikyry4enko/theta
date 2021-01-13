﻿using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Theta.Constants;
using Xamarin.Forms;

namespace Theta.ViewModels
{
    class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public virtual ICommand BackCommand => new Command(async () => { await navigationService.GoBackAsync(null, null, animated: false); });

        protected const string Params = "params";

        protected readonly INavigationService navigationService;

        public INavigationParameters CreateParameters(object obj)
        {
            var parameters = new NavigationParameters();
            parameters.Add(Params, obj);
            return parameters;
        }

        protected T GetParameters<T>(INavigationParameters parameters) where T : class
        {
            return parameters[Params] as T;
        }

        protected object GetParameters(INavigationParameters parameters)
        {
            return parameters[Params];
        }

        protected bool HasParameters(INavigationParameters parameters)
        {
            return parameters.ContainsKey(Params);
        }

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(Initialize)}");
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatedFrom)}");
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatedTo)}");
        }

        public virtual void Destroy()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(Destroy)}");
        }

        public virtual void OnAppearing()
        {

        }

        public virtual void OnDisappearing()
        {

        }
    }
}