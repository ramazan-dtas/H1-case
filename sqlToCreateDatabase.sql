CREATE DATABASE SydvestBo

CREATE TABLE Areas
(
  "id"            int          NOT NULL IDENTITY(1,1),
  "name"          varchar(256),
  "consultant_id" int          NOT NULL,
  CONSTRAINT PK_Areas PRIMARY KEY (id)
)
GO

CREATE TABLE Consultants
(
  "id"   int          NOT NULL IDENTITY(1,1),
  "name" varchar(256) NOT NULL,
  CONSTRAINT PK_Consultants PRIMARY KEY (id)
)
GO

CREATE TABLE HouseOwners
(
  "id"   int          NOT NULL IDENTITY(1,1),
  "name" varchar(256) NOT NULL,
  CONSTRAINT PK_HouseOwners PRIMARY KEY (id)
)
GO

CREATE TABLE Houses
(
  "id"           int          NOT NULL IDENTITY(1,1),
  "name"         varchar(256) NOT NULL,
  "address"      varchar(256) NOT NULL,
  "area_id"      int          NOT NULL,
  "owner_id"     int          NOT NULL,
  "inspector_id" int          NOT NULL,
  "standard_id"  int          NOT NULL DEFAULT 1,
  CONSTRAINT PK_Houses PRIMARY KEY (id)
)
GO

CREATE TABLE Inspectors
(
  "id"          int          NOT NULL IDENTITY(1,1),
  "name"        varchar(256) NOT NULL,
  "hourly_rate" float        NOT NULL DEFAULT 10.00,
  CONSTRAINT PK_Inspectors PRIMARY KEY (id)
)
GO

CREATE TABLE Reservations
(
  "id"         int          NOT NULL IDENTITY(1,1),
  "start_date" date         NOT NULL,
  "end_date"   date         NOT NULL,
  "house_id"   int          NOT NULL,
  "guest_name" varchar(256) NOT NULL,
  CONSTRAINT PK_Reservations PRIMARY KEY (id)
)
GO

CREATE TABLE SeasonPrices
(
  "id"         int          NOT NULL IDENTITY(1,1),
  "name"       varchar(256),
  "multiplier" float        NOT NULL,
  CONSTRAINT PK_SeasonPrices PRIMARY KEY (id)
)
GO

CREATE TABLE Standards
(
  "id"    int          NOT NULL IDENTITY(1,1),
  "name"  varchar(256) NOT NULL,
  "price" float        NOT NULL,
  CONSTRAINT PK_Standards PRIMARY KEY (id)
)
GO

CREATE TABLE Weeks
(
  "id"        int NOT NULL IDENTITY(1,1),
  "season_id" int NOT NULL
)
GO

ALTER TABLE Reservations
  ADD CONSTRAINT FK_Houses_TO_Reservations
    FOREIGN KEY (house_id)
    REFERENCES Houses (id)
    ON DELETE CASCADE
GO

ALTER TABLE Houses
  ADD CONSTRAINT FK_HouseOwners_TO_Houses
    FOREIGN KEY (owner_id)
    REFERENCES HouseOwners (id)
    ON DELETE CASCADE
GO

ALTER TABLE Areas
  ADD CONSTRAINT FK_Consultants_TO_Areas
    FOREIGN KEY (consultant_id)
    REFERENCES Consultants (id)
    ON DELETE CASCADE
GO

ALTER TABLE Houses
  ADD CONSTRAINT FK_Areas_TO_Houses
    FOREIGN KEY (area_id)
    REFERENCES Areas (id)
    ON DELETE CASCADE
GO

ALTER TABLE Houses
  ADD CONSTRAINT FK_Inspectors_TO_Houses
    FOREIGN KEY (inspector_id)
    REFERENCES Inspectors (id)
    ON DELETE CASCADE
GO

ALTER TABLE Weeks
  ADD CONSTRAINT FK_SeasonPrices_TO_Weeks
    FOREIGN KEY (season_id)
    REFERENCES SeasonPrices (id)
    ON DELETE CASCADE
GO

ALTER TABLE Houses
  ADD CONSTRAINT FK_Standards_TO_Houses
    FOREIGN KEY (standard_id)
    REFERENCES Standards (id)
    ON DELETE CASCADE
GO