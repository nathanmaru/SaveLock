using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using ShareLockRevamp.Adapters;
using ShareLockRevamp.EventListeners;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareLockRevamp;
using ShareLockRevamp.Fragments;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Content;
using ShareLockRevamp.Helpers;

namespace ShareLockRevamp.Activities.Extenders
{
    public class HomeExtenders : AppCompatActivity
    {

        public static List<DoorLock> doorLockList;
        public static List<DoorLock> filterdoorLockList;
        DoorLock thisDoorLock;
        DoorLockListener DoorLockListener;

        public static void ShowHomeLayout()
        {
            MainActivity.HomePage.Visibility = Android.Views.ViewStates.Visible;
            //MainActivity.EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
            //MainActivity.VisitLayoutPage.Visibility = Android.Views.ViewStates.Gone;
        }

        

        public static void RetrieveData()
        {
            DoorLockListener doorLockListener = new DoorLockListener();
            doorLockListener.Create();
            //doorLockListener.DoorLockRetrived += DoorLockListener_DoorLockRetrived;
            doorLockListener.FilterDoorLockRetrived += DoorLockListener_FilterDoorLockRetrived;
        }

        private static void DoorLockListener_FilterDoorLockRetrived(object sender, DoorLockListener.FilterDoorLockDataEventArgs e)
        {
            filterdoorLockList = e.FilterDoorLock;
            SetUpDoorLockRecycler();
        }

       

        private static void SetUpDoorLockRecycler()
        {
            
            HomeExtenders foo = new HomeExtenders();
            foo.click();
            
        }

        public void click()
        {
            DoorLockAdapter doorLockAdapter;
            MainActivity.doorLockRecyle.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(MainActivity.doorLockRecyle.Context));
            doorLockAdapter = new DoorLockAdapter(filterdoorLockList);
            doorLockAdapter.ItemClick += DoorLockAdapter_ItemClick;
            MainActivity.doorLockRecyle.SetAdapter(doorLockAdapter);
        }

        private void DoorLockAdapter_ItemClick(object sender, DoorLockAdapterClickEventArgs e)
        {
            DoorLockClick.ID = filterdoorLockList[e.Position].ID;
            MainActivity.doorLockID.Text = filterdoorLockList[e.Position].DoorLockId;
            MainActivity.doorLockName.Text = filterdoorLockList[e.Position].DoorLockName;
            MainActivity.doorpassword.Text= filterdoorLockList[e.Position].Password;
            MainActivity.address.Text = filterdoorLockList[e.Position].Address;
            MainActivity.username.Text = filterdoorLockList[e.Position].FamilyName;
            MainActivity.EditDoorLayout.Visibility = Android.Views.ViewStates.Visible;
            MainActivity.HomePage.Visibility = Android.Views.ViewStates.Gone;
        }

        

        
    }
}