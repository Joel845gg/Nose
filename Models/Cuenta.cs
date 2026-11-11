namespace CooperativaApi.Models;

public enum EstadoCuenta
{
    Activa,
    Bloqueada
}

public class Cuenta
{
    public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    public int Id { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string Titular { get; set; } = string.Empty;
    public string CedulaTitular { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public EstadoCuenta Estado { get; set; }

}