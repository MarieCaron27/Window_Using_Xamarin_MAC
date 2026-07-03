// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace window
{
	partial class DocumentPanel
	{
		[Outlet]
		AppKit.NSPanel DocumentPanelController { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DocumentPanelController != null) {
				DocumentPanelController.Dispose ();
				DocumentPanelController = null;
			}

		}
	}
}
