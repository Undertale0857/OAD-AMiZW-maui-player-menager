/*************************************************************************
 * nazwa klasy:
 * DeletePlayerCommand
 *
 * opis:
 * Klasa przechowuje dane potrzebne do wykonania polecenia usunięcia
 * gracza z repozytorium.
 *
 * pola / właściwości:
 * Id, int, identyfikator gracza przeznaczonego do usunięcia
 *
 * autor:
 * Kornel Tworek
 *************************************************************************/
namespace MauiApp3.Commands
{
    public class DeletePlayerCommand
    {
        public int Id { get; }

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayerCommand
         *
         * parametry wejściowe:
         * id, int, identyfikator gracza do usunięcia
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Tworzy polecenie usunięcia gracza i zapisuje jego identyfikator.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        public DeletePlayerCommand(int id)
        {
            Id = id;
        }
    }
}