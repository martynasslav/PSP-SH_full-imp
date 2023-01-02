using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly DbEntities _dbEntities;

        public ShiftRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Shift> GetShifts()
        {
            return _dbEntities.Shifts.AsEnumerable();
        }

        public Shift? GetShiftById(string id)
        {
            return _dbEntities.Shifts.FirstOrDefault(s => s.Id == id);
        }

        public void InsertShift(Shift shift)
        {
            _dbEntities.Shifts.Add(shift);
        }

        public void DeleteShift(Shift shift)
        {
            _dbEntities.Shifts.Remove(shift);
        }

        public void UpdateShift(Shift shift)
        {
            var item = _dbEntities.Shifts.First(s => s.Id == shift.Id);
            item.CheckInDate = shift.CheckInDate;
            item.CheckOutDate = shift.CheckOutDate;
            item.EmployeeId = shift.EmployeeId;
            item.StartDate = shift.StartDate;
            item.FinishDate = shift.FinishDate;
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}