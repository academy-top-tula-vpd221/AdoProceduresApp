DECLARE @priceMin DECIMAL(9,2)
DECLARE @priceMax DECIMAL(9,2)
DECLARE @priceAvg DECIMAL(9,2)

EXEC PriceRange @priceMin OUT, @priceMax OUT, @priceAvg OUT

SELECT @priceMin, @priceMax, @priceAvg