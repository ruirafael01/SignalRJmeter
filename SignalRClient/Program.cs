using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRClient
{
    class Program
    {
        private const int _waitTimeForSignalRHUB = 2000; //MILLISECONDS
        public static HubConnection _connection;
        
        static void Main(string[] args)
        {
            Task.Run( async () => await Task.Delay(_waitTimeForSignalRHUB));

            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44334/hub")
                .Build();

            Task.WaitAny(_connection.StartAsync());

            Task.WaitAny(Menu());
        }

        private static async Task<bool> Menu()
        {
            
            char key = 'A';
            bool streamInvoked = false;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            while (key != 'Q')
            {
                Console.WriteLine("Press [Q] for QUIT | [S] to start the streaming from SignalR HUB");

                key = Console.ReadKey().KeyChar;

                if(key == 'S' && streamInvoked is false)
                {
                    try
                    {
                        InvokeSignalRStream(cancellationToken);

                        await Task.Delay(2000);

                        streamInvoked = true;

                    } catch(Exception ex)
                    {
                        Console.WriteLine($"An error has occurred: {ex.Message}");
                    }
                }
                else if(key == 'S' && streamInvoked is true)
                {
                    Console.WriteLine($"Stream has already been invoked from SignalRHub");
                }

                Console.WriteLine(Environment.NewLine);
            }

            cancellationTokenSource.Cancel();

            Console.WriteLine("Closing application");

            return true;
        }

        private static async Task InvokeSignalRStream(CancellationToken cancellationToken)
        {
            var channel = await _connection.StreamAsChannelAsync<SignalRInfo>("GetExampleMessages", cancellationToken);

            while (await channel.WaitToReadAsync())
            {
                while (channel.TryRead(out SignalRInfo data))
                {
                    Console.Write($"\rMessage Received: | {data.Message} |");
                }
            }
        }
    }
}
