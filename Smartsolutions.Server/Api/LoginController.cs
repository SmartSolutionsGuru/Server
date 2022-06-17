using Microsoft.AspNetCore.Mvc;
using SmartSolutions.DataStore.Entites;
using SmartSolutions.DataStore.Repository;

namespace Smartsolutions.Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region [private Members]
        private readonly ILogger<CompaniesController> _logger;
        private readonly IRepository _repository;
        #endregion

        #region [Properties]
        public IEnumerable<ProprietorInfo> Companies { get; set; }
        public ProprietorInfo Company { get; set; }
        #endregion

        #region [Constructor]
        //public CompaniesController(ILogger<CompaniesController> logger, IRepository repository)
        //{
        //    _logger = logger;
        //    _repository = repository;
        //}
        #endregion

        //[Route("Login")]
        [HttpGet]
        public IEnumerable<string> Login()
        {
            return new List<string> { "Shabab", "Butt" };
        }



    }
}
