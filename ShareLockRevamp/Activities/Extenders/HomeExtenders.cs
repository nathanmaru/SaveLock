using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using ShareLockRevamp.Adapters;
using ShareLockRevamp.EventListeners;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp.Activities.Extenders
{
    public class HomeExtenders 
    {

        public static List<DoorLock> doorLockList;
        public static List<DoorLock> filterdoorLockList;
        public static void ShowHomeLayout()
        {
            MainActivity.HomePage.Visibility = Android.Views.ViewStates.Visible;
            
            SetUpDoorLockRecycler();
        }

        public static void RetrieveData()
        {
            DoorLockListener doorLockListener = new DoorLockListener();
            doorLockListener.Create();
            doorLockListener.DoorLockRetrived += DoorLockListener_DoorLockRetrived;
        }

        private static void DoorLockListener_DoorLockRetrived(object sender, DoorLockListener.DoorLockDataEventArgs e)
        {
            filterdoorLockList = e.DoorLock;
            SetUpDoorLockRecycler();
        }

        private static void SetUpDoorLockRecycler()
        {
            DoorLockAdapter doorLockAdapter;
            MainActivity.doorLockRecyle.SetLayoutManager(new LinearLayoutManager(MainActivity.doorLockRecyle.Context));
            doorLockAdapter = new DoorLockAdapter(filterdoorLockList);
            doorLockAdapter.ItemClick += DoorLockAdapter_ItemClick;
            MainActivity.doorLockRecyle.SetAdapter(doorLockAdapter);

        }

        private static void DoorLockAdapter_ItemClick(object sender, DoorLockAdapterClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}