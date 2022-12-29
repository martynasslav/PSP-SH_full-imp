using PoSSapi.Classes;

namespace PoSSapi.Repositories
{
    public interface IShiftRepository
    {
        IEnumerable<Shift> GetShifts();
        Shift? GetShiftById(string id);
        void InsertShift(Shift shift);
        void DeleteShift(Shift shift);
        void UpdateShift(Shift shift);
        void Save();
    }
}
