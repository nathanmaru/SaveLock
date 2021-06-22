using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using ShareLockRevamp.Helpers;
using ShareLockRevamp.Models;
using ShareLockRevamp.EventListeners;

namespace ShareLockRevamp.Fragments
{
    public class AddDoorLockFragment : AndroidX.Fragment.App.DialogFragment
    {
        EditText DoorId;
        EditText Doorname;
        EditText Password;
        EditText Address;
        EditText OwnerName;
        Button Addbtn;
        //ActiveUser activeusername;
        DoorLockStockListener stockListener;
        List<Stock> stockList;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AddDoorLocksLayout, container, false);
            DoorId = (EditText)view.FindViewById(Resource.Id.doorLockId);
            Doorname = (EditText)view.FindViewById(Resource.Id.doorLockName);
            Password = (EditText)view.FindViewById(Resource.Id.doorLockPassword);
            Address = (EditText)view.FindViewById(Resource.Id.doorLockAddress);
            OwnerName = (EditText)view.FindViewById(Resource.Id.familyOwner);
            Addbtn = (Button)view.FindViewById(Resource.Id.addDoorLockBtn);

            Addbtn.Click += Addbtn_Click;

            return view;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields() == 0)
            {
                if(CheckIfDoorID() == 0)
                {
                    string doorId = DoorId.Text;
                    string doorName = Doorname.Text;
                    string password = Password.Text;
                    string address = Address.Text;
                    string ownername = OwnerName.Text;

                    HashMap doorlockInfo = new HashMap();
                    doorlockInfo.Put("Key", doorId);
                    doorlockInfo.Put("DoorName", doorName);
                    doorlockInfo.Put("Password", password);
                    doorlockInfo.Put("Username", ActiveUser.Username);
                    doorlockInfo.Put("Address", address);
                    doorlockInfo.Put("FamilyName", ownername);
                    doorlockInfo.Put("OTP", "not set");

                    AndroidX.AppCompat.App.AlertDialog.Builder dialog = new AndroidX.AppCompat.App.AlertDialog.Builder(Activity);
                    dialog.SetTitle("Adding DoorLock");
                    dialog.SetMessage("Are you sure?");
                    dialog.SetPositiveButton("Continue", (senderAlert, args) =>
                    {
                        DatabaseReference newNoteRef = AppDataHelper.GetDatabase().GetReference("doorLockInfo").Push();
                        newNoteRef.SetValue(doorlockInfo);
                        ModifyStock();
                        Toast.MakeText(Addbtn.Context, "DoorLock Added!", ToastLength.Short).Show();
                        this.Dismiss();
                    });
                    dialog.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        dialog.Dispose();
                    });
                    dialog.Show();
                }
                else
                {
                    Toast.MakeText(Addbtn.Context, "DoorLockId is Invalid!", ToastLength.Short).Show();
                }
                
            }
            else
            {
                Toast.MakeText(Addbtn.Context, "Don't leave empty fields!", ToastLength.Short).Show();
            }
            
        }

        private void ModifyStock()
        {
            AppDataHelper.GetDatabase().GetReference("doorlocks/" + DoorId.Text + "/Fullname").SetValue("Sold");
        }

        private int CheckIfDoorID()
        {
            RetrieveStocks();
            if (CheckIfAvailable() == 1)
            {
                return 0;
            }
            else 
            { 
                return 1; 
            }
            
            
        }

        private int CheckIfAvailable()
        {
            List<Stock> SearchResult = (from stock in stockList
                                          where stock.ID == DoorId.Text &&
                                          stock.Status == "Available"
                                          select stock).ToList();
            return SearchResult.Count;
        }

        private void RetrieveStocks()
        {
            stockListener = new DoorLockStockListener();
            stockListener.Create();
            stockListener.StockRetrived += StockListener_StockRetrived;
        }

        private void StockListener_StockRetrived(object sender, DoorLockStockListener.StockDataEventArgs e)
        {
            stockList = e.Stock;
        }

        private int CheckEmptyFields()
        {
            if (DoorId.Text == "" || DoorId.Text == null) return 1;
            if (Doorname.Text == "" || Doorname.Text == null) return 1;
            if (Password.Text == "" || Password.Text == null) return 1;
            if (Address.Text == "" || Address.Text == null) return 1;
            if (OwnerName.Text == "" || OwnerName.Text == null) return 1;

            return 0;
        }
    }
}