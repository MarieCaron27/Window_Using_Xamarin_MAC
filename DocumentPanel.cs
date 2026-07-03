using System;

using AppKit;
using Foundation;

namespace window;

[Register("DocumentPanel")]
public partial class DocumentPanel : NSViewController
{
    public DocumentPanel() : base("DocumentPanel", null)
    {
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        // Do any additional setup after loading the view.
    }
}