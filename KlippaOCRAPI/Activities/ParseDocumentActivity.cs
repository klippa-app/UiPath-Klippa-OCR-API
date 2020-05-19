using System;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Klippa.OCRAPI.Activities
{
    public class ParseDocument: AsyncCodeActivity 
    {
        [Category("Input")]
        [DefaultValue("https://custom-ocr.klippa.com/api/v1")]
        [Description(@"The base path to the Klippa API.")]
        public InArgument<string> BasePath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description(@"The auth key provided by Klippa.")]
        public InArgument<string> APIKey { get; set; }

        [Category("Input")]
        [Description(@"The document to scan as a file available at this URL.")]
        public InArgument<string> DocumentURL { get; set; }

        [Category("Input")]
        [Description(@"The document to scan as a file path.")]
        public InArgument<string> DocumentPath { get; set; }

        [Category("Input")]
        [Description(@"The document to scan as a file stream.")]
        public InArgument<Stream> DocumentStream { get; set; }

        [Category("Input")]
        [Description(@"The document to scan as a byte array.")]
        public InArgument<byte[]> DocumentBytes { get; set; }

        [Category("Input")]
        [Description(@"The filename to use when using DocumentStream/DocumentBytes.")]
        public InArgument<string> DocumentFilename { get; set; }

        [Category("Input")]
        [Description(@"The template to use for parsing. Empty for default parsing.")]
        public InArgument<string> Template { get; set; }

        [Category("Input")]
        [DefaultValue(PDFTextExtractionType.Fast)]
        [Description(@"PDF Text extraction. Use full when you want the best quality scan, use fast when you want fast scan results. Fast will try to extract the text from the PDF. Full will actually scan the full PDF, which is slower.")]
        public InArgument<PDFTextExtractionType> PDFTextExtraction { get; set; }

        [Category("Input")]
        [Description(@"Extra metadata in JSON format to give to the parser. Only works with templates that are configured to accept user data.")]
        public InArgument<string> UserData { get; set; }

        [Category("Input")]
        [Description(@"The external ID of the user data set.")]
        public InArgument<string> UserDataSetExternalID { get; set; }

        [Category("Output")]
        [Description(@"The HTTP headers of the response.")]
        public OutArgument<HttpResponseHeaders> HTTPResponseHeaders { get; set; }

        [Category("Output")]
        [Description(@"The body of the response.")]
        public OutArgument<string> HTTPResponseBody { get; set; }

        [Category("Output")]
        [Description(@"The status code of the response.")]
        public OutArgument<HttpStatusCode> HTTPResponseStatus { get; set; }

        [Category("Output")]
        [Description(@"Whether the request was succesful (200 OK status).")]
        public OutArgument<bool> RequestSuccesful { get; set; }

        [Category("Output")]
        [Description(@"The parsed API response.")]
        public OutArgument<APIResponse> APIResponse { get; set; }

        private HttpClient httpClient;

        public ParseDocument()
        {
            BasePath = "https://custom-ocr.klippa.com/api/v1";
            PDFTextExtraction = PDFTextExtractionType.Fast;
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(600)
            };
        }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var task = ExecuteAsync(context);
            var tacs = new TaskCompletionSource<HttpResponseMessage>(state);

            task.ContinueWith(t =>
            {
                if (t.IsFaulted) tacs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled) tacs.TrySetCanceled();
                else tacs.TrySetResult(t.Result);
                callback?.Invoke(tacs.Task);
            });

            return tacs.Task;
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            var task = result as Task<HttpResponseMessage>;

            if (task.IsFaulted) throw task.Exception.InnerException;
            if (task.IsCanceled || context.IsCancellationRequested)
            {
                context.MarkCanceled();
                return;
            }

            try
            {
                var response = task.Result;
                var requestSuccesful = response.StatusCode == HttpStatusCode.OK;
                var output = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                HTTPResponseStatus.Set(context, response.StatusCode);
                HTTPResponseBody.Set(context, output);
                HTTPResponseHeaders.Set(context, response.Headers);
                RequestSuccesful.Set(context, requestSuccesful);

                try
                {
                    APIResponse deserializedAPIResponse = JsonConvert.DeserializeObject<APIResponse>(output);
                    APIResponse.Set(context, deserializedAPIResponse);
                }
                catch (Exception)
                {

                }
            }
            catch (OperationCanceledException)
            {
                context.MarkCanceled();
            }
            finally
            {
                if (task.Result != null)
                {
                    task.Result.Dispose();
                }
            }
        }

        Task<HttpResponseMessage> ExecuteAsync(CodeActivityContext context)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            FileStream fs = null;

            var basePath = BasePath.Get(context);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(basePath + "/parseDocument"),
                Method = HttpMethod.Post,
            };

            var apiKey = APIKey.Get(context);
            request.Headers.Add("X-Auth-Key", apiKey);

            var documentURL = DocumentURL.Get(context);
            if (documentURL != null && !documentURL.Equals(""))
            {
                formData.Add(new StringContent(documentURL, Encoding.UTF8), "url");
            }

            var template = Template.Get(context);
            if (template != null && !template.Equals(""))
            {
                formData.Add(new StringContent(template, Encoding.UTF8), "template");
            }

            var pdfTextExtraction = PDFTextExtraction.Get(context);
            if (pdfTextExtraction == PDFTextExtractionType.Fast)
            {
                formData.Add(new StringContent("fast", Encoding.UTF8), "pdf_text_extraction");
            }
            else if (pdfTextExtraction == PDFTextExtractionType.Full)
            {
                formData.Add(new StringContent("full", Encoding.UTF8), "pdf_text_extraction");
            }

            var userData = UserData.Get(context);
            if (userData != null && !userData.Equals(""))
            {
                formData.Add(new StringContent(userData, Encoding.UTF8), "user_data");
            }

            var userDataSetExternalID = UserDataSetExternalID.Get(context);
            if (userDataSetExternalID != null && !userDataSetExternalID.Equals(""))
            {
                formData.Add(new StringContent(userDataSetExternalID, Encoding.UTF8), "user_data_set_external_id");
            }

            var documentFilename = DocumentFilename.Get(context);
            var documentBytes = DocumentBytes.Get(context);
            var documentPath = DocumentPath.Get(context);
            var documentStream = DocumentStream.Get(context);
            if (documentBytes != null && documentBytes.Length > 0)
            {
                formData.Add(new ByteArrayContent(documentBytes), "document", documentFilename);
            }
            else if (documentPath != null && !documentPath.Equals(""))
            {
                fs = File.OpenRead(documentPath);
                formData.Add(new StreamContent(fs), "document", Path.GetFileName(documentPath));
            }
            else if (documentStream != null && documentStream.Length > 0)
            {
                formData.Add(new StreamContent(documentStream), "document", documentFilename);
            }

            request.Content = formData;
            request.Properties["RequestTimeout"] = TimeSpan.FromSeconds(600);

            return httpClient.SendAsync(request);
        }
    }
}
