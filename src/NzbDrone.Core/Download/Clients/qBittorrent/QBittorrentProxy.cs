using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using NzbDrone.Common;
using NzbDrone.Common.Serializer;
using NzbDrone.Core.Rest;
using NLog;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace NzbDrone.Core.Download.Clients.QBittorrent
{
    public interface IQBittorrentProxy
    {
    }

    class QBittorrentProxy : IQBittorrentProxy
    {
    }
}
