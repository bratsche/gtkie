using System;
using Gtk;
using System.Windows.Forms;

public partial class MainWindow : Gtk.Window
{
    private WebBrowser browser = null;

    [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetParent")]
    internal static extern System.IntPtr SetParent([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWndChild, [System.Runtime.InteropServices.InAttribute()] System.IntPtr hWndNewParent);

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
        Build();
        browser = new WebBrowser();
        browser.Height = 300;
        browser.Width = 400;

        var socket = new Gtk.Socket();
        this.Add(socket);
        socket.SetSizeRequest(400, 300);
        socket.Realize();
        socket.Show();

        var browser_handle = browser.Handle;
        IntPtr window_handle = (IntPtr) socket.Id;
        SetParent(browser_handle, window_handle);

        browser.Navigate("http://www.google.com/");
    }

    protected override void OnSizeAllocated (Gdk.Rectangle allocation)
    {
        base.OnSizeAllocated(allocation);

        if (this.IsRealized)
        {
            System.Drawing.Size size = new System.Drawing.Size(allocation.Width, allocation.Height);
            browser.Size = size;
        }
    }

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Gtk.Application.Quit ();
		a.RetVal = true;
	}
}
