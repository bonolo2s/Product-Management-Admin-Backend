using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.AdminBackend.Core.DTOs;
using ProductManagement.AdminBackend.Core.Interfaces;
using ProductManagement.AdminBackend.Core.Services;

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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-admin")]
        public async Task<IActionResult> Create([FromBody] CreateAdminDto dto)
        {
            var admin = await _service.CreateAdminAsync(dto, GetCurrentUserId());
            return Ok(admin);
        }


        [Authorize]
        [HttpGet("get-admin/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var admin = await _service.GetAdminByIdAsync(id);
            return admin == null ? NotFound() : Ok(admin);
        }


        [Authorize]
        [HttpGet("get-all-admins")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAdminsAsync());
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminDto dto)
        {
            var admin = await _service.UpdateAdminAsync(id, dto, GetCurrentUserId());
            return Ok(admin);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("disable-admin/{id}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            var result = await _service.DisableAdminAsync(id, GetCurrentUserId());
            return result ? Ok() : NotFound();
        }


        private Guid GetCurrentUserId()
        {
            var claim = User.FindFirst("id")?.Value;
            return claim == null ? Guid.Empty : Guid.Parse(claim);
        }
    }
}
