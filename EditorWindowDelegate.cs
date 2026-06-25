using window;

namespace SourceWriter
{
    public class EditorWindowDelegate : NSWindowDelegate
    {
        public NSWindow Window { get; set; }
        

        public EditorWindowDelegate(NSWindow window)
        {
            Window = window;
        }
        
        /*public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var viewController = Window.ContentViewController as ViewController;

            NSNotificationCenter.DefaultCenter.AddObserver(
                NSText.DidChangeNotification,
                _ =>
                {
                    Window.DocumentEdited = true;
                });
        }*/

        public override bool WindowShouldClose(NSObject sender)
        {
            if (Window.DocumentEdited)
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Critical,
                    InformativeText = "Save changes to document before closing window?",
                    MessageText = "Save Document"
                };

                alert.AddButton("Save");
                alert.AddButton("Lose Changes");
                alert.AddButton("Cancel");

                var result = alert.RunSheetModal(Window);

                switch (result)
                {
                    case 1000:
                    {
                        var viewController = Window.ContentViewController as ViewController;

                        if (Window.RepresentedUrl != null)
                        {
                            File.WriteAllText(
                                Window.RepresentedUrl.Path,
                                viewController.EditorTextView.Value   // remplacer par la vraie propriété
                            );

                            return true;
                        }

                        var dlg = new NSSavePanel();
                        dlg.Title = "Save Document";

                        dlg.BeginSheet(Window, (nint rslt) =>
                        {
                            if (rslt == 1)
                            {
                                var path = dlg.Url.Path;

                                File.WriteAllText(
                                    path,
                                    viewController.EditorTextView.Value // remplacer par la vraie propriété
                                );

                                Window.DocumentEdited = false;
                                Window.SetTitleWithRepresentedFilename(Path.GetFileName(path));
                                Window.RepresentedUrl = dlg.Url;
                                Window.Close();
                            }
                        });

                        return false;
                    }

                    case 1001:
                        return true;

                    case 1002:
                        return false;
                }
            }

            return true;
        }
    }
}