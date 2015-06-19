using System;
using UIKit;
using Foundation;
using CustomViews;
using AucklandArtApp;

namespace Camera
{
    public class ImagePickerController : UIImagePickerController
    {
        public ImagePickerController()
        {
            SideMenuController SideMenuController = new SideMenuController();

            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            SourceType = UIImagePickerControllerSourceType.Camera;

            FinishedPickingMedia += (sender, e) =>
            {
                UIImage image = (UIImage)e.Info.ObjectForKey(
                                    new NSString("UIImagePickerControllerOriginalImage"));

                if (image != null)
                {
                    this.InvokeOnMainThread(() =>
                        {
                            image.SaveToPhotosAlbum(delegate(UIImage img, NSError err)
                                { 
                                });
                            string pngFilename = System.IO.Path.Combine(documentsDirectory, "Photo.png"); // hardcoded filename, overwrites each time
                            NSData imgData = image.AsPNG();
                            NSError SaveErr = null;
                            if (imgData.Save(pngFilename, false, out SaveErr))
                            {
                                Console.WriteLine("saved as " + pngFilename);
                            }
                            else
                            {
                                Console.WriteLine("NOT saved as" + pngFilename + " because" + SaveErr.LocalizedDescription);
                            }
                        });
                }
                SideMenuController.MapController();
                GC.Collect();
            };

            // Handle cancellation of picker.
            Canceled += (sender, e) =>
            {
                SideMenuController.MapController();
                GC.Collect();
            };
        }
    }
}