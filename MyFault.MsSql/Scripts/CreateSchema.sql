create table Fault
(
	FaultId char(64) default newid() not null
		constraint Fault_FaultId_pk
			primary key,
	Message nvarchar(2000) not null,
	Data nvarchar(max) not null,
	CreatedTime datetime default getdate() not null
)
go

create table Instance
(
	InstanceId uniqueidentifier default newid() not null
		constraint Instance_InstanceId_pk
			primary key,
	FaultId char(64) not null
		constraint Instance_Fault_FaultId_fk
			references Fault,
	CreatedTime datetime not null
)
go

create table InstanceData
(
	InstanceId uniqueidentifier not null
		constraint InstanceData_Instance_InstanceId_fk
			references Instance,
	DataTypeId tinyint,
	DataKey varchar(255) not null,
	DataValue nvarchar(max),
	constraint InstanceData_InstanceId_DataKey_pk
		primary key (InstanceId, DataKey)
)
go

create table InstanceDataBinary
(
	InstanceId uniqueidentifier not null
		constraint InstanceDataBinary_Instance_InstanceId_fk
			references Instance,
	DataKey varchar(255) not null,
	DataContentType varchar(255),
	DataContents varbinary,
	constraint InstanceDataBinary_InstanceId_Name_pk
		primary key (InstanceId, DataKey)
)
go

