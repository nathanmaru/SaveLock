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
    public class UserInfo
    {
        public static string Username { get; set; }
        public string ID { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}