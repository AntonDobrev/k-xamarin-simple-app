﻿using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kinvey;
using Xamarin.Forms.PlatformConfiguration;
using KXStarterApp.Interfaces;

namespace KXStarterApp
{
	public partial class KXStarterAppPage : ContentPage
	{
		private const string COLLECTION_NAME = "books";
		private const string username = "";
		private const string password = "";

		public Client KinveyClient { get; private set; }

		public KXStarterAppPage()
		{
			InitializeComponent();
			try
			{
				//await BuildClient(); 
				KinveyClient = GetKinveyClient(AppConstants.KINVEY_APIKEY, AppConstants.KINVEY_APPSECRET);
			}
			catch (Exception e)
			{
				DisplayAlert("General Exception", e.Message, "OK");
			}
		}

		private Client GetKinveyClient(string appKey, string appSecret)
		{
			Client kClient = new Client.Builder(appKey, appSecret)
							  // .setBaseURL(baseUrl)
							   .setFilePath(DependencyService.Get<ISQLite>().GetPath())
							   .setOfflinePlatform(DependencyService.Get<ISQLite>().GetConnection())
									   .Build();

			return kClient;
		}

		//private async Task<Client> BuildClient(string appKey, string appSecret, string baseUrl)
		//{
		//	Client.Builder cb = new Client.Builder(appKey, appSecret)
		//		//.setFilePath (NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User) [0].ToString ())
		//		.setFilePath(DependencyService.Get<ISQLite>().GetPath())
		//		.setOfflinePlatform(DependencyService.Get<ISQLite>().GetConnection());
		//	//.setLogger (delegate (string msg) { Console.WriteLine (msg); })

		//	return cb.Build();
		//}
  
		// login with authorization code grant (login page) and MIC v2 
	    void OnLoginMicV2Clicked(object sender, EventArgs args) 
		{
			Client.SharedClient.MICApiVersion = "v2";
            DependencyService.Get<IMICLogin>().LoginWithAuthorizationCodeGrant(AppConstants.REDIRECT_URL);
		}

		// login with authorization code grant (login page) and MIC v3
		void OnLoginMicV3Clicked(object sender, EventArgs args)
        {
			Client.SharedClient.MICApiVersion = "v3";

			DependencyService.Get<IMICLogin>().LoginWithAuthorizationCodeGrant(AppConstants.REDIRECT_URL);
        }

		async void OnButtonClicked(object sender, EventArgs args)
		{
			try
			{
				if (!Client.SharedClient.IsUserLoggedIn())
				{
					await User.LoginAsync(username, password);
				}

				DataStore<Book> dataStore = DataStore<Book>.Collection(COLLECTION_NAME, DataStoreType.SYNC);

				Button button = (Button)sender;

				if (button.Text == "Add Book")
				{
					Book b = new Book();
					b.Title = "Crime and Punishment";
					b.Genre = "Fiction";
					await dataStore.SaveAsync(b);

					await DisplayAlert("Book Added!",
										"The button labeled '" + button.Text + "' has been clicked, and the book '" + b.Title + "' has been added.",
										"OK");
				}
				else if (button.Text == "Push")
				{
					PushDataStoreResponse<Book> dsr = await dataStore.PushAsync();
					await DisplayAlert("Local Data Pushed!",
									   "The button labeled '" + button.Text + "' has been clicked, and " + dsr.PushCount + " book(s) has/have been pushed to Kinvey.",
										"OK");
				}
				else if (button.Text == "Pull")
				{
					PullDataStoreResponse<Book> dsr = await dataStore.PullAsync();
					await DisplayAlert("Local Data Pulled!",
									   "The button labeled '" + button.Text + "' has been clicked, and " + dsr.PullCount + " book(s) has/have been pulled from Kinvey.",
										"OK");
				}
				else if (button.Text == "Sync")
				{
					SyncDataStoreResponse<Book> dsr = await dataStore.SyncAsync();
					await DisplayAlert("Local Data Synced!",
									   "The button labeled '" + button.Text + "' has been clicked, and " + dsr.PullResponse.PullCount + " book(s) has/have been synced with Kinvey.",
										"OK");
				}
			}
			catch (KinveyException ke)
			{
				await DisplayAlert("Kinvey Exception",
								   ke.Error + " | " + ke.Description + " | " + ke.Debug,
								   "OK");
			}
			catch (Exception e)
			{
				await DisplayAlert("General Exception",
								   e.Message,
								   "OK");
			}
		}
	}
}

