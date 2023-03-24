using System.Collections.ObjectModel;
using PV178.Homeworks.HW03.Model;

namespace PV178.Homeworks.HW03.DataLoading.DataImporter
{
    public interface IDataImporter
    {
        IReadOnlyList<AttackedPerson> ListAllAttackedPeople();
        ReadOnlyCollection<Country> ListAllCountries();
        IReadOnlyList<SharkAttack> ListAllSharkAttacks();
        ReadOnlyCollection<SharkSpecies> ListAllSharkSpecies();
    }
}