using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Byte.Library.Logging.Replay
{
    public class Replay
    {
        private static readonly TimeSpan timeout = new TimeSpan(0, 0, 10);
        private static readonly string userAgent = @"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";

        private Uri targetUri;
        private string logFilePath;
        private Func<LogEntry, bool> discriminator;
        private int fireCount;
        private int processedCount;
        private LineParser lineParser;
        private int totalRequestLimit;

        public delegate void ProcessedHandler(HttpStatusCode statusCode, TimeSpan elapsed, Uri uriProcessed, int processedCount);
        public event ProcessedHandler Processed;

        public Replay(
            string targetHost,
            int targetPort,
            string logPath,
            LineParser lineParser,
            int totalRequestLimit
            )
            : this(targetHost, targetPort, logPath, lineParser, totalRequestLimit, (e) => true) { }

        public Replay(
            string targetHost, 
            int targetPort,
            string logFilePath, 
            LineParser lineParser, 
            int totalRequestLimit, 
            Func<LogEntry, bool> discriminator
            )
        {
            var targetUriBuilder = new UriBuilder(Uri.UriSchemeHttp, targetHost, targetPort);
            this.targetUri = targetUriBuilder.Uri;

            this.logFilePath = logFilePath;
            this.lineParser = lineParser;
            this.discriminator = discriminator;
            this.fireCount = 0;
            this.processedCount = 0;
            this.totalRequestLimit = totalRequestLimit;
        }

        public void Run()
        {
            //TODO: check if file exists and is accessible and act accordingly
            var fileInfo = new FileInfo(this.logFilePath);
            this.RunFile(fileInfo);
        }

        private void RunFile(FileInfo fileInfo)
        {
            string path = fileInfo.FullName;

            foreach (var line in File.ReadLines(path))
            {
                if (this.fireCount >= this.totalRequestLimit)
                {
                    break;
                }

                try
                {
                    LogEntry logEntry = this.lineParser.GetLogEntry(line);
                    if (logEntry != null)
                    {
                        logEntry.Uri = this.RetargetUri(logEntry.Uri);
                        this.FireRequest(logEntry);
                        this.fireCount++;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to process line: {0}", line);
                }
            }
        }

        private Uri RetargetUri(Uri uri)
        {
            Uri retargetUri = new Uri(this.targetUri, uri);
            return retargetUri;
        }

        private void FireRequest(LogEntry entry)
        {
            if (discriminator(entry))
            {
                HttpWebRequest request = this.GenerateRequest(entry);

                Task.Factory.StartNew(
                    () =>
                    {
                        DateTime start = DateTime.Now;
                        HttpStatusCode statusCode = this.GetResponseStatus(request);
                        DateTime end = DateTime.Now;

                        TimeSpan elapsed = end - start;

                        this.processedCount++;

                        if (Processed != null)
                        {
                            Processed(statusCode, elapsed, entry.Uri, this.processedCount);
                        }
                    });
            }
        }

        private HttpWebRequest GenerateRequest(LogEntry entry)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(entry.Uri);

            request.Method = "GET";
            request.Timeout = (int)timeout.TotalMilliseconds;
            request.UserAgent = userAgent;

            return request;
        }

        private HttpStatusCode GetResponseStatus(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode;
                }
            }
            catch (Exception)
            {
                return HttpStatusCode.NotFound;
            }
        }
    }
}
