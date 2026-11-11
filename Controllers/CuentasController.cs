using CooperativaApi.Data;
using CooperativaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CooperativaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CuentasController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{numero}")]
    public async Task<IActionResult> GetCuenta(string numero)
    {
        var cuenta = await _dbContext.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numero);
        if (cuenta == null)
        {
            return NotFound(new { mensaje = $"Cuenta con número {numero} no encontrada." });
        }

        return Ok(new { numero = cuenta.NumeroCuenta, titular = cuenta.Titular, saldo = cuenta.Saldo, estado = cuenta.Estado.ToString() });
    }

    [HttpPost("{numero}/depositos")]
    public async Task<IActionResult> RealizarDeposito(string numero, [FromBody] MovimientoRequest request)
    {
        if (request.Monto <= 0)
        {
            return BadRequest(new { mensaje = "El monto del depósito debe ser mayor a cero." });
        }

        var cuenta = await _dbContext.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numero);
        if (cuenta == null)
        {
            return NotFound(new { mensaje = $"Cuenta con número {numero} no encontrada." });
        }

        if (cuenta.Estado != EstadoCuenta.Activa)
        {
            return BadRequest(new { mensaje = "La cuenta está bloqueada. No se pueden realizar depósitos." });
        }

        cuenta.Saldo += request.Monto;

        _dbContext.Movimientos.Add(new Movimiento
        {
            CuentaId = cuenta.Id,
            Tipo = TipoMovimiento.Deposito,
            Monto = request.Monto,
            Fecha = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();

        return Ok(new { mensaje = "Depósito realizado con éxito.", saldo = cuenta.Saldo });
    }
}

public record MovimientoRequest(decimal Monto);