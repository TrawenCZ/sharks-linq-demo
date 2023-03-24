using PV178.Homeworks.HW03.DataLoading.DataContext;

namespace PV178.Homeworks.HW03.DataLoading.Factory
{
    public class DataContextFactory : IDataContextFactory
    {
        public IDataContext CreateDataContext()
        {
            var importer = new DataImporter.DataImporter();
            return new DataContext.DataContext(importer.ListAllSharkAttacks(),
                importer.ListAllAttackedPeople(),
                importer.ListAllSharkSpecies(),
                importer.ListAllCountries());
        }
    }
}
