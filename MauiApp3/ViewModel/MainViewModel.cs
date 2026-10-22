
using MauiApp3.Commands;
using MauiApp3.Model;
using MauiApp3.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp3.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GetPlayerByIdQueryHandler _getPlayerByIdQueryHandler;
        private readonly GetPlayersQueryHandler _getPlayersQueryHandler;
        private readonly AddPlayerCommandHandler _addPlayerCommandHandler;
        private readonly DeletePlayerCommandHandler _deletePlayerCommandHandler;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Player> Players { get; } = new();

        public string PlayerName { get; set; } = "";
        public string PlayerLevel { get; set; } = "";
        public string PlayerId { get; set; } = "";
        public string DeletePlayerId { get; set; } = "";

        private string _searchResult = "";

        public string SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPlayerCommandButton { get; }
        public ICommand FindPlayerCommandButton { get; }
        public ICommand DeletePlayerCommandButton { get; }

        /*************************************************************************
         * nazwa funkcji:
         * MainViewModel
         *
         * parametry wejściowe:
         * getPlayerByIdQueryHandler, GetPlayerByIdQueryHandler, handler wyszukiwania
         * getPlayersQueryHandler, GetPlayersQueryHandler, handler pobierania listy
         * addPlayerCommandHandler, AddPlayerCommandHandler, handler dodawania gracza
         * deletePlayerCommandHandler, DeletePlayerCommandHandler, handler usuwania
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Inicjalizuje handlery i polecenia interfejsu.
         * Na końcu pobiera aktualną listę graczy.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        public MainViewModel(
            GetPlayerByIdQueryHandler getPlayerByIdQueryHandler,
            GetPlayersQueryHandler getPlayersQueryHandler,
            AddPlayerCommandHandler addPlayerCommandHandler,
            DeletePlayerCommandHandler deletePlayerCommandHandler)
        {
            _getPlayerByIdQueryHandler = getPlayerByIdQueryHandler;
            _getPlayersQueryHandler = getPlayersQueryHandler;
            _addPlayerCommandHandler = addPlayerCommandHandler;
            _deletePlayerCommandHandler = deletePlayerCommandHandler;

            AddPlayerCommandButton = new Command(AddPlayer);
            FindPlayerCommandButton = new Command(FindPlayer);
            DeletePlayerCommandButton = new Command(DeletePlayer);

            LoadPlayers();
        }

        /*************************************************************************
         * nazwa funkcji:
         * AddPlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Sprawdza poprawność poziomu gracza, tworzy polecenie dodania
         * gracza, przekazuje je do handlera i odświeża listę.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        private void AddPlayer()
        {
            if (!int.TryParse(PlayerLevel, out int playerLevel))
            {
                return;
            }

            AddPlayerCommand command =
                new AddPlayerCommand(PlayerName, playerLevel);

            _addPlayerCommandHandler.Handle(command);

            LoadPlayers();
        }

        /*************************************************************************
         * nazwa funkcji:
         * FindPlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Wyszukuje gracza na podstawie Id i wyświetla jego nazwę
         * oraz poziom albo komunikat o błędzie.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        private void FindPlayer()
        {
            if (!int.TryParse(PlayerId, out int playerId))
            {
                SearchResult = "Niepoprawne ID";
                return;
            }

            GetPlayerByIdQuery query =
                new GetPlayerByIdQuery(playerId);

            Player? player =
                _getPlayerByIdQueryHandler.Handle(query);

            if (player == null)
            {
                SearchResult = "Nie znaleziono gracza";
                return;
            }

            SearchResult = $"{player.Name} - poziom {player.Level}";
        }

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Sprawdza poprawność Id gracza, tworzy polecenie usunięcia
         * i przekazuje je do handlera. W przypadku powodzenia
         * odświeża listę i czyści pole Id.
         * Wyświetla komunikat o wyniku operacji.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        private async void DeletePlayer()
        {
            if (!int.TryParse(DeletePlayerId, out int id) || id <= 0)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(
                    "Błąd",
                    "Wpisz poprawne, dodatnie Id gracza.",
                    "OK");

                return;
            }

            DeletePlayerCommand command = new DeletePlayerCommand(id);

            bool deleted = _deletePlayerCommandHandler.Handle(command);

            if (!deleted)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(
                    "Błąd",
                    "Nie znaleziono gracza o podanym Id.",
                    "OK");

                return;
            }

            LoadPlayers();

            DeletePlayerId = "";
            OnPropertyChanged(nameof(DeletePlayerId));

            await Application.Current!.Windows[0].Page!.DisplayAlert(
                "Sukces",
                "Gracz został usunięty.",
                "OK");
        }

        /*************************************************************************
         * nazwa funkcji:
         * LoadPlayers
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Pobiera graczy z handlera zapytania, czyści kolekcję
         * i wypełnia ją aktualnymi danymi.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        private void LoadPlayers()
        {
            GetPlayersQuery query = new GetPlayersQuery();

            List<Player> players =
                _getPlayersQueryHandler.Handle(query);

            Players.Clear();

            foreach (Player player in players)
            {
                Players.Add(player);
            }
        }

        /*************************************************************************
         * nazwa funkcji:
         * OnPropertyChanged
         *
         * parametry wejściowe:
         * propertyName, string, nazwa zmienionej właściwości
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Informuje interfejs użytkownika o zmianie właściwości.
         *
         * autor:
         * Kornel Tworek
         *************************************************************************/
        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}