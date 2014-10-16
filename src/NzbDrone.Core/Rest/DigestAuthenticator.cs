using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using RestSharp;

// By Uwe Keim: http://blog.magerquark.de/digest-authentication-with-restsharp/
// If query string values cause authentication issues in the future, see: http://www.ifjeffcandoit.com/2013/05/16/digest-authentication-with-restsharp/

namespace NzbDrone.Core.Rest
{

    public class DigestAuthenticator : IAuthenticator
    {
        private readonly String _username;
        private readonly String _password;

        public DigestAuthenticator(String username, String password)
        {
            _username = username;
            _password = password;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.Credentials = new NetworkCredential(_username, _password);
        }
    }
}