DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_SearchProducts`(field1 varchar(20), cond1 varchar(5), 
field2 varchar(20), cond2 varchar(5), 
field3 int, cond3 varchar(5), 
field4 varchar(20), cond4 varchar(5), 
field5 varchar(20))
BEGIN
	SET @finalQ = 'SELECT * FROM Products WHERE 1=1 ';
    
    IF field1 IS NOT NULL AND field1 <> '' THEN
		SET @finalQ = CONCAT(@finalQ, 'AND ProductName = "',field1,'" ');
	END IF;
    
    IF field2 IS NOT NULL AND field2 <> '' AND cond1 IS NOT NULL THEN
		SET @finalQ = CONCAT(@finalQ, cond1,' Size = "',field2,'" ');
	END IF;
    
    IF field3 IS NOT NULL AND field3 <> -1 AND cond2 IS NOT NULL THEN
		SET @finalQ = CONCAT(@finalQ, cond2,' Price = ',field3,' ');
	END IF;
    
    IF field4 IS NOT NULL AND field4 <> '' AND cond3 IS NOT NULL THEN
		SET @finalQ = CONCAT(@finalQ, cond3,' MfgDate = "',field4,'" ');
	END IF;
    
    IF field5 IS NOT NULL AND field5 <> '' AND cond4 IS NOT NULL THEN
		SET @finalQ = CONCAT(@finalQ, cond4,' Category = "',field5,'" ');
	END IF;
    
    PREPARE stmt FROM @finalQ;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
END$$
DELIMITER ;
