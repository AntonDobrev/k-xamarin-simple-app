using Xamarin.Forms;
using SQLite.Net.Platform.XamarinAndroid;

[assembly: Dependency(typeof(SQLite_Android))]
public class SQLite_Android : KXStarterApp.ISQLite
{
	public SQLite_Android() { }
	public SQLite.Net.Interop.ISQLitePlatform GetConnection()
	{
		//required to support newer Android versions - old SQLite package does not support them 
		return new SQLitePlatformAndroidN();
	}

	public string GetPath()
	{
		return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
	}
}
