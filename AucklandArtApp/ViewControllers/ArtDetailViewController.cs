
using System;

using Foundation;
using UIKit;
using AucklandArtApp;
using ExternalMaps.Plugin;
using AucklandArtAppShared;
using CustomViews;
using Xamarin.Controls;

namespace ViewControllers
{
    public partial class ArtDetailViewController : UIViewController
    {
        public int Art { get; set; }

        CustomMenu _menuView;

        SideMenuController SideMenuController;

        public ArtDetailViewController()
            : base("ArtDetailViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AlertCenter.Default.PostMessage("Navigate to", Artworks.Arts[Art].Title,
                UIImage.FromFile("phoneMap.png"), delegate
                {
                });

            _menuView = new CustomMenu(View);
            _menuView.SetViewController = this;

            SideMenuController = new SideMenuController();

            SetupButtons();
            SetupText();
        }

        private void SetupButtons()
        {
            takeMeBtn.TouchUpInside += (object sender, EventArgs e) => CrossExternalMaps.Current.NavigateTo(Artworks.Arts[Art].Title, Artworks.Arts[Art].Latitude, Artworks.Arts[Art].Longitude);
            takeMeBtn.Layer.CornerRadius = 20;
            takeMeBtn.BackgroundColor = UIColor.FromWhiteAlpha(0.75f, 0.5f);
        }

        private void SetupText()
        {
            titleLbl.Text = Artworks.Arts[Art].Title;
            artTextView.Text = Artworks.Arts[Art].Description;
        }
    }
}

