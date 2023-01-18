# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)
  
## Auth

### Register

``` js
POST {{host}}/auth/register
```

#### Register Request

```js
{
    "firstName":"Amichai",
    "lastName":"Mantinband",
    "email":"amichai@mantinband.com",
    "password":"Amiko1232!"
}
```

#### Register Response

```js
200 OK
```

```js
    "id":"00000000-0000-0000-0000-000000000000",
    "firstName":"Amichai",
    "lastName":"Mantinband",
    "email":"amichai@mantinband.com",
    "token":""
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
    "email":"amichai@mantinband.com",
    "password":"Amiko1232!"
}
```

#### Login Response

```json
{
    "id":"00000000-0000-0000-0000-000000000000",
    "firstName":"Amichai",
    "lastName":"Mantinband",
    "email":"amichai@mantinband.com",
    "token":""
}
```
