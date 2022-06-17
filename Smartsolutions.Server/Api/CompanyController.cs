using Microsoft.AspNetCore.Mvc;
using SmartSolutions.DataStore.Entites;
using SmartSolutions.DataStore.Repository;

namespace Smartsolutions.Server.Api
{
    public class CompanyController : Controller
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
        public CompanyController(ILogger<CompaniesController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
            Companies = new List<ProprietorInfo>();
        }
        #endregion

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IEnumerable<ProprietorInfo>> GetAllAsync()
        {
             Companies = await _repository.GetAllAsync<ProprietorInfo>();
            return Companies;
        }
    }
}
