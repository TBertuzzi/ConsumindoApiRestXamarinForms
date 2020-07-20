using ConsumindoApiRestXamarinForms.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiRestXamarinForms.Services.Refit
{
    public interface IRefitApiService
    {
        [Get("/pokemon/{id}")]
        Task<Pokemon> GetPokemonAsync(int id);
    }
}
