using System.Globalization;

namespace PV178.Homeworks.HW03.Model
{
    /// <summary>
    /// Represents shark species
    /// </summary>
    public class SharkSpecies
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Common name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Common name
        /// </summary>
        public string? AlsoKnownAs { get; set; }

        /// <summary>
        /// Official latin name
        /// </summary>
        public string? LatinName { get; set; }

        /// <summary>
        /// Length in meters
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Weight in kg
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Max speed in km/h
        /// </summary>
        public int? TopSpeed { get; set; }

        public SharkSpecies()
        {

        }

        public SharkSpecies(int id, string name, string alsoKnownAs, string latinName, double length, int weight, int? topSpeed)
        {
            Id = id;
            Name = name;
            AlsoKnownAs = alsoKnownAs;
            LatinName = latinName;
            Length = length;
            Weight = weight;
            TopSpeed = topSpeed;
        }

        public override string ToString()
        {
            var aka = string.IsNullOrEmpty(AlsoKnownAs) ? "" : $"({AlsoKnownAs})";
            var topSpeed = TopSpeed.HasValue ? $" and can reach speeds up to {TopSpeed.Value} km/h." : ".";
            return $"ID: {Id} {Name} {aka}, max. length: {Length.ToString(CultureInfo.InvariantCulture)}m, weight: {Weight}kg{topSpeed}";
        }

        protected bool Equals(SharkSpecies other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SharkSpecies)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
