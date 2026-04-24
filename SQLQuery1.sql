 select * from admin_access_festival;
DELETE from master_access_member where id=@id;

declare @name nvarchar(50);
declare @id nvarchar(50);
update admin_access_member (name, id) VALUES (@name, @id)
                               
declare @id nvarchar(50);
DELETE FROM  admin_access_member WHERE id = @id

CREATE TABLE super_admin (
    username NVARCHAR(50) NOT NULL PRIMARY KEY,
    password NVARCHAR(50) NOT NULL,
    full_name NVARCHAR(50) NULL
);
CREATE TABLE admin_access_member (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100)
)

CREATE TABLE admin_access_festival (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100)
)
select * from admin_access_festival;

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE
);

CREATE TABLE UserCredentials (
    Id INT PRIMARY KEY IDENTITY,
    Email NVARCHAR(100) UNIQUE,
    Password NVARCHAR(100),
    CanLogin BIT DEFAULT 1
);

SELECT * FROM UserCredentials WHERE Email = 'senjam@gmail.com';
SELECT  * from Customers
Select * from UserCredentials;


create TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(15),
    Address NVARCHAR(250),
    State NVARCHAR(50),
    Country NVARCHAR(50),
    Pincode NVARCHAR(10),
    PassportNumber NVARCHAR(50),
    GovtID NVARCHAR(100),
    IDType NVARCHAR(50),
    EditStatus NVARCHAR(20) DEFAULT 'None'
);

ALTER TABLE Users
ADD Today_Date DATE ;

ALTER TABLE Users
ADD DOB DATE;
ALTER TABLE Users
ADD username NVARCHAR(50);


ALTER TABLE Users
ADD PasswordHash NVARCHAR(200), -- hashed password
    Nationality NVARCHAR(50);
   
   INSERT INTO Users
(Name, DOB, Phone, Email, State, Country, Pincode, Address, PasswordHash, IDType, GovtID, PassportNumber, Nationality, Today_Date, username)
VALUES
('Test User', '1990-01-01', '1234567890', 'test@example.com', 'Test State', 'Test Country', '12345', 'Test Address', 'hashedpassword', 'Aadhaar', '123456789012', NULL, 'Indian', GETDATE(), 'testuser');
    ALTER TABLE Users
    ADD RequestID INT IDENTITY(1,1), 
    IDDocumentPath NVARCHAR(200);


drop TABLE UserUpdateRequests (
    RequestID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    FestivalID INT FOREIGN KEY REFERENCES Festivals(FestivalID),
    FullName NVARCHAR(100),
    FestivalName NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    RequestDate DATE,
    IDDocumentPath NVARCHAR(200),
     
);
ALTER TABLE UsersUpdateRequests
ADD UserID INT ;

CREATE TABLE UserUpdateRequestsByAdmin (
    RequestID INT PRIMARY KEY IDENTITY,
    UserID INT,
    NewName NVARCHAR(100),
    NewEmail NVARCHAR(100),
    NewPhone NVARCHAR(15),
    Message NVARCHAR(500),
    Status NVARCHAR(20), 
    RequestDate DATE
);

create TABLE Festivals (
    FestivalID INT PRIMARY KEY IDENTITY,
    FestivalName NVARCHAR(100),

    TicketCost DECIMAL(10,2),
    IsActive BIT DEFAULT 0,
    CONSTRAINT UQ_FestivalName UNIQUE(FestivalName),
    ApprovedBySuperAdmin BIT NOT NULL DEFAULT 0
);
ALTER TABLE Festivals
ADD Status NVARCHAR(20) DEFAULT 'Pending';

ALTER TABLE Festivals
ADD Venue NVARCHAR(200) NULL;


SELECT * FROM Festivals WHERE ApprovedBySuperAdmin = 1 AND IsActive = 1;

SELECT * FROM Festivals WHERE Status = 'Approved';


CREATE TABLE FestivalRequests (
    RequestID INT PRIMARY KEY IDENTITY,
    RequestType NVARCHAR(20), -- FestivalName or FestivalCost
    FestivalName NVARCHAR(100),
    RequestedCost DECIMAL(10,2) NULL,
    Message NVARCHAR(500),
    Status NVARCHAR(20), -- Awaiting, Approved, Rejected
    RequestDate DATE
);


create TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    FestivalID INT NOT NULL,
    Quantity INT DEFAULT 1,  -- always 1
    CostPerTicket DECIMAL(10,2) NOT NULL,
    Start_Date_Of_Booking DATE  NULL,
    End_Date_Of_Booking DATE NULL, -- only used if continuous booking is allowed
    
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (FestivalID) REFERENCES Festivals(FestivalID)
);
ALTER TABLE Bookings ADD FestivalName NVARCHAR(100);
ALTER TABLE Bookings
ADD BookingDate DATE
ALTER TABLE Bookings
ADD Venue NVARCHAR(200) NULL;

ALTER TABLE Bookings
ADD TicketNo NVARCHAR(50) NULL;
    SeatNo NVARCHAR(10) NULL;   
ALTER TABLE Bookings DROP COLUMN Venue;


DELETE FROM Bookings
WHERE TicketID = 15;

select * from Bookings;



ALTER TABLE Users
ADD RequestID UNIQUEIDENTIFIER DEFAULT NEWID();

ALTER TABLE Users
ADD RequestID UNIQUEIDENTIFIER NULL;

CREATE TABLE FestivalBookingDates (
    BookingDateID INT PRIMARY KEY IDENTITY,
    FestivalID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    AllowMultipleDays BIT NOT NULL DEFAULT 0, -- 0 = single day, 1 = continuous booking allowed
    BookingEnabled BIT NOT NULL DEFAULT 1,   -- Admin can disable booking completely
    FOREIGN KEY (FestivalID) REFERENCES Festivals(FestivalID)
);
INSERT INTO FestivalBookingDates(FestivalID, StartDate, EndDate)
VALUES (1, '2025-08-20', '2025-08-25');

ALTER TABLE FestivalBookingDates
ADD DisableSunday BIT NOT NULL DEFAULT 0;

SELECT f.TicketCost, fb.StartDate, fb.EndDate
FROM Festivals f
LEFT JOIN FestivalBookingDates fb ON f.FestivalID = fb.FestivalID
WHERE f.FestivalID =  1



create TABLE Ticket_Template (
    TicketID INT Null,         
    FestivalID INT UNIQUE NOT NULL,   -- Each festival can have only 1 ticket
    FestivalName NVARCHAR(100) NOT NULL, -- Editable later
    TicketPath NVARCHAR(200),         -- Ticket image path
    CONSTRAINT FK_TicketTemplate_Festivals FOREIGN KEY (FestivalID) 
        REFERENCES Festivals(FestivalID)
);


DELETE FROM Ticket_Template
WHERE TicketID = 15;


ALTER TABLE Ticket_Template
ADD IsUsed BIT NOT NULL DEFAULT 0;

SELECT TicketID, FestivalID, FestivalName 
FROM Ticket_Template;

CREATE TABLE Cart (
    CartId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,               -- link cart to logged-in user
    TicketNo NVARCHAR(50) NOT NULL,
    Festival NVARCHAR(100) NOT NULL,
    Venue NVARCHAR(200) NOT NULL,
    SeatNo NVARCHAR(50) NOT NULL,
    Traveler NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
ALTER TABLE Cart ADD BookingID INT IDENTITY(1,1) PRIMARY KEY;
ALTER TABLE Cart ADD FestivalName NVARCHAR(100);
ALTER TABLE Cart ADD BookingDate DATE;
ALTER TABLE Cart ADD TravellerName NVARCHAR(100);
ALTER TABLE Cart ADD BookingID INT NULL;
ALTER TABLE Cart
ADD CONSTRAINT FK_Cart_Booking
FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID);
ALTER TABLE Cart ADD TicketNo NVARCHAR(100);

DELETE FROM Bookings;


DBCC CHECKIDENT ('Bookings', RESEED, 0);


DBCC CHECKIDENT ('Bookings', RESEED, 0);