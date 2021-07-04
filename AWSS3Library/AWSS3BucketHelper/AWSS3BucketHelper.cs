using Amazon.S3;
using Amazon.S3.Model;
using AWSS3Library.AWSS3Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Library.AWSS3BucketHelper
{
    public class AWSS3BucketHelper : IAWSS3BucketHelper
    {
        private readonly IAmazonS3 s3Client;
        private readonly ServiceConfiguration settings;

        public AWSS3BucketHelper(IAmazonS3 s3Client, IOptions<ServiceConfiguration> settings)
        {
            this.s3Client = s3Client;
            this.settings = settings.Value;
        }
        public async Task<bool> DeleteImage(string key)
        {
            try
            {
                DeleteObjectResponse response = await s3Client.DeleteObjectAsync(settings.AWSS3.BucketName, key);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
                    return true;
                else
                    return false;
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        public async Task<Stream> GetImage(string key)
        {
            try
            {
                GetObjectResponse response = await s3Client.GetObjectAsync(settings.AWSS3.BucketName, key);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return response.ResponseStream;
                else
                    return null;
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        public async Task<ListVersionsResponse> ImagesList()
        {
            try
            {
                return await s3Client.ListVersionsAsync(settings.AWSS3.BucketName);
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        public async Task<bool> UploadImage(MemoryStream inputStream, string fileName)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = inputStream,
                    BucketName = settings.AWSS3.BucketName,
                    Key = fileName
                };
                PutObjectResponse response = await s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }
    }
}
