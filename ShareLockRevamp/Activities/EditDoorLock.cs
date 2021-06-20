using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp.Activities
{
    [Activity(Label = "EditDoorLock")]
    public class EditDoorLock : Activity
    {
        EditText doorLockID;
        EditText doorLockName;
        EditText doorpassword;
        EditText address;
        EditText username;
        Button saveEdit;
        Button deleteThis;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.EditDoorLock);
            // Create your application here
            doorLockID = (EditText)FindViewById(Resource.Id.doorLockId);
            doorLockName = (EditText)FindViewById(Resource.Id.doorlockName);
            doorpassword = (EditText)FindViewById(Resource.Id.doorLockPassword);
            address = (EditText)FindViewById(Resource.Id.doorLockAddress);
            username = (EditText)FindViewById(Resource.Id.familyOwner);
            saveEdit = (Button)FindViewById(Resource.Id.EditDoorLockButton);
            deleteThis = (Button)FindViewById(Resource.Id.DeleteDoorLockButton);

            doorLockID.Text = DoorLockClick.DoorLockId;

        }
    }
}