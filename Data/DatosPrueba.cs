using CooperativaApi.Models;

namespace CooperativaApi.Data;

public static class DatosPrueba
{
    public static void Inicializar(AppDbContext db)
    {
        if (!db.Cuentas.Any())
        {
            var cuenta1 = new Cuenta
            {
                NumeroCuenta = "1234567890",
                Titular = "Juan Perez",
                CedulaTitular = "123456789",
                Saldo = 1000.00m,
                Estado = EstadoCuenta.Activa
            };

            var cuenta2 = new Cuenta
            {
                NumeroCuenta = "0987654321",
                Titular = "Maria Lopez",
                CedulaTitular = "987654321",
                Saldo = 500.00m,
                Estado = EstadoCuenta.Activa
            };

            var cuenta3 = new Cuenta
            {
                NumeroCuenta = "1122334455",
                Titular = "Carlos Sanchez",
                CedulaTitular = "112233445",
                Saldo = 750.00m,
                Estado = EstadoCuenta.Activa
            };

            var cuenta4 = new Cuenta
            {
                NumeroCuenta = "5566778899",
                Titular = "Ana Torres",
                CedulaTitular = "556677889",
                Saldo = 1200.00m,
                Estado = EstadoCuenta.Bloqueada
            };

            db.Cuentas.AddRange(cuenta1, cuenta2, cuenta3, cuenta4);
            db.SaveChanges();
        }
    }

}