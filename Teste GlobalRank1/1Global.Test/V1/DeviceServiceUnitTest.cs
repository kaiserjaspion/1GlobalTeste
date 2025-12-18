using _1Global.Application.V1.Services;
using _1Global.Data.DTO;
using _1Global.Domain.Context;
using _1Global.Domain.V1.Repository;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Core.Metadata.Edm;

namespace _1Global.Test.V1
{
    public class DeviceServiceUnitTest
    {

        private readonly DeviceService _service;
        private Device _device;
        private Faker _faker;

        public DeviceServiceUnitTest()
        {
            _faker = new Faker();

            var data = new List<Device>
            {
                new Device
                {
                    Id = 0,
                    Name = "Name0",
                    Brand = "Brand0",
                    State = Data.Enum.DeviceState.Available,
                },
                new Device
                {
                    Id = 1,
                    Name = "Name1",
                    Brand = "Brand1",
                    State = Data.Enum.DeviceState.Available,
                },
                new Device
                {
                    Id = 2,
                    Name = "Name2",
                    Brand = "Brand2",
                    State = Data.Enum.DeviceState.Available,
                }
            }.AsQueryable();

            var mockSet = new  Mock<DbSet<Device>>();

            mockSet.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var options = new DbContextOptionsBuilder<DeviceContext>().Options;
            var mockContext = new Mock<DeviceContext>(options);
            mockContext.Setup(c => c.Devices).Returns(mockSet.Object);


            _service = new DeviceService(new DeviceRepository(mockContext.Object));
            _device = new Device {
                Name = _faker.Random.AlphaNumeric(50),
                Brand = _faker.Random.AlphaNumeric(50),
                State = Data.Enum.DeviceState.Available,
                CreationTime = DateTime.Now
            };
        }

        [Fact]
        public void CreateNewDeviceUnitTest()
        {
            _device.Id = 3;
            var result = _service.CreateNewDevice(_device).Result;
            _device = result;
            Assert.IsType<Device>(result);
        }

        [Fact]
        public async void EditNewDeviceUnitTest()
        {
            _device.Id = 2;
            _device = await _service.CreateNewDevice(_device);
            _device.Name = _faker.Random.AlphaNumeric(50);
            var result =await _service.EditNewDevice(_device);
            Assert.Equal(_device.Name, result.Name);
        }

        [Fact]
        public async  void EditNewDeviceWithInUseUnitTest()
        {
            _device.Id = 2;
            _device.State = Data.Enum.DeviceState.InUse;
            _device = await _service.CreateNewDevice(_device);
            _service.CreateNewDevice(_device);
            _device.Name = _faker.Random.AlphaNumeric(50);
            var result = await _service.EditNewDevice(_device);
            Assert.NotEqual(_device.Name, result.Name);
        }

        [Fact]
        public void GetDeviceUnitTest()
        {
            Assert.IsType<Device>(_service.GetDevice(1).Result);
        }
        [Fact]
        public async void GetAllDevicesUnitTest()
        {
            var result = await _service.GetAllDevices();
            Assert.IsType<List<Device>>(result);
        }
        [Fact]
        public void GetAllDevicesByBrandUnitTest()
        {
            Assert.IsType<List<Device>>(_service.GetAllDevicesByBrand().Result);
        }
        [Fact]
        public void GetAllDevicesByStateUnitTest()
        {
            Assert.IsType<List<Device>>(_service.GetAllDevicesByState().Result);
        }
        [Fact]
        public void DeleteDeviceUnitTest()
        {
            Assert.True(_service.DeleteDevice(1).Result);
        }
    }
}
