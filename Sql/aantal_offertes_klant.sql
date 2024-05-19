select k.naam, count(o.id) as 'aantal'
from offerte o
join klant k on k.id = o.klantnummer
group by k.naam;