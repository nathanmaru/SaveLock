using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using ShareLockRevamp.Helpers;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp.EventListeners
{
    public class DoorLockListener : Java.Lang.Object, IValueEventListener
    {
        /*List<DoorLock> doorLockList = new List<DoorLock>();
        public event EventHandler<DoorLockDataEventArgs> DoorLockRetrived;
        public class DoorLockDataEventArgs : EventArgs
        {
            public List<DoorLock> DoorLock { get; set; }
        }*/
        List<DoorLock> filterdoorLockList = new List<DoorLock>();
        public event EventHandler<FilterDoorLockDataEventArgs> FilterDoorLockRetrived;
        public class FilterDoorLockDataEventArgs : EventArgs
        {
            public List<DoorLock> FilterDoorLock { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                //doorLockList.Clear();
                filterdoorLockList.Clear();
                foreach (DataSnapshot memberData in child)
                {
                    /*DoorLock doorLock = new DoorLock();
                    doorLock.ID = memberData.Key;
                    doorLock.DoorLockId = memberData.Child("Key").Value.ToString();
                    doorLock.DoorLockName = memberData.Child("DoorName").Value.ToString();
                    doorLock.Password = memberData.Child("Password").Value.ToString();
                    doorLock.Username = memberData.Child("Username").Value.ToString();
                    doorLock.FamilyName = memberData.Child("FamilyName").Value.ToString();
                    doorLock.Address = memberData.Child("Address").Value.ToString();
                    doorLock.OneTimePassword = memberData.Child("OTP").Value.ToString();
                    doorLockList.Add(doorLock);*/
                    if(memberData.Child("Username").Value.ToString() == ActiveUser.Username)
                    {
                        DoorLock filterdoorLock = new DoorLock();
                        filterdoorLock.ID = memberData.Key;
                        filterdoorLock.DoorLockId = memberData.Child("Key").Value.ToString();
                        filterdoorLock.DoorLockName = memberData.Child("DoorName").Value.ToString();
                        filterdoorLock.Password = memberData.Child("Password").Value.ToString();
                        filterdoorLock.Username = memberData.Child("Username").Value.ToString();
                        filterdoorLock.FamilyName = memberData.Child("FamilyName").Value.ToString();
                        filterdoorLock.Address = memberData.Child("Address").Value.ToString();
                        filterdoorLock.OneTimePassword = memberData.Child("OTP").Value.ToString();
                        filterdoorLockList.Add(filterdoorLock);
                    }
                }
                //DoorLockRetrived.Invoke(this, new DoorLockDataEventArgs { DoorLock = doorLockList });
                FilterDoorLockRetrived.Invoke(this, new FilterDoorLockDataEventArgs { FilterDoorLock = filterdoorLockList });
            }
        }
        public void Create()
        {
            DatabaseReference doorlockRef = AppDataHelper.GetDatabase().GetReference("doorLockInfo");
            doorlockRef.AddValueEventListener(this);
        }
    }
}