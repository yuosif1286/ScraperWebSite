using System.Net;
using System.Web;
using HtmlAgilityPack;

namespace ScraperWebSite
{
    public class ScraperService
    {
        public string? Search = "";

        private string? GetSearch(string? search)
            => string.IsNullOrEmpty(search) ? "laptop" : search;

        public async Task<List<ProductDetails>> DoScraperTamata()
        {
            // Create an HttpClient with a 2-minute timeout
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(2);
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.35.0");
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("Cookie",
                "f247977ea8a656998d367a2d9bc4f1a8=a0c6f005838c94b209476f00a4ae9962");

            try
            {
                // Make the GET request and read the response content
                var response = await httpClient.GetAsync("https://www.tamata.com/catalogsearch/result/?q=laptop");
                var result = await response.Content.ReadAsStringAsync();

                // Process the result as needed
                Console.WriteLine(result);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions, e.g., timeout or network issues
                Console.WriteLine($"Error: {ex.Message}");
            }

            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();
            web.Timeout = 200000;

            // loading the target web page 
            var document =
                await web.LoadFromWebAsync($"https://www.tamata.com/catalogsearch/result/?q={GetSearch(Search)}");

            // Introduce a delay (e.g., 5 seconds) to allow dynamic content to load

            var contentHTMLElements = document.DocumentNode.QuerySelectorAll("div.product-item-info");
            var productHTMLElements = contentHTMLElements.QuerySelectorAll("div.details");

            // iterating over the list of product elements 
            foreach (var productHTMLElement in productHTMLElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a.product-item-link").InnerText
                    .Trim());

                var available = productHTMLElement.QuerySelector("div.availability");

                var Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a.product-item-link").InnerText
                    .Trim());
                var Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.price").InnerText
                    .Trim());
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a.product-item-link").InnerText
                        .Trim()),
                    Description = "No thing in this site",
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.price").InnerText
                        .Trim()),
                    Img = " "
                    //   Img = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value)
                });
            }

            return Details;
        }

        public async Task<List<ProductDetails>> DoScraperElryan()
        {
            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();
            web.Timeout = 300000;

            // loading the target web page 
            var document = await web.LoadFromWebAsync($"https://www.elryan.com/ar/search?q={GetSearch(Search)}");

            // Introduce a delay (e.g., 5 seconds) to allow dynamic content to load
            // await Task.Delay(1000); // Adjust the delay time as needed

            // Wait for a specific element with class 'container' to appear
            var maxAttempts = 10;
            var currentAttempt = 0;
            var contentHTMLElements = document.DocumentNode.QuerySelectorAll("div.container");

            while (contentHTMLElements.Count == 0 && currentAttempt < maxAttempts)
            {
                await Task.Delay(1000); // Adjust the delay time as needed
                document = await web.LoadFromWebAsync($"https://www.elryan.com/ar/search?q={GetSearch(Search)}");
                contentHTMLElements = document.DocumentNode.QuerySelectorAll("div.container");
                currentAttempt++;
            }


            // Continue processing the HTML document
            var rowHTMLElements = contentHTMLElements.QuerySelectorAll("div.align-center");
            var productHTMLElements = rowHTMLElements.QuerySelectorAll("a");
            // Process the HTML document as needed


            // iterating over the list of product elements 
            foreach (var productHTMLElement in productHTMLElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("p").InnerText
                    .Trim());
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("p").InnerText.Trim()),
                    Description = "the site doesn't has description",
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.price-regular").InnerText
                        .Trim()),
                    Img = " "
                });
            }

            return Details;
        }

        public async Task<List<ProductDetails>> DoScraperPhco()
        {
            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();

            using (var httpClient = new HttpClient())
            {
                // Create HttpRequestMessage with custom headers
                var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                    $"https://miswag.com/search?query={GetSearch(Search)}");

                // Add headers to the request message
                requestMessage.Headers.Add("Accept",
                    "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                requestMessage.Headers.Add("Accept-Language", "ar,en-US;q=0.9,en;q=0.8");
                requestMessage.Headers.Add("Cache-Control", "max-age=0");
                requestMessage.Headers.Add("Cookie",
                    "_ga=GA1.1.1968799561.1702683695; rl_user_id=RudderEncrypt%3AU2FsdGVkX19c6Jj5l4XhqeoZCdnuj2Bz56EVQIA60Rs%3D; rl_trait=RudderEncrypt%3AU2FsdGVkX19TufJrejs1GJ8HoN9zduEw6A28y82w%2BGw%3D; rl_group_id=RudderEncrypt%3AU2FsdGVkX1%2F72gQhgr1kEv4RWm3vuSQ7p%2FYwmgzKsyo%3D; rl_group_trait=RudderEncrypt%3AU2FsdGVkX19auTEctcywkmJ9k9agIiKks8VS%2Ft0jZIc%3D; rl_anonymous_id=RudderEncrypt%3AU2FsdGVkX1%2BvuPwlc6Zu3%2Fpzb5s89TqV5ia1hIorG7YKACESK4NDj7KaQBWs7egPbXbk8AncUawZCZbGVTgpXw%3D%3D; rl_page_init_referrer=RudderEncrypt%3AU2FsdGVkX1%2BHGFOXDLJR%2BBSIl3NGQthDkWFVCgbJJVBfSt1wEelR4TEdKr%2FUNaYO; rl_page_init_referring_domain=RudderEncrypt%3AU2FsdGVkX1%2Bo%2BT8s7RaEBZrL8gNSxNsHyTVsb3KJww2KgZLRXb9UIT0%2BPknxuh1g; __anonToken=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MDI2ODM3MDAsInVpZCI6Im0tNjk1NjgzNiIsInRvayI6ImNjM2UzZWU1LTBmNTQtNGNkZi05MzAyLWFhOTI2ZWQ5YTRmZSIsImRpZCI6Ik5IdzRaRFEyTnpGaE55MWpNVEZsTFRReU56Y3RPRGsxTkMwMk1HTm1NemMzWlRNMll6aDhjSEp2WkE9PSIsImlzcyI6Im1pc3dhZy5hcGkifQ.OpMSLp4QFyZP92fxcPgNZ0snBBa6C7E2M82PgFWuebM; __deviceId=NHw4ZDQ2NzFhNy1jMTFlLTQyNzctODk1NC02MGNmMzc3ZTM2Yzh8cHJvZA==; _ga_VRK6EPJTTL=GS1.1.1702683701.1.0.1702683701.0.0.0; rl_session=RudderEncrypt%3AU2FsdGVkX19FXVCTg8z%2FeGljbtRxlGDpvTNU4yRJnV%2BpML93JWfiS6WAuRfFDiB3swGj%2Bq4R%2B9YpmvEYJXyfZ2WRnV4AWXcEfarpzos8%2Fbf1Y7XUZLr77N%2FbXTP4wwJQ70O%2BKMu1uTwShQB32ywaTw%3D%3D; _ga_8L964EFNHS=GS1.1.1702683694.1.1.1702683714.0.0.0; _clck=1ai12u7%7C2%7Cfhl%7C0%7C1444; _clsk=belisn%7C1702685814434%7C4%7C1%7Cw.clarity.ms%2Fcollect");
                // Add other headers...

                // Send the request using HttpClient
                var response = await httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    // Content has been modified. Read the new content.
                    var content = await response.Content.ReadAsStringAsync();

                    // Load the content into HtmlDocument
                    var document = new HtmlDocument();
                    document.LoadHtml(content);

                    var productHTMLElements = document.DocumentNode.QuerySelectorAll("#SearchPage");
                    var sectionHTMLElements = document.DocumentNode.QuerySelectorAll("section");

                    // Process the HTML document as needed
                }
                else if (response.StatusCode == HttpStatusCode.NotModified)
                {
                    // Use a cached version of the content, or handle accordingly
                    Console.WriteLine("Content not modified. Using cached content.");
                }
                else
                {
                    // Handle other status codes if needed
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

          /*  foreach (var productHTMLElement in productHTMLElements)
            {
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("h2").InnerText.Trim()),
                    Description = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.description").InnerText),
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.price").InnerText.Trim()),
                    Img = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value)
                });
            }*/

            return Details;
        }
    }
}