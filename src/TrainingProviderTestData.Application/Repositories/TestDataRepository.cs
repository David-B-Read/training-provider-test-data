
namespace TrainingProviderTestData.Application.Repositories
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Configuration;
    using Dapper;
    using Interfaces;
    using Models;

    public class TestDataRepository : ITestDataRepository, IDisposable
    {
        private readonly IApplicationConfiguration _configuration;
        private readonly SqlConnection _connection;

        public TestDataRepository(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
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

            var recordCreated = await _connection.ExecuteAsync(sql);
        }
    }
}
