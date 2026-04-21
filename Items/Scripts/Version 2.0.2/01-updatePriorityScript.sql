USE DIP_MasterData
GO

WITH SortedCities AS
(
    SELECT 
        Id,
        ROW_NUMBER() OVER (
            ORDER BY 
                CASE 
                    WHEN Title COLLATE Persian_100_CI_AS LIKE N'%[آ-ی]%' THEN 1 
                    ELSE 2 
                END,
                
                Title COLLATE Persian_100_CI_AS
        ) AS NewPriority
    FROM [DIP_MasterData].[dbo].[Cities]
)
UPDATE C
SET C.Priority = S.NewPriority
FROM [DIP_MasterData].[dbo].[Cities] C
INNER JOIN SortedCities S ON C.Id = S.Id;


Go

WITH SortedProvinces AS
(
    SELECT 
        Id,
        ROW_NUMBER() OVER (
            ORDER BY 
                CASE 
                    WHEN Title COLLATE Persian_100_CI_AS LIKE N'%[آ-ی]%' THEN 1 
                    ELSE 2 
                END,
                
                Title COLLATE Persian_100_CI_AS
        ) AS NewPriority
    FROM [DIP_MasterData].[dbo].Provinces
)
UPDATE C
SET C.Priority = S.NewPriority
FROM [DIP_MasterData].[dbo].Provinces C
INNER JOIN SortedProvinces S ON C.Id = S.Id;