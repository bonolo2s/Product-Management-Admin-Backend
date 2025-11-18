using Microsoft.AspNetCore.Mvc;
using ProductManagement.AdminBackend.Core.DTOs;
using ProductManagement.AdminBackend.Core.Interfaces;

namespace ProductManagement.AdminBackend.API.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdminDto dto)
        {
            var admin = await _service.CreateAdminAsync(dto, GetCurrentUserId());
            return Ok(admin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var admin = await _service.GetAdminByIdAsync(id);
            return admin == null ? NotFound() : Ok(admin);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAdminsAsync());

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminDto dto)
        {
            var admin = await _service.UpdateAdminAsync(id, dto, GetCurrentUserId());
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            var result = await _service.DisableAdminAsync(id, GetCurrentUserId());
            return result ? Ok() : NotFound();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
            => Ok(await _service.LoginAsync(dto));

        private Guid GetCurrentUserId()
        {
            var claim = User.FindFirst("id")?.Value;
            return claim == null ? Guid.Empty : Guid.Parse(claim);
        }
    }
}
