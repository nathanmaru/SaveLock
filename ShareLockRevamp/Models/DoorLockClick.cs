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
    public class DoorLockClick
    {
        public static string ID { get; set; }
        public static string DoorLockId { get; set; }
        public static string DoorLockName { get; set; }
        public static string Password { get; set; }
        public static string Username { get; set; }
        public static string Address { get; set; }

        public static string FamilyName { get; set; }
        public static string OneTimePassword { get; set; }
    }
}