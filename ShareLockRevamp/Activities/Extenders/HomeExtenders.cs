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

namespace ShareLockRevamp.Activities.Extenders
{
    public class HomeExtenders : AppCompatActivity
    {

        public static List<DoorLock> doorLockList;
        public static List<DoorLock> filterdoorLockList;
        

        public static void ShowHomeLayout()
        {
            MainActivity.HomePage.Visibility = Android.Views.ViewStates.Visible;
            
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
            DoorLock thisDoorLock = filterdoorLockList[e.Position];
            EditDoorLockFragment editDoorLockFragment = new EditDoorLockFragment(thisDoorLock);
            var trans = SupportFragmentManager.BeginTransaction();
            editDoorLockFragment.Show(trans, "edit");
        }
    }
}