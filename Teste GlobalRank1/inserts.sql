USE DevicesDatabase
GO

-- Cadastramento de Regioes

INSERT INTO dbo.Device(Name, Brand,DeviceState,CreationTime)
VALUES ('DEVICE1','MOTOROLA',1,GETDATE())


INSERT INTO dbo.Device(Name, Brand,DeviceState,CreationTime)
VALUES ('DEVICE2','MOTOROLA',2,GETDATE())


INSERT INTO dbo.Device(Name, Brand,DeviceState,CreationTime)
VALUES ('DEVICE3','SAMSUNG',1,GETDATE())


INSERT INTO dbo.Device(Name, Brand,DeviceState,CreationTime)
VALUES ('DEVICE4','SAMSUNG',2,GETDATE())


INSERT INTO dbo.Device(Name, Brand,DeviceState,CreationTime)
VALUES ('DEVICE5','SAMSUNG',3,GETDATE())
