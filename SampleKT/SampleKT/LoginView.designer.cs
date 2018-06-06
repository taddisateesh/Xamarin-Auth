// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SampleKT
{
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UIButton BtnFacebook { get; set; }

		[Outlet]
		UIKit.UIButton BtnGoogle { get; set; }

		[Outlet]
		UIKit.UIButton BtnLinkedIn { get; set; }

		[Outlet]
		UIKit.UIButton BtnTwitter { get; set; }

		[Outlet]
		UIKit.UIImageView ImgVUser { get; set; }

		[Outlet]
		UIKit.UILabel LblDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnFacebook != null) {
				BtnFacebook.Dispose ();
				BtnFacebook = null;
			}

			if (BtnGoogle != null) {
				BtnGoogle.Dispose ();
				BtnGoogle = null;
			}

			if (BtnTwitter != null) {
				BtnTwitter.Dispose ();
				BtnTwitter = null;
			}

			if (BtnLinkedIn != null) {
				BtnLinkedIn.Dispose ();
				BtnLinkedIn = null;
			}

			if (ImgVUser != null) {
				ImgVUser.Dispose ();
				ImgVUser = null;
			}

			if (LblDescription != null) {
				LblDescription.Dispose ();
				LblDescription = null;
			}

			if (LblName != null) {
				LblName.Dispose ();
				LblName = null;
			}
		}
	}
}
