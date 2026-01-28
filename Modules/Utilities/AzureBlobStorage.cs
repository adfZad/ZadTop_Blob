using System;
using System.IO;
using System.Web;
using System.Configuration;
using Azure.Storage.Blobs; // Requires Azure.Storage.Blobs NuGet package
using ZadHolding.PortalBase;

namespace ZadHolding.Utilities
{
    public class AzureBlobStorage : IFileStorage
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public AzureBlobStorage()
        {
            _connectionString = System.Configuration.ConfigurationManager.AppSettings["AzureStorageConnectionString"];
            _containerName = System.Configuration.ConfigurationManager.AppSettings["AzureStorageContainerName"];
        }

        public string SaveFile(HttpPostedFile file, string directory, string fileName)
        {
            // directory format from FileManager is: fileType \ fileName(0,1) \ fileName(0,2) \
            // We can flatten this or keep it. Blob storage supports slashes in names to simulate folders.
            // Let's normalize backslashes to forward slashes.
            
            string blobPath = Path.Combine(directory, fileName).Replace("\\", "/");
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            // containerClient.CreateIfNotExists();
            // Allow public access? Or assumes SAS? 
            // For now, let's assume public container or we will just upload.
            // Usually we set access policy on the container.
            
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            
            using (var stream = file.InputStream)
            {
                blobClient.Upload(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        public void SaveFile(Stream stream, string directory, string fileName)
        {
            string blobPath = Path.Combine(directory, fileName).Replace("\\", "/");
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            // containerClient.CreateIfNotExists();
            
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            
            // Rewind stream if possible
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            
            blobClient.Upload(stream, true);
        }

        public string GetFileUrl(string directory, string fileName)
        {
            string blobPath = Path.Combine(directory, fileName).Replace("\\", "/");
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            
            return blobClient.Uri.ToString();
        }

        public bool FileExists(string directory, string fileName)
        {
            string blobPath = Path.Combine(directory, fileName).Replace("\\", "/");
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            
            return blobClient.Exists();
        }

        public Stream GetFileStream(string directory, string fileName)
        {
            string blobPath = Path.Combine(directory, fileName).Replace("\\", "/");
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            
            if (blobClient.Exists())
            {
                var downloadInfo = blobClient.Download();
                return downloadInfo.Value.Content;
            }
            return null;
        }
    }
}
