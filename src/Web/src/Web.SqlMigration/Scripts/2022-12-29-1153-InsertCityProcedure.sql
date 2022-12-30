IF OBJECT_ID('SP_City_Insert', 'P') IS NULL
BEGIN
    EXEC('
        CREATE PROCEDURE SP_City_Insert(
            @MapId INT = 1,
            @Caption varchar(20) = NULL,
            @NEWID INT OUTPUT
        ) AS
            BEGIN

            IF @MapId IS NULL
            BEGIN
                RAISERROR (15600, -1, -1, ''SP_City_Insert'');
            END

            IF @Caption IS NULL
            BEGIN
                RAISERROR (15600, -1, -1, ''SP_City_Insert'');
            END

            SET NOCOUNT ON
            INSERT INTO tbl_Cities
            VALUES
            (
                @MapId,
                @Caption
            )

            SET @NEWID = SCOPE_IDENTITY()
            END
    ')
END