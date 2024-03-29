﻿
namespace TrainingProviderTestData.Application.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Configuration;
    using Dapper;
    using Interfaces;
    using Microsoft.Extensions.Logging;
    using Models;

    public class TestDataRepository : ITestDataRepository, IDisposable
    {
        private readonly IApplicationConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly ILogger<TestDataRepository> _logger;

        public TestDataRepository(IApplicationConfiguration configuration, ILogger<TestDataRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var connectionString = _configuration.ConnectionString;
            _connection = new SqlConnection(connectionString);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public async Task<bool> ImportCharityData(CharityDataEntry charityData)
        {
            string sql = $"INSERT INTO [dbo].[CharityCommissionData] " +
                         "([CharityName] " +
                         ",[CharityNumber] " +
                         ",[DateRegistered] " +
                         ",[DateRemoved]) " +
                         "VALUES " +
                         "(@charityName, @charityNumber, @dateRegistered, @dateRemoved)";

            var recordCreated = await _connection.ExecuteAsync(sql,
                new
                {
                    charityData.CharityName,
                    charityData.CharityNumber,
                    charityData.DateRegistered,
                    charityData.DateRemoved
                });
            var success = await Task.FromResult(recordCreated > 0);

            return await Task.FromResult(success);
        }

        public async Task DeleteCharityData()
        {
            string sql = "DELETE FROM [dbo].[CharityCommissionData] "; 

            var result = await _connection.ExecuteAsync(sql);
        }

        public async Task<bool> ImportCompaniesHouseData(CompaniesHouseDataEntry companyData)
        {
            string sql = $"INSERT INTO [dbo].[CompaniesHouseData] " +
                         "([CompanyName] " +
                         ",[CompanyNumber] " +
                         ",[IncorporationDate]" +
                         ",[DissolutionDate]" +
                         ",[CompanyCategory]" +
                         ",[CompanyStatus]) " +
                         "VALUES " +
                         "(@companyName, @companyNumber, @incorporationDate, @dissolutionDate, @companyCategory, @companyStatus)";

            if (!IsValidIncorporationDate(companyData.IncorporationDate))
            {
                _logger.LogWarning($"Invalid incorporation date for company {companyData.CompanyNumber} : {companyData.IncorporationDate}");
                companyData.IncorporationDate = null;
            }

            var recordCreated = await _connection.ExecuteAsync(sql,
                new
                {
                    companyData.CompanyName,
                    companyData.CompanyNumber,
                    companyData.IncorporationDate,
                    companyData.DissolutionDate,
                    companyData.CompanyCategory,
                    companyData.CompanyStatus
                });
            var success = await Task.FromResult(recordCreated > 0);

            return await Task.FromResult(success);
        }

        public async Task DeleteCompaniesHouseData()
        {
            string sql = "DELETE FROM [dbo].[CompaniesHouseData] ";

            var result = await _connection.ExecuteAsync(sql);
        }

        public async Task<bool> ImportUkrlpData(UkrlpDataEntry ukrlpData)
        {
            string sql = $"INSERT INTO [dbo].[UkrlpData] " +
                         "([UKPRN], " +
                         "[LegalName], " +
                         "[TradingName], " +
                         "[PrimaryVerificationSource], " +
                         "[CompanyNumber], " +
                         "[CharityNumber], " +
                         "[Status]) " +
                         "VALUES " +
                         "(@ukprn, @legalName, @tradingName, @primaryVerificationSource, @companyNumber, @charityNumber, @status)";

            var recordCreated = await _connection.ExecuteAsync(sql,
                new
                {
                    ukrlpData.UKPRN,
                    ukrlpData.LegalName,
                    ukrlpData.TradingName,
                    ukrlpData.PrimaryVerificationSource,
                    ukrlpData.CompanyNumber,
                    ukrlpData.CharityNumber,
                    ukrlpData.Status
                });
            var success = await Task.FromResult(recordCreated > 0);

            return await Task.FromResult(success);
        }

        public async Task DeleteUkrlpData()
        {
            string sql = "DELETE FROM [dbo].[UkrlpData] ";

            var result = await _connection.ExecuteAsync(sql);
        }

        public async Task<IEnumerable<string>> GetCompaniesListedInUkrlp()
        {
            string sql = $"SELECT [CompanyNumber] " +
                         "FROM [dbo].[UKRLPData] " +
                         "Where [Status] = 'Active' " +
                         "AND PrimaryVerificationSource = 'COMPANY'";

            return await _connection.QueryAsync<string>(sql);
        }

        public async Task UpdateCompanyOfficerData(string companyNumber, int directorsCount, int pscCount)
        {
            string sql = $"UPDATE [dbo].[CompaniesHouseData] " +
                         "SET DirectorsCount = @directorsCount, PSCsCount = @pscCount " +
                         "Where CompanyNumber = @companyNumber";

            var recordUpdated = await _connection.ExecuteAsync(sql,
                new
                {
                    directorsCount,
                    pscCount,
                    companyNumber
                });
        }

        private bool IsValidIncorporationDate(DateTime? incorporationDate)
        {
            if (!incorporationDate.HasValue)
            {
                return true;
            }

            if (incorporationDate.Value.Year < 1753 || incorporationDate.Value.Year > 9999)
            {
                return false;
            }

            return true;
        }
    }
}
