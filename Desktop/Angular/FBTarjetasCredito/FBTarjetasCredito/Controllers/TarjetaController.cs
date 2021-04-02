using FBTarjetasCredito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBTarjetasCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {


        private ApplicationDBContext _context;

        public TarjetaController(ApplicationDBContext context)
        {
            this._context = context;
        }


        #region GET de todas las tarjetas
        // GET: api/<TarjetaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listTarjetas = await _context.TarjetaCreditoDB.ToListAsync();
                return Ok(listTarjetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Para obtener una sola tarjeta
        // GET api/<TarjetaController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        #endregion


        #region POST para introducir tarjetas
        // POST api/<TarjetaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarjetaCredito tarjeta)
        {
            try
            {
                _context.Add(tarjeta);
               await _context.SaveChangesAsync();
                return Ok(tarjeta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        #endregion

        #region PUT para actualizar tarjeta

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TarjetaCredito tarjeta)
        {

            try
            {
                
                    if(id != tarjeta.Id)
                {
                    return NotFound();
                }
                else
                {
                     _context.Update(tarjeta);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "La tarjeta se actualizó con exito!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion



        #region DELETE para eliminar tarjeta
        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarjeta = await _context.TarjetaCreditoDB.FindAsync(id);

                if(tarjeta == null)
                {
                    return NotFound();
                }
                    _context.TarjetaCreditoDB.Remove(tarjeta);
                    await _context.SaveChangesAsync();
                return Ok(new { message ="La tarjeta fue eliminada con exito!"});

                        
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        #endregion
    }
}
