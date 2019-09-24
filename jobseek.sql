create table jobseek(
    JobseekId int primary key identity,
	JobCreatedDate date,
	RefUserId int foreign key references UserMaster(UserId),
	RefJobId int foreign key references ManageJob(JobId)
)