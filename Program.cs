using System;
using System.Text;
using System.Threading;
using ThreadingExampleApp;

namespace MutexTask
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var match = new Match(copasity: 5);

            for(int i = 1; i <= 10; i++)
            {
                int playerId = i;
                new Thread(() =>
                {
                    match.ConnectingPlayer(playerId);
                }).Start();

                Thread.Sleep(new Random().Next(300, 1500));                
            }

            match.CickPlayers(5);
        }
    }
}