using  Core.DAL.Interfaces;
using  Core.POCO; 
using System.Collections.Generic;
using System.Linq; 

namespace  Core.DAL
{
    public class CountriesRepository : ICountriesRepository
    {
        DBContext db;
        public CountriesRepository(DBContext db)
        {
            this.db = db;
        }
                

        public List<Country> GetAllCountries()
        {
            return db.Countries.ToList();
        }

        public Town[] GetTownsByCountry(int id)
        {
            var data = from
                       c in db.Countries
                       join
                       t in db.Towns
                       on c.CountryID equals t.CountryID
                       where c.CountryID == id
                       select t;
                       

            Town[] towns = data.ToArray();

            return towns;
        }

        public Town[] GetTownsByCountryName(string id)
        { 
            var country = db.Countries.FirstOrDefault(a => a.CountryName == id);

            if (country != null)
            {
                Town[] towns = GetTownsByCountry(country.CountryID);
                return towns;
            }
            return null;
        }
    }
}
