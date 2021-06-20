using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ShareLockRevamp.Models;
using System;
using System.Collections.Generic;

namespace ShareLockRevamp.Adapters
{
    class AllDoorLockAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AllDoorLockAdapterClickEventArgs> ItemClick;
        public event EventHandler<AllDoorLockAdapterClickEventArgs> ItemLongClick;
        List<DoorLock> items;

        public AllDoorLockAdapter(List<DoorLock> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.SearchItem;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new AllDoorLockAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as AllDoorLockAdapterViewHolder;
            holder.FamilyName.Text = items[position].FamilyName;
            holder.Owner.Text = items[position].Username;
            holder.Address.Text = items[position].Address;
            holder.DoorLockName.Text = items[position].DoorLockName;

        }

        public override int ItemCount => items.Count;

        void OnClick(AllDoorLockAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(AllDoorLockAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class AllDoorLockAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView FamilyName;
        public TextView DoorLockName;
        public TextView Owner;
        public TextView Address;

        public AllDoorLockAdapterViewHolder(View itemView, Action<AllDoorLockAdapterClickEventArgs> clickListener,
                            Action<AllDoorLockAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            FamilyName = (TextView)itemView.FindViewById(Resource.Id.familyName);
            DoorLockName = (TextView)itemView.FindViewById(Resource.Id.DoorLockName);
            Owner = (TextView)itemView.FindViewById(Resource.Id.ownerUsername);
            Address = (TextView)itemView.FindViewById(Resource.Id.address);
            itemView.Click += (sender, e) => clickListener(new AllDoorLockAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new AllDoorLockAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class AllDoorLockAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}