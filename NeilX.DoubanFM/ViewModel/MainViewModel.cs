using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using NeilX.DoubanFM.Services;
using NeilX.DoubanFM.View.UserControls.ControlsModel;
using NeilX.DoubanFM.View;
using System;
using NeilX.DoubanFM.Core;

namespace NeilX.DoubanFM.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private static readonly Guid Token = Guid.NewGuid();
        private Frame _navigationService;
        private int _selectedMenuIndex = -1;
        private ObservableCollection<MenuListItem> _menuList;

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                InitializeMenuData();
            }
            else
            {
                // Code runs "for real"
                InitializeMenuData();
                RegisterMessenger();
            }


        }



        public int SelectedMenuIndex
        {
            get { return _selectedMenuIndex; }
            set
            {
                if (Set(ref _selectedMenuIndex, value))
                {
                    OnSelectedMenuIndexChanged(value);
                }
            }
        }


        public ObservableCollection<MenuListItem> MenuList
        {
            get { return _menuList; }
            set
            {
                Set(ref _menuList, value);
            }
        }




        public PlayerSessionService PlayerSession
        {
            get
            {

                return ServiceLocator.Current.GetInstance<PlayerSessionService>();
            }

        }

        public Frame NavigationService
        {
            get
            {
                return _navigationService;
            }
            private set
            {
                _navigationService = value;
            }
        }



        #region Public  Methods
        public void SetupNavigationService(object sender, object e)
        {
            NavigationService = (Frame)sender;
            SelectedMenuIndex = 1;
            NavigationService.Navigated += _navigationService_Navigated;
        }

        public void NavigationServiceGoBack()
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }






        #endregion


        #region private Helper Methods
        private void InitializeMenuData()
        {
            MenuList = new ObservableCollection<MenuListItem>();
            MenuListItem m;
            m = new MenuListItem
            {
                Name = "搜索",
                IsEnabled = true,
                GotoView = MenuGotoView.SearchView,
                Icon =
       "M9.84375,0.9375 C9.2627,0.937501 8.71582,1.04858 8.20312,1.27075 C7.69043,1.49292 7.24365,1.79443 6.86279,2.17529 C6.48193,2.55615 6.18042,3.00293 5.95825,3.51562 C5.73608,4.02832 5.625,4.5752 5.625,5.15625 C5.625,5.7373 5.73608,6.28418 5.95825,6.79688 C6.18042,7.30957 6.48193,7.75635 6.86279,8.13721 C7.24365,8.51807 7.69043,8.81958 8.20312,9.04175 C8.71582,9.26392 9.2627,9.375 9.84375,9.375 C10.4248,9.375 10.9717,9.26392 11.4844,9.04175 C11.9971,8.81958 12.4438,8.51807 12.8247,8.13721 C13.2056,7.75635 13.5071,7.30957 13.7292,6.79688 C13.9514,6.28418 14.0625,5.7373 14.0625,5.15625 C14.0625,4.5752 13.9514,4.02832 13.7292,3.51562 C13.5071,3.00293 13.2056,2.55615 12.8247,2.17529 C12.4438,1.79443 11.9971,1.49292 11.4844,1.27075 C10.9717,1.04858 10.4248,0.937501 9.84375,0.9375 z M9.84375,0 C10.3174,1E-06 10.7739,0.061036 11.2134,0.183105 C11.6528,0.305177 12.063,0.478517 12.4438,0.703125 C12.8247,0.927735 13.1726,1.19751 13.4875,1.51245 C13.8025,1.82739 14.0723,2.17529 14.2969,2.55615 C14.5215,2.93701 14.6948,3.34717 14.8169,3.78662 C14.939,4.22607 15,4.68262 15,5.15625 C15,5.62988 14.939,6.08643 14.8169,6.52588 C14.6948,6.96533 14.5215,7.37549 14.2969,7.75635 C14.0723,8.13721 13.8025,8.48511 13.4875,8.80005 C13.1726,9.11499 12.8247,9.38477 12.4438,9.60938 C12.063,9.83398 11.6528,10.0073 11.2134,10.1294 C10.7739,10.2515 10.3174,10.3125 9.84375,10.3125 C9.2334,10.3125 8.64868,10.21 8.0896,10.0049 C7.53052,9.7998 7.0166,9.50439 6.54785,9.11865 L0.79834,14.8608 C0.705566,14.9536 0.595703,15 0.46875,15 C0.341797,15 0.231934,14.9536 0.13916,14.8608 C0.046387,14.7681 0,14.6582 0,14.5312 C0,14.4043 0.046387,14.2944 0.13916,14.2017 L5.88135,8.45215 C5.49561,7.9834 5.2002,7.46948 4.99512,6.9104 C4.79004,6.35132 4.6875,5.7666 4.6875,5.15625 C4.6875,4.68262 4.74854,4.22607 4.87061,3.78662 C4.99268,3.34717 5.16602,2.93701 5.39062,2.55615 C5.61523,2.17529 5.88501,1.82739 6.19995,1.51245 C6.51489,1.19751 6.86279,0.927735 7.24365,0.703125 C7.62451,0.478517 8.03467,0.305177 8.47412,0.183105 C8.91357,0.061036 9.37012,1E-06 9.84375,0 z"
            };
            MenuList.Add(m);
            m = new MenuListItem
            {
                Name = "电台列表",
                IsEnabled = true,
                GotoView = MenuGotoView.RadioListView,
                Icon =
        "M0,11.25 L9.375,11.25 L9.375,12.1875 L0,12.1875 z M0,8.4375 L9.375,8.4375 L9.375,9.375 L0,9.375 z M15,7.9541 L15,13.125 C15,13.2764 14.9646,13.4094 14.8938,13.5242 C14.823,13.6389 14.7302,13.7366 14.6155,13.8171 C14.5007,13.8977 14.3738,13.9587 14.2346,14.0002 C14.0955,14.0417 13.96,14.0625 13.8281,14.0625 C13.6963,14.0625 13.5608,14.0417 13.4216,14.0002 C13.2825,13.9587 13.1555,13.8977 13.0408,13.8171 C12.926,13.7366 12.8333,13.6389 12.7625,13.5242 C12.6916,13.4094 12.6562,13.2764 12.6562,13.125 C12.6562,12.9736 12.6916,12.8406 12.7625,12.7258 C12.8333,12.6111 12.926,12.5134 13.0408,12.4329 C13.1555,12.3523 13.2825,12.2913 13.4216,12.2498 C13.5608,12.2083 13.6963,12.1875 13.8281,12.1875 C13.9014,12.1875 13.9795,12.1948 14.0625,12.2095 L14.0625,9.15527 L11.25,9.8584 L11.25,14.0625 C11.25,14.2188 11.2109,14.3555 11.1328,14.4727 C11.0547,14.5898 10.9558,14.6875 10.8362,14.7656 C10.7166,14.8438 10.5835,14.9023 10.437,14.9414 C10.2905,14.9805 10.1514,15 10.0195,15 C9.8877,15 9.74854,14.9805 9.60205,14.9414 C9.45557,14.9023 9.32251,14.8438 9.20288,14.7656 C9.08325,14.6875 8.98438,14.5898 8.90625,14.4727 C8.82812,14.3555 8.78906,14.2188 8.78906,14.0625 C8.78906,13.9062 8.82812,13.7695 8.90625,13.6523 C8.98438,13.5352 9.08325,13.4375 9.20288,13.3594 C9.32251,13.2812 9.45557,13.2227 9.60205,13.1836 C9.74854,13.1445 9.8877,13.125 10.0195,13.125 C10.0684,13.125 10.1172,13.1274 10.166,13.1323 C10.2148,13.1372 10.2637,13.1445 10.3125,13.1543 L10.3125,9.12598 z M0,5.625 L15,5.625 L15,6.5625 L0,6.5625 z M0,2.8125 L15,2.8125 L15,3.75 L0,3.75 z"
            };

            MenuList.Add(m);
            m = new MenuListItem
            {
                Name = "我的音乐",
                IsEnabled = true,
                GotoView = MenuGotoView.MyMusicView,
                Icon =
                    "M8.4375,10.3125 L15,10.3125 L15,11.25 L8.4375,11.25 z M8.4375,6.5625 L15,6.5625 L15,7.5 L8.4375,7.5 z M3.75,3.75 C3.49121,3.75 3.24829,3.80005 3.02124,3.90015 C2.79419,4.00024 2.59644,4.13452 2.42798,4.30298 C2.25952,4.47144 2.12524,4.67041 2.02515,4.8999 C1.92505,5.12939 1.875,5.37109 1.875,5.625 C1.875,5.88379 1.92505,6.12671 2.02515,6.35376 C2.12524,6.58081 2.25952,6.77856 2.42798,6.94702 C2.59644,7.11548 2.79419,7.24976 3.02124,7.34985 C3.24829,7.44995 3.49121,7.5 3.75,7.5 C4.00391,7.5 4.24561,7.44995 4.4751,7.34985 C4.70459,7.24976 4.90356,7.11548 5.07202,6.94702 C5.24048,6.77856 5.37476,6.58081 5.47485,6.35376 C5.57495,6.12671 5.625,5.88379 5.625,5.625 C5.625,5.37109 5.57495,5.12939 5.47485,4.8999 C5.37476,4.67041 5.24048,4.47144 5.07202,4.30298 C4.90356,4.13452 4.70459,4.00024 4.4751,3.90015 C4.24561,3.80005 4.00391,3.75 3.75,3.75 z M8.4375,2.8125 L15,2.8125 L15,3.75 L8.4375,3.75 z M3.75,2.8125 C4.13574,2.8125 4.49951,2.88696 4.84131,3.03589 C5.18311,3.18481 5.48096,3.38623 5.73486,3.64014 C5.98877,3.89404 6.19019,4.19189 6.33911,4.53369 C6.48804,4.87549 6.5625,5.23926 6.5625,5.625 C6.5625,6.06934 6.46118,6.4917 6.25854,6.89209 C6.05591,7.29248 5.77637,7.62451 5.41992,7.88818 C5.7373,8.04932 6.02417,8.24829 6.28052,8.48511 C6.53686,8.72192 6.75537,8.9856 6.93604,9.27612 C7.1167,9.56665 7.25586,9.87915 7.35352,10.2136 C7.45117,10.5481 7.5,10.8936 7.5,11.25 L6.5625,11.25 C6.5625,10.8643 6.48804,10.5005 6.33911,10.1587 C6.19019,9.81689 5.98877,9.51904 5.73486,9.26514 C5.48096,9.01123 5.18311,8.80981 4.84131,8.66089 C4.49951,8.51196 4.13574,8.4375 3.75,8.4375 C3.36426,8.4375 3.00049,8.51196 2.65869,8.66089 C2.31689,8.80981 2.01904,9.01123 1.76514,9.26514 C1.51123,9.51904 1.30981,9.81689 1.16089,10.1587 C1.01196,10.5005 0.9375,10.8643 0.9375,11.25 L0,11.25 C0,10.8936 0.048828,10.5481 0.146484,10.2136 C0.244141,9.87915 0.383301,9.56665 0.563965,9.27612 C0.744629,8.9856 0.963135,8.72192 1.21948,8.48511 C1.47583,8.24829 1.7627,8.04932 2.08008,7.88818 C1.72363,7.62451 1.44409,7.29248 1.24146,6.89209 C1.03882,6.4917 0.9375,6.06934 0.9375,5.625 C0.9375,5.23926 1.01196,4.87549 1.16089,4.53369 C1.30981,4.19189 1.51123,3.89404 1.76514,3.64014 C2.01904,3.38623 2.31689,3.18481 2.65869,3.03589 C3.00049,2.88696 3.36426,2.8125 3.75,2.8125 z"
            };

            MenuList.Add(m);



        }



        #region Messenget Helper Methods
        private void RegisterMessenger()
        {
            Messenger.Default.Register<NotificationMessage<SongList>>(this, MyMusicView.Token, HandleMyMusicViewMsg);
            Messenger.Default.Register<NotificationMessage<SongList>>(this, SongListView.Token, HandleSongListViewMsg);
        }

        private void UnRegisterMessenger()
        {
            Messenger.Default.Unregister(this);
        }

        private void HandleMyMusicViewMsg(NotificationMessage<SongList> msg)
        {
            if (msg.Notification == "GotoSongListView")
            {
                _navigationService.Navigate(typeof(SongListView), msg.Content);
            }
        }

        private void HandleSongListViewMsg(NotificationMessage<SongList> msg)
        {
            if (msg.Notification == "GotoEditView")
            {
                _navigationService.Navigate(typeof(SongListEditView), msg.Content);
            }
            else if (msg.Notification == "Update")
            {
                NavigateToView(MenuGotoView.RadioListView);
                _navigationService.BackStack.Clear();
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        #endregion

        #region NavigationService

        private void _navigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.SourcePageType == typeof(RadioListView) && SelectedMenuIndex != 1)
            {
                _selectedMenuIndex = 1;
                RaisePropertyChanged(nameof(SelectedMenuIndex));
            }
            else if (e.SourcePageType == typeof(SearchView) && SelectedMenuIndex != 0)
            {
                _selectedMenuIndex = 0;
                RaisePropertyChanged(nameof(SelectedMenuIndex));
            }
            else if ((e.SourcePageType == typeof(View.MyMusicView)
                || e.SourcePageType == typeof(View.SongListView)
                || e.SourcePageType == typeof(View.SongListEditView))
                && SelectedMenuIndex != 2)
            {
                _selectedMenuIndex = 2;
                RaisePropertyChanged(nameof(SelectedMenuIndex));
            }
        }

        private void OnSelectedMenuIndexChanged(int value)
        {
            if (MenuList != null)
            {
                NavigateToView(MenuList[value].GotoView);
            }
        }

        private void NavigateToView(MenuGotoView view)
        {
            switch (view)
            {
                case MenuGotoView.SearchView:
                    _navigationService.Navigate(typeof(SearchView));
                    break;
                case MenuGotoView.RadioListView:
                    _navigationService.Navigate(typeof(RadioListView));
                    break;
                case MenuGotoView.MyMusicView:
                    _navigationService.Navigate(typeof(MyMusicView));
                    break;
                default:
                    break;
            }
        }



        #endregion

        #endregion

        public override void Cleanup()
        {
            UnRegisterMessenger();
        }


    }
}