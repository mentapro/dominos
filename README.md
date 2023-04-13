# Domino's Pizza 🍕
Test assignment for Domino's interview

## How to run

`docker-compose up -d`

That command will create Postgres database, will wait until database is ready and then will run backend.

Backend will initialize database by itself: So it can take some time because there is a lot of data.

### Swagger will be available on:
`localhost:5004/swagger`

Swagger supports OpenAPI and endpoints are fully documented there.

## Infrastructure

Service is using PostgreSQL as a main database for storing vouchers. Database can be up via docker-compose file.

Service is responsible for creating database and applying migrations. There is two indexes on `name` column. Because column name is widely used by searches through endpoints. There is basic btree index and gin index. GIN index is needed because we use `LIKE` operator for searching part of string. Btree index is needed for full string comparison.

In the C# code I have minimized amount for conversions so the code is as efficient as possible. Also the code is nicely structured so there is different project with different responsibilities: repository for aggregates, query handlers for reporting, and domain aggregate as a core logic. API is designed with Minimal APIs, using FluentValidator for request validation.

## Endpoints
`/api/vouchers?limit=10&offset=0`

Returns list of vouchers.
Can have `name` as query parameter - for filtering by name (full string comparison). Also it supports pagination.

`/api/vouchers/{voucherId:guid}`

Returns one voucher by ID.

`/api/vouchers/cheapest-by-product?product_code=C100CD`

Returns one cheapest voucher by product code.

`/api/vouchers/autocomplete?name_search=Piz&limit=10&offset=0`

Returns autocomplete for vouchers: searching vouchers by substring of name. Also supports pagination.
