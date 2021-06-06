# ScanEventService

## Assumptions made
* Since the instructions said to use .NET framework I decided to create a windows service using .NET framework 4.7.2. The windows service runs every 60 seconds and calls the API to get the latest parcel scan events.
* I assumed that the event ids are consecutive and after processing a batch of events, I get the max event id and save it to the database.
* I followed the instructions and created a table called ParcelScanEventHistory which stores the following information: EventId, ParcelId, ParcelType, CreatedDateTimeUtc, StatusCode and RunId. I added a new column called ScannedDateTimeUtc which logs the time the parcel was scanned into the system. 
* The instructions said the application should be resilient and should be able to handle new event types. I've created an enum which handles the three event types PICKUP, STATUS and DELIVERY. In the future, we will need to add more events to the windows service first via the enum otherwise it will fail to deserialise the JSON from the API if a new event type is introduced. 
* In terms of logging, I've created a file called log.log that will store all the logs. The file location for the log file is in the bin folder and the release type of the application (debug or release). 
* I've used the Entity Framework (EF) Code First approach to create the database schema and connect to a database that is hosted in SQL Server in local. The reason I've used EF is because it hides all the complexity of using SQL and doing all the setup of tables and columns. Also, querying for results can be easily done using LINQ.  
* I've used asyncs and awaits throughout the service to make it asynchoronous which means there is no chance for the main thread to hang when it's processing parcel events.  


## Improvements required to productionise the application
* A proper database schema. Currently, I've created two tables ParcelScanEventHistory and ParcelScanEvent. We need more tables such as Parcel (that holds parcel specific information), User (that holds information about the parcel recipient) and Device (which device is used to scan the parcel). 
* We need to setup a database for UAT and Prod. 
* We need to setup proper CI/CD pipelines and cloud infrasturcture. We need to build our production builds in TeamCity and we can deploy them using Octopus deploy to an AWS EC2 instance. 
* Parcel Scan API urls for UAT and Prod. This will help greatly in testing the service and making sure it works as expected. 

## Adding another worker to read from the same Scan Events API
* Adding in another worker can be quite risky because this greatly increases the chance of causing race conditions. My recommendation would be to not add another worker but to scale the existing worker vertically (by adding more CPUs, RAM, SSD) so it can handle more load from the API. 
* If we were going to add another worker, then we would need to add a locking mechanism to the tables that the worker will read and update. This can be done in SQL server using the hints TABLOCK and HOLDLOCK. The locks on the tables will be held until the transaction has either been aborted or committed. The other worker will wait until it gets access to the table and then lock it for its use.  

## Setup required to get the service started
* You need to install the windows service using [link](https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services)
* The event service should show in the services application in Windows OS. It's called **ScanEventService**.
* The API link has been set to http://localhost/v1/scans/scanevents in appsettings. This needs to be updated to a proper service that returns the correct JSON following the API contract otherwise this service will log a 404 error in log.log
* Futhermore, a local database called EventScanner was setup to develop this application. This needs to be updated to your local database otherwise this application will not be able to store event data. 
* Before running the application, the 'update-database' command needs to run to setup the database schema. This can be done using this [link](https://www.entityframeworktutorial.net/code-first/code-based-migration-in-code-first.aspx#:~:text=Execute%20the%20Update%2DDatabase%20command,know%20more%20about%20the%20command)