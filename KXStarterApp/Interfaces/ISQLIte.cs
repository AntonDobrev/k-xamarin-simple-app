using System;


namespace KXStarterApp
{
	public interface ISQLite
    {
        SQLite.Net.Interop.ISQLitePlatform GetConnection();
        string GetPath();
    }
}
