IF OBJECT_ID('SP_Map_Insert', 'P') IS NULL
BEGIN
    EXEC('
        CREATE PROCEDURE SP_Map_Insert(
            @Caption varchar(20) = NULL,
            @Description varchar(100) = NULL,
            @NEWID INT OUTPUT
        ) AS
            BEGIN

            IF @Caption IS NULL
            BEGIN
                RAISERROR (15600, -1, -1, ''SP_Map_Insert'');
            END

            SET NOCOUNT ON
            INSERT INTO tbl_Maps
            VALUES
            (
                @Caption,
                @Description
            )

            SET @NEWID = SCOPE_IDENTITY()
            END
    ')
END