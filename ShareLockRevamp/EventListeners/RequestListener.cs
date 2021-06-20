using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using ShareLockRevamp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareLockRevamp.Helpers;

namespace ShareLockRevamp.EventListeners
{
    public class RequestListener : Java.Lang.Object, IValueEventListener
    {
        List<Request> requestList = new List<Request>();
        public event EventHandler<RequestDataEventArgs> RequestRetrived;

        public class RequestDataEventArgs : EventArgs
        {
            public List<Request> Request { get; set; }
        }
        List<Request> visitList = new List<Request>();
        public event EventHandler<VisitsDataEventArgs> VisitsRetrived;

        public class VisitsDataEventArgs : EventArgs
        {
            public List<Request> Visit { get; set; }
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
                requestList.Clear();
                foreach (DataSnapshot memberData in child)
                {
                    if (memberData.Child("VisitorUserName").ToString() == ActiveUser.Username)
                    {
                        Request yourVisits = new Request();
                        yourVisits.ID = memberData.Key;
                        yourVisits.DoorLockId = memberData.Child("DoorLockID").Value.ToString();
                        yourVisits.Fullname = memberData.Child("Fullname").Value.ToString();
                        yourVisits.Message = memberData.Child("Message").Value.ToString();
                        yourVisits.OwnerUsername = memberData.Child("OwnerUsername").Value.ToString();
                        yourVisits.Status = memberData.Child("isApprove").Value.ToString();
                        yourVisits.RequestDate = memberData.Child("RequestDate").Value.ToString();
                        yourVisits.VisitorUsername = memberData.Child("VisitorUserName").Value.ToString();
                        yourVisits.OneTimePassword = memberData.Child("OTP").Value.ToString();

                        visitList.Add(yourVisits);
                    }
                    if (memberData.Child("OwnerUsername").ToString() == ActiveUser.Username)
                    {
                        Request request = new Request();
                        request.ID = memberData.Key;
                        request.DoorLockId = memberData.Child("DoorLockID").Value.ToString();
                        request.Fullname = memberData.Child("Fullname").Value.ToString();
                        request.Message = memberData.Child("Message").Value.ToString();
                        request.OwnerUsername = memberData.Child("OwnerUsername").Value.ToString();
                        request.Status = memberData.Child("isApprove").Value.ToString();
                        request.RequestDate = memberData.Child("RequestDate").Value.ToString();
                        request.VisitorUsername = memberData.Child("VisitorUserName").Value.ToString();
                        request.OneTimePassword = memberData.Child("OTP").Value.ToString();
                        requestList.Add(request);
                    }
                    
                }
                RequestRetrived.Invoke(this, new RequestDataEventArgs { Request = requestList });
                VisitsRetrived.Invoke(this, new VisitsDataEventArgs { Visit = visitList });
            }
        }
        public void Create()
        {
            DatabaseReference requestRef = AppDataHelper.GetDatabase().GetReference("requestInfo");
            requestRef.AddValueEventListener(this);
        }
    }
}