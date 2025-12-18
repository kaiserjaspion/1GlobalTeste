using _1Global.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1Global.Application.V1.Services.Interfaces
{
    public interface IDeviceService
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
