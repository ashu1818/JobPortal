create table CountryMaster(
  
  CountryId int primary key identity,
  CountryName varchar(50) not null unique


)

create table StateMaster(

 StateId int primary key identity,
 StateName varchar(40) not null unique,
 RefCountryId int foreign key references CountryMaster(CountryId)
)

create table CityMaster(
 CityId int primary key identity,
 CityName varchar(40) not null unique,
 RefStateId int foreign key references StateMaster(StateId)

)