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
    public class DoorLockStockListener : Java.Lang.Object, IValueEventListener
    {
        List<Stock> stockList = new List<Stock>();
        public event EventHandler<StockDataEventArgs> StockRetrived;
        public class StockDataEventArgs : EventArgs
        {
            public List<Stock> Stock { get; set; }
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
                
                stockList.Clear();
                foreach (DataSnapshot memberData in child)
                {
                    Stock stock = new Stock();
                    stock.ID = memberData.Key;
                    stock.DoorLockName = memberData.Child("Doorname").Value.ToString();
                    stock.Price = memberData.Child("Price").Value.ToString();
                    stock.Status = memberData.Child("Status").Value.ToString();
                    stock.DateAdded = memberData.Child("DateAdded").Value.ToString();
                    stock.DateModified = memberData.Child("DateModified").Value.ToString();

                    stockList.Add(stock);
                    
                }

                StockRetrived.Invoke(this, new StockDataEventArgs { Stock = stockList });
            }
        }
        public void Create()
        {
            DatabaseReference stockRef = AppDataHelper.GetDatabase().GetReference("doorlocks");
            stockRef.AddValueEventListener(this);
        }
       
    }
}