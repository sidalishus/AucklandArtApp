using System;
using UIKit;
using CoreGraphics;
using AucklandArtApp;
using Util;
using Social;

namespace CustomViews
{
    public class CustomMenu : UIView
    {
        SideMenuController SideMenuController;

        public UIViewController SetViewController { get; set; }

        UIButton backButton;
        UIButton menuButton;
        UIButton discoverButton;
        UIButton findButton;
        UIButton camButton;
        UIButton facebookButton;
        UIButton twitterButton;

        bool menuOpen;

        public CustomMenu(UIView view)
        {
            SideMenuController = new SideMenuController();

            backButton = new UIButton(UIButtonType.Custom);
            backButton.Frame = new CGRect(5, 25, 45, 45);
            backButton.SetImage(UIImage.FromFile("back_white"), UIControlState.Normal);
            backButton.BackgroundColor = UIColor.FromWhiteAlpha(0.3f, 0.75f);
            backButton.Layer.CornerRadius = 21.5f;
            backButton.TouchUpInside += delegate
            {
                SideMenuController.ContentController();
            };

            menuButton = new UIButton(UIButtonType.Custom);
            menuButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 30, 45, 45);
            menuButton.SetImage(UIImage.FromFile("menu_white"), UIControlState.Normal);
            menuButton.BackgroundColor = UIColor.FromWhiteAlpha(0.3f, 0.75f);
            menuButton.Layer.CornerRadius = 21.5f;
            menuButton.TouchUpInside += delegate
            {   
                if (menuOpen == false)
                {
                    OpenMenu();
                }
                else
                {
                    CloseMenu();
                }
            };

            findButton = new UIButton(UIButtonType.Custom);
            findButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 85, 45, 45);
            findButton.SetImage(UIImage.FromFile("phoneMap"), UIControlState.Normal);
            findButton.TouchUpInside += delegate
            {
                SideMenuController.MapController();
            };
            findButton.Alpha = 0;

            discoverButton = new UIButton(UIButtonType.Custom);
            discoverButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 135, 45, 45);
            discoverButton.SetImage(UIImage.FromFile("art"), UIControlState.Normal);
            discoverButton.TouchUpInside += delegate
            {
                SideMenuController.ContentController();
            };
            discoverButton.Alpha = 0;

            camButton = new UIButton(UIButtonType.Custom);
            camButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 185, 45, 45);
            camButton.SetImage(UIImage.FromFile("camera"), UIControlState.Normal);
            camButton.TouchUpInside += delegate
            {
                SideMenuController.ImageController();
            };
            camButton.Alpha = 0;

            facebookButton = new UIButton(UIButtonType.Custom);
            facebookButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 235, 45, 45);
            facebookButton.SetImage(UIImage.FromFile("facebook"), UIControlState.Normal);
            facebookButton.TouchUpInside += delegate
            {
                var slComposer = SLComposeViewController.FromService(SLServiceType.Facebook);

                if (SLComposeViewController.IsAvailable(SLServiceKind.Facebook))
                {
                    slComposer = SLComposeViewController.FromService(SLServiceType.Facebook);
                    slComposer.CompletionHandler += (result) =>
                    {
                        InvokeOnMainThread(() =>
                            {
                                SetViewController.DismissViewController(true, null);
                            });
                    };
                    SetViewController.PresentViewController(slComposer, true, null);
                }
            };
            facebookButton.Alpha = 0;

            twitterButton = new UIButton(UIButtonType.Custom);
            twitterButton.Frame = new CGRect(UIScreen.MainScreen.Bounds.Right - 55, 285, 45, 45);
            twitterButton.SetImage(UIImage.FromFile("twitter"), UIControlState.Normal);
            twitterButton.TouchUpInside += delegate
            {
                var slComposer = SLComposeViewController.FromService(SLServiceType.Twitter);

                if (SLComposeViewController.IsAvailable(SLServiceKind.Twitter))
                {
                    slComposer = SLComposeViewController.FromService(SLServiceType.Twitter);
                    slComposer.SetInitialText("#AucklandArtApp");
                    slComposer.CompletionHandler += (result) =>
                    {
                        InvokeOnMainThread(() =>
                            {
                                SetViewController.DismissViewController(true, null);
                            });
                    };
                    SetViewController.PresentViewController(slComposer, true, null);
                }
            };
            twitterButton.Alpha = 0;

            view.AddSubviews(backButton, menuButton, findButton, discoverButton, camButton, facebookButton, twitterButton);
        }

        private void OpenMenu()
        {
            UIView.Animate(0.5f, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    menuButton.SetImage(UIImage.FromFile("menu_white_vertical"), UIControlState.Normal);

                    discoverButton.Zoom(true, 0.3f, null);
                    findButton.Zoom(true, 0.4f, null);
                    camButton.Zoom(true, 0.5f, null);
                    facebookButton.Zoom(true, 0.6f, null);
                    twitterButton.Zoom(true, 0.7f, null);
                },
                () =>
                {
                    menuOpen = !menuOpen;
                }
            );
        }

        private void CloseMenu()
        {
            UIView.Animate(0.5f, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    menuButton.SetImage(UIImage.FromFile("menu_white"), UIControlState.Normal);

                    discoverButton.Zoom(false, 0.3f, null);
                    findButton.Zoom(false, 0.4f, null);
                    camButton.Zoom(false, 0.5f, null);
                    facebookButton.Zoom(false, 0.6f, null);
                    twitterButton.Zoom(false, 0.7f, null);
                },
                () =>
                {
                    menuOpen = !menuOpen;
                }
            );
        }
    }
}