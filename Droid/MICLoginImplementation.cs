using System;
using Android.Content;
using Kinvey;
using KXStarterApp.Droid;
using KXStarterApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(MICLoginImplementation))]
namespace KXStarterApp.Droid
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
					var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));

                    var context = Android.App.Application.Context;

                    context.StartActivity(intent);
                }
            });
		}
	}
}
