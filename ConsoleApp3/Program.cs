//Cristian Camilo Rojas Cruz
//213023_206
//Ingenieria en sistemas
//Derechos de creacion: Propiedad Camilo Rojas


using System;
using System.Collections.Generic;

class Habitacion
{
    public int Numero { get; set; }
    public string Estado { get; set; }
    public int CantidadPersonas { get; set; }
    public bool TieneAireAcondicionado { get; set; }
    public bool TieneVentilador { get; set; }
    public decimal PrecioPorDia { get; set; }
    public bool Reservada { get; set; }
}

class Reservacion
{
    public int HabitacionNumero { get; set; }
    public DateTime FechaEntrada { get; set; }
    public DateTime FechaSalida { get; set; }
    public int CantidadPersonas { get; set; }
    public decimal MontoReservacion { get; set; }
}

class Program
{
    static List<Habitacion> habitaciones = new List<Habitacion>();
    static List<Reservacion> reservaciones = new List<Reservacion>();

    static void Main()
    {
        // Iniciamos proceso de reserva 
        ReservarHabitaciones();

        while (true)
        {
            Console.WriteLine("1. Ver habitaciones disponibles");
            Console.WriteLine("2. Hacer una reserva");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La opcion que digitaste no es valida. Intenta de nuevo.");
                Console.ResetColor();
                continue;
            }

            switch (opcion)
            {
                case 1:
                    MostrarHabitacionesDisponibles();
                    break;
                case 2:
                    RealizarReservacion();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opcion que digitaste no es valida. Intenta de nuevo.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void ReservarHabitaciones()
    {
        for (int i = 101; i <= 105; i++)
        {
            habitaciones.Add(new Habitacion
            {
                Numero = i,
                Estado = "Disponible",
                CantidadPersonas = 2,
                TieneAireAcondicionado = true,
                TieneVentilador = false,
                PrecioPorDia = 900,
                Reservada = false
            });
        }
    }

    static void MostrarHabitacionesDisponibles()
    {
        Console.WriteLine("Estas son las Habitaciones disponibles:");
        foreach (var habitacion in habitaciones)
        {
            if (!habitacion.Reservada)
            {
                Console.WriteLine($"Habitacion #{habitacion.Numero}, Estado: {habitacion.Estado}");
            }
        }
    }

    static void RealizarReservacion()
    {
        MostrarHabitacionesDisponibles();
        Console.Write("Ingresa el numero de la habitacion que deseas reservar: ");
        if (!int.TryParse(Console.ReadLine(), out int habitacionNumero))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El numero de habitacion digitado no es valido. Comprueba de nuevo.");
            Console.ResetColor();
            return;
        }

        var habitacion = habitaciones.Find(h => h.Numero == habitacionNumero);

        if (habitacion == null || habitacion.Reservada)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("La habitacion que digitaste no esta disponible para reservar.");
            Console.ResetColor();
            return;
        }

        Console.Write("Favor ingresar la fecha de entrada (dd/mm/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime fechaEntrada))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("La fecha que digitaste no es valida. Intenta nuevamente.");
            Console.ResetColor();
            return;
        }

        Console.Write("Favor ingresar la fecha de salida (dd/mm/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime fechaSalida))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("La fecha de salida que digitaste no es valida. Intenta nuevamente.");
            Console.ResetColor();
            return;
        }

        Console.Write("Cantidad de personas : ");
        if (!int.TryParse(Console.ReadLine(), out int cantidadPersonas))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cantidad de personas digitada no es valida. Revisa e intenta de nuevo.");
            Console.ResetColor();
            return;
        }

        decimal montoReservacion = CalcularMontoReservacion(habitacion, fechaEntrada, fechaSalida, cantidadPersonas);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Monto a pagar por la reserva: ${montoReservacion}");
        Console.ResetColor();

        habitacion.Reservada = true;

        reservaciones.Add(new Reservacion
        {
            HabitacionNumero = habitacion.Numero,
            FechaEntrada = fechaEntrada,
            FechaSalida = fechaSalida,
            CantidadPersonas = cantidadPersonas,
            MontoReservacion = montoReservacion
        });

        Console.WriteLine("Tu reserva se ha realizado exitosamente.");
    }

    static decimal CalcularMontoReservacion(Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas)
    {
        TimeSpan duracionReserva = fechaSalida - fechaEntrada;
        decimal montoTotal = habitacion.PrecioPorDia * (decimal)duracionReserva.Days;
        return montoTotal;
    }
}

// Gracias por su atencion,espero que haya sido de su gusto