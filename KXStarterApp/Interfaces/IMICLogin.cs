using System;
using Kinvey;

namespace KXStarterApp.Interfaces
{
    public interface IMICLogin
    {
		void LoginWithAuthorizationCodeGrant(string redirectUrl);

        // TODO - add support for login with resource owner 

        // TODO - think if it is still required to login with automatic code flow 
    }
}
