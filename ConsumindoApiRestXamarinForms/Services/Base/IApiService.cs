using ConsumindoApiRestXamarinForms.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumindoApiRestXamarinForms.Services.Base
{
    public interface IApiService
    {
        Task<List<Pokemon>> GetPokemonsAsync();
    }
}
