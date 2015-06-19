using System;
using UIKit;
using CoreGraphics;

namespace Views
{
    public class LoadingView : UIView
    {
        // control declarations
        UIActivityIndicatorView activitySpinner;
        UILabel loadingLabel;

        public LoadingView(CGRect frame)
            : base(frame)
        {
            // configurable bits
            BackgroundColor = UIColor.Red;
            Alpha = 0;
            AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            nfloat labelHeight = 22;
            nfloat labelWidth = Frame.Width - 20;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            // create the activity spinner, center it horizontall and put it 5 points above center x
            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.White);
            activitySpinner.Frame = new CGRect(
                centerX - (activitySpinner.Frame.Width / 2),
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
            AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            // create and configure the "Loading Data" label
            loadingLabel = new UILabel(new CGRect(
                    centerX - (labelWidth / 2),
                    centerY,
                    labelWidth,
                    labelHeight
                ));
            loadingLabel.BackgroundColor = UIColor.Clear;
            loadingLabel.TextColor = UIColor.White;
            loadingLabel.Text = "Finding Directions";
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
            AddSubview(loadingLabel);
        }

        /// <summary>
        /// Fades in the control
        /// </summary>
        public void Show()
        {
            UIView.Animate(
                0.3, // duration
                () =>
                {
                    Alpha = 0.3f;
                },
                () =>
                {
                    //RemoveFromSuperview();
                }
            );
        }

        /// <summary>
        /// Fades out the control
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.3, // duration
                () =>
                {
                    Alpha = 0;
                },
                () =>
                {
                    //RemoveFromSuperview();
                }
            );
        }
    };
}

