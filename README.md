﻿# Layered Architecture

#### Notes are all currently in rough form, application is working but is a work in progress

Reference architecture similar to what we use in work and a base for future projects

You will need the "simple" database and the script to create this is in the OtherBits folder, the applications connection string is held in the userConnectionStrings.config file

The application is either hosted in IIS or hosted in OWIN via a Windows Service.  

The IIS version, when ran, shows a HTML page with a list of links to run the various 'get' api methods

![](./architecture diagram.png)

# The Layers
## Infrastructure

This is the actual database access, using Entity Framework as ORM.  This is also where the concrete implementations for the repositories reside

Should you decide on a different ORM or database system then this is the layer to be swapped out

## Domain

This is where the domain model lives that is mapped to the actual database by the infrastructure layer.  The interfaces for the repositories are held in this layer

## Application
This is where the application logic and data transfer object reside

The architecture uses DTO's for transfering data to the client rather than the entities themselves so that more lightweight objects can / are transferred over the wire

## Web Services
Hosted in IIS
Sets up the DI (using Autofac)

Holds the WebAPI which is just a very thin facade to the application layer

## WindowsService
A Windows Service that hosts the WebAPI using OWIN

The WebAPI is just a very thin facade to the application layer

This can be ran either as a console application or a Windows Service (see the readme for the WindowsService)
