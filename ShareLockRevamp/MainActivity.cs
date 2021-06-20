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
using Android.Content;
using ShareLockRevamp.Activities;
using ShareLockRevamp.Helpers;
using Firebase.Database;

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

        //EditDoorLockFragment editDoorLockFragment;
        public static EditText doorLockID;
        public static EditText doorLockName;
        public static EditText doorpassword;
        public static EditText address;
        public static EditText username;
        public static Button saveEdit;
        public static Button deleteThis;
        public static LinearLayout EditDoorLayout;
        DoorLockListener DoorLockListener;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

           
            //Header
            textMessage = FindViewById<TextView>(Resource.Id.message);

            ConnectHomeViews();
            ConnectEditViews();
            HomeExtenders.ShowHomeLayout();
            HomeExtenders.RetrieveData();

            

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        private void ConnectEditViews()
        {
            EditDoorLayout = (LinearLayout)FindViewById(Resource.Id.EditDoorLayout);
            doorLockID = (EditText)FindViewById(Resource.Id.doorLockId);
            doorLockName = (EditText)FindViewById(Resource.Id.doorlockName);
            doorpassword = (EditText)FindViewById(Resource.Id.doorLockPassword);
            address = (EditText)FindViewById(Resource.Id.doorLockAddress);
            username = (EditText)FindViewById(Resource.Id.familyOwner);
            saveEdit = (Button)FindViewById(Resource.Id.EditDoorLockButton);
            deleteThis = (Button)FindViewById(Resource.Id.DeleteDoorLockButton);
            saveEdit.Click += SaveEdit_Click;
            deleteThis.Click += DeleteThis_Click;
        }

        private void DeleteThis_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder editDoorLock = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            editDoorLock.SetTitle("Saving Changes");
            editDoorLock.SetMessage("Are you sure?");
            editDoorLock.SetPositiveButton("Continue", (deleteAlert, args) =>
            {
                DatabaseReference reference = AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID);
                reference.RemoveValue();
                Toast.MakeText(MainActivity.saveEdit.Context, "Door Lock Deleted!", ToastLength.Short).Show();
                EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
                HomePage.Visibility = Android.Views.ViewStates.Visible;

            });
            editDoorLock.SetNegativeButton("Cancel", (deleteAlert, args) =>
            {
                editDoorLock.Dispose();
            });
            editDoorLock.Show();
        }

        private void SaveEdit_Click(object sender, EventArgs e)
        {
              string id = MainActivity.doorLockID.Text;
                string doorname = MainActivity.doorLockName.Text;
                string password = MainActivity.doorpassword.Text;
                string address = MainActivity.address.Text;
                string ownername = MainActivity.username.Text;

                AndroidX.AppCompat.App.AlertDialog.Builder editDoorLock = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
                editDoorLock.SetTitle("Saving Changes");
                editDoorLock.SetMessage("Are you sure?");
                editDoorLock.SetPositiveButton("Continue", (deleteAlert, args) =>
                {
                    AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID + "/DoorName").SetValue(doorname);
                    AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID + "/Key").SetValue(id);
                    AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID + "/Password").SetValue(password);
                    AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID + "/Address").SetValue(password);
                    AppDataHelper.GetDatabase().GetReference("doorLockInfo/" + DoorLockClick.ID + "/OwnerName").SetValue(password);
                    Toast.MakeText(MainActivity.saveEdit.Context, "Changes Saved!", ToastLength.Short).Show();
                    EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
                    HomePage.Visibility = Android.Views.ViewStates.Visible;

                });
                editDoorLock.SetNegativeButton("Cancel", (deleteAlert, args) =>
                {
                    editDoorLock.Dispose();
                });
                editDoorLock.Show();
          
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

