using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace jsreport.Core.Client
{
    /// <summary>
    /// Output of jsreport rendering process
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Stream with report
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Report content type like application/pdf
        /// </summary>
        public MediaTypeHeaderValue ContentType { get; set; }

        /// <summary>
        /// Report file extension like "html" or "pdf"
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// The full response
        /// </summary>
        public HttpResponseMessage Response { get; set; }
    }
}
