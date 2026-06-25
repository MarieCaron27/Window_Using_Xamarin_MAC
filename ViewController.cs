using ObjCRuntime;
using SourceWriter;

namespace window;

public partial class ViewController : NSViewController
{
    protected ViewController(NativeHandle handle) : base(handle)
    {
        // This constructor is required if the view controller is loaded from a xib or a storyboard.
        // Do not put any initialization here, use ViewDidLoad instead.
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        NSNotificationCenter.DefaultCenter.AddObserver(
            NSText.DidChangeNotification,
            HandleTextChanged);
    }
    
    public override void ViewWillAppear ()
    {
        base.ViewWillAppear ();
        
        var screenFrame = View.Window.Screen.VisibleFrame;

        var x = screenFrame.GetMidX() - 512;
        var y = screenFrame.GetMidY() - 384;

        View.Window.SetFrame(
            new CGRect(x, y, 1024, 768),
            true
        );

        // Set Window Title
        this.View.Window.Title = "Ma fenêtre"; //Puts a title to the window
        
        this.View.Window.Delegate = new EditorWindowDelegate(this.View.Window); //Allows the user to save or not the document
        //CloseWindow();
    }
    
    public void CloseWindow ()
    {
        this.View.Window.Close(); //Closes the window
    }
    
    public NSTextView EditorTextView
    {
        get
        {
            return DocumentEditor.DocumentView as NSTextView; //Allows us to access the text view in ViewControllerDesigner
        }
    }
    
    private void HandleTextChanged(NSNotification notification)
    {
        View.Window.DocumentEdited = true;
    }
}