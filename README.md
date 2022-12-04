# VetClinic_KAVK

## Sistemos paskirtis 

Projekto tikslas – Sukurti patogia sistema šeimininkams užregistruojant savo augintinius veterinarinei apžiurai

Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis darbuotojai ir vartotojai bei aplikacijų programavimo sąsaja (angl. trump. API). Vartotojas prisiregistravės galės priregistruoti savo augintinį ir matyti jo vizitų ir procedurų istoriją. Darbuotojas, norėdamas naudotis šia platforma, gaus priskirtą rolę iš administratoriaus, kurios pagalba prisijungus prie internetinės aplikacijos galės matyti savo srities užregistruotus vizitus, užpildyti informaciją apie specifinį augintinį, bei matyti savo specialybei skirtą informaciją. Administratorius suteiks darbuotojams roles ir galės daryti viską ką galį darbuotojas ir prisijungės vartotojas.

## Funkciniai reikalavimai

### Neregistruotas sistemos naudotojas galės: 
1.	Peržiūrėti platformos reprezentacinį puslapį

### Registruotas sistemos naudotojas galės: 
1.	Peržiūrėti platformos reprezentacinį puslapį
2.	Užregistruoti savo augintinį ir peržiūrėtį vizitų, procedūrų istoriją.
3.	Atsijungti nuo internetinės aplikacijos
4.	Prisijungti prie platformos

### Registruotas  sistemos naudotojas gales:
1.	Atsijungti nuo internetinės aplikacijos
2.	Prisijungti prie platformos
3.	Peržiūrėti visus gyvūnus
4.	Pasirinkus vizitą:
a.	Papildytį vizito kortelę su atliktomis procedūromis
5.	Matyti vizitų istoriją
### Administratorius galės: 
1.	Suteikti darbuotojo rolę
2.	Matyti ir daryti viską ką gali vartotojai ir darbuotojai
4.	Šalinti darbuotojus
 
## Sistemos architektūra

Sistemos sudedamosios dalys:
•	Kliento pusė (ang. Front-End) – naudojant React JSX; 
•	Serverio pusė (angl. Back-End) – naudojant C# ASP.NET. Duomenų bazė – MySQL.

1 Pav. Pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas VetClinic API.

![image_2022-09-18_144727603](https://user-images.githubusercontent.com/78723344/190900494-8dfc03c5-ff0b-4525-830d-cdd63fa218f3.png)

<p align="center"> 1 Pav. Sistemos VetClinic diegimo diagrama </p>

