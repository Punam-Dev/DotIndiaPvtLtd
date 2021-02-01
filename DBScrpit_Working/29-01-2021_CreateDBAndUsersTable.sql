Create DataBase DotIndiaPvtLtd

Create Table Users
(
	UserID Char(36) Primary Key Not Null,
	UserName nVarChar(10) Not Null,
	UserRoles nVarChar(512) Not Null,
	Password nVarChar(30) Not Null,
	FirstName nVarChar(20) Not Null,
	LastName nVarChar(30) Not Null,
	Email nVarChar(20) Not Null,
	Phone nVarChar(15) Not Null,
	Address nVarChar(2048),
	DOB Date,
	CallerName nVarChar(512),
	ActivationDate DateTime,
	Status Bit,
	WorkStatus Bit,
	OTP int,
	UserCreatedByUserID Char(36),
	UserCreatedDate DateTime Not Null,
	UserUpdatedByUserID Char(36),
	UserUpdatedDate DateTime
)

Go

Insert Into Users Values('07cfcf51-188c-4dca-a7c6-a3a0802124d3','admin','Admin','123','Admin', '', 'admin@gmail.com','9090909090', Null, GETDATE(), 
'Admin', Null, 1, 1, Null, '07cfcf51-188c-4dca-a7c6-a3a0802124d3', GETDATE(), Null,Null)

--Truncate Table Users