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
using System.Linq;

namespace ShareLockRevamp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        
        //Header
        TextView textMessage;

        //Home
        public static AndroidX.RecyclerView.Widget.RecyclerView doorLockRecyle;
        public static LinearLayout HomePage;
        public static ImageView addDoorLockBtn;
        AddDoorLockFragment addDoorLockFragment;
        
        //EditDoor
        public static EditText doorLockID;
        public static EditText doorLockName;
        public static EditText doorpassword;
        public static EditText address;
        public static EditText username;
        public static Button saveEdit;
        public static Button deleteThis;
        public static LinearLayout EditDoorLayout;
        DoorLockListener DoorLockListener;
        

        //Profile
        ImageView profileBtn;
        LinearLayout profile;
        EditText fullname;
        EditText usernametxt;
        EditText email;
        EditText password;
        Button edit;
        Button logout;
        ImageView back;
        List<Account> AccountList;
        

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

           
            //Header
            textMessage = FindViewById<TextView>(Resource.Id.message);

            ConnectHomeViews();
            ConnectEditViews();
            ConnectProfileViews();
            
            
            HomeExtenders.ShowHomeLayout();
            HomeExtenders.RetrieveData();

        }

        private void RetrieveProfile()
        {
            AccountListener accountListener = new AccountListener();
            accountListener.Create();
            accountListener.AccountRetrived += AccountListener_AccountRetrived;
            
        }

        private void AccountListener_AccountRetrived(object sender, AccountListener.AccountDataEventArgs e)
        {
            AccountList = e.Account;
            fullname.Text = ActiveUser.Fullname;
            usernametxt.Text = ActiveUser.Username;
            email.Text = ActiveUser.Email;
            password.Text = ActiveUser.Password;
        }

        private void ConnectProfileViews()
        {
            profileBtn = (ImageView)FindViewById(Resource.Id.profileBtn);
            profile = (LinearLayout)FindViewById(Resource.Id.profileLayout);
            fullname = (EditText)FindViewById(Resource.Id.fullnameTxt);
            usernametxt = (EditText)FindViewById(Resource.Id.username);
            email = (EditText)FindViewById(Resource.Id.emailText);
            password = (EditText)FindViewById(Resource.Id.passwordTxt);

            edit = (Button)FindViewById(Resource.Id.saveEditBtn);
            logout = (Button)FindViewById(Resource.Id.logoutBtn);
            back = (ImageView)FindViewById(Resource.Id.back);

            back.Click += Back_Click;
            edit.Click += Edit_Click;
            profileBtn.Click += ProfileBtn_Click;
            logout.Click += Logout_Click;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            var intent1 = new Intent(this, typeof(LoginActivity));
            ActiveUser.Username = null;
            
            StartActivity(intent1);
        }

        private void ProfileBtn_Click(object sender, EventArgs e)
        {
            profile.Visibility = Android.Views.ViewStates.Visible;
            HomePage.Visibility = Android.Views.ViewStates.Gone;
            EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
            RetrieveProfile();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            

            AndroidX.AppCompat.App.AlertDialog.Builder editDoorLock = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            editDoorLock.SetTitle("Saving Changes");
            editDoorLock.SetMessage("Are you sure?");
            editDoorLock.SetPositiveButton("Continue", (deleteAlert, args) =>
            {
                AppDataHelper.GetDatabase().GetReference("accountInfo/" + ActiveUser.ID + "/Fullname").SetValue(fullname.Text);
                AppDataHelper.GetDatabase().GetReference("accountInfo/" + ActiveUser.ID + "/Email").SetValue(email.Text);
                AppDataHelper.GetDatabase().GetReference("accountInfo/" + ActiveUser.ID + "/Username").SetValue(usernametxt.Text);
                AppDataHelper.GetDatabase().GetReference("accountInfo/" + ActiveUser.ID + "/Password").SetValue(password.Text);
                
                Toast.MakeText(MainActivity.saveEdit.Context, "Changes Saved!", ToastLength.Short).Show();
                EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
                profile.Visibility = Android.Views.ViewStates.Gone;
                HomePage.Visibility = Android.Views.ViewStates.Visible;
                ActiveUser.Username = usernametxt.Text;

            });
            editDoorLock.SetNegativeButton("Cancel", (deleteAlert, args) =>
            {
                editDoorLock.Dispose();
            });
            editDoorLock.Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            profile.Visibility = Android.Views.ViewStates.Gone;
            HomePage.Visibility = Android.Views.ViewStates.Visible;
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
            HomePage = (LinearLayout)FindViewById(Resource.Id.homeLayout);
            doorLockRecyle = (AndroidX.RecyclerView.Widget.RecyclerView)FindViewById(Resource.Id.doorLockRecycler);
            addDoorLockBtn = (ImageView)FindViewById(Resource.Id.add);
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
        

    }
}

