create table UserMaster(
  UserId int primary key identity,
  UserName varchar(50),
  UserAddress1 varchar(200),
  UserAddress2 varchar(200),
  UserGender varchar(20),
  UserDOB date,
  UserContact int,
  UserEmail varchar(100),
  UserSkills varchar(100),
  UserExperience decimal(10,2),
  UserDoc varchar(max),
  UCreatedBy varchar(20),
  UModifiedBy varchar(20),
  UCreatedDate date,
  UModifiedDate date,
  RefDepartmentId int Foreign key references Department(DepId),
  RefCityId int Foreign key references CityMaster(CityId)

)