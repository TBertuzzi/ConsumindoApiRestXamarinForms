using ConsumindoApiRestXamarinForms.Helpers;
using ConsumindoApiRestXamarinForms.Models;
using ConsumindoApiRestXamarinForms.Services.Base;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiRestXamarinForms.Services.Flurl
{
    public class FlurlService : IApiService
    {
        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            try
            {
                var httpClient = new HttpClient();

                for (int i = 1; i < 20; i++)
                {

                    Pokemon pokemon = 
                        await $"{Constantes.ApiBaseUrl}pokemon/{i}".GetJsonAsync<Pokemon>();

                    pokemons.Add(pokemon);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return pokemons;
        }


    }
}
