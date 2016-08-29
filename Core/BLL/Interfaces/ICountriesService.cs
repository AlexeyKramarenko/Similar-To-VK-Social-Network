using System.Collections.Generic; 
using  Core.POCO;

namespace  Core.BLL.Interfaces
{
    public interface ICountriesService
    {
        List<Country> GetAllCountries();
        Town[] GetTownsByCountry(int id);
        Town[] GetTownsByCountryName(string id);
    }
}