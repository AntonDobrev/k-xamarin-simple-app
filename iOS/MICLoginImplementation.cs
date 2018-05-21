using System;
using Foundation;
using Kinvey;
using KXStarterApp.Interfaces;
using KXStarterApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MICLoginImplementation))]
namespace KXStarterApp.iOS
{
	public class MICLoginImplementation : IMICLogin
	{

		public MICLoginImplementation()
		{

		}

		void IMICLogin.LoginWithAuthorizationCodeGrant(string redirectUrl)
        {
            User.LoginWithMIC(redirectUrl, new KinveyMICDelegate<User>()
            {
                onSuccess = (u) => { Console.WriteLine("logged in as: " + u.Id); },
                onError = (error) => { Console.WriteLine("something went wrong: " + error.Message); },
                onReadyToRender = (url) =>
                {
					UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
                }
            });
        }
	}
  }



			       