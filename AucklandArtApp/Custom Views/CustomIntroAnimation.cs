using System;
using UIKit;
using CoreGraphics;

namespace CustomViews
{
    public class CustomIntroAnimation : UIView
    {
        UIImageView overlay;
        UIImageView imageView;

        public CustomIntroAnimation(UIImage image, UIColor color)
        {
            overlay = new UIImageView(UIScreen.MainScreen.Bounds);                     
            overlay.BackgroundColor = color;
            overlay.Alpha = 0.3f;

            imageView = new UIImageView(UIScreen.MainScreen.Bounds);
            imageView.Bounds = new CGRect(UIScreen.MainScreen.Bounds.Width / 2 - 65, UIScreen.MainScreen.Bounds.Height / 2 - 65, 130, 130);
            imageView.Image = image;

            AddSubviews(overlay, imageView);

            RemoveIntro();
        }

        public void RemoveIntro()
        {
            UIView.Animate(1.5f, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                {
                    Alpha = 0;
                },
                () =>
                {      
                      
                }
            );
        }
    }
}