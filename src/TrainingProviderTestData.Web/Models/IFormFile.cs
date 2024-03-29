﻿
namespace TrainingProviderTestData.Web.Models
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IFormFile
    {
        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        Stream OpenReadStream();
        void CopyTo(Stream target);
        Task CopyToAsync(Stream target, CancellationToken cancellationToken);
    }
}
