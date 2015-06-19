using System;
using UIKit;
using Util;

namespace CustomViews
{
    public class CustomTableViewStyle : UITableView
    {
        public CustomTableViewStyle(CoreGraphics.CGRect frame)
        {
            Initialize(frame);
        }

        void Initialize(CoreGraphics.CGRect frame)
        {
            Frame = frame;
            AutoresizingMask = UIViewAutoresizing.All;
            ScrollEnabled = true;
            SectionFooterHeight = 5;
            AllowsSelection = true;
            AllowsMultipleSelection = false;
            SeparatorColor = UIColor.LightGray;
            RowHeight = UIScreen.MainScreen.Bounds.Height / 5;   // Your table row height!
            SeparatorInset = UIEdgeInsets.Zero;
            DelaysContentTouches = true;
            CanCancelContentTouches = true;
        }
    }
}

