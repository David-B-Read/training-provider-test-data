# Training Provider Test Data

#### Requirements

- Install [Visual Studio 2017 Community](https://www.visualstudio.com/downloads/) 
- Install [SQL Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

## Setup instructions

- Publish the database project **TrainingProviderTestData.Database.sqlproj** to your local SQL Server instance to create the database
- Download the latest version of the full copy of [Companies House data](http://download.companieshouse.gov.uk/en_output.html) and save as `src\TrainingProviderTestData.IntegrationTests\TestData\CompaniesHouse.csv`
- Download the latest version of [OpenCharities data](http://opencharities.org/charities.csv.zip) and save as `src\TrainingProviderTestData.IntegrationTests\TestData\charities.csv`
- Obtain the latest copy of the UKRLP Stakeholder Comprehensive Report and save as `src\TrainingProviderTestData.IntegrationTests\TestData\UKRLP.xlsx`
- Run the integration tests in project **TrainingProviderTestData.IntegrationTests.csproj** to populate the database with the test data

