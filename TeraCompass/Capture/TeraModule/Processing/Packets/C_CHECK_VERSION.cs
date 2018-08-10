﻿using System.Diagnostics;
using System.IO;
using System.Windows;
using TeraCompass.Tera.Core.Game.Services;

namespace TeraCompass.Processing.Packets
{
    internal class C_CHECK_VERSION
    {
        internal C_CHECK_VERSION(Tera.Core.Game.Messages.Client.C_CHECK_VERSION message)
        {
            if (!Directory.Exists(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/")))
                Directory.CreateDirectory(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/"));
            Debug.WriteLine("VERSION0 = " + message.Versions[0]);
            OpcodeDownloader.DownloadIfNotExist(message.Versions[0], Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/"));
            if (!File.Exists(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/{message.Versions[0]}.txt")) && !File.Exists(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/protocol.{message.Versions[0]}.map")))
            {
                Debug.Write("Unknown client version: " + message.Versions[0]);
                PacketProcessor.Instance.Exit();
                return;
            }
            var opCodeNamer = new OpCodeNamer(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/{message.Versions[0]}.txt"));
            var sysMsgNamer = new OpCodeNamer(Path.Combine(BasicTeraData.Instance.ResourceDirectory, $"data/opcodes/smt_{message.Versions[0]}.txt"));
            TeraSniffer.Instance.Connected = true;
            PacketProcessor.Instance.MessageFactory = new MessageFactory(opCodeNamer, PacketProcessor.Instance.Server.Region, message.Versions[0], false, sysMsgNamer);

            if (TeraSniffer.Instance.ClientProxyOverhead + TeraSniffer.Instance.ServerProxyOverhead > 0x1000)
            {
                Debug.Write("Client Proxy overhead: " + TeraSniffer.Instance.ClientProxyOverhead + "\r\nServer Proxy overhead: " +
                                       TeraSniffer.Instance.ServerProxyOverhead);
            }
        }
    }
}