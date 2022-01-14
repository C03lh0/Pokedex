using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.Storage;

namespace Repository.Local
{
    public static class Images
    {
        private static string _repoPath;
        private const string _imageURL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/";

        public static async Task CreateRepository()
        {
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
            string imagesPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "images");
            _repoPath = imagesPath;
        }

        public static void DownloadPokemonImage(int pokemonID)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(_imageURL + $"{pokemonID}.png", Path.Combine(_repoPath, $"{pokemonID}.png"));
            }
        }
        public static string RepoPath { get { return _repoPath; } set { _repoPath = value; } }
    }
}
