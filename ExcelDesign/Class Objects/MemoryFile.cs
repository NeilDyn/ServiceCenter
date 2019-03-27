using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZendeskApi.Contracts.Models;

namespace ExcelDesign.Class_Objects
{
    public class MemoryFile : HttpPostedFileBase, IHttpPostedFile, IDisposable
    {
        readonly Stream stream;
        readonly string contentType;
        readonly string fileName;

        public MemoryFile(Stream streamP, string contentTypeP, string fileNameP)
        {
            stream = streamP;
            contentType = contentTypeP;
            fileName = fileNameP;
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }

        public override string FileName
        {
            get { return fileName; }
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}