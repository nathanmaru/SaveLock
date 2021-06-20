using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using ShareLockRevamp.Models;

namespace ShareLockRevamp.Adapters
{
    class YourVisitsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<YourVisitsAdapterClickEventArgs> ItemClick;
        public event EventHandler<YourVisitsAdapterClickEventArgs> ItemLongClick;
        List<Request> items;

        public YourVisitsAdapter(List<Request> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.YourVisitsItem;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new YourVisitsAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as YourVisitsAdapterViewHolder;
            holder.FamilyName.Text = items[position].FamilyName;
            holder.DoorLockName.Text = items[position].DoorLockName;
            holder.RequestDate.Text = items[position].RequestDate;
            holder.StatusTxt.Text = items[position].Status;
            holder.Address.Text = items[position].Address;
        }

        public override int ItemCount => items.Count;

        void OnClick(YourVisitsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(YourVisitsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class YourVisitsAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView FamilyName;
        public TextView RequestDate;
        public TextView DoorLockName;
        public TextView StatusTxt;
        public TextView Address;

        public YourVisitsAdapterViewHolder(View itemView, Action<YourVisitsAdapterClickEventArgs> clickListener,
                            Action<YourVisitsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            FamilyName = (TextView)itemView.FindViewById(Resource.Id.familyName);
            RequestDate = (TextView)itemView.FindViewById(Resource.Id.requestDate);
            DoorLockName = (TextView)itemView.FindViewById(Resource.Id.DoorLockName);
            StatusTxt = (TextView)itemView.FindViewById(Resource.Id.statusTxt);
            Address = (TextView)itemView.FindViewById(Resource.Id.address);
            itemView.Click += (sender, e) => clickListener(new YourVisitsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new YourVisitsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class YourVisitsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}