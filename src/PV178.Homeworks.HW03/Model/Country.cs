using System.Globalization;
using PV178.Homeworks.HW03.Model.Enums;

namespace PV178.Homeworks.HW03.Model
{
    /// <summary>
    /// Represents country, where the shark attack occured.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Official name of the country
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Official code of the country (AF - for afghanistan, ...)
        /// </summary>
        public string? CountryCode { get; set; }

        /// <summary>
        /// Continent, where the country is located
        /// </summary>
        public string? Continent { get; set; }

        /// <summary>
        /// The total population count
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// Total square kilometers of the country
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// Type of government of the country
        /// </summary>
        public GovernmentForm GovernmentForm { get; set; }

        /// <summary>
        /// Country currency name
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Country currency code
        /// </summary>
        public string? CurrencyCode { get; set; }

        /// <summary>
        /// Birthrate percentage
        /// </summary>
        public double Birthrate { get; set; }

        /// <summary>
        /// Deathrate percentage
        /// </summary>
        public double Deathrate { get; set; }

        /// <summary>
        /// Average life expectancy in years
        /// </summary>
        public double LifeExpectancy { get; set; }

        public Country() { }

        public Country(int id, string name, string countryCode, string continent, int population, int area, GovernmentForm governmentForm, string currency, string currencyCode, double birthrate, double deathrate, double lifeExpectancy)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            Continent = continent;
            Population = population;
            Area = area;
            GovernmentForm = governmentForm;
            Currency = currency;
            CurrencyCode = currencyCode;
            Birthrate = birthrate;
            Deathrate = deathrate;
            LifeExpectancy = lifeExpectancy;
        }

        public override string ToString()
        {
            return $"ID: {Id} {GovernmentForm} {Name} at {Continent} with population of {Population}. Demography: birthrate: {Birthrate.ToString(CultureInfo.InvariantCulture)}, deathrate: {Deathrate.ToString(CultureInfo.InvariantCulture)}, expected lifetime is {LifeExpectancy.ToString(CultureInfo.InvariantCulture)}";
        }

        protected bool Equals(Country other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && Equals((Country)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
