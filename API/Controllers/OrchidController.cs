using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Bắt buộc phải đăng nhập mới truy cập được controller này
    public class OrchidController : ControllerBase
    {
        private readonly IOrchidService _orchidService;

        public OrchidController(IOrchidService orchidService)
        {
            _orchidService = orchidService;
        }

        // Ai cũng có thể xem (miễn là đã đăng nhập)
        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả hoa lan")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orchidService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy hoa lan theo ID")]
        public async Task<IActionResult> GetById(int id)
        {
            var orchid = await _orchidService.GetByIdAsync(id);
            if (orchid == null)
                return NotFound();
            return Ok(orchid);
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Tìm kiếm hoa lan theo tên hoặc mô tả")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _orchidService.SearchAsync(keyword);
            return Ok(result);
        }

        // Chỉ admin mới được tạo mới
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [SwaggerOperation(Summary = "Tạo mới hoa lan")]
        public async Task<IActionResult> Create([FromBody] OrchidDTO orchidDto)
        {
            var created = await _orchidService.CreateAsync(orchidDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Chỉ admin mới được cập nhật
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [SwaggerOperation(Summary = "Cập nhật thông tin hoa lan")]
        public async Task<IActionResult> Update(int id, [FromBody] OrchidDTO orchidDto)
        {
            if (id != orchidDto.Id)
                return BadRequest("ID không khớp");

            var updated = await _orchidService.UpdateAsync(orchidDto);
            return Ok(updated);
        }

        // Chỉ admin mới được xóa
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [SwaggerOperation(Summary = "Xoá hoa lan theo ID")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _orchidService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}

