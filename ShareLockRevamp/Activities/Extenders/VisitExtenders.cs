using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShareLockRevamp.Models;
using AndroidX.AppCompat.App;
using ShareLockRevamp.Adapters;
using ShareLockRevamp.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using ShareLockRevamp.Helpers;

namespace ShareLockRevamp.Activities.Extenders
{
    public class VisitExtenders
    {
        
        RequestListener requestListener;
        List<Request> visitList;



        public void ShowVisitPage()
        {
            MainActivity.HomePage.Visibility = Android.Views.ViewStates.Gone;
            MainActivity.EditDoorLayout.Visibility = Android.Views.ViewStates.Gone;
            MainActivity.VisitLayoutPage.Visibility = Android.Views.ViewStates.Visible;
        }
        public void RetrieveData()
        {
            requestListener = new RequestListener();
            requestListener.Create();
            requestListener.VisitsRetrived += RequestListener_VisitsRetrived;
        }

        private void RequestListener_VisitsRetrived(object sender, RequestListener.VisitsDataEventArgs e)
        {
            visitList = e.Visit;
            SetUpRecycleView();

        }

        private static void SetUpRecycleView()
        {
            /*YourVisitsAdapter yourVisitsAdapter;
            List<Request> visitList = new List<Request>();
            MainActivity MainActivity = new MainActivity();
            MainActivity.yourRequests.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(MainActivity.yourRequests.Context));
            //yourVisitsAdapter = new YourVisitsAdapter(visitList);
            //yourVisitsAdapter.Delete += MainActivity.YourVisitsAdapter_Delete;
            MainActivity.yourRequests.SetAdapter(yourVisitsAdapter);*/
        }
        
        

       
    }
}