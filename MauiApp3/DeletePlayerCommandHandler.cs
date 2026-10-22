using MauiApp3.Data;

/*************************************************************************
 * nazwa klasy:
 * DeletePlayerCommandHandler
 *
 * opis:
 * Klasa obsługuje polecenie usunięcia gracza.
 * Przekazuje identyfikator gracza do repozytorium i zwraca informację,
 * czy gracz został usunięty.
 *
 * pola / właściwości:
 * _playerRepository, PlayerRepository, repozytorium graczy
 *
 * autor:
 * Kornel Tworek
 *************************************************************************/
using MauiApp3.Data;

namespace MauiApp3.Commands
{
    public class DeletePlayerCommandHandler
    {
        private readonly PlayerRepository _playerRepository;

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayerCommandHandler
         *
         * parametry wejściowe:
         * playerRepository, PlayerRepository, repozytorium graczy
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Zapisuje referencję do repozytorium wykorzystywanego
         * podczas usuwania gracza.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        public DeletePlayerCommandHandler(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /*************************************************************************
         * nazwa funkcji:
         * Handle
         *
         * parametry wejściowe:
         * command, DeletePlayerCommand, polecenie zawierające Id gracza
         *
         * wartość zwracana:
         * bool, true jeśli gracz został usunięty, w przeciwnym razie false
         *
         * opis funkcji:
         * Odczytuje Id z polecenia i zleca repozytorium usunięcie gracza.
         * Zwraca informację o wyniku operacji.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        public bool Handle(DeletePlayerCommand command)
        {
            if (command == null)
            {
                return false;
            }

            return _playerRepository.DeleteById(command.Id);
        }
    }
}