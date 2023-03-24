using System;
using PV178.Homeworks.HW03.Model.Enums;

namespace PV178.Homeworks.HW03.Model
{
    /// <summary>
    /// Represents shark attack
    /// </summary>
    public class SharkAttack
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique identifier of the shark species (always known)
        /// </summary>
        public int SharkSpeciesId { get; set; }

        /// <summary>
        /// Unique identifier of the involved person (null if its unknown)
        /// </summary>
        public int? AttackedPersonId { get; set; }

        /// <summary>
        /// Unique identifier of the country (null if its unknown)
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// DateTime of the attack
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Type of the attack
        /// </summary>
        public AttackType? Type { get; set; }

        /// <summary>
        /// Region of the country where attack happened
        /// </summary>
        public string? Area { get; set; }

        /// <summary>
        /// Attack location
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Activity of the involved person
        /// (what person was doeing when the attack occured)
        /// </summary>
        public string? Activity { get; set; }

        /// <summary>
        /// Injuries caused by the attack
        /// </summary>
        public string? Injury { get; set; }

        /// <summary>
        /// Severenity of the attack (if person has survived)
        /// </summary>
        public AttackSeverenity? AttackSeverenity { get; set; }

        /// <summary>
        /// Name of the attack investigator,
        /// who can be either a person or an institute
        /// </summary>
        public string? Investigator { get; set; }

        public SharkAttack()
        {

        }

        public SharkAttack(int id, int attackedPersonId, DateTime dateTime, AttackType type, int countryId, string area, string location, string activity, string injury, AttackSeverenity fatal, int speciesId, string investigator)
        {
            Id = id;
            AttackedPersonId = attackedPersonId;
            DateTime = dateTime;
            Type = type;
            CountryId = countryId;
            Area = area;
            Location = location;
            Activity = activity;
            Injury = injury;
            AttackSeverenity = fatal;
            SharkSpeciesId = speciesId;
            Investigator = investigator;
        }

        public override string ToString()
        {
            var attackedPersonId = AttackedPersonId.HasValue ? $" on person with ID: {AttackedPersonId.Value}" : ".";
            var country = CountryId.HasValue ? $"in country with ID: {CountryId.Value} " : "";
            return $"ID: {Id} {Type} attack ({AttackSeverenity}) {country}by species with ID: {SharkSpeciesId} at {DateTime?.ToLongDateString()} {DateTime?.ToShortTimeString()}" + attackedPersonId;
        }

        protected bool Equals(SharkAttack other)
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
            return obj.GetType() == this.GetType() && Equals((SharkAttack)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
