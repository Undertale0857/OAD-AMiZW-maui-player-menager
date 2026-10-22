using MauiApp3.Model;
using System.Linq;

namespace MauiApp3.Data
{
    public class PlayerRepository
    {
        public List<Player> Players { get; } = new()
        {
            new Player
            {
                Id = 1,
                Name = "test",
                Level = 1500,
            },
            new Player
            {
                Id = 31,
                Name = "noname",
                Level = 4021
            }
        };

        /*************************************************************************
         * nazwa funkcji:
         * DeleteById
         *
         * parametry wejściowe:
         * id, int, identyfikator gracza do usunięcia
         *
         * wartość zwracana:
         * bool, true jeśli gracz został usunięty, w przeciwnym razie false
         *
         * opis funkcji:
         * Wyszukuje gracza o podanym Id w liście Players.
         * Jeśli gracz istnieje, usuwa go z listy.
         * Jeśli gracz nie istnieje, pozostawia listę bez zmian.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        public bool DeleteById(int id)
        {
            Player? player = Players.FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                return false;
            }

            return Players.Remove(player);
        }
    }
}