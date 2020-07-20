using ConsumindoApiRestXamarinForms.Helpers;
using ConsumindoApiRestXamarinForms.Models;
using ConsumindoApiRestXamarinForms.Services.Base;
using ConsumindoApiRestXamarinForms.Services.Flurl;
using ConsumindoApiRestXamarinForms.Services.HttpExtensionService;
using ConsumindoApiRestXamarinForms.Services.Refit;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConsumindoApiRestXamarinForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _key = "PokemonCache";

        public ObservableCollection<Pokemon> Pokemons { get; }
        IApiService _ApiService;
        IRefitApiService _ApiServiceRefit;

        public ICommand CarregarHttpExtensionCommand { get; }
        public ICommand CarregarRefitCommand { get; }
        public ICommand CarregarFlurlCommand { get; }


        public MainViewModel()
        {
            Titulo = "Consumindo API";

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            _ApiServiceRefit = RestService.For<IRefitApiService>(Constantes.ApiBaseUrl);

            Pokemons = new ObservableCollection<Pokemon>();

            CarregarHttpExtensionCommand = new Command(ExecuteCarregarHttpExtensionCommand);
            CarregarRefitCommand = new Command(ExecuteCarregarRefitCommand);
            CarregarFlurlCommand = new Command(ExecuteCarregarFlurlCommand);
        }

        private async void ExecuteCarregarFlurlCommand()
        {
            await CarregarPaginaFlurl();
        }

        private async void ExecuteCarregarRefitCommand()
        {
            await CarregarPaginaRefit();
        }

        private async void ExecuteCarregarHttpExtensionCommand()
        {
            await CarregarPaginaHttpExtension();
        }

        private async Task CarregarPaginaHttpExtension()
        {
            try
            {
                Ocupado = true;

                _ApiService = new HttpExtensionService();

                var pokemons = await _ApiService.GetPokemonsAsync();

                Pokemons.Clear();

                foreach (var pokemon in pokemons)
                {
                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);
                    Pokemons.Add(pokemon);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                _ApiService = null;
                Ocupado = false;
            }
        }

        private async Task CarregarPaginaRefit()
        {
            try
            {
                Ocupado = true;
                Pokemons.Clear();

                for (int i = 1; i < 20; i++)
                {

                    var pokemon = await _ApiServiceRefit.GetPokemonAsync(i);

                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);

                    Pokemons.Add(pokemon);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

                Ocupado = false;
            }
        }

        private async Task CarregarPaginaFlurl()
        {
            try
            {
                Ocupado = true;

                _ApiService = new FlurlService();

                var pokemons = await _ApiService.GetPokemonsAsync();

                Pokemons.Clear();

                foreach (var pokemon in pokemons)
                {
                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);
                    Pokemons.Add(pokemon);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                _ApiService = null;
                Ocupado = false;
            }
        }


        private static byte[] GetImageStreamFromUrl(string url)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    var imageBytes = webClient.GetByteArrayAsync(url).Result;

                    return imageBytes;

                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;

            }
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;

            if (IsNotConnected)
                await Application.Current.MainPage.DisplayAlert("Atenção", "Estamos sem internet :(", "OK");
        }

    }
}
