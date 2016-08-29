using  Core.BLL.Interfaces;
using  Core.DAL.Interfaces;
using  Core.POCO;
using System.Collections.Generic;

namespace  Core.BLL
{
    public class CountriesService : LogicLayer, ICountriesService
    {
        IUnitOfWork Database;
        public CountriesService(IUnitOfWork db)
        {
            Database = db;
        }

        public List<Country> GetAllCountries()
        {
            string key = "Countries_AllCountries";

            List<Country> countries = null;

            if (Cache[key] != null)
                countries = (List<Country>)Cache[key];
            else
            {
                countries = Database.Countries.GetAllCountries();
                Cache[key] = countries;
            }
            return countries;
        }

        public Town[] GetTownsByCountry(int id)
        {
            string key = "Countries_Towns_" + id;

            Town[] towns = null;

            if (Cache[key] != null)
                towns = (Town[])Cache[key];
            else
            {
                towns = Database.Countries.GetTownsByCountry(id);
                Cache[key] = towns;
            }
            return towns;
        }

        public Town[] GetTownsByCountryName(string id)
        {
            string key = "Countries_Towns_" + id;

            Town[] towns = null;

            if (Cache[key] != null)
                towns = (Town[])Cache[key];
            else
            {
                towns = Database.Countries.GetTownsByCountryName(id);
                Cache[key] = towns;
            }
            return towns;
        }
    }
}
