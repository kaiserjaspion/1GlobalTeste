using _1Global.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1Global.Domain.V1.Repository.Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> CreateNewDevice(Device item);
        Task<Device> EditNewDevice(Device item);
        Task<Device> GetDevice(int Id);
        Task<List<Device>> GetAllDevices();
        Task<List<Device>> GetAllDevicesByBrand();
        Task<List<Device>> GetAllDevicesByState();
        Task<bool> DeleteDevice(int Id);
    }
}
