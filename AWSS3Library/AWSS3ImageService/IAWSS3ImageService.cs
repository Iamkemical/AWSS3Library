using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AWSS3Library.AWSS3ImageHelper
{
    public interface IAWSS3ImageService
    {
        Task<bool> UploadImage(MemoryStream uploadFileName);
        Task<List<string>> ImagesList();
        Task<Stream> GetImages(string key);
        Task<bool> UpdateImage(MemoryStream uploadFileName, string key);
        Task<bool> DeleteImage(string key);
    }
}
