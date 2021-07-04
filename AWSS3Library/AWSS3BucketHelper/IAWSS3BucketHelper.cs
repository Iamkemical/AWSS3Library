using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Library.AWSS3BucketHelper
{
    public interface IAWSS3BucketHelper
    {
        Task<bool> UploadImage(MemoryStream inputStream, string fileName);
        Task<ListVersionsResponse> ImagesList();
        Task<Stream> GetImage(string key);
        Task<bool> DeleteImage(string key);
    }
}
