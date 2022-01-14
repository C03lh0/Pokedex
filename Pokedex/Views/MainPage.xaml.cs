using Windows.UI.Xaml.Controls;
using Pokedex.PokedexViewModel;
using Windows.UI.Xaml;
using Model.Model.Pokémon_Classes;
using Model.Model.Ability_Classes;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.System;
using Windows.Graphics.Display;
using System.Drawing;
using Windows.UI.ViewManagement;

namespace Pokedex.Views
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {   
            this.InitializeComponent();
        }

        public System.Windows.Input.ICommand LauchWikiPokemonUriCommand { get { return new Microsoft.Toolkit.Mvvm.Input.RelayCommand(LauchWikiPokemonUri); } }

        private async void PokemonInformationButtonEvent(object sender, RoutedEventArgs e)
        {
            PokemonViewModel pvm = new PokemonViewModel();
            Button pokemonButton = (Button)sender;
            int ID = int.Parse(pokemonButton.DataContext.ToString());
            Pokemon clickedPokemon = await pvm.FindPokemonByIDAsyncForInformationDialog(ID);
            string clickedPokemonAbilites = "| ";


            foreach (AbilityWrapper currentAbilityWrapper in clickedPokemon.Abilities)
            {
                clickedPokemonAbilites += currentAbilityWrapper.Ability.Name + " | ";
            }

            CornerRadius cornerRadius = new CornerRadius();
            cornerRadius.BottomLeft = 10;
            cornerRadius.BottomRight = 10;
            cornerRadius.TopLeft = 10;
            cornerRadius.TopRight = 10;

            ContentDialog pokemonInformation = new ContentDialog
            {

                Title = $"\b{clickedPokemon.Name}".ToUpper(),
                Content = $"\bHP: " + $"{clickedPokemon.Status.HP}" +
                          $"\n\bAttack: " + $"{clickedPokemon.Status.Attack}" +
                          $"\n\bSpecial Attack: " + $"{clickedPokemon.Status.SpecialAttack}" +
                          $"\n\bDefense: " + $"{clickedPokemon.Status.Defense}" +
                          $"\n\bSpecial Defense: " + $"{clickedPokemon.Status.SpecialDefense}" +
                          $"\n\bAbilities: " + $"{clickedPokemonAbilites}",
                CloseButtonText = "Fechar",
            };
            pokemonInformation.CornerRadius = cornerRadius;
            await pokemonInformation.ShowAsync();
        }

        private void ShowRegisterPageViewButtonEvent(object sender, RoutedEventArgs e)
        {
           // RegisterPageFrame.Navigate(typeof(Page));
            RegisterPageFrame.Visibility = Visibility.Visible;
            RegisterPageFrame.Navigate(typeof(RegisterPage));
            RegisterPage.HomeButton = BackHomeButton;
        }

        private void RegisterPageFrameCollapsedButtonEvent(object sender, RoutedEventArgs e)
        {
            RegisterPageFrame.Visibility = Visibility.Collapsed;
        }

        private async void ShowInformationDialogButtonEvent(object sender, RoutedEventArgs e)
        {
          
            CornerRadius cornerRadius = new CornerRadius();
            cornerRadius.BottomLeft = 10;
            cornerRadius.BottomRight = 10;
            cornerRadius.TopLeft = 10;
            cornerRadius.TopRight = 10;

            ContentDialog pokemonInformation = new ContentDialog
            {

                Title = $"\bInformações".ToUpper(),
                Content = $"\bVocê é um treinador pokémon de primeira viagem?\n" + "\n\b> Não se preocupe, nossa pokédex é sua parceira\n   Click no botão \"Universo pokémon\".\n" +
                          $"\n\bDicas e FeedBack? Manda um e-mail: \n" + $"\n\b> PokedexAtenciosaComOUsuario@gmail.com\n" +
                          $"\n By Aline Lira, Cristofer Silva and Matheus Coelho.",
                PrimaryButtonText = "Universo pokémon",
                CloseButtonText = "Fechar",
            };

            pokemonInformation.PrimaryButtonCommand = LauchWikiPokemonUriCommand;
            pokemonInformation.CornerRadius = cornerRadius;
            await pokemonInformation.ShowAsync();

        }

        public void LauchWikiPokemonUri()
        {
            Launcher.LaunchUriAsync(new Uri("https://pt.wikipedia.org/wiki/Pokémon"));
        }
    }
}
