using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.BottomNavigation;
using ShareLockRevamp.Activities.Extenders;
using ShareLockRevamp.Adapters;
using ShareLockRevamp.EventListeners;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;

namespace ShareLockRevamp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Header
        TextView textMessage;

        //Home
        public static RecyclerView doorLockRecyle;
        public static LinearLayout HomePage;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Header
            textMessage = FindViewById<TextView>(Resource.Id.message);

            ConnectHomeViews();
            HomeExtenders.ShowHomeLayout();
            HomeExtenders.RetrieveData();

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        private void ConnectHomeViews()
        {
            HomePage = (LinearLayout)FindViewById(Resource.Id.HomeLayout);
            doorLockRecyle = (RecyclerView)FindViewById(Resource.Id.doorlocksRecyclerView);
            
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    textMessage.SetText(Resource.String.title_home);
                    HomeExtenders.ShowHomeLayout();
                    HomeExtenders.RetrieveData();

                    return true;
                case Resource.Id.navigation_dashboard:
                    textMessage.SetText(Resource.String.title_dashboard);
                    
                    return true;
                case Resource.Id.navigation_notifications:
                    textMessage.SetText(Resource.String.title_notifications);
                    
                    return true;
            }
            return false;
        }
    }
}

