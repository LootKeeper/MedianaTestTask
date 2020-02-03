using MedianaTestTask.Jobs;
using MedianaTestTask.Jobs.ConcreteJobs;
using MedianaTestTask.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedianaTestTask.Network
{
    public class Receiver : IIntJobTask
    {
        public bool IsDone => _isDone;

        bool _isDone;
        ReceiverSettings _settings;
        IPayloadValidator _validator;
        IResponseNormalizer _normalizer;
        readonly int _timeout = 15000;
        public Receiver(ReceiverSettings settings, IPayloadValidator validator, IResponseNormalizer normalizer)
        {
            _settings = settings;
            _isDone = false;
            _validator = validator;
            _normalizer = normalizer;
        }

        public async ValueTask<int> Perform()
        {
            try
            {
                using (var client = new TcpClient(_settings.Destination, _settings.Port))
                using (var stream = client.GetStream())
                    return await MakeRequest(stream, client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while attemt to get answer for: {_settings.NumberToSend}. Exception: {ex.ToString()}");
                await Task.Delay(10000);
                return await Perform();
            }
        }

        private async ValueTask<int> MakeRequest(NetworkStream stream, TcpClient client)
        {
            using (stream)
            {
                var payload = GetPayload();
                stream.ReadTimeout = _timeout;
                stream.WriteTimeout = _timeout;
                await stream.WriteAsync(payload, 0, payload.Length);
                return await ReadResponse(stream, client);
            }
        }

        private async ValueTask<int> ReadResponse(NetworkStream stream, TcpClient client)
        {
            var buffer = new byte[1024];
            var responseLength = 0;
            StringBuilder message = new StringBuilder();
            while (stream.CanRead && !IsPayloadReceived(message.ToString()))
            {
                if (stream.DataAvailable)
                {
                    responseLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                    message.AppendFormat("{0}", Encoding.Default.GetString(buffer, 0, responseLength));
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
            return Normalize(message.ToString());
        }

        private bool IsPayloadReceived(string message)
        {
            return _validator.IsValid(message);
        }

        private byte[] GetPayload()
        {
            var preparedString = $"{_settings.NumberToSend}\r\n";
            return Encoding.Default.GetBytes(preparedString);
        }

        private int Normalize(string response)
        {
            return _normalizer.Normalize(response);
        }
    }
}
