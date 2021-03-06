// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Json;
using Foundation;
using Newtonsoft.Json;
using UIKit;
using Xamarin.Auth;

namespace SampleKT
{
	//https://github.com/xamarin/Xamarin.Auth/blob/master/GettingStarted.md
	//https://github.com/HoussemDellai/Xamarin.Auth
	public partial class LoginView : UIViewController
	{
		public LoginView(IntPtr handle) : base(handle)
		{
		}

		string linkedInId = "81fb51d1f8h2lz";
		string linkedInSecret = "ApNoyBIJzymmAubQ";

		string twitterConsumerKey = "RUoHTehSbDrpm3VfKJEPMMmU3";
		string twitterSecret = "TIzeB3WC9XZd7VmaktSexoitWN4ljo5xBSLHir2rRl4daxjdJI";
		string twitterCalbbackUrl = "http://mobile.twitter.com";

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			BtnFacebook.TouchUpInside += (sender, e) =>
			{
				//Fb_TouchUpInside();
				Fb_TouchUpInside(sender as UIButton);
			};

			BtnGoogle.TouchUpInside += (sender, e) =>
			{
				loginUsingEmail();
			};


			BtnTwitter.TouchUpInside += (sender, e) =>
            {
				TwitterAuth_TouchUpInside(sender as UIButton);
            };


			BtnLinkedIn.TouchUpInside += (sender, e) =>
            {
				LinkedInAuth_TouchUpInside(sender as UIButton);
            };
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			//NavigationController.SetNavigationBarHidden(true, false);
		}

		#region for Twitter
		public void TwitterAuth_TouchUpInside(UIButton sender)
		{
			// http://dev.twitter.com/apps
			var auth = new OAuth1Authenticator(
				consumerKey: twitterConsumerKey,
				consumerSecret: twitterSecret,
					requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
					authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
					accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
					callbackUrl: new Uri("http://mobile.twitter.com"));

			var ui = auth.GetUI();

			auth.Completed += TwitterAuth_Completed;

			PresentViewController(ui, true, null);
		}

		async void TwitterAuth_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				var request = new OAuth1Request("GET",
							   new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
							   null,
							   e.Account);

				var response = await request.GetResponseAsync();

				var json = response.GetResponseText();

				var twitterUser = JsonConvert.DeserializeObject<TwitterUser>(json);

				LblName.Text = twitterUser.name;
				//IdLabel.Text = twitterUser.id.ToString();
				LblDescription.Text = twitterUser.description;
				ImgVUser.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(twitterUser.profile_image_url_https)));
				//CoverImage.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(twitterUser.profile_banner_url)));
			}

			DismissViewController(true, null);
		}

        #endregion
		#region for LinkedIn
		public void LinkedInAuth_TouchUpInside(UIButton sender)
		{
			// https://developer.linkedin.com/my-apps
			var auth = new OAuth2Authenticator(
				clientId: linkedInId,
				clientSecret: linkedInSecret,
				 scope: "r_basicprofile",
				 authorizeUrl: new Uri("https://www.linkedin.com/uas/oauth2/authorization"),
				 redirectUrl: new Uri("https://www.youtube.com/c/HoussemDellai"),
				 accessTokenUrl: new Uri("https://www.linkedin.com/uas/oauth2/accessToken"));

			var ui = auth.GetUI();

			auth.Completed += LinkedInAuth_Completed;

			PresentViewController(ui, true, null);
		}

		async void LinkedInAuth_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				var request = new OAuth2Request(
					"GET",
					new Uri("https://api.linkedin.com/v1/people/~:(id,firstName,lastName,headline,picture-url,summary,educations,three-current-positions,honors-awards,site-standard-profile-request,location,api-standard-profile-request,phone-numbers)?"
						  + "format=json"
						  + "&oauth2_access_token="
						  + e.Account.Properties["access_token"]),
					null,
					e.Account);

				var linkedInResponse = await request.GetResponseAsync();
				var json = linkedInResponse.GetResponseText();
				var linkedInUser = JsonValue.Parse(json);

				var name = linkedInUser["firstName"] + " " + linkedInUser["lastName"];
				var id = linkedInUser["id"];
				var description = linkedInUser["headline"];
				var picture = linkedInUser["pictureUrl"];

				LblName.Text += name;
				//IdLabel.Text += id;
				LblDescription.Text = description;
				ImgVUser.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(picture)));

			}
			DismissViewController(true, null);
		}
		#endregion

		#region for FaceBook
		//clientSecret:"3c6d200481c8de7f9824585dad6442fa", 
		public void Fb_TouchUpInside(UIButton sender)
		{
			var auth = new OAuth2Authenticator(clientId: "200870847406469", scope: "", authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

			auth.Completed += Auth_Completed;
			var ui = auth.GetUI();
			PresentViewController(ui, true, null);
		}
		private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"), null, e.Account);
				var response = await request.GetResponseAsync();
				var user = JsonValue.Parse(response.GetResponseText());
				var fbName = user["name"];
				//var fbCover = user["cover"]["source"];  
				var fbProfile = user["picture"]["data"]["url"];
				LblName.Text = fbName.ToString();  
				//imgCover.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(fbCover)));  
				ImgVUser.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(fbProfile)));  
			}
			DismissViewController(true, null);
		}
#endregion


		#region for Google

		// These values do not need changing

		//791017382257-1d7ool2aou9p140f71f1a5560vt0sqqt.apps.googleusercontent.com
		string appId = "709380887597-56vva0au5iammfcitj3gk4ktlanttabs.apps.googleusercontent.com";
         string Scope = "https://www.googleapis.com/auth/userinfo.email";
         string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
         string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
         string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
		string RedirectUrl = "com.googleusercontent.apps.709380887597-56vva0au5iammfcitj3gk4ktlanttabs:/oauth2redirect";//"com.googleusercontent.apps.730898572978-cbjmh4ouj69vu6igrt1tti6tr49v8ct0:/oauth2redirect";//"com.sat.com.CMES:/oauth2redirect"; //"730898572978-cbjmh4ouj69vu6igrt1tti6tr49v8ct0.apps.googleusercontent.com:/oauth2redirect";//"com.sat.com.CMES:/oauth2redirect";
        void loginUsingEmail()
        {
			AppDelegate.Authenticator = new OAuth2Authenticator(
				clientId: appId,
                clientSecret: null,
                // scope:"https://www.googleapis.com/auth/userinfo.email",
                scope: Scope,
                authorizeUrl: new Uri(AuthorizeUrl),
                redirectUrl: new Uri(RedirectUrl),
                accessTokenUrl: new Uri(AccessTokenUrl), getUsernameAsync: null,
                isUsingNativeUI: true);

			var viewController = AppDelegate.Authenticator.GetUI();
			AppDelegate.Authenticator.Completed += GoogleAuth_Completed;
            PresentViewController(viewController, true, null);

            
        }

		private async void GoogleAuth_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			// We presented the UI, so it's up to us to dimiss it on iOS.
            DismissViewController(true, null);
            
            if (e.IsAuthenticated)
            {
                var account = e.Account;
                var request = new OAuth2Request("GET", new Uri(UserInfoUrl), null, account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = response.GetResponseText();
                    var user = JsonConvert.DeserializeObject<UserDetailsGoogleDto>(userJson);
					LblName.Text = user.Name;
                    //IdLabel.Text += id;
					LblDescription.Text = user.Email;
					ImgVUser.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(user.Picture)));
                }

            }
            else
            {
                // The user cancelled
            }
		}
        #endregion

	}
}
