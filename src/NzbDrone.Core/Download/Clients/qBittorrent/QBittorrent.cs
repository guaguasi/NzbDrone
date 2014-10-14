﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using NzbDrone.Common;
using NzbDrone.Common.Disk;
using NzbDrone.Common.EnvironmentInfo;
using NzbDrone.Common.Http;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.MediaFiles.TorrentInfo;
using NzbDrone.Core.Parser;
using NzbDrone.Core.Parser.Model;
using NzbDrone.Core.RemotePathMappings;
using NzbDrone.Core.Validation;
using NLog;
using FluentValidation.Results;

namespace NzbDrone.Core.Download.Clients.QBittorrent
{
    public class QBittorrent : TorrentClientBase<QBittorrentSettings>
    {
        private readonly IQBittorrentProxy _proxy;

        public QBittorrent(IQBittorrentProxy proxy,
                        ITorrentFileInfoReader torrentFileInfoReader,
                        IHttpClient httpClient,
                        IConfigService configService,
                        IDiskProvider diskProvider,
                        IParsingService parsingService,
                        IRemotePathMappingService remotePathMappingService,
                        Logger logger)
            : base(torrentFileInfoReader, httpClient, configService, diskProvider, parsingService, remotePathMappingService, logger)
        {
            _proxy = proxy;
        }

        protected override String AddFromMagnetLink(RemoteEpisode remoteEpisode, String hash, String magnetLink)
        {
            /*
            _proxy.AddTorrentFromUrl(magnetLink, Settings);
            _proxy.SetTorrentLabel(hash, Settings.TvCategory, Settings);

            var isRecentEpisode = remoteEpisode.IsRecentEpisode();

            if (isRecentEpisode && Settings.RecentTvPriority == (int)QBittorrentPriority.First ||
                !isRecentEpisode && Settings.OlderTvPriority == (int)QBittorrentPriority.First)
            {
                _proxy.MoveTorrentToTopInQueue(hash, Settings);
            }
            */
            return hash;
        }

        protected override String AddFromTorrentFile(RemoteEpisode remoteEpisode, String hash, String filename, Byte[] fileContent)
        {
            /*
            _proxy.AddTorrentFromFile(filename, fileContent, Settings);
            _proxy.SetTorrentLabel(hash, Settings.TvCategory, Settings);

            var isRecentEpisode = remoteEpisode.IsRecentEpisode();

            if (isRecentEpisode && Settings.RecentTvPriority == (int)QBittorrentPriority.First ||
                !isRecentEpisode && Settings.OlderTvPriority == (int)QBittorrentPriority.First)
            {
                _proxy.MoveTorrentToTopInQueue(hash, Settings);
            }
            */
            return hash;
        }

        public override IEnumerable<DownloadClientItem> GetItems()
        {
            /*
            List<QBittorrentTorrent> torrents;

            try
            {
                torrents = _proxy.GetTorrents(Settings);
            }
            catch (DownloadClientException ex)
            {
                _logger.ErrorException(ex.Message, ex);
                return Enumerable.Empty<DownloadClientItem>();
            }
            */
            var queueItems = new List<DownloadClientItem>();
            /*
            foreach (var torrent in torrents)
            {
                if (torrent.Label != Settings.TvCategory)
                {
                    continue;
                }

                var item = new DownloadClientItem();
                item.DownloadClientId = torrent.Hash;
                item.Title = torrent.Name;
                item.TotalSize = torrent.Size;
                item.Category = torrent.Label;
                item.DownloadClient = Definition.Name;
                item.RemainingSize = torrent.Remaining;
                if (torrent.Eta != -1)
                {
                    item.RemainingTime = TimeSpan.FromSeconds(torrent.Eta);
                }

                var outputPath = _remotePathMappingService.RemapRemoteToLocal(Settings.Host, torrent.RootDownloadPath);

                if (outputPath == null || Path.GetFileName(outputPath) == torrent.Name)
                {
                    item.OutputPath = outputPath;
                }
                else if (OsInfo.IsWindows && outputPath.EndsWith(":"))
                {
                    item.OutputPath = Path.Combine(outputPath + Path.DirectorySeparatorChar, torrent.Name);
                }
                else
                {
                    item.OutputPath = Path.Combine(outputPath, torrent.Name);
                }

                if (torrent.Status.HasFlag(QBittorrentTorrentStatus.Error))
                {
                    item.Status = DownloadItemStatus.Failed;
                }
                else if (torrent.Status.HasFlag(QBittorrentTorrentStatus.Loaded) &&
                         torrent.Status.HasFlag(QBittorrentTorrentStatus.Checked) && torrent.Remaining == 0 && torrent.Progress == 1.0)
                {
                    item.Status = DownloadItemStatus.Completed;
                }
                else if (torrent.Status.HasFlag(QBittorrentTorrentStatus.Paused))
                {
                    item.Status = DownloadItemStatus.Paused;
                }
                else if (torrent.Status.HasFlag(QBittorrentTorrentStatus.Started))
                {
                    item.Status = DownloadItemStatus.Downloading;
                }
                else
                {
                    item.Status = DownloadItemStatus.Queued;
                }

                // 'Started' without 'Queued' is when the torrent is 'forced seeding'
                item.IsReadOnly = torrent.Status.HasFlag(QBittorrentTorrentStatus.Queued) || torrent.Status.HasFlag(QBittorrentTorrentStatus.Started);

                queueItems.Add(item);
            }
            */
            return queueItems;
        }

        public override void RemoveItem(String id)
        {
            //_proxy.RemoveTorrent(id, false, Settings);
        }

        public override String RetryDownload(String id)
        {
            throw new NotSupportedException();
        }

        public override DownloadClientStatus GetStatus()
        {
            /*
            var config = _proxy.GetConfig(Settings);

            String destDir = null;

            if (config.GetValueOrDefault("dir_active_download_flag") == "true")
            {
                destDir = config.GetValueOrDefault("dir_active_download");
            }

            if (config.GetValueOrDefault("dir_completed_download_flag") == "true")
            {
                destDir = config.GetValueOrDefault("dir_completed_download");

                if (config.GetValueOrDefault("dir_add_label") == "true")
                {
                    destDir = Path.Combine(destDir, Settings.TvCategory);
                }
            }
            */
            var status = new DownloadClientStatus
            {
                IsLocalhost = Settings.Host == "127.0.0.1" || Settings.Host == "localhost"
            };
            /*
            if (destDir != null)
            {
                status.OutputRootFolders = new List<String> { _remotePathMappingService.RemapRemoteToLocal(Settings.Host, destDir) };
            }
            */
            return status;
        }

        protected override void Test(List<ValidationFailure> failures)
        {
            /*
            failures.AddIfNotNull(TestConnection());
            if (failures.Any())
                return;
            failures.AddIfNotNull(TestGetTorrents());
            */
        }

/*
        private ValidationFailure TestConnection()
        {
            try
            {
                var version = _proxy.GetVersion(Settings);

                if (version < 25406)
                {
                    return new ValidationFailure(string.Empty, "Old QBittorrent client with unsupported API, need 3.0 or higher");
                }
            }
            catch (DownloadClientAuthenticationException ex)
            {
                _logger.ErrorException(ex.Message, ex);
                return new NzbDroneValidationFailure("Username", "Authentication failure")
                {
                    DetailedDescription = "Please verify your username and password."
                };
            }
            catch (WebException ex)
            {
                _logger.ErrorException(ex.Message, ex);
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    return new NzbDroneValidationFailure("Host", "Unable to connect")
                    {
                        DetailedDescription = "Please verify the hostname and port."
                    };
                }
                else
                {
                    return new NzbDroneValidationFailure(String.Empty, "Unknown exception: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);
                return new NzbDroneValidationFailure(String.Empty, "Unknown exception: " + ex.Message);
            }

            return null;
        }

        private ValidationFailure TestGetTorrents()
        {
            try
            {
                _proxy.GetTorrents(Settings);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);
                return new NzbDroneValidationFailure(String.Empty, "Failed to get the list of torrents: " + ex.Message);
            }

            return null;
        }
 */
    }
}