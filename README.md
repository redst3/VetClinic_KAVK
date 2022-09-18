# VetClinic_KAVK

Sistemos paskirtis 

Projekto tikslas – Suteikti patogu aptarnavimą augintinių šeimininkams, kurie norėtu užregistruoti juos apžiūrai.

Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis darbuotojai ir vartotojai bei aplikacijų programavimo sąsaja (angl. trump. API). Darbuotojas, norėdamas naudotis šia platforma, gaus prisijungimus iš administratoriaus prie internetinės aplikacijos ir galės matyti savo srities užregistruotus vizitus, užpildyti informaciją apie specifinį augintinį, bei matyti savo specialybei skirtą informaciją. Administratorius registruos ir suteiks darbuotojams prisijungimus, matys darbuotojų veiklą.

Funkciniai reikalavimai

 Neregistruotas sistemos naudotojas galės: 
1.	Peržiūrėti platformos reprezentacinį puslapį
2.	Užregistruoti savo augintinį specifiniam vizitui.

Registruotas sistemos naudotojas gales:
1.	Atsijungti nuo internetinės aplikacijos
2.	Prisijungti prie platformos
3.	Peržiūrėti šios dienos vizitus
4.	Pasirinkus vizitą:
a.	Papildytį vizito kortelę su atliktomis procedūromis
b.	Išsaugoti ir pakeisti būseną į atliktą
5.	Matyti vizitų istoriją
Administratorius galės: 
1.	Suteikti darbuotojui prisijungimą
2.	Matyti visus vizitus
3.	Matyti specifinio darbuotojo vizitus
4.	Šalinti darbuotojus
5.	Patvirtinti vizitus
 
Sistemos architektūra

Sistemos sudedamosios dalys:
•	Kliento pusė (ang. Front-End) – naudojant Vue.js; 
•	Serverio pusė (angl. Back-End) – naudojant C# ASP.NET. Duomenų bazė – MySQL.

1 Pav. Pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas VetClinic API.

 
Pav. 1 Sistemos VetClinic diegimo diagrama

