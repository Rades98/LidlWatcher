

namespace App1.Utils
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Object that gets price and name from specified URL
    /// </summary>
    public sealed class LidlDataGet
    {
        /// <summary>
        /// The HTML
        /// </summary>
        private readonly string _html;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; private set; }

        public string ImageUrl { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LidlDataGet"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public LidlDataGet(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            _html = reader.ReadToEnd();
        }

        #region Public methods

        /// <summary>
        /// Gets the name and price.
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            Name = GetTitle();
            Price = Convert.ToDecimal(GetDataFromHtml("m-price__price\">\n"));
            ImageUrl = GetSpecifiedDataFromHtml("property=\"og:image\" content=\"", "\">", false);
            return $"{Name}: {Price} Kč";
        }

        #endregion

        #region Private methods   

        /// <summary>
        /// Gets the data from HTML.
        /// </summary>
        /// <param name="propertyString">The property string.</param>
        /// <returns></returns>
        private string GetDataFromHtml(string propertyString)
        {
            return GetSpecifiedDataFromHtml(propertyString, $"\n</div>");
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <returns></returns>
        private string GetTitle()
        {
            return GetSpecifiedDataFromHtml("<title>", "- Lidl-Shop.cz</title>", false);
        }

        /// <summary>
        /// Gets the specified data from HTML.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="withoutWhiteSpace"></param>
        /// <returns></returns>
        private string GetSpecifiedDataFromHtml(string start, string end, bool withoutWhiteSpace = true)
        {
            var nHtml = withoutWhiteSpace ? _html.Replace(" ", "") : _html;
            var x = nHtml.Remove(0, nHtml.IndexOf(start, StringComparison.Ordinal));
            var y = x.Remove(x.IndexOf(end, StringComparison.Ordinal), x.Length - x.IndexOf(end, StringComparison.Ordinal));

            var pFrom = y.IndexOf(start, StringComparison.Ordinal) + start.Length;

            return y[pFrom..];
        }

        #endregion
    }
}
