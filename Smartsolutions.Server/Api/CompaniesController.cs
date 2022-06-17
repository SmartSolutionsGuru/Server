using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using SmartSolutions.DataStore.Entites;
using SmartSolutions.DataStore.Repository;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Smartsolutions.Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        #region Private Members
        private readonly IRepository _repository;
        private IConfiguration _configuration;
        #endregion

        #region Properties
        public IEnumerable<ProprietorInfo> Companies { get; set; }
        public ProprietorInfo Company { get; set; }
        #endregion
        #region Constructor
        public CompaniesController(ILogger<CompaniesController> logger
                                   , IRepository repository
                                   , IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        #endregion

        // GET: api/<CompaniesController>
        [HttpGet]
       // [Route("GetCompanies")]
        public Task<IEnumerable<ProprietorInfo>> Get()
        {
            return _repository.GetAllAsync<ProprietorInfo>();
        }
        [HttpGet]
        [Route("GetCompanyByPhone")]
        public async Task<ProprietorInfo> GetCompanyByPhone(string phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                //SyncService syncService = new SyncService();
                Expression<Func<ProprietorInfo, bool>> result = s => !string.IsNullOrEmpty(s.MobileNumber) && s.MobileNumber.Equals(phone);
                Company = await _repository.GetOneAsync<ProprietorInfo>(filter: result);
                //if(Company != null)
                //{
                //   var token =  ReturnJwtToken(Company);
                //}

                //if (Company != null && !string.IsNullOrEmpty(Company?.MobileNumber))
                //{
                //    OtpService SmsService = new OtpService();
                //    var otpResult = SmsService.OtpValue;
                //}
            }
            return Company;

        }
        // GET api/<CompaniesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CompaniesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CompaniesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompaniesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private string ReturnJwtToken(ProprietorInfo user)
        {
            //string jwtToken = string.Empty;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim("UserName", user.ProprietorName));
            claims.Add(new Claim("DisplayName", user.BussinessName));
            claims.Add(new Claim("PhoneNumber", user.MobileNumber));
           
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken= new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audiance"], claims, expires: DateTime.UtcNow.AddMinutes(10),signingCredentials:creds);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
