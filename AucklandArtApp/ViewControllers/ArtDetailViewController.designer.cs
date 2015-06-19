// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ViewControllers
{
	[Register ("ArtDetailViewController")]
	partial class ArtDetailViewController
	{
		[Outlet]
		UIKit.UITextView artTextView { get; set; }

		[Outlet]
		UIKit.UIButton backBtn { get; set; }

		[Outlet]
		UIKit.UIButton mapBtn { get; set; }

		[Outlet]
		UIKit.UIButton takeMeBtn { get; set; }

		[Outlet]
		UIKit.UILabel titleLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (backBtn != null) {
				backBtn.Dispose ();
				backBtn = null;
			}

			if (mapBtn != null) {
				mapBtn.Dispose ();
				mapBtn = null;
			}

			if (takeMeBtn != null) {
				takeMeBtn.Dispose ();
				takeMeBtn = null;
			}

			if (titleLbl != null) {
				titleLbl.Dispose ();
				titleLbl = null;
			}

			if (artTextView != null) {
				artTextView.Dispose ();
				artTextView = null;
			}
		}
	}
}
