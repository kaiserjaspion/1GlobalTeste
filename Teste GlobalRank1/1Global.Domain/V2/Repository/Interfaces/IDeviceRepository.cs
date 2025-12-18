using _1Global.Data.DTO;
using _1Global.Data.V2.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1Global.Domain.V2.Repository.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> CreateNewDevice(Device item);
        Task<Device> EditNewDevice(Device item);
        Task<Device> GetDevice(int Id);
        Task<List<Device>> GetAllDevices(EnumOrderBy OrderBy);
        Task<bool> DeleteDevice(int Id);
    }
}
