using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NzbDrone.Core.Download.Clients.QBittorrent
{
    class QBittorrentTorrentStatus
    {
        /*
         * error - some error occurred, applies to paused torrents
         * pausedUP - torrent is paused and has finished downloading
         * pausedDL - torrent is paused and has NOT finished downloading
         * queuedUP - queuing is enabled and torrent is queued for upload
         * queuedDL - queuing is enabled and torrent is queued for download
         * uploading - torrent is being seeded and data is being transfered
         * stalledUP - torrent is being seeded, but no connection were made
         * stalledDL - torrent is being downloaded, but no connection were made
         * checkingUP - torrent has finished downloading and is being checked; this status also applies to preallocation (if enabled) and checking resume data on qBt startup
         * checkingDL - same as checkingUP, but torrent has NOT finished downloading
         * downloading - torrent is being downloaded and data is being transfered
         */

        public const String Error = "error";
        public const String PausedUP = "pausedUp";
        public const String PausedDL = "pausedDL";
        public const String QueuedUP = "queuedUP";
        public const String QueuedDL = "queuedDL";
        public const String Uploading = "uploading";
        public const String StalledUP = "stalledUP";
        public const String StalledDL = "stalledDL";
        public const String CheckingUP = "checkingUP";
        public const String CheckingDL = "checkingDL";
        public const String Downloading = "downloading";
    }
}
