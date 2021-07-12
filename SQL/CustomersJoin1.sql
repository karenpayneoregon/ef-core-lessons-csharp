
/*
	
	PhoneTypeIdenitfier
	1 = home
	2 = cell
	3 = work

*/

DECLARE @PhoneType AS INT= 3;   -- Office
DECLARE @ContactType AS INT= 7; -- Owner
SELECT C.CustomerIdentifier, C.CompanyName, C.City, C.PostalCode, Contacts.ContactId, Countries.CountryIdentifier, Countries.Name AS Country, C.Phone AS [Customer Phone], Devices.PhoneTypeIdentifier, Devices.PhoneNumber AS [Contact Phone], C.ModifiedDate FROM Customers AS C INNER JOIN ContactType AS CT ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier INNER JOIN Countries ON C.CountryIdentifier = Countries.CountryIdentifier INNER JOIN Contacts ON C.ContactId = Contacts.ContactId INNER JOIN ContactDevices AS Devices ON Contacts.ContactId = Devices.ContactId WHERE Devices.PhoneTypeIdentifier = @PhoneType AND C.ContactTypeIdentifier = @ContactType;

SELECT        C.CustomerIdentifier, C.CompanyName, C.City, C.PostalCode, Contacts.ContactId, Countries.CountryIdentifier, Countries.Name AS Country, C.Phone AS [Customer Phone], Devices.PhoneTypeIdentifier, 
                         Devices.PhoneNumber AS [Contact Phone], C.ModifiedDate
FROM            Customers AS C INNER JOIN
                         ContactType AS CT ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier INNER JOIN
                         Countries ON C.CountryIdentifier = Countries.CountryIdentifier INNER JOIN
                         Contacts ON C.ContactId = Contacts.ContactId INNER JOIN
                         ContactDevices AS Devices ON Contacts.ContactId = Devices.ContactId
WHERE Devices.PhoneTypeIdentifier = @PhoneType
      AND C.ContactTypeIdentifier = @ContactType;






