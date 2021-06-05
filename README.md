# ScanEventService

## Assumption made
* Since the instructions said to use .NET framework I decided to create a windows service using .NET framework 4.7.2. The windows service runs every 60 seconds and calls the API to get the latest parcel scan events.
* I assumed that the event ids are consecutive and after processing a batch of events, I get the max event id and save it to the database.
* I followed the instructions and created a table called ParcelScanEventHistory which stores the following information: EventId, ParcelId, ParcelType, CreatedDateTimeUtc, StatusCode and RunId. I added a new column called ScannedDateTimeUtc which logs the time the parcel was scanned into the system. 
* The instructions said the application should be resilient and should be able to handle new event types. I've created an enum which handles the three event types PICKUP, STATUS and DELIVERY. In the future, we will need to add more events to the windows service first via the enum otherwise it will fail to deserialise the JSON from the API if a new event type is introduced. 
* In terms of logging, I've created a file called log.log that will store all the logs. The file location for the log file is in the bin folder and the release type of the application (debug or release). 
* I've used the Entity Framework (EF) Code First approach to create the database schema and connects to a database that is hosted in SQL Server locally. The reason I've used EF is because it hides all the complexity of using SQL and doing all the setup of tables and columns and querying of data can be easily done through using C# LINQ. 
* I've used asyncs and awaits throughout the service to make it asynchoronous which means there is no chance for the main thread to hang when it's processing 
parcel events.  


## Improvements required to productionise the application
* A proper databsae schema. Currently, I've created two tables ParcelScanEventHistory and ParcelScanEvent. We need more tables such as Parcel (that holds parcel specific information), User (that holds information about the parcel recipient) and Device (which device is used to scan the parcel). 
* We need to setup a database for UAT and Prod. 
* We need to setup proper CI/CD pipelines and cloud infrasturcture. We need to build our production builds in TeamCity and we can deploy them using Octopus deploy to an AWS EC2 instance. 
