using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Java.Util;
using ShareLockRevamp.EventListeners;
using ShareLockRevamp.Helpers;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp.Activities
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        EditText fullname;
        EditText username;
        EditText email;
        EditText password;
        Button signUpBtn;
        TextView loginRedirector;
        List<Account> AccountList;
        AccountListener accountListener;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //resets activeusername
            ActiveUser.Username = null;

            // Create your application here
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SignUpLayout);

            fullname = (EditText)FindViewById(Resource.Id.fullnameTxt);
            username = (EditText)FindViewById(Resource.Id.usernameTxt);
            email = (EditText)FindViewById(Resource.Id.emailText);
            password = (EditText)FindViewById(Resource.Id.passwordTxt);
            signUpBtn = (Button)FindViewById(Resource.Id.signupBtn);
            loginRedirector = (TextView)FindViewById(Resource.Id.loginRedirect);
            RetriveData();

            signUpBtn.Click += SignUpBtn_Click;
            loginRedirector.Click += LoginRedirector_Click;

        }

        private void RetriveData()
        {
            accountListener = new AccountListener();
            accountListener.Create();
            accountListener.AccountRetrived += AccountListener_AccountRetrived;
        }

        private void AccountListener_AccountRetrived(object sender, AccountListener.AccountDataEventArgs e)
        {
            AccountList = e.Account;
        }

        private void LoginRedirector_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            //Check Text Fields
            if (checkFields() == 0)
            {
                if (CheckExistingUserName() == 0 && CheckExistingEmails() == 0)
                {
                    SignUp();
                    Toast.MakeText(signUpBtn.Context, "Account Created!", ToastLength.Short).Show();
                    var intent = new Intent(this, typeof(MainActivity));

                    ActiveUser.Username = username.Text;
                    intent.PutExtra("userName", username.Text);
                    //pass username through extras
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(signUpBtn.Context, "Username or Email already exist!", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(signUpBtn.Context, "Don't Leave Fields Empty", ToastLength.Short).Show();
            }
            
            
            
        }

        private int checkFields()
        {
            if (username.Text == "") return 1;
            if (password.Text == "") return 1;
            if (fullname.Text == "") return 1;
            if (email.Text == "") return 1;

            return 0;
        }

        private void SignUp()
        {
            string fullName = fullname.Text;
            string userName = username.Text;
            string emailAdd = email.Text;
            string passWord = password.Text;

            HashMap userInfo = new HashMap();
            userInfo.Put("Fullname", fullName);
            userInfo.Put("Username", userName);
            userInfo.Put("Email", emailAdd);
            userInfo.Put("Password", passWord);
            
            DatabaseReference newUserRef = AppDataHelper.GetDatabase().GetReference("accountInfo").Push();
            newUserRef.SetValue(userInfo);
        }

        public int CheckExistingUserName()
        {
            List<Account> SearchResult =
                (from account in AccountList
                 where account.Username.Contains(username.Text) 
                 select account).ToList();
            return SearchResult.Count;
        }
        public int CheckExistingEmails()
        {
            List<Account> SearchResult =
                (from account in AccountList
                 where account.Email.Contains(email.Text)
                 select account).ToList();
            return SearchResult.Count;
        }
    }
}