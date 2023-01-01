namespace Web.SqlRepository.Repositories;

public static class SqlQueries
{
    public static class Map
    {
        public const string INSERT = @"
            INSERT INTO tbl_Maps
            OUTPUT inserted.Id
            VALUES (@Caption, @Description)";

        public const string SELECT_WITH_CITIES = @"
            SELECT TOP (1) m.Id, m.Caption, m.Description FROM tbl_Maps as m
            SELECT c.Id, c.Caption, c.MapId FROM tbl_Cities as c
                WHERE c.MapId =
                (
                    SELECT TOP (1) maps.Id
                    FROM tbl_Maps as maps
                    ORDER BY maps.Id
                )";
    }

    public static class City
    {
        public const string INSERT = @"
            INSERT INTO tbl_Cities
            OUTPUT inserted.Id
            VALUES
            (
                @MapId,
                @Caption
            )";
    }
}