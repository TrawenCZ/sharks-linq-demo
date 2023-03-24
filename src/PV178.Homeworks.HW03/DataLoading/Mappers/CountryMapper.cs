using CsvHelper.Configuration;
using PV178.Homeworks.HW03.Model;

namespace PV178.Homeworks.HW03.DataLoading.Mappers
{
    public sealed class CountryMapper : ClassMap<Country>
    {
        public CountryMapper()
        {
            Map(m => m.Id);
            Map(m => m.Name);
            Map(m => m.Area);
            Map(m => m.Continent);
            Map(m => m.CountryCode);
            Map(m => m.Currency);
            Map(m => m.CurrencyCode);
            Map(m => m.GovernmentForm);
            Map(m => m.Population);
            Map(m => m.Birthrate);
            Map(m => m.Deathrate);
            Map(m => m.LifeExpectancy);
        }
    }
}