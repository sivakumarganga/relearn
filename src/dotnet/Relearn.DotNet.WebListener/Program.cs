using System.Net;
using System.Linq;

namespace Relearn.DotNet.WebListener
{

    class Program
    {

        static void Main(string[] args)
        {
            bool shouldExit = false;
            using (var shouldExitWaitHandle = new ManualResetEvent(shouldExit))
            using (var listener = new HttpListener())
            {
                /* Handling the Console Cancel Key*/
                Console.CancelKeyPress += (sender, e) =>
                {
                    e.Cancel = true;
                    shouldExit = true;
                    shouldExitWaitHandle.Set();
                };
                int port = 8096;
                listener.Prefixes.Add($"http://*:{port}/");

                listener.Start();
                Console.WriteLine($"Server listening at port {port}");

                /*
                This is the loop where everything happens, we loop until an
                exit is requested
                 */

                while (!shouldExit)
                {
                    /*
                    Every request to the http server will result in a new
                    HttpContext
                     */
                    var contextAsyncResult = listener.BeginGetContext((IAsyncResult asyncResult) =>
                        {
                            // Get the context from request
                            var context = listener.EndGetContext(asyncResult);
                            // Print the request url on server console
                            Console.WriteLine("----------------------START------------------------");
                            Console.WriteLine("Request received");
                            Console.WriteLine("Raw Url:{0}", context.Request.RawUrl);
                            Console.WriteLine("HttpMethod:{0}", context.Request.HttpMethod);
                            Console.WriteLine("ProtocolVersion:{0}", context.Request.ProtocolVersion);
                            Console.WriteLine("ContentType:{0}", context.Request.ContentType);

                            string requestBody = string.Empty;
                            if (context.Request.HttpMethod == "POST")
                            {
                                using (var reader = new StreamReader(context.Request.InputStream))
                                {
                                    requestBody = reader.ReadToEnd();
                                    Console.WriteLine(requestBody);
                                }
                            }

                            /*
                            Use s StreamWriter to write text to the response
                            stream
                             */
                            using (var writer =
                                new StreamWriter(context.Response.OutputStream)
                            )
                            {
                                writer.WriteLineAsync("Welcome web world");
                                writer.WriteLineAsync("This is a simple web server");
                                writer.WriteLineAsync($"Request url: {context.Request.RawUrl}");
                                writer.WriteLineAsync($"Request method: {context.Request.HttpMethod}");
                                if (context.Request.HttpMethod == "POST")
                                {
                                    writer.WriteLineAsync($"Request body: {requestBody}");
                                }
                                writer.WriteLineAsync($"Request protocol: {context.Request.ProtocolVersion}");
                                writer.WriteLineAsync($"Request content type: {context.Request.ContentType}");
                                writer.WriteLineAsync($"Request user agent: {context.Request.UserAgent}");
                            }
                            Console.WriteLine("----------------------END------------------------");
                        },
                        null
                    );

                    /*
                    Wait for the program to exit or for a new request 
                     */
                    WaitHandle.WaitAny(new WaitHandle[]{
                        contextAsyncResult.AsyncWaitHandle,
                        shouldExitWaitHandle
                    });
                }

                listener.Stop();
                Console.WriteLine("Server stopped");
            }
        }
    }
}

