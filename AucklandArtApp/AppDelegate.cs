using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Google.Maps;


namespace AucklandArtApp
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        const string MapsApiKey = "AIzaSyDE7m0BZjnjRfoyGVNFAHhZLiIUN01NNVM";

        public RootViewController RootViewController { get { return window.RootViewController as RootViewController; } }

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MapServices.ProvideAPIKey(MapsApiKey);
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);
			
            // If you have defined a root view controller, set it here:
            window.RootViewController = new RootViewController();
			
            // make the window visible
            window.MakeKeyAndVisible();
			
            return true;
        }
    }
}

