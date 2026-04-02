## All Service List

*   Catalog (Products)    
*   Basket (Carts)    
*   Discount (Coupons)
*   Order (Checkout)
   
# Docker Servers

*   MongoDB -> docker run -d -p 27017:27017 --name mongodb-server -e MONGO_INITDB_ROOT_USERNAME="admin" -e MONGO_INITDB_ROOT_PASSWORD="pass1234567" mongo
*   Redis -> docker run -d --name redis-server -p 6379:6379 redis
*   PostgreSql -> docker run --name discount-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=123456 -e POSTGRES_DB=DiscountDb -p 5432:5432 -d postgres
*   Mssql -> docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password100" -p 1433:1433 --name order-sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

# Catalog Service :

*   MediatR    
*   MongoDB    
*   Swagger
    

# Basket Service :

*   MediatR    
*   Redis    
*   Swagger
*   Grpc - (Client)
    

# Discount Service :

*   MediatR    
*   PostgreSql    
*   Dapper    
*   Grpc - (Server)


# Order Service :

*   MediatR
*   Mssql Server
*   Entity Framework Core
*   Fluent Validation (with a custom validator)
*   Swagger