using _1Global.Application.V2.Services;
using _1Global.Data.DTO;
using _1Global.Domain.Context;
using _1Global.Domain.V2.Repository;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace _1Global.Test.V2
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

            var mockSet = new Mock<DbSet<Device>>();

            mockSet.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var options = new DbContextOptionsBuilder<DeviceContext>().Options;
            var mockContext = new Mock<DeviceContext>(options);
            mockContext.Setup(c => c.Devices).Returns(mockSet.Object);


            _service = new DeviceService(new DeviceRepository(mockContext.Object));
            _device = new Device
            {
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
            var result = await _service.EditNewDevice(_device);
            Assert.Equal(_device.Name, result.Name);
        }

        [Fact]
        public async void EditNewDeviceWithInUseUnitTest()
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
        public async void GetDeviceUnitTest()
        {
            Assert.IsType<Device>(await _service.GetDevice(1));
        }


        [Fact]
        public async void GetAllDevicesByAllUnitTest()
        {
            Assert.IsType<List<Device>>(await _service.GetAllDevices(Data.V2.Enum.EnumOrderBy.none));
        }
        [Fact]
        public async void GetAllDevicesByBrandUnitTest()
        {
            Assert.IsType<List<Device>>(await _service.GetAllDevices(Data.V2.Enum.EnumOrderBy.brand));
        }
        [Fact]
        public async void GetAllDevicesByStatusUnitTest()
        {
            Assert.IsType<List<Device>>(await _service.GetAllDevices(Data.V2.Enum.EnumOrderBy.status));
        }

        [Fact]
        public async void DeleteDeviceUnitTest()
        {
            Assert.True(await _service.DeleteDevice(_device.Id));
        }
    }
}
