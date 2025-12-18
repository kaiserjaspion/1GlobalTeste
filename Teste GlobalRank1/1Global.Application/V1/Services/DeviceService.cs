using _1Global.Application.V1.Services.Interfaces;
using _1Global.Data.DTO;
using _1Global.Domain.V1.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1Global.Application.V1.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Device> CreateNewDevice(Device item)
            => await _repository.CreateNewDevice(item);

        public async Task<Device> EditNewDevice(Device item)
            => await _repository.EditNewDevice(item);
        
        public async Task<Device> GetDevice(int Id)
            => await _repository.GetDevice(Id);
        
        public async Task<List<Device>> GetAllDevices()
            => await _repository.GetAllDevices();
        
        public async Task<List<Device>> GetAllDevicesByBrand()
            => await _repository.GetAllDevicesByBrand();
        
        public async Task<List<Device>> GetAllDevicesByState()
            => await _repository.GetAllDevicesByState();
        
        public async Task<bool> DeleteDevice(int Id)
            => await _repository.DeleteDevice(Id);
        
    }
}
