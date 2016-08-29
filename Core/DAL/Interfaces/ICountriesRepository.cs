using System.Collections.Generic;
using  Core.POCO;

namespace  Core.DAL.Interfaces
{
    public interface ICountriesRepository
    {
      
        List<Country> GetAllCountries();

        Town[] GetTownsByCountry(int id);

        Town[] GetTownsByCountryName(string id);
    }
}