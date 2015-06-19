using System;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreAnimation;

namespace AucklandArtApp
{
    public partial class IntroController : BaseController
    {
        UIImageView overlay;
        UIImageView imageView;
        UIImageView aucklandNightImageView;

        public IntroController()
            : base(null, null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavController.NavigationBarHidden = true;

            overlay = new UIImageView(UIScreen.MainScreen.Bounds);                     
            overlay.BackgroundColor = UIColor.Black;

            imageView = new UIImageView(UIScreen.MainScreen.Bounds);
            imageView.Bounds = new CGRect(UIScreen.MainScreen.Bounds.Width / 2 - 65, UIScreen.MainScreen.Bounds.Height / 2 - 65, 130, 130);
            imageView.Image = UIImage.FromFile("art");
            imageView.AnimationImages = new UIImage[]
            {
                UIImage.FromFile("art"),
                UIImage.FromFile("Exp"),
                UIImage.FromFile("phoneMap"),
                UIImage.FromFile("ar"),
                UIImage.FromFile("camera")
            };
            imageView.AnimationRepeatCount = 0;
            imageView.AnimationDuration = 3.5f;
            imageView.StartAnimating();

            aucklandNightImageView = new UIImageView(new CGRect(0, View.Frame.Bottom - 160, View.Frame.Width, 160));
            aucklandNightImageView.Image = UIImage.FromFile("auckland_night");
            aucklandNightImageView.Alpha = 0.15f;

            View.AddSubviews(overlay, imageView, aucklandNightImageView);

            SplashEnd();
        }

        private void SplashEnd()
        {        
            UIView.Animate(1, 1f, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    imageView.Alpha = 0;      
                    aucklandNightImageView.Alpha = 1; 
                },
                () =>
                {
                    NavController.PushViewController(new ContentController(), true);
                }
            );
        }
    }
}