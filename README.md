# VetClinic_KAVK

## Sistemos paskirtis

Projekto tikslas – Sukurti patogia sistema šeimininkams užregistruojant savo augintinius veterinarinei apžiurai

Veikimo principas – pačią kuriamą platformą sudaro dvi dalys: internetinė aplikacija, kuria naudosis darbuotojai ir vartotojai bei aplikacijų programavimo sąsaja (angl. trump. API). Vartotojas prisiregistravės galės priregistruoti savo augintinį ir matyti jo vizitų ir procedurų istoriją. Darbuotojas, norėdamas naudotis šia platforma, gaus priskirtą rolę iš administratoriaus, kurios pagalba prisijungus prie internetinės aplikacijos galės matyti savo srities užregistruotus vizitus, užpildyti informaciją apie specifinį augintinį, bei matyti savo specialybei skirtą informaciją. Administratorius suteiks darbuotojams roles ir galės daryti viską ką galį darbuotojas ir prisijungės vartotojas.

## Funkciniai reikalavimai

### Neregistruotas sistemos naudotojas galės:

1. Peržiūrėti platformos reprezentacinį puslapį

### Registruotas sistemos naudotojas galės:

1. Peržiūrėti platformos reprezentacinį puslapį
2. Užregistruoti savo augintinį ir peržiūrėtį vizitų, procedūrų istoriją.
3. Atsijungti nuo internetinės aplikacijos
4. Prisijungti prie platformos

### Registruotas sistemos naudotojas gales:

1. Atsijungti nuo internetinės aplikacijos
2. Prisijungti prie platformos
3. Peržiūrėti visus gyvūnus
4. Pasirinkus vizitą:
   a. Papildytį vizito kortelę su atliktomis procedūromis
5. Matyti vizitų istoriją

### Administratorius galės:

1. Suteikti darbuotojo rolę
2. Matyti ir daryti viską ką gali vartotojai ir darbuotojai
3. Šalinti darbuotojus

## Sistemos architektūra

Sistemos sudedamosios dalys:

1. Kliento pusė (ang. Front-End) – naudojant React JSX;
2. Serverio pusė (angl. Back-End) – naudojant C# ASP.NET. Duomenų bazė – MySQL.

1 Pav. Pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTP protokolą. Šios sistemos veikimui (pvz., duomenų manipuliavimui su duomenų baze) yra reikalingas VetClinic API.

![image_2022-09-18_144727603](https://user-images.githubusercontent.com/78723344/190900494-8dfc03c5-ff0b-4525-830d-cdd63fa218f3.png)

<p align="center"> 1 Pav. Sistemos VetClinic diegimo diagrama </p>

# API specifikacija

## Naudotojų API metodai

### GET /users

Gražina sąrašą sistemos naudotojų, prieinamas tik administratoriams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/users`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |

#### Užklausos pavyzdys

`GET https://clinicapi.azurewebsites.net/api/users`

#### Atsakymo pavyzdys

```
[
    {
        "id": "string",
        "userName": "string",
        "email": "string"
    }
]
```

### GET /employees

Gražina sąrašą sistemos darbuotojų, prieinamas tik administratoriams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/employees`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |

#### Užklausos pavyzdys

`GET https://clinicapi.azurewebsites.net/api/employees`

#### Atsakymo pavyzdys

```
[
    {
        "id": "string",
        "userName": "string",
        "email": "string"
    }
]
```

### DELETE /remove

Ištrina naudotoją su nurodytu id per URL, funkcija prieinama tik administratoriams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/remove`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas | Pavyzdys                               |
| ----------- | ----------- | ------------ | -------------------------------------- |
| id          | Taip        | Naudotojo id | `0e7ccf9e-c58d-4019-81cd-d419470e511e` |

#### Užklausos pavyzdys

`DELETE https://clinicapi.azurewebsites.net/api/remove`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (204 No content)
```

### PUT /update

Pakeičia naudotojo su nurodytu id per URL rolę, funkcija prieinama tik administratoriams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/update`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas | Pavyzdys                               |
| ----------- | ----------- | ------------ | -------------------------------------- |
| id          | Taip        | Naudotojo id | `0e7ccf9e-c58d-4019-81cd-d419470e511e` |

#### Užklausos pavyzdys

`PUT https://clinicapi.azurewebsites.net/api/update`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (204 No content)
```

### POST /register

Sukuria naują naudotoją su nurodytais parametrais

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/register`

#### Atsakymų kodai

| Pavadinimas | Kodas |
| ----------- | ----- |
| No Content  | 201   |
| Bad request | 400   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas          | Pavyzdys         |
| ----------- | ----------- | --------------------- | ---------------- |
| username    | Taip        | Naudotojo vardas      | `redas`          |
| password    | Taip        | Naudotojo slaptažodis | `Slaptazodis1!`  |
| email       | Taip        | Naudotojo Paštas      | `redas@kavk.com` |

#### Užklausos pavyzdys

`POST https://clinicapi.azurewebsites.net/api/register`

```
{
  "password": "string",
  "userName": "string",
  "email": "string"
}
```

#### Atsakymo pavyzdys

```
{
  "id": "string",
  "username": "string",
  "email": "string",
}
```

### POST /login

Gražina naudotojo sugeneruotą žetoną, kuris vėliau yra naudojamas atpažinti naudotojo rolei

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/login`

#### Atsakymų kodai

| Pavadinimas | Kodas |
| ----------- | ----- |
| OK          | 200   |
| Bad request | 400   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas          | Pavyzdys        |
| ----------- | ----------- | --------------------- | --------------- |
| username    | Taip        | Naudotojo vardas      | `redas`         |
| password    | Taip        | Naudotojo slaptažodis | `Slaptazodis1!` |

#### Užklausos pavyzdys

`POST https://clinicapi.azurewebsites.net/api/login`

```
{
  "username": "string",
  "password": "string"
}
```

#### Atsakymo pavyzdys

```
{
    "accessToken" : "string"
}
```

## Gyvunų API metodai

### GET /animals

Gražina sąrašą visų gyvūnų

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals`, funkcija prieinama prisijungusiems naudotojams

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |

#### Užklausos pavyzdys

`GET https://clinicapi.azurewebsites.net/api/animals?pageNumber=1&pageSize=10`

#### Atsakymo pavyzdys

```
[
    {
        "id": 1,
        "name": "Fin",
        "type": "Dog",
        "breed": "Buldog",
        "age": 1,
        "userId": "3cb8a1ee-d5e7-4cf1-b985-aee1ce6ded0d"
    }
]
```

### GET /animals/{id}

Gražina gyvuną pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/api/animals/{id}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`GET  https://clinicapi.azurewebsites.net/api/animals/1`

#### Atsakymo pavyzdys

```
{
    "id": 1,
    "name": "Fin",
    "type": "Dog",
    "breed": "Buldog",
    "age": 1,
    "userId": "3cb8a1ee-d5e7-4cf1-b985-aee1ce6ded0d"
}
```

### POST /animals

Sukuria gyvuną nurodytais parametrais, funkcija prieinama tik vartotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/animals/`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas      | Pavyzdys             |
| ----------- | ----------- | ----------------- | -------------------- |
| age         | Taip        | Gyvūno amžius     | `5`                  |
| owner       | Taip        | Gyvūno savininkas | `redass`             |
| breed       | Taip        | Gyvūno rūšis      | `Whit`               |
| name        | Taip        | Gyvūno vardas     | `Jojo`               |
| type        | Taip        | Gyvūno tipas      | `owner`              |
| userId      | Taip        | Naudotojo ID      | `13a1a31-13a35-ad12` |

#### Užklausos pavyzdys

`POST  https://clinicapi.azurewebsites.net/api/animals/`

```
{
    "age": 4,
    "breed": "Whit",
    "name": "Jojo",
    "type": "Suo",
    "owner": "Anonymous"
    "userid": "13a1a31-13a35-ad12"
}
```

### PUT /animals/{id}

Atnaujiną gyvūno duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik vartotojams.

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas      | Pavyzdys             |
| ----------- | ----------- | ----------------- | -------------------- |
| age         | Taip        | Gyvūno amžius     | `5`                  |
| owner       | Taip        | Gyvūno savininkas | `redass`             |
| breed       | Taip        | Gyvūno rūšis      | `Whit`               |
| name        | Taip        | Gyvūno vardas     | `Jojo`               |
| type        | Taip        | Gyvūno tipas      | `owner`              |
| userId      | Taip        | Naudotojo ID      | `13a1a31-13a35-ad12` |

#### Užklausos pavyzdys

`PUT https://clinicapi.azurewebsites.net/api/animals/1`

```
{
    "age": 4,
    "breed": "Whit",
    "name": "Jojo",
    "type": "Suo",
    "owner": "Anonymous"
    "userid": "13a1a31-13a35-ad12"
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /animals/{id}

Ištrina gyvūną su nurodytu id per URL, funkcija prieinama tik vartotojams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas | Pavyzdys |
| ----------- | ----------- | ------------ | -------- |
| id          | Taip        | Gyvūno id    | `5`      |

#### Užklausos pavyzdys

`DELETE https://clinicapi.azurewebsites.net/api/animals/1`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```

## Visitu API metodai

### GET /animals/{id}/visits

Gražina sąrašą visų specifinio gyvūno visitų

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits`, funkcija prieinama prisijungusiems naudotojams

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |

#### Užklausos pavyzdys

`GET https://clinicapi.azurewebsites.net/api/animals/1/visits`

#### Atsakymo pavyzdys

```
[
    {
        "id": 1,
        "description": "2020/12/05 - Health",
        "isFinished": true,
        "animalId": 1
    }
]
```

### GET /animals/{id}/visits/{visitId}

Gražina gyvuno visitą pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/api/animals/{id}/visits/{visitId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`GET  https://clinicapi.azurewebsites.net/api/animals/1`

#### Atsakymo pavyzdys

```
    {
        "id": 1,
        "description": "2020/12/05 - Health",
        "isFinished": true,
        "animalId": 1
    }
```

### POST /animals/{id}/visits/

Sukuria gyvuno visitą nurodytais parametrais, funkcija prieinama tik darbuotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/animals/{id}/visits`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas   | Pavyzdys |
| ----------- | ----------- | -------------- | -------- |
| description | Taip        | Visito aprašas | `Health` |

#### Užklausos pavyzdys

`POST  https://clinicapi.azurewebsites.net/api/animals/{id}/visits`

```
{

    "description": "string"
}
```

### PUT /animals/{id}/visits/{visitId}

Atnaujiną visito duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik darbuotojams.

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas   | Pavyzdys |
| ----------- | ----------- | -------------- | -------- |
| description | Taip        | Visito aprašas | `Health` |

#### Užklausos pavyzdys

`PUT https://clinicapi.azurewebsites.net/api/animals/1/visits/1`

```
{
    "description": "string"
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /animals/{id}/visits/{visitId}

Ištrina visitą su nurodytu id per URL, funkcija prieinama tik darbuotojams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`DELETE https://clinicapi.azurewebsites.net/api/animals/1/visits/1`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```

## Procedūrų API metodai

### GET /animals/{id}/visits/{visitId}/procedures

Gražina sąrašą visų specifinio visito proceduras

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}/procedures`, funkcija prieinama prisijungusiems naudotojams

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |

#### Užklausos pavyzdys

`GET  https://clinicapi.azurewebsites.net/api/animals/1/visits/1/procedures`

#### Atsakymo pavyzdys

```
[
    {
        "id": 1,
        "name": "Teeth",
        "description": "Teeth whitening",
        "cost": 45.00,
        "visitId": 1,
        "animalId": 1
    }
]
```

### GET /animals/{id}/visits/{visitId}/procedures/{procedureId}

Gražina visito procedurą pagal id, kuris perduodamas per URL, funkcija prieinama prisijungusiems naudotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}/procedures/{procedureId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`GET  https://clinicapi.azurewebsites.net/api/animals/1/procedures/1`

#### Atsakymo pavyzdys

```
    {
        "id": 1,
        "name": "Teeth",
        "description": "Teeth whitening",
        "cost": 45.00,
        "visitId": 1,
        "animalId": 1
    }
```

### POST /animals/{id}/visits/{visitId}/procedures

Sukuria visito procedūrą nurodytais parametrais, funkcija prieinama tik darbuotojams

#### Metodo URL

` https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}/procedures`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys          |
| ----------- | ----------- | ---------------------- | ----------------- |
| name        | Taip        | Procedūros pavadinimas | `Teeth`           |
| description | Taip        | Procedūros aprašas     | `Teeth whitening` |
| cost        | Taip        | Procedūros kaina       | `55`              |

#### Užklausos pavyzdys

`POST   https://clinicapi.azurewebsites.net/api/animals/3/visits/2/procedures`

```
{
    "name": "string",
    "description": "string",
    "cost": int
}
```

### PUT /animals/{id}/visits/{visitId}/procedures/{procedureId}

Atnaujiną procedūros duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik darbuotojams.

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}/procedures/{procedureId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys          |
| ----------- | ----------- | ---------------------- | ----------------- |
| name        | Taip        | Procedūros pavadinimas | `Teeth`           |
| description | Taip        | Procedūros aprašas     | `Teeth whitening` |
| cost        | Taip        | Procedūros kaina       | `55`              |

#### Užklausos pavyzdys

`PUT https://clinicapi.azurewebsites.net/api/animals/1/visits/1`

```
{
    "name": "string",
    "description": "string",
    "cost": 11.51
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /animals/{id}/visits/{visitId}/procedures/{procedureId}

Ištrina procedūrą su nurodytu id per URL, funkcija prieinama tik darbuotojams

#### Metodo URL

`https://clinicapi.azurewebsites.net/api/animals/{id}/visits/{visitId}/procedures/{procedureId}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`DELETE  https://clinicapi.azurewebsites.net/api/animals/3/visits/2/procedures/3`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```
