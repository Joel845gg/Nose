namespace CooperativaApi.Models;

public enum TipoMovimiento
{
    Deposito,
    Retiro
}

public class Movimiento
{
    public Cuenta Cuenta { get; set; } = null!;
    public int Id { get; set; }
    public int CuentaId { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }

}

