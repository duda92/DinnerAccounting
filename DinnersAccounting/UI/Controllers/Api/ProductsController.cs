using System.Net;
using System.Net.Http;
using System.Web.Http;
using DA.Dinners.Domain.Abstract;
using DA.Dinners.Model;

namespace UI.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiController
    {
        //
        // GET: /Product/
        private readonly IProductRepository productRepository;
        private readonly IPropositionRepository propositionRepository;

        public ProductsController(IProductRepository productRepository, IPropositionRepository propositionRepository)
        {
            this.productRepository = productRepository;
            this.propositionRepository = propositionRepository;
        }

        [AcceptVerbs("GET", "POST")]
        public Product Get(int id)
        {
            Product product = productRepository.Find(id);
            if (product == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return product;
        }

        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                productRepository.InsertOrUpdate(model);
                productRepository.Save();

                var response = new HttpResponseMessage(HttpStatusCode.Accepted);
                return response;
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage Delete(int id)
        {
            var product = productRepository.Find(id);
            if (product == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            productRepository.Delete(id);
            productRepository.Save();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage Create(int id, Product product)
        {
            var proposition = propositionRepository.Find(id);
            if (product == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            proposition.Products.Add(product);
            propositionRepository.InsertOrUpdate(proposition);
            propositionRepository.Save();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}