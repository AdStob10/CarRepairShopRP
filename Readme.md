# Car Repair Shop
This is a simple ASP.NET app for managing a small repair shop

## Accounts
There are 3 predefined users
mechanic@example.pl
admin@example.pl
client@example.pl

With password Test!@#123

## Repairs and Cars
Mechanic can create a "Repair" object which describe the "Car" and main problem.
Mechanic can specify a repair description and change the status during the process.
Every mechanic see repairs of all clients and can assigned himself to a specific repair.
Clients see only their repairs and they can't create or modify repairs. Mechanic is responsible for that.

## Replaced Parts
Every "Repair" can have 0 or more "Replaced Parts"
Mechanic can create/edit/delete replaced parts.
To every replaced part mechanic can add old part image and bill for new part.

## Invoices
Mechanic can issue invoice and "close" repair.
After that he can't modify repair properties or add new parts.

## Visits
Client can schedule a appointment to repair shop.
First he create a "Visit" with proposed date of appointment.
After that mechanic can accept date or propose another one.
If mechanic changed the client date, client can only accept new date or cancel visits.
