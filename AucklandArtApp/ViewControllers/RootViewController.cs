using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace AucklandArtApp
{
    public partial class RootViewController : UIViewController
    {
        public SidebarController SidebarController { get; private set; }

        public NavController NavController { get; private set; }

        public SideMenuController SideMenuController { get; private set; }

        public ContentController ContentController { get; private set; }

        public MapViewController MapViewController { get; private set; }

        public RootViewController()
            : base(null, null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavController = new NavController();
            ContentController = new ContentController();
            MapViewController = new MapViewController();

            SideMenuController = new SideMenuController();

            NavController.PushViewController(new MapViewController(), false);

            SidebarController = new SidebarController(this, NavController, SideMenuController);
            SidebarController.MenuWidth = (int)UIScreen.MainScreen.Bounds.Width;
        }
    }
}
