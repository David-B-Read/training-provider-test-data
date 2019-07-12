# Training Provider Test Data

#### Requirements

- Install [Visual Studio 2017 Community](https://www.visualstudio.com/downloads/) or higher
- Install [SQL Server Express](https://www.microsoft.com/en-au/sql-server/sql-server-editions-express) or higher

## Setup instructions

- Create a new database called TrainingProviderData in your local SQL Server instance
- Publish the database project **TrainingProviderTestData.Database.sqlproj** to your local SQL Server instance to create the database structure and sample reports
- Download and un-zip the company data as one file from [Companies House data](http://download.companieshouse.gov.uk/en_output.html) and save as `src\TrainingProviderTestData.IntegrationTests\TestData\CompaniesHouse.csv`
- Download the latest version of [OpenCharities data](http://opencharities.org/charities.csv.zip) and save as `src\TrainingProviderTestData.IntegrationTests\TestData\charities.csv`
- Obtain the latest copy of the UKRLP Stakeholder Comprehensive Report and save as `src\TrainingProviderTestData.IntegrationTests\TestData\UKRLP.xlsx`
- Check that the connection strings in the NUnit tests in **TrainingProviderTestData.IntegrationTests.csproj** match your local configuration
- Run the integration tests in project **TrainingProviderTestData.IntegrationTests.csproj** to populate the database with the test data


