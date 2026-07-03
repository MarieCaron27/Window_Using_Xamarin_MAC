namespace window;
using AppKit;

[Register("AppDelegate")]
public class AppDelegate : NSApplicationDelegate
{
    public int UntitledWindowCount { get; set;} =1;
    
    public override void DidFinishLaunching(NSNotification notification)
    {
        // Insert code here to initialize your application
        // Display panel
        var panel = new DocumentPanel();
        panel.MakeKeyAndOrderFront (this);
    }

    public override void WillTerminate(NSNotification notification)
    {
        // Insert code here to tear down your application
    }
    
    public override NSApplicationTerminateReply ApplicationShouldTerminate (NSApplication sender)
    {
        // See if any window needs to be saved first
        foreach (NSWindow window in NSApplication.SharedApplication.AccessibilityWindows) {
            if (window.Delegate != null && !window.Delegate.WindowShouldClose (this)) {
                // Did the window terminate the close?
                return NSApplicationTerminateReply.Cancel;
            }
        }

        // Allow normal termination
        return NSApplicationTerminateReply.Now;
    }
    
    [Export ("newDocument:")]
    void NewDocument (NSObject sender) {
        //var window = NSApplication.SharedApplication.KeyWindow; //Shows the window that is currently active
        
        // Get new window
        var storyboard = NSStoryboard.FromName ("Main", null);
        var controller = storyboard.InstantiateInitialController() as NSWindowController;
        
        // Display
        controller?.ShowWindow(this);

        // Set the title
        controller?.Window.Title = (++UntitledWindowCount == 1) ? "untitled" : string.Format ("untitled {0}", UntitledWindowCount);
    }
    
    [Export("openDocument:")]
    void OpenDocument(NSObject sender)
    {
        var panel = new NSOpenPanel();

        panel.Begin(result =>
        {
            if (result != 1)
                return;

            var path = panel.Url.Path;

            // Vérifie si le fichier est déjà ouvert
            foreach (NSWindow window in NSApplication.SharedApplication.AccessibilityWindows)
            {
                var content = window.ContentViewController as ViewController;

                if (content != null && content.FilePath == path)
                {
                    window.MakeKeyAndOrderFront(this);
                    return;
                }
            }

            // Sinon, créer une nouvelle fenêtre
            var storyboard = NSStoryboard.FromName("Main", null);
            var controller = storyboard.InstantiateInitialController() as NSWindowController;

            controller?.ShowWindow(this);

            var viewController = controller?.Window.ContentViewController as ViewController;
            viewController?.LoadFile(path);
        });
    }
}