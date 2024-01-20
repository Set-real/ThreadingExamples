using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingExampleApp
{
    internal class Match
    {
        private readonly Semaphore semaphore;
        private List<int> players;

        public Match(int copasity)
        {
            semaphore = new Semaphore(copasity, copasity);
            players = new List<int>();
            Console.WriteLine($"Матч на {copasity} игроков начат");
        }

        public void ConnectingPlayer(int clientId)
        {
            semaphore.WaitOne();

            players.Add(clientId);
            Console.WriteLine($"Игрок {clientId} подключился. Сейчас игроков в матче: {players.Count}");

            Thread.Sleep(new Random().Next(20000, 40000));

            if (!players.Contains(clientId))
                return;

            players.Remove(clientId);
            Console.WriteLine($"Игрок {clientId} завершил сессию. Осталось игроков {players.Count}");

            if (players.Count < 1)
                Console.WriteLine("Матч завершен");

            semaphore.Release();
        }

        public void CickPlayers(int clientId)
        {
            Thread.Sleep(3000);
            Console.WriteLine($"Игрок {clientId} был исключен за читы, тварюга такая");
            players.Remove(clientId);

            semaphore.Release();
        }
    }
}
