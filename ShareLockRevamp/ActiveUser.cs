﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareLockRevamp
{
    public class ActiveUser
    {
        public static string Username { get; set; }
        public static string ID { get; set; }
        public static string Fullname { get; set; }
        
        public static string Email { get; set; }
        public static string Password { get; set; }
    }
}