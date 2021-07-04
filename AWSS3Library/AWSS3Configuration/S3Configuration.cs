using System;
using System.Collections.Generic;
using System.Text;

namespace AWSS3Library.AWSS3Configuration
{
    public class ServiceConfiguration
    {
        public AWSS3Configuration AWSS3 { get; set; }
    }
    public class AWSS3Configuration
    {
        public string BucketName { get; set; }
    }
}
