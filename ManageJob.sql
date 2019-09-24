create table ManageJob(
  JobId int primary key identity,
  JobName varchar(200),
  JobSkills varchar(200),
  JobExpirience varchar(100),
  JobIsActive bit,
  JobDocuments varchar(max),
  JobCreatedBy varchar(100),
  JobModifiedBy varchar(100),
  JobCreatedDate date,
  JobModifiedDate date,
 RefDepartmentId int foreign key references Department(DepId),
 RefCityId int foreign key references CityMaster(CityId)
)