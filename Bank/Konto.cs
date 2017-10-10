using System;

namespace Bank
{
    public class Konto
    {
        public decimal Kontostand { get; private set; }

        public void Einzahlen(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException(nameof(betrag), $"{nameof(betrag)} must not be negativ.");

            Kontostand += betrag;
        }

        public void Abheben(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException(nameof(betrag), $"{nameof(betrag)} must not be negativ.");

            if (Kontostand - betrag < 0)
                throw new InvalidOperationException($"{nameof(betrag)} must not be greater than kontostand.");

            System.Diagnostics.Debug.WriteLine($"In Method: {DateTime.Now.DayOfWeek}");
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                throw new InvalidOperationException($"Am Wochenende darf kein Geld ausbezahlt werden.");

            Kontostand -= betrag;
        }
    }
}
