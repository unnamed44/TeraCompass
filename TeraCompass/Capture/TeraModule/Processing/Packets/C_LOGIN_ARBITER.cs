﻿using System.Diagnostics;
using System.IO;

namespace TeraCompass.Processing.Packets
{
    class C_LOGIN_ARBITER
    {
        internal C_LOGIN_ARBITER(Tera.Core.Game.Messages.Client.C_LOGIN_ARBITER message)
        {
            if (OpcodeDownloader.DownloadSysmsg(PacketProcessor.Instance.MessageFactory.Version, PacketProcessor.Instance.MessageFactory.ReleaseVersion,
                Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/")))
            {
                PacketProcessor.Instance.MessageFactory.ReloadSysMsg();
            };
            BasicTeraData.Instance.Servers.Language = message.Language;
            Debug.Write("ARBITER message");
        }
    }
}