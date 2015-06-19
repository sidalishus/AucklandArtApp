using System;
using CoreGraphics;
using UIKit;
using AucklandArtApp;
using Camera;
using ViewControllers;
using Util;

namespace AucklandArtApp
{
    public partial class SideMenuController : BaseController
    {
        public ContentController contentController;
        public ArtDetailViewController artDetailViewController;
        public MapViewController mapViewController;
        public ImagePickerController imageController;

        public SideMenuController()
            : base(null, null)
        {            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavController.NavigationBarHidden = true;
        }

        public void ContentController()
        {
            contentController = new ContentController();
            SidebarController.ChangeContentView(contentController);
        }

        public void ArtDetailController(int artId)
        {
            artDetailViewController = new ArtDetailViewController();
            artDetailViewController.Art = artId;
            SidebarController.ChangeContentView(artDetailViewController);
        }

        public void MapController()
        {
            mapViewController = new MapViewController();
            SidebarController.ChangeContentView(mapViewController);
        }

        public void ImageController()
        {
            imageController = new ImagePickerController();
            SidebarController.ChangeContentView(imageController);
        }
    }
}