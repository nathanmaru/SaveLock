using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp.Models
{
    public class Stock
    {
        public string ID { get; set; }
        public string DoorLockName { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public string DateAdded { get; set; }

        public string DateModified { get; set; }
        
    }
}