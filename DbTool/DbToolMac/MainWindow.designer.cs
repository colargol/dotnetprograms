// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace DbToolMac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField StatusField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSComboBox ContextBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSComboBox ConnectionBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton ConnectionButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView ResultTextBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView ResultTable { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextView EditorBox { get; set; }

		[Action ("Connection_Click:")]
		partial void Connection_Click (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (StatusField != null) {
				StatusField.Dispose ();
				StatusField = null;
			}

			if (ContextBox != null) {
				ContextBox.Dispose ();
				ContextBox = null;
			}

			if (ConnectionBox != null) {
				ConnectionBox.Dispose ();
				ConnectionBox = null;
			}

			if (ConnectionButton != null) {
				ConnectionButton.Dispose ();
				ConnectionButton = null;
			}

			if (ResultTextBox != null) {
				ResultTextBox.Dispose ();
				ResultTextBox = null;
			}

			if (ResultTable != null) {
				ResultTable.Dispose ();
				ResultTable = null;
			}

			if (EditorBox != null) {
				EditorBox.Dispose ();
				EditorBox = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
