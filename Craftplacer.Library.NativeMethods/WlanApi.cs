using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Craftplacer.Library.NativeMethods
{
    public static class WlanApi
    {
        [DllImport("Wlanapi.dll")]
        public static extern int WlanOpenHandle(uint dwClientVersion, IntPtr pReserved, [Out] out uint pdwNegotiatedVersion, out IntPtr phClientHandle);

        [DllImport("Wlanapi.dll")]
        public static extern void WlanHostedNetworkStartUsing(IntPtr hClientHandle, out uint pFailReason, IntPtr pvReserved);

        [DllImport("Wlanapi.dll")]
        public static extern void WlanHostedNetworkStopUsing(IntPtr hClientHandle, out uint pFailReason, IntPtr pvReserved);

        [DllImport("Wlanapi.dll")]
        public static extern void WlanHostedNetworkQueryStatus(IntPtr hClientHandle, [Out]out WLAN_HOSTED_NETWORK_STATUS ppWlanHostedNetworkStatus, IntPtr pvReserved);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WLAN_HOSTED_NETWORK_STATUS
        {
            public uint HostedNetworkState;
            public Guid IPDeviceID;
            public DOT11_MAC_ADDRESS wlanHostedNetworkBSSID;
            public DOT11_PHY_TYPE dot11PhyType;
            public uint ulChannelFrequency; // ULONG
            public uint dwNumberOfPeers; // DWORD
            public WLAN_HOSTED_NETWORK_PEER_STATE[] PeerList;
        }

        public enum WLAN_HOSTED_NETWORK_PEER_AUTH_STATE
        {
            wlan_hosted_network_peer_state_invalid,
            wlan_hosted_network_peer_state_authenticated
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WLAN_HOSTED_NETWORK_PEER_STATE
        {
            public DOT11_MAC_ADDRESS PeerMacAddress;
            public WLAN_HOSTED_NETWORK_PEER_AUTH_STATE PeerAuthState;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DOT11_MAC_ADDRESS
        {
            public byte one;
            public byte two;
            public byte three;
            public byte four;
            public byte five;
            public byte six;
        }

        public enum DOT11_PHY_TYPE : uint
        {
            unknown = 0,
            any = 0,
            fhss = 1,
            dsss = 2,
            irbaseband = 3,
            ofdm = 4,
            hrdsss = 5,
            erp = 6,
            ht = 7,
            vht = 8,
            IHV_start = 0x80000000,
            IHV_end = 0xffffffff
        }
    }
}