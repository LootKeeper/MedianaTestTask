using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTask.Network
{
    public class ReceiverSettings
    {
        public static string DefaultDestination => "195.133.144.219";
        public static int DefaultPort => 2012;
        public string Destination => DefaultDestination;
        public int Port => DefaultPort;
        public int NumberToSend => _numberToSend;
        private int _numberToSend;

        public ReceiverSettings(int numberToSend)
        {
            _numberToSend = numberToSend;
        }
    }
}
