using PV178.Homeworks.HW03.DataLoading.DataContext;

namespace PV178.Homeworks.HW03.DataLoading.Factory
{
    public interface IDataContextFactory
    {
        IDataContext CreateDataContext();
    }
}
