
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Kinvey;
using KinveyXamarinAndroid;

namespace KXStarterApp.Droid
{
	[Activity(Label = "KXStarterApp.Droid", 
	          Icon = "@drawable/icon", 
	          Theme = "@style/MyTheme", 
	          MainLauncher = true, 
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
	          LaunchMode = LaunchMode.SingleTop // this (or similar) is required to call onNewIntent and handle the login with MIC
	         )]

    [IntentFilter(new[] { Intent.ActionMain, Intent.ActionView },
	              Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault }, DataScheme = AppConstants.REDIRECT_URL_SCHEME)]
 
	// required for Kinvey MIC login - adds the corresponding intent filter in the AndroidManifest.XML


	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        // required for Kinvey MIC login
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Client.SharedClient.ActiveUser.OnOAuthCallbackRecieved(intent);
        }
	}
}
