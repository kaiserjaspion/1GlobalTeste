using _1Global.Data.DTO;
using _1Global.Data.Enum;
using _1Global.Data.V2.Enum;
using _1Global.Domain.Context;
using _1Global.Domain.V2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace _1Global.Domain.V2.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceContext _context;
        public DeviceRepository(DeviceContext context)
        {
            _context = context;
        }

        public async Task<Device> CreateNewDevice (Device item)
        {
            _context.Devices.Add(item);
             _context.SaveChanges();
            return item;
        }

        public async Task<Device> EditNewDevice(Device item)
        {
            if ( _context.Devices.Any(x => x.Id == item.Id && item.State != DeviceState.InUse))
            {
                _context.Devices.Where(p => p.Id == item.Id)
                    .ExecuteUpdate(sets => sets
                        .SetProperty(p => p.Name, item.Name)
                        .SetProperty(p => p.Brand, item.Brand)
                        .SetProperty(p => p.State, item.State));
                 _context.SaveChanges();
                return item;
            }
            return _context.Devices.FirstOrDefault(x => x.Id == item.Id);
        }

        public async Task<Device> GetDevice(int Id)
            =>  _context.Devices.FirstOrDefault(x => x.Id == Id);


        public async Task<List<Device>> GetAllDevices(EnumOrderBy OrderBy)
            =>  _context.Devices.ToList();

        public async Task<bool> DeleteDevice(int Id)
        {
            var result = _context.Devices.FirstOrDefault(x => x.Id == Id);
            if (result != null || result.State != DeviceState.InUse)
            {
                _context.Devices.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
