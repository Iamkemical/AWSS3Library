using Amazon.S3;
using Amazon.S3.Model;
using AWSS3Library.AWSS3BucketHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Library.AWSS3ImageHelper
{
    public class AWSS3ImageService : IAWSS3ImageService
    {
        private readonly IAWSS3BucketHelper s3BucketHelper;

        /// <summary>
        /// Dependencies injected into the AWSS3ImageService
        /// </summary>
        /// <param name="s3BucketHelper"></param>
        /// <param name="dbContext"></param>
        public AWSS3ImageService(IAWSS3BucketHelper s3BucketHelper)
        {
            this.s3BucketHelper = s3BucketHelper;
        }

        /// <summary>
        /// DeleteImage - Deletes Image from AWS S3 Bucket
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteImage(string key)
        {
            try
            {
                // Deletes image based on key (file name)
                return await s3BucketHelper.DeleteImage(key);
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        /// <summary>
        /// ImagesList - Gets a list of images in the S3 Bucket
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> ImagesList()
        {
            try
            {
                // Gets the ListVersion metadata for the images
                ListVersionsResponse listVersions = await s3BucketHelper.ImagesList();
                return listVersions.Versions.Select(c => c.Key).ToList();
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        /// <summary>
        /// GetImages - Gets image from S3 based on key (file name) passed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Stream> GetImages(string key)
        {
            try
            {
                Stream fileStream = await s3BucketHelper.GetImage(key);
                if (fileStream == null)
                {
                    Exception ex = new Exception("Image Not Found");
                    throw ex;
                }
                else
                {
                    return fileStream;
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }

        /// <summary>
        /// UpdateImage - Updates Image on S3
        /// </summary>
        /// <param name="uploadFileName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> UpdateImage(MemoryStream uploadFileName, string key)
        {
            try
            {
                return await s3BucketHelper.UploadImage(uploadFileName, key);
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }
        /// <summary>
        /// UploadImage - Uploads images to S3 bucket
        /// </summary>
        /// <param name="uploadFileName"></param>
        /// <param name="existingUser"></param>
        /// <returns></returns>
        public async Task<bool> UploadImage(MemoryStream uploadFileName)
        {
            try
            {
                string imageName = string.Empty;
                imageName = Guid.NewGuid().ToString();

                // Upload images to S3 bucket
                return await s3BucketHelper.UploadImage(uploadFileName, imageName);
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex);
            }
        }
    }
}
