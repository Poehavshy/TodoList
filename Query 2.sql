SELECT C."Id", "ClientName"
FROM public."Clients" C 
LEFT JOIN public."ClientContacts" CC 
ON C."Id" = CC."ClientId" 
GROUP BY C."Id" 
HAVING COUNT(CC."Id") > 2
