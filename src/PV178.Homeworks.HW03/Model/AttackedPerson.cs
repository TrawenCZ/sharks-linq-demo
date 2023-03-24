using PV178.Homeworks.HW03.Model.Enums;

namespace PV178.Homeworks.HW03.Model
{
    /// <summary>
    /// Represents attacked person, note that
    /// every person has been attacked only once.
    /// </summary>
    public class AttackedPerson
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name and the surname of the person
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Person gender
        /// </summary>
        public Sex Sex { get; set; }

        private int? age;
        /// <summary>
        /// Age of the person (has value null if unavailable)
        /// </summary>
        public int? Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != null && value == 0)
                {
                    age = null;
                }
                else
                {
                    age = value;
                }
            }
        }

        public AttackedPerson()
        {
        }

        public AttackedPerson(int id, string name, Sex sex, int? age)
        {
            Id = id;
            Name = name;
            Sex = sex;
            Age = age;
        }

        public override string ToString()
        {
            var ageReport = Age.HasValue ? $" aged {Age.Value}" : string.Empty;
            return $"ID: {Id} {Sex} {Name}{ageReport}";
        }

        protected bool Equals(AttackedPerson other)
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
            return obj.GetType() == this.GetType() && Equals((AttackedPerson)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
