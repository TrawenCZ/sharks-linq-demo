using CsvHelper.Configuration;
using PV178.Homeworks.HW03.Model;

namespace PV178.Homeworks.HW03.DataLoading.Mappers
{
    public sealed class SharkMapper : ClassMap<SharkSpecies>
    {

        public SharkMapper()
        {
            Map(m => m.Id);
            Map(m => m.LatinName);
            Map(m => m.Name);
            Map(m => m.AlsoKnownAs);
            Map(m => m.TopSpeed);
            Map(m => m.Weight);
            Map(m => m.Length);
        }
    }
}
