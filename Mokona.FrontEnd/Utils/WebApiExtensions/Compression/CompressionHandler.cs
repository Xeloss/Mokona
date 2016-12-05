namespace Mokona.FrontEnd.WebApiExtensions
{
    using Mokona.FrontEnd.WebApiExtensions.Compression.Compressors;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles the response compression according to the client's supported 
    /// encoding. Currently supports GZip and Deflate.
    /// (http://benfoster.io/blog/aspnet-web-api-compression)
    /// </summary>
    public class CompressionHandler : DelegatingHandler
    {
        public Collection<ICompressor> Compressors { get; private set; }

        public CompressionHandler()
        {
            Compressors = new Collection<ICompressor>();

            Compressors.Add(new GZipCompressor());
            Compressors.Add(new DeflateCompressor());
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Headers.AcceptEncoding.Count > 0 && response.Content != null)
            {
                // As per RFC2616.14.3:
                // Ignores encodings with quality == 0
                // If multiple content-codings are acceptable, then the acceptable content-coding with the highest non-zero qvalue is preferred.
                var compressor = (from encoding in request.Headers.AcceptEncoding
                                  let quality = encoding.Quality ?? 1.0
                                  where quality > 0
                                  join c in Compressors on encoding.Value.ToLowerInvariant() equals c.EncodingType.ToLowerInvariant()
                                  orderby quality descending
                                  select c).FirstOrDefault();

                if (compressor != null)
                {
                    response.Content = new CompressedContent(response.Content, compressor);
                }
            }

            return response;
        }
    }
}
