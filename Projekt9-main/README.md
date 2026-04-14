# Projekt9

This project is a simple ASP.NET Core Web API written in C#.

It was made as a warehouse task.  
The API adds a product to a warehouse based on data sent in the request.

The project includes:
- a basic API endpoint
- request and response models
- SQL Server database operations
- a version that uses a stored procedure

## What it does

The main endpoint checks if:
- the product exists
- the warehouse exists
- there is a matching order
- the order was not completed before

If everything is correct, the order is marked as completed and a new record is added to `Product_Warehouse`.

There is also a second endpoint that does the same operation using a stored procedure.

## Technologies

- C#
- ASP.NET Core Web API
- .NET 9
- SQL Server

## Endpoints

### POST `/api/warehouse`

Adds a product to a warehouse using SQL commands in the controller.

### POST `/api/warehouse/procedure`

Adds a product to a warehouse using a stored procedure.

## Example request body

```json
{
  "idProduct": 1,
  "idWarehouse": 1,
  "amount": 10,
  "createdAt": "2025-01-01T10:00:00"
}
