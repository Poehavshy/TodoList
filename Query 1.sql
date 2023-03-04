SELECT "ClientName", COUNT(cc."Id") 
FROM public."Clients" C 
LEFT JOIN public."ClientContacts" CC 
ON C."Id" = CC."ClientId" 
GROUP BY C."Id" 
ORDER BY COUNT(CC."Id") DESC
