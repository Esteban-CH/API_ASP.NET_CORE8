using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using API_HOSTING_GAAAAAAA_.Models;
using Microsoft.AspNetCore.Cors;

namespace API_HOSTING_GAAAAAAA_.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly BdapiContext _dbcontext;

        public ProductoController(BdapiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public ActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Listado de Productos", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public ActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no Encontrado");
            }
            try
            {
                oProducto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Obtenido", Response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = oProducto });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto obj)
        {
            try
            {
                _dbcontext.Productos.Add(obj);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Registrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto obj)
        {
            Producto oProducto = _dbcontext.Productos.Find(obj.IdProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no Encontrado");
            }
            try
            {
                oProducto.CodigoBarra = obj.CodigoBarra is null ? oProducto.CodigoBarra : obj.CodigoBarra;
                oProducto.Descripcion = obj.Descripcion is null ? oProducto.Descripcion : obj.Descripcion;
                oProducto.Marca = obj.Marca is null ? oProducto.Marca : obj.Marca;
                oProducto.IdCategoria = obj.IdCategoria is null ? oProducto.IdCategoria : obj.IdCategoria;
                oProducto.Precio = obj.Precio is null ? oProducto.Precio : obj.Precio;

                _dbcontext.Productos.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Actualizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no Encontrado");
            }
            try
            {
                

                _dbcontext.Productos.Remove(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}
