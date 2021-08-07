using Microsoft.AspNetCore.SignalR;
using SignalRApacheJMeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRApacheJMeter.SignalR
{
    public class SignalRHub : Hub
    {
        private readonly List<SignalRInfo> _listOfInformationMessages;

        public SignalRHub()
        {
            _listOfInformationMessages = new List<SignalRInfo>();
        }

        /// <summary>
        /// Method that continuously returns some data to the HUB Client that invoked this method, therefore it´s a Stream Method
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<SignalRInfo> StreamHUBMethod(CancellationToken cancellationToken)
        {
            int messageNumber = 1;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var signalRInfo = new SignalRInfo(GenerateValidGuidForInfo(), $"Message nº {messageNumber} from SignalR Hub", DateTime.Now);
                _listOfInformationMessages.Add(signalRInfo);

                yield return signalRInfo;

                messageNumber++;
            }
        }

        /// <summary>
        /// Method that returns a 'SignalRInfo' object after it´s invocation, therefore it´s a "Normal HUB Method"
        /// </summary>
        /// <param name="inputString">String sent on the invokation of the method</param>
        /// <returns></returns>
        public async Task<SignalRInfo> NormalHUBMethod(string inputString)
        {
            var signalRInfo = new SignalRInfo(GenerateValidGuidForInfo(), $"Message received on SignalRHub: {inputString}", DateTime.Now);
            _listOfInformationMessages.Add(signalRInfo);

            return signalRInfo;
        }

        private Guid GenerateValidGuidForInfo()
        {
            Guid newGuid = Guid.NewGuid();

            while (_listOfInformationMessages.Any( x => x.Id == newGuid))
            {
                newGuid = Guid.NewGuid();
            }

            return newGuid;
        }
    }
}
