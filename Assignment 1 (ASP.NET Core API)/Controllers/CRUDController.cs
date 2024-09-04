using Assignment_1__ASP.NET_Core_API_.Data;
using Assignment_1__ASP.NET_Core_API_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Assignment_1__ASP.NET_Core_API_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CRUDController : ControllerBase
    {
        public readonly DataContext dataContext;
        public CRUDController(DataContext context)
        {
            this.dataContext = context;
        }
        [HttpGet("/keys/{key}")]
        public async Task<IActionResult> GetAsync([FromRoute] int key)
        {
            try
            {
                    var data = await this.dataContext.Key_Values.FirstOrDefaultAsync(item=>item.Key==key);
              
                    if (data!=null)
                    {
                        return Ok(data);
                    } else {
                            return StatusCode(StatusCodes.Status404NotFound);
                    }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Keys")]
        public async Task<IActionResult> Post([FromBody] Key_Value data)
        {
            try
            {
                var key_Value = await this.dataContext.Key_Values.FirstOrDefaultAsync(item=>item.Key==data.Key);
                if(key_Value==null)
                {
                    await this.dataContext.Key_Values.AddAsync(data);
                    await this.dataContext.SaveChangesAsync();
                    return Ok(data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict);
                }
            }
            catch (DbUpdateException ex)
            {
                // Log the detailed error
                Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                return BadRequest("An error occurred while saving the data.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("/Keys/{key}/{value}")]
        public async Task<IActionResult> PatchAsync([FromRoute] int key,[FromRoute] string value) {
            try
            {
                var keyValue = await this.dataContext.Key_Values.FirstOrDefaultAsync(item=>item.Key==key);
                if (keyValue != null) {
                    keyValue.Value = value;
                    this.dataContext.Key_Values.Update(keyValue);
                    await this.dataContext.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/Keys/{key}")]
        public async Task<IActionResult> delete([FromRoute] int key) {
            try
            {
                var keyValue = await this.dataContext.Key_Values.FirstOrDefaultAsync(item => item.Key == key);
                if (keyValue != null)
                {
                    this.dataContext.Key_Values.Remove(keyValue);
                    await this.dataContext.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
