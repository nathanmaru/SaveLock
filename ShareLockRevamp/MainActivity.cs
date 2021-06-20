using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Android.Support.V7.Widget;
using Google.Android.Material.BottomNavigation;
using ShareLockRevamp.Activities.Extenders;
using ShareLockRevamp.Adapters;
using ShareLockRevamp.EventListeners;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using Android.Support.V7.RecyclerView;
using ShareLockRevamp.Fragments;

namespace ShareLockRevamp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Header
        TextView textMessage;

        //Home
        public static AndroidX.RecyclerView.Widget.RecyclerView doorLockRecyle;
        public static LinearLayout HomePage;
        public static ImageView addDoorLockBtn;
        AddDoorLockFragment addDoorLockFragment;

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

            AddDoorLockFragment addDoorLockFragment = new AddDoorLockFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            addDoorLockFragment.Show(trans, "new member");


            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        private void ConnectHomeViews()
        {
            HomePage = (LinearLayout)FindViewById(Resource.Id.HomeLayout);
            doorLockRecyle = (AndroidX.RecyclerView.Widget.RecyclerView)FindViewById(Resource.Id.doorlocksRecyclerView);
            addDoorLockBtn = (ImageView)FindViewById(Resource.Id.addDoorLock);
            addDoorLockBtn.Click += AddDoorLockBtn_Click;
        }

        private void AddDoorLockBtn_Click(object sender, EventArgs e)
        {
            addDoorLockFragment = new AddDoorLockFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            addDoorLockFragment.Show(trans, "new door lock");
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

