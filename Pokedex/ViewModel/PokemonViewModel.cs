using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Type = Model.Model.Type_Classes.Type;
using Repository.Local;
using Model.Model.Pokémon_Classes;
using Repository.Web;
using Model.Model;
using Model.Model.Status_Classes;
using Model.Model.Ability_Classes;
using Model.Model.Type_Classes;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.UI.Popups;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Pokedex.Views;
using Windows.UI.ViewManagement;
using System.Numerics;
using System.Drawing;
using Windows.Storage;

namespace Pokedex.PokedexViewModel
{
    public class PokemonViewModel : ObservableObject
    {
        private int _indexComboBox;

        private int _currentPageView;

        private string _searchPokemon;

        private int _pokemonFromUserHP;
      
        private bool _buttonClickEnable;

        private int _pokemonFromUserSpeed;

        private int _pokemonFromUserHeight;

        private int _pokemonFromUserWeight;

        private string _pokemonFromUserName;

        private string _pokemonFromUserType;

        private int _pokemonFromUserExperience;

        private int _pokemonFromUserAttackScore;

        private const int _pokemonMaxLimit = 250;

        private int _pokemonFromUserDefenseScore;

        private string _pokemonFromUserAbilities;

        private const int _amountOfPokemonPerPage = 10;

        private int _pokemonFromUserSpecialAttackScore;

        private int _pokemonFromUserSpecialDefenseScore;

        private ObservableCollection<Pokemon> _pokemonCollection;

        private Visibility _progrssRingVisibility = Visibility.Visible;

        private Visibility _buttonHomeVisibility = Visibility.Collapsed;

        private Visibility _buttonsInitialPageVisibility = Visibility.Visible;

        private Visibility _alertTypesBoxButtonVisibility = Visibility.Visible;

        private Visibility _alertAbilitiesBoxButtonVisibility = Visibility.Visible;

        public StorageFolder ImagesSouce { get; set; }

        public StorageFile ImageFilePokemonFromUser { get; set; }

        public ICommand CommandBackHome { get { return new RelayCommand(BackHome); } }

        public ICommand NextPageCommandButton { get { return new RelayCommand(NextPage); } }

        public ICommand AlertTypesBoxCommand { get { return new RelayCommand(AlertTypesBox); } }

        public ICommand PreviousPageCommandButton { get { return new RelayCommand(PreviousPage); } }

        public ICommand AddPokemonFromUserCommand { get { return new RelayCommand(ShowButtonHome); } }

        public ICommand AlertAbilitiesBoxCommand { get { return new RelayCommand(AlertAbilitiesBox); } }

        public ICommand ProcessQueryCommand { get { return new AsyncRelayCommand(ProcessQueryAsync); } }

        public ICommand CreatePokemonFromUserCommand { get { return new RelayCommand(CreatePokemonFromUser); } }

        public ICommand CommandLoadPictureNewPokemon { get { return new AsyncRelayCommand(LoadPictureNewPokemon); } }

        public Visibility AlertTypesBoxButtonVisibility
        {
            get { return _alertTypesBoxButtonVisibility; }
            set { SetProperty(ref _alertTypesBoxButtonVisibility, value); }
        }

        public Visibility AlertAbilitiesBoxButtonVisibility
        {
            get { return _alertAbilitiesBoxButtonVisibility; }
            set { SetProperty(ref _alertAbilitiesBoxButtonVisibility, value); }
        }

        public Visibility ButtonHomeVisibility
        {
            get { return _buttonHomeVisibility; }
            set { SetProperty(ref _buttonHomeVisibility, value); }
        }

        public Visibility ButtonsInitialPageVisibility
        {
            get { return _buttonsInitialPageVisibility; }
            set { SetProperty(ref _buttonsInitialPageVisibility, value); }
        }

        public Visibility ProgrssRingVisibility
        {
            get { return _progrssRingVisibility; }
            set { SetProperty(ref _progrssRingVisibility, value); }
        }

        public string InsertPokemonFromUserType
        {
            get { return _pokemonFromUserType; }
            set { SetProperty(ref _pokemonFromUserType, value.ToLower().Replace(" ", String.Empty)); }
        }

        public int InsertPokemonFromUserSpeed
        {
            get { return _pokemonFromUserSpeed; }
            set { SetProperty(ref _pokemonFromUserSpeed, value); }
        }
        public int InsertPokemonFromUserHP
        {
            get { return _pokemonFromUserHP; }
            set { SetProperty(ref _pokemonFromUserHP, value); }
        }

        public int InsertPokemonFromUserWeight
        {
            get { return _pokemonFromUserWeight; }
            set { SetProperty(ref _pokemonFromUserWeight, value); }
        }

        public int InsertPokemonFromUserHeight
        {
            get { return _pokemonFromUserHeight; }
            set { SetProperty(ref _pokemonFromUserHeight, value); }
        }

        public int InsertPokemonFromUserExperience
        {
            get { return _pokemonFromUserExperience; }
            set { SetProperty(ref _pokemonFromUserExperience, value); }
        }

        public string InsertPokemonFromUserName
        {
            get { return _pokemonFromUserName; }
            set { SetProperty(ref _pokemonFromUserName, value.ToLower()); }
        }
        public int InsertPokemonFromUserAttackScore
        {
            get { return _pokemonFromUserAttackScore; }
            set { SetProperty(ref _pokemonFromUserAttackScore, value); }
        }
        public int InsertPokemonFromUserDefenseScore
        {
            get { return _pokemonFromUserDefenseScore; }
            set { SetProperty(ref _pokemonFromUserDefenseScore, value); }
        }
        public int InsertPokemonFromUserSpecialAttackScore
        {
            get { return _pokemonFromUserSpecialAttackScore; }
            set { SetProperty(ref _pokemonFromUserSpecialAttackScore, value); }
        }
        public int InsertPokemonFromUserSpecialDefenseScore
        {
            get { return _pokemonFromUserSpecialDefenseScore; }
            set { SetProperty(ref _pokemonFromUserSpecialDefenseScore, value); }
        }
        public string InsertPokemonFromUserAbilities
        {
            get { return _pokemonFromUserAbilities; }
            set { SetProperty(ref _pokemonFromUserAbilities, value.ToLower().Replace(" ", String.Empty)); }
        }

        public int IndexComboBox
        {
            get { return _indexComboBox; }
            set { _indexComboBox = value; }
        }

        public bool ButtonClickEnable
        {
            get { return _buttonClickEnable; }
            set { SetProperty(ref _buttonClickEnable, value); }
        }
        public string SearchPokemon
        {
            get { return _searchPokemon; }
            set { SetProperty(ref _searchPokemon, value.ToLower().Replace(" ", String.Empty)); }
        }

        public int CurrentPageView
        {
            get { return _currentPageView; }
            set { SetProperty(ref _currentPageView, value); }
        }

        public ObservableCollection<Pokemon> PokemonCollection
        {
            get { return _pokemonCollection; }
            set { SetProperty(ref _pokemonCollection, value); }
        }

        public PokemonViewModel()
        {
            IndexComboBox = -1;
            CurrentPageView = 0;
            PokemonCollection = new ObservableCollection<Pokemon>();
            Init();
        }

        public async void Init()
        {
            await Database.InitializeDatabase();
            await Images.CreateRepository();
            await LoadPokemonInformation(CurrentPageView);
            ProgrssRingAndClickEnable();
        }

        private async void CreatePokemonFromUser()
        {
            if (MakeSureAllBoxesAreFilled())
            {
                Pokemon wantedPokemon;
                int currentPokemonFromUserID = 250;
                List<Pokemon> pokemonsFromUser = new List<Pokemon>();
                do
                {
                    currentPokemonFromUserID++;
                    wantedPokemon = Database.FindPokemonByID(currentPokemonFromUserID);
                    pokemonsFromUser.Add(wantedPokemon);

                } while (wantedPokemon != null);

                int ID = pokemonsFromUser.Count + 250;

                string[] newPokemonFromAbilities = InsertPokemonFromUserAbilities.Split(",");

                string[] newPokemonFromTypes = InsertPokemonFromUserType.Split(",");

                Pokemon newPokemon = new Pokemon(ID, InsertPokemonFromUserName, InsertPokemonFromUserExperience, InsertPokemonFromUserHeight,
                    InsertPokemonFromUserWeight, InsertPokemonFromUserHP, InsertPokemonFromUserAttackScore, InsertPokemonFromUserDefenseScore,
                    InsertPokemonFromUserSpecialAttackScore, InsertPokemonFromUserSpecialDefenseScore, InsertPokemonFromUserSpeed);

                bool successfullyInserted = Database.InsertPokemon(newPokemon);

                if (successfullyInserted)
                {
                    try
                    {
                        await ImageFilePokemonFromUser.CopyAsync(ImagesSouce, $"{ID}.png");
                    }
                    catch (Exception)
                    {

                        _ = ShowMessageDialog("Pokémon salvo sem imagem","Alerta");
                    }
                    

                    for (int i = 0; i < newPokemonFromAbilities.Length; i++)
                    {
                        Database.InsertPokemonAbility(ID, newPokemonFromAbilities[i]);
                    }

                    for (int i = 0; i < newPokemonFromTypes.Length; i++)
                    {
                        Database.InsertPokemonType(ID, newPokemonFromTypes[i]);
                    }

                    _ = ShowMessageDialog($"Pokémon {InsertPokemonFromUserName} de ID = {ID} foi criado com sucesso!", "Alerta de Criação");

                }
                else
                {
                    _ = ShowMessageDialog($"O Pokémon não foi criado tente novamente!", "Alerta de Criação");
                }
            }
            else
            {
                _ = ShowMessageDialog($"Você precisa, no mínimo, preencher os campos de nome, tipos e abilidades.", "Alerta de Criação");
            }
        }

        public bool MakeSureAllBoxesAreFilled()
        {
            if (InsertPokemonFromUserName != null && InsertPokemonFromUserAbilities != null && InsertPokemonFromUserType != null)
            {
                if (!InsertPokemonFromUserName.Equals("") && !InsertPokemonFromUserAbilities.Equals("") && !InsertPokemonFromUserType.Equals(""))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task LoadPictureNewPokemon()
        {

            Pokemon wantedPokemon;
            int currentPokemonFromUserID = 250;
            List<Pokemon> pokemonsFromUser = new List<Pokemon>();
            do
            {
                currentPokemonFromUserID++;
                wantedPokemon = Database.FindPokemonByID(currentPokemonFromUserID);
                pokemonsFromUser.Add(wantedPokemon);

            } while (wantedPokemon != null);

            int ID = pokemonsFromUser.Count + 250;


            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                ImageFilePokemonFromUser = file;

                StorageFolder pathPicturesPokemons = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");

                ImagesSouce = pathPicturesPokemons;

                _ = ShowMessageDialog("Foto Salva", "Alerta");
            }
            else
            {
                _ = ShowMessageDialog("Operação não realizada", "Alerta");
            }
        }

        public async Task<bool> CheckImageInFolder(int pokemonID)
        {

            StorageFolder pathPicturesPokemons = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");
            try
            {
                StorageFile file = await pathPicturesPokemons.GetFileAsync($"{pokemonID}.png");
                return false;
            }
            catch (Exception)
            {
                return true;
            }
            
            
        }

        public async Task LoadPokemonInformation(int currentPageView)
        {
            var taskGetTenPokemonsByPaging = await GetTenPokemonsByPaging(currentPageView);
            List<Pokemon> tenPokemon = taskGetTenPokemonsByPaging;
            foreach (Pokemon currentPokemon in tenPokemon)
            {
                currentPokemon.ImageFilePath = Images.RepoPath + "\\" + currentPokemon.ID + ".png";
                _pokemonCollection.Add(currentPokemon);
            }
        }
        public async Task LoadPokemonsInformationsFromUser()
        {
            Pokemon currentUserPokemon;
            int currentID = 250;

            do
            {
                currentID++;
                currentUserPokemon = Database.FindPokemonByID(currentID);

                if (currentUserPokemon != null)
                {
                    currentUserPokemon.ImageFilePath = Images.RepoPath + "\\" + currentUserPokemon.ID + ".png";
                    _pokemonCollection.Add(currentUserPokemon);
                }
            } while (currentUserPokemon != null);
        }

        private async void BackHome()
        { 
            ButtonsInitialPageVisibility = Visibility.Visible;
            ButtonHomeVisibility = Visibility.Collapsed;
            PokemonCollection.Clear();
            await LoadPokemonInformation(CurrentPageView);
        }

        private void ShowButtonHome()
        {
            ButtonsInitialPageVisibility = Visibility.Collapsed;
            ButtonHomeVisibility = Visibility.Visible;
            CurrentPageView = 0;
        }

        private void AlertTypesBox()
        {
            _ = ShowMessageDialog($"Digite os nomes desse atributo separados por vígula, quando houver mais de um.", "Alerta de Criação");
            AlertTypesBoxButtonVisibility = Visibility.Collapsed;
        }

        private void AlertAbilitiesBox()
        {
            _ = ShowMessageDialog($"Digite os nomes desse atributo separados por vígula, quando houver mais de um.", "Alerta de Criação");
            AlertAbilitiesBoxButtonVisibility = Visibility.Collapsed;
        }

        private bool areThereUserCreatedPokemons() 
        {
            Pokemon pokemon = Database.FindPokemonByID(251);
            if (pokemon != null)
            {
                return true;
            }

            return false;
        }

        private void ProgrssRingAndClickEnable()
        {
            if (ProgrssRingVisibility == Visibility.Visible)
            {
                ProgrssRingVisibility = Visibility.Collapsed;
                ButtonClickEnable = true;
            }
            else
            {
                ProgrssRingVisibility = Visibility.Visible;
                ButtonClickEnable = false;
            }
        }

        private async Task ShowMessageDialog(string msg, string title)
        {
            var dialog = new MessageDialog(msg);
            dialog.Title = title;
            await dialog.ShowAsync();
        }

        private async void NextPage()
        {
            if (CurrentPageView == 24 && areThereUserCreatedPokemons())
            {
                CurrentPageView++;
                PokemonCollection.Clear();
                ProgrssRingAndClickEnable();
                await LoadPokemonInformation(CurrentPageView);
                ProgrssRingAndClickEnable();
            }
            else if (CurrentPageView < 24)
            {
                CurrentPageView++;
                PokemonCollection.Clear();
                ProgrssRingAndClickEnable();
                await LoadPokemonInformation(CurrentPageView);
                ProgrssRingAndClickEnable();
            }
            else
            {
                _ = ShowMessageDialog("Você está na página final.", "Alerta de Navegação");
            }
        }

        private async void PreviousPage()
        {
            if (CurrentPageView > 0)
            {
                CurrentPageView--;
                PokemonCollection.Clear();
                ProgrssRingAndClickEnable();
                await LoadPokemonInformation(CurrentPageView);
                ProgrssRingAndClickEnable();
            }
            else
            {
                _ = ShowMessageDialog("Você está na primeira página.", "Alerta de Navegação");
            }
        }

        public async Task<List<Pokemon>> GetTenPokemonsByPaging(int page)
        {
            List<Pokemon> pokemonList = Database.GetTenPokemon(page);
            if (pokemonList.Count == _amountOfPokemonPerPage)
            {
                return pokemonList;
            }
            else if (page < 25)
            {
                pokemonList.Clear();
                int counter = 0;
                int lastPokemonID = page * 10;
                int timesToRepeat = _amountOfPokemonPerPage;

                while (counter < timesToRepeat && lastPokemonID <= _pokemonMaxLimit)
                {
                    lastPokemonID++;
                    Pokemon pokemon = await API.ConsumeAPIByIDAsync(lastPokemonID);
                    pokemon.Status = CheckPokemonStatus(pokemon.StatusList);
                    Database.InsertPokemon(pokemon);

                    pokemon.Types.ForEach(t => Database.InsertPokemonType(pokemon.ID, t.Type.Name));
                    pokemon.Abilities.ForEach(a => Database.InsertPokemonAbility(pokemon.ID, a.Ability.Name));

                    Images.DownloadPokemonImage(pokemon.ID);

                    pokemonList.Add(pokemon);
                    counter++;
                }
            }
            return pokemonList;
        }

        public async Task ProcessQueryAsync()
        {
            if ((SearchPokemon == null || SearchPokemon.Equals("")) && IndexComboBox != 3)
            {
                _ = ShowMessageDialog("Preencha o espaço em branco!", "Alerta de Busca");
                BackHome();
            }
            else
            {
                switch (IndexComboBox)
                {
                    case -1:
                        _ = ShowMessageDialog("Por favor selecione uma busca ao lado!", "Alerta: Caixa de Opções");
                        BackHome();
                        break;

                    case 0:
                        if (SearchPokemon.Length < 3)
                        {
                            _ = ShowMessageDialog("Por favor digite pelo meno 3 caracteres!", "Alerta de Busca");
                            BackHome();
                        }
                        else
                        {
                            PokemonCollection.Clear();
                            ProgrssRingAndClickEnable();
                            await FindPokemonByNameAsync(SearchPokemon);
                            ProgrssRingAndClickEnable();

                            if (PokemonCollection.Count != 0)
                            {
                                ShowButtonHome();
                            }
                            else
                            {
                                BackHome();
                            }
                        }
                        break;

                    case 1:
                        PokemonCollection.Clear();
                        ProgrssRingAndClickEnable();
                        await FindPokemonByType(SearchPokemon);
                        ProgrssRingAndClickEnable();

                        if (PokemonCollection.Count > 0)
                        {
                            ShowButtonHome();
                        }
                        else
                        {
                            BackHome();
                        }
                        break;

                    case 2:
                        try
                        {
                            int pokemonID = Int32.Parse(SearchPokemon);
                            PokemonCollection.Clear();
                            ProgrssRingAndClickEnable();
                            await FindPokemonByIDAsync(pokemonID);
                            ProgrssRingAndClickEnable();

                            if (PokemonCollection.Count != 0)
                            {
                                ShowButtonHome();
                            }
                            else
                            {
                                BackHome();
                            }

                        }
                        catch (FormatException)
                        {
                            _ = ShowMessageDialog("ID Inválido!", "Alerta de Busca");
                        }
                        break;

                    case 3:
                        if (areThereUserCreatedPokemons())
                        {
                            PokemonCollection.Clear();
                            ProgrssRingAndClickEnable();
                            await LoadPokemonsInformationsFromUser();
                            ProgrssRingAndClickEnable();
                            ShowButtonHome();
                        }
                        else 
                        {
                            BackHome();
                            _ = ShowMessageDialog("Você ainda não criou nenhum pokémon!", "Alerta de Busca");
                        }

                        break;

                }
            }
        }

        public async Task FindPokemonByIDAsync(int pokemonID)
        {

            Pokemon pokemonByID = Database.FindPokemonByID(pokemonID);
            if (pokemonByID != null)
            {
                pokemonByID.ImageFilePath = Images.RepoPath + "\\" + pokemonByID.ID + ".png";
                PokemonCollection.Add(pokemonByID);
            }
            else if (pokemonID > 0 && pokemonID < 251)
            {
                Pokemon pokemon = await API.ConsumeAPIByIDAsync(pokemonID);
                pokemon.Status = CheckPokemonStatus(pokemon.StatusList);

                if (Database.InsertPokemon(pokemon))
                {
                    pokemon.Types.ForEach(t => Database.InsertPokemonType(pokemon.ID, t.Type.Name));
                    pokemon.Abilities.ForEach(a => Database.InsertPokemonAbility(pokemon.ID, a.Ability.Name));
                    Images.DownloadPokemonImage(pokemon.ID);
                    pokemon.ImageFilePath = Images.RepoPath + "\\" + pokemon.ID + ".png";

                    PokemonCollection.Add(pokemon);
                }
            }
            else
            {
                _ = ShowMessageDialog("Pokémon não encontrado! Por favor, digite um ID entre 1 e 250.", "Alerta de Busca");
            }
        }

        public async Task FindPokemonByNameAsync(string pokemonName)
        {
            List<Pokemon> pokemonListByName = Database.FindPokemonByName(pokemonName);
            ObservableCollection<Pokemon> pokemonObservableCollectionByName;
            if (pokemonListByName.Count > 0)
            {
                pokemonObservableCollectionByName = new ObservableCollection<Pokemon>(pokemonListByName);
                foreach (Pokemon currentPokemon in pokemonListByName)
                {
                    currentPokemon.ImageFilePath = Images.RepoPath + "\\" + currentPokemon.ID + ".png";
                }
                PokemonCollection = pokemonObservableCollectionByName;
            }
            else
            {
                try
                {
                    Pokemon pokemon = await API.ConsumeAPIByNameAsync(pokemonName);
                    pokemon.Status = CheckPokemonStatus(pokemon.StatusList);
                    if (Database.InsertPokemon(pokemon))
                    {
                        pokemon.Types.ForEach(t => Database.InsertPokemonType(pokemon.ID, t.Type.Name));
                        pokemon.Abilities.ForEach(a => Database.InsertPokemonAbility(pokemon.ID, a.Ability.Name));
                        Images.DownloadPokemonImage(pokemon.ID);
                        pokemon.ImageFilePath = Images.RepoPath + "\\" + pokemon.ID + ".png";

                        pokemonListByName.Add(pokemon);
                        pokemonObservableCollectionByName = new ObservableCollection<Pokemon>(pokemonListByName);
                        PokemonCollection = pokemonObservableCollectionByName;
                    }
                }
                catch (Exception)
                {
                    _ = ShowMessageDialog("Pokémon não encontrado!", "Alerta de Busca");
                }
            }
        }

        public async Task FindPokemonByType(string pokemonType)
        {
            int counter = 0;
            int timesToRepeat;

            try
            {
                PokemonSummarizedList summarizedList = await API.ConsumeAPIByTypeAsync(pokemonType);
                timesToRepeat = summarizedList.PokemonList.Count;
                List<Pokemon> pokemonListByType = new List<Pokemon>();
                while (counter < timesToRepeat)
                {
                    string pokemonURL = summarizedList.PokemonList.ElementAt(counter).Pokemon.URL.Replace("/", "");
                    pokemonURL = Regex.Match(pokemonURL, "[0-9]+$").Value;
                    int pokemonID = int.Parse(pokemonURL);

                    if (pokemonID > _pokemonMaxLimit)
                    {
                        break;
                    }

                    Pokemon pokemon = await API.ConsumeAPIByIDAsync(pokemonID);
                    pokemon.Status = CheckPokemonStatus(pokemon.StatusList);
                    if(await CheckImageInFolder(pokemonID))
                    {
                      Images.DownloadPokemonImage(pokemon.ID);
                    }
                    pokemon.ImageFilePath = Images.RepoPath + "\\" + pokemon.ID + ".png";
                    pokemonListByType.Add(pokemon);
                    counter++;
                }
                ObservableCollection<Pokemon> pokemonObservableCollectionByType = new ObservableCollection<Pokemon>(pokemonListByType);
                PokemonCollection = pokemonObservableCollectionByType;
            }
            catch (Exception)
            {
                _ = ShowMessageDialog("Tipo de Pokémon não encontrado!", "Alerta de Busca");
            }
        }

        private Status CheckPokemonStatus(List<StatusWrapper> statusList)
        {
            Status pokemonStatus = new Status();

            foreach (StatusWrapper stat in statusList)
            {
                switch (stat.StatusInfo.Name)
                {
                    case "hp":
                        pokemonStatus.HP = stat.Value;
                        break;
                    case "attack":
                        pokemonStatus.Attack = stat.Value;
                        break;
                    case "defense":
                        pokemonStatus.Defense = stat.Value;
                        break;
                    case "special-attack":
                        pokemonStatus.SpecialAttack = stat.Value;
                        break;
                    case "special-defense":
                        pokemonStatus.SpecialDefense = stat.Value;
                        break;
                    case "speed":
                        pokemonStatus.Speed = stat.Value;
                        break;
                }
            }
            return pokemonStatus;
        }

        public async Task<Pokemon> FindPokemonByIDAsyncForInformationDialog(int pokemonID)
        {
            Pokemon pokemonByID = Database.FindPokemonByID(pokemonID);

            if (pokemonByID != null)
            {
                pokemonByID.ImageFilePath = Images.RepoPath + "\\" + pokemonByID.ID + ".png";
                return pokemonByID;
            }
            else if (pokemonID > 0 && pokemonID < 251)
            {
                Pokemon pokemon = await API.ConsumeAPIByIDAsync(pokemonID);
                pokemon.Status = CheckPokemonStatus(pokemon.StatusList);

                return pokemon;
            }
            else
            {
                _ = ShowMessageDialog("Pokémon não encontrado! Por favor, digite um ID entre 0 e 250.", "Alerta de Busca");
            }
            return pokemonByID;
        }
    }
}