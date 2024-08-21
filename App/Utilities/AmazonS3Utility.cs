using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.App.Utilities
{
    public interface IAmazonS3Utility
    {
        Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(IFormFile formFile, string bucketName, string filePath, S3CannedACL s3CannedACL);
        Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(string filePathSource, string bucketName, string filePath, S3CannedACL s3CannedACL, string format = null);
        string GetResourceUrl(string bucketName, string filePath, DateTime expiredTime);
        string GetPublicUrl(string publicDomain, string bucketName, string filePath);
        string GetRelativeUrl(string fullPath);
        bool IsS3Path(string path);
        Task<(bool Result, string ErrorMessage)> SaveFolderAmazonS3Async(string folderPath, string bucketName, S3CannedACL s3CannedACL, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, string keyPrefix = null);
        Task<(bool result, string errorMessage)> DeleteResourceAsync(string bucketName, string fileName);
        Task<(bool result, string errorMessage)> DeleteMultipleResourceAsync(string bucketName, List<string> fileNames);
        Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(IFormFile fromFile, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath);
        Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(MemoryStream memoryStream, string bucketName, string filePath, S3CannedACL s3CannedACL);
        Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(MemoryStream memoryStream, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath);
        Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(string filePathSource, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath);
    }
    public class AmazonS3Utility : IAmazonS3Utility
    {
        private readonly IConfiguration Configuration;
        private readonly IAmazonS3 S3Client;
        public AmazonS3Utility(IConfiguration configuration)
        {
            Configuration = configuration;
            AmazonS3Config amazonS3Config = new()
            {
                ServiceURL = Configuration["AmazonS3:ServiceURL"],
                ForcePathStyle = true
            };
            S3Client = new AmazonS3Client(Configuration["AmazonS3:AccessKeyId"], Configuration["AmazonS3:SecretAccessKey"], amazonS3Config);
        }
        public async Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(IFormFile formFile, string bucketName, string filePath, S3CannedACL s3CannedACL)
        {

            try
            {
                using (MemoryStream memoryStream = new())
                {
                    await formFile.CopyToAsync(memoryStream);
                    TransferUtilityUploadRequest transferUtilityUploadRequest = new()
                    {
                        InputStream = memoryStream,
                        BucketName = bucketName,
                        Key = filePath,
                        CannedACL = s3CannedACL
                    };
                    TransferUtility transferUtility = new(S3Client);
                    await transferUtility.UploadAsync(transferUtilityUploadRequest);
                    if (s3CannedACL == S3CannedACL.PublicRead || s3CannedACL == S3CannedACL.PublicReadWrite)
                    {
                        return (GetPublicUrl(Configuration["AmazonS3:PublicDomain"], bucketName, filePath), null);
                    }
                    return (null, null);
                };
            }
           catch (AmazonS3Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }

        }
        public async Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(MemoryStream memoryStream, string bucketName, string filePath, S3CannedACL s3CannedACL)
        {
            try
            {
                TransferUtilityUploadRequest transferUtilityUploadRequest = new()
                {
                    InputStream = memoryStream,
                    BucketName = bucketName,
                    Key = filePath,
                    CannedACL = s3CannedACL
                };
                TransferUtility transferUtility = new(S3Client);
                await transferUtility.UploadAsync(transferUtilityUploadRequest);
                if (s3CannedACL == S3CannedACL.PublicRead || s3CannedACL == S3CannedACL.PublicReadWrite)
                {
                    return (GetPublicUrl(Configuration["AmazonS3:PublicDomain"], bucketName, filePath), null);
                }
                return (null, null);
            }
            catch (AmazonS3Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
            finally
            {
                memoryStream.Close();
            }
        }

        public async Task<(string PublicResourceUrl, string ErrorMessage)> SaveFileAmazonS3Async(string filePathSource, string bucketName, string filePath, S3CannedACL s3CannedACL, string formart = null)
        {
            try
            {
                TransferUtilityUploadRequest transferUtilityUploadRequest = new()
                {
                    FilePath = filePathSource,
                    BucketName = bucketName,
                    Key = filePath,
                    CannedACL = s3CannedACL,
                };
                if(formart != null && formart.Equals("text"))
                {
                    transferUtilityUploadRequest.ContentType = "text/plain; charset=UTF-8";
                }
                TransferUtility transferUtility = new(S3Client);
                await transferUtility.UploadAsync(transferUtilityUploadRequest);
                if (!await IsS3FileExists(bucketName, filePath, null))
                    return (null, "upload file false");
                if (s3CannedACL == S3CannedACL.PublicRead || s3CannedACL == S3CannedACL.PublicReadWrite)
                {
                    return (GetPublicUrl(Configuration["AmazonS3:PublicDomain"], bucketName, filePath), null);
                }
                return (null, null);
            }
            catch (AmazonS3Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
        }

        public async Task<(bool Result, string ErrorMessage)> SaveFolderAmazonS3Async(string folderPath, string bucketName, S3CannedACL s3CannedACL, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, string keyPrefix = null)
        {
            try
            {
                TransferUtilityUploadDirectoryRequest transferUtilityUploadDirectoryRequest = new()
                {

                    Directory = folderPath,
                    BucketName = bucketName,
                    CannedACL = s3CannedACL,
                    SearchPattern = searchPattern,
                    SearchOption = searchOption,
                    KeyPrefix = keyPrefix

                };
                TransferUtility transferUtility = new(S3Client);
                await transferUtility.UploadDirectoryAsync(transferUtilityUploadDirectoryRequest);
                return (true, null);
            }
            catch (AmazonS3Exception ex)
            {
                return (false, ex.GetBaseException().ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.GetBaseException().ToString());
            }
        }
        public async Task<bool> IsS3FileExists(string bucketName, string fileName, string versionId)
        {
            try
            {
                var request = new GetObjectMetadataRequest()
                {
                    BucketName = bucketName,
                    Key = fileName,
                    VersionId = !string.IsNullOrEmpty(versionId) ? versionId : null
                };

                var response = await S3Client.GetObjectMetadataAsync(request);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public string GetResourceUrl(string bucketName, string filePath, DateTime expiredTime)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            if (filePath.StartsWith("http"))
            {
                return filePath;
            }
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = filePath,
                Expires = expiredTime,
                Protocol = Protocol.HTTP
            };
            return S3Client.GetPreSignedURL(urlRequest).Replace(Configuration["AmazonS3:ServiceURL"].ToString(), Configuration["AmazonS3:PublicDomain"].ToString());
        }
        // https://[REGION].amazonaws.com/[BUCKET-NAME]/[FILE-NAME].[FILE-TYPE] or https://s3.amazonaws.com/[BUCKET-NAME]/[FILE-NAME].[FILE-TYPE]
        public string GetPublicUrl(string publicDomain, string bucketName, string filePath)
        {
            return $"{publicDomain}/{bucketName}/{filePath}";
        }

        public string GetRelativeUrl(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) return null;
            int paramIndex = fullPath.LastIndexOf("?");
            if (paramIndex != -1)
            {
                return fullPath.Substring(0, paramIndex).Replace($"{Configuration["AmazonS3:PublicDomain"]}/{Configuration["AmazonS3:BucketName"]}/", "");
            }
            return fullPath.Replace($"{Configuration["AmazonS3:PublicDomain"]}/{Configuration["AmazonS3:BucketName"]}/", "");
        }

        public bool IsS3Path(string path)
        {
            return path.Contains(Configuration["AmazonS3:PublicDomain"]);
        }

        public async Task<(bool result, string errorMessage)> DeleteResourceAsync(string bucketName, string filePath)
        {
            try
            {
                DeleteObjectRequest objectDeleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = filePath
                };
                DeleteObjectResponse response = await S3Client.DeleteObjectAsync(objectDeleteRequest);
                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, ex.GetBaseException().ToString());
            }
        }
        public async Task<(bool result, string errorMessage)> DeleteMultipleResourceAsync(string bucketName, List<string> filePath)
        {
            try
            {
                List<KeyVersion> keyVersions = await PutObjectsAsync(bucketName, filePath);
                DeleteObjectsRequest multiObjectDeleteRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = keyVersions
                };
                DeleteObjectsResponse response = await S3Client.DeleteObjectsAsync(multiObjectDeleteRequest);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.GetBaseException().ToString());
            }
        }
        private async Task<List<KeyVersion>> PutObjectsAsync(string bucketName, List<string> keys)
        {
            List<KeyVersion> keyVersions = new();
            foreach (string key in keys)
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                };
                PutObjectResponse response = await S3Client.PutObjectAsync(request);
                KeyVersion keyVersion = new KeyVersion
                {
                    Key = key
                };
                keyVersions.Add(keyVersion);
            }
            return keyVersions;
        }
        public async Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(IFormFile fromFile, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath)
        {
            try
            {
                if(!string.IsNullOrEmpty(filePath))
                {
                    (bool result, string errorMessage) = await DeleteResourceAsync(bucketName, filePath);
                    if (!result)
                    {
                        return (null, errorMessage);
                    }
                }    

                (string PublicResourceUrl, string ErrorMessage) = await SaveFileAmazonS3Async(fromFile, bucketName, newFilePath, s3CannedACL);
                if (PublicResourceUrl is null)
                {
                    return (null, ErrorMessage);
                }
                return (PublicResourceUrl, null);
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
        }
        public async Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(string filePathSource, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    (bool result, string errorMessage) = await DeleteResourceAsync(bucketName, filePath);
                    if (!result)
                    {
                        return (null, errorMessage);
                    }
                }
                (string PublicResourceUrl, string ErrorMessage) = await SaveFileAmazonS3Async(filePathSource, bucketName, newFilePath, s3CannedACL);
                if (PublicResourceUrl is null)
                {
                    return (null, ErrorMessage);
                }
                return (PublicResourceUrl, null);
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
        }
        public async Task<(string resourceUrl, string errorMessage)> UpdateResourceAsync(MemoryStream memoryStream, string bucketName, S3CannedACL s3CannedACL, string filePath, string newFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    (bool result, string errorMessage) = await DeleteResourceAsync(bucketName, filePath);
                    if (!result)
                    {
                        return (null, errorMessage);
                    }
                }
                (string PublicResourceUrl, string ErrorMessage) = await SaveFileAmazonS3Async(memoryStream, bucketName, newFilePath, s3CannedACL);
                if (PublicResourceUrl is null)
                {
                    return (null, ErrorMessage);
                }
                return (PublicResourceUrl, null);
            }
            catch (Exception ex)
            {
                return (null, ex.GetBaseException().ToString());
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }

}
