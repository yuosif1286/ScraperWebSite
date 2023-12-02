using System.Web;
using HtmlAgilityPack;

namespace ScraperWebSite
{
    public class ScraperService
    {
        public string? Search = "";

        private string? GetSearch(string? search)
        => string.IsNullOrEmpty(search) ? "laptop" : search;
        public async Task<List<ProductDetails>> DoScraperSenq()
        {
            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();
            // loading the target web page 
            var document = web.Load($"https://www.senq.com.my/catalogsearch/result?q={GetSearch(Search)}");

            // Introduce a delay (e.g., 5 seconds) to allow dynamic content to load

            var contentHTMLElements = document.DocumentNode.QuerySelectorAll("div.product-container");
            var productHTMLElements = contentHTMLElements.QuerySelectorAll("div.item-container");

            // iterating over the list of product elements 
            foreach (var productHTMLElement in productHTMLElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.MuiTypography-root").InnerText
                    .Trim());
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("p").InnerText.Trim()),
                    Description = "the site doesn't has description",
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.MuiTypography-root").InnerText
                        .Trim()),
                    Img = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value)
                });
            }

            return Details;
        }

        public async Task<List<ProductDetails>> DoScraperSenheng()
        {
            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();
            // loading the target web page 
            var document = web.Load($"https://www.senheng.com.my/catalogsearch/result?q={GetSearch(Search)}");

            // Introduce a delay (e.g., 5 seconds) to allow dynamic content to load
            // await Task.Delay(1000); // Adjust the delay time as needed

            var contentHTMLElements = document.DocumentNode.QuerySelectorAll("div.content");
            var rowHTMLElements = contentHTMLElements.QuerySelectorAll("div.row");
            var productHTMLElements = rowHTMLElements.QuerySelectorAll("div.container-product__item");


            // iterating over the list of product elements 
            foreach (var productHTMLElement in productHTMLElements)
            {
                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.MuiTypography-root").InnerText
                    .Trim());
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("p").InnerText.Trim()),
                    Description = "the site doesn't has description",
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.MuiTypography-root").InnerText
                        .Trim()),
                    Img = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value)
                });
            }

            return Details;
        }

        public async Task<List<ProductDetails>> DoScraperPhco()
        {
            List<ProductDetails> Details = new List<ProductDetails>();
            var web = new HtmlWeb();
            // loading the target web page 
            var document = web.Load($"https://www.phco.my/filterSearch?adv=false&cid=0&mid=0&vid=0&q={GetSearch(Search)}&sid=false&isc=true&orderBy=11");

            //   var productHTMLElements = document.DocumentNode.QuerySelectorAll("div.details");
            var productHTMLElements = document.DocumentNode.QuerySelectorAll("div.product-item");
            // iterating over the list of product elements 
            foreach (var productHTMLElement in productHTMLElements)
            {
                Details.Add(new ProductDetails()
                {
                    Name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("h2").InnerText.Trim()),
                    Description = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("div.description").InnerText),
                    Price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("span.price").InnerText.Trim()),
                    Img = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value)
                });
            }

            return Details;
        }
        /*  public async Task<List<PokemonProduct>> DoScraperPhco()
          {
              var web = new HtmlWeb();
              // loading the target web page 
              var document = web.Load("https://scrapeme.live/shop/");
              var pokemonProducts = new List<PokemonProduct>();
              // selecting all HTML product elements from the current page 
              var productHTMLElements = document.DocumentNode.QuerySelectorAll("li.product");
              // iterating over the list of product elements 
              foreach (var productHTMLElement in productHTMLElements)
              {
                  // scraping the interesting data from the current HTML element 
                  var url = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a").Attributes["href"].Value);
                  var image = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value);
                  var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a").InnerText);
                  var price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector(".price").InnerText);
                  // instancing a new PokemonProduct object 
                  var pokemonProduct = new PokemonProduct() { Url = url, Image = image, Name = name, Price = price };
                  // adding the object containing the scraped data to the list 
                  pokemonProducts.Add(pokemonProduct);
              }

              return pokemonProducts;
          }

          public class PokemonProduct
          {
              public string? Url { get; set; }
              public string? Image { get; set; }
              public string? Name { get; set; }
              public string? Price { get; set; }
          }*/


    }
}
