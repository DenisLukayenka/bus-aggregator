IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE' AND ROUTINE_NAME="SP_Country_Insert")
BEGIN
    CREATE PROCEDURE [dbo].[SP_Country_Insert]
    (
        @Id INT NOT NULL,
        @Caption varchar(20) NOT NULL,
        @Description varchar(100) NULL
    )
    AS
    INSERT INTO tbl_Countries
    VALUES
    (
        @Id,
        @Caption,
        @Description
    )
    GO;
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE' AND ROUTINE_NAME="SP_City_Insert")
BEGIN
    CREATE PROCEDURE [dbo].[SP_City_Insert]
    (
        @Id INT NOT NULL,
        @CountryId INT NOT NULL,
        @Caption varchar(20) NOT NULL
    )
    AS
    INSERT INTO tbl_Cities
    VALUES
    (
        @Id,
        @CountryId,
        @Caption
    )
    GO;
END