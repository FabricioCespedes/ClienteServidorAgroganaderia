using Libreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP.Clases
{
    class Duenio : Persona
    {
        // Definición de variables.
        private string correoElectronico;

        private int numeroCelular;

        private Finca fincaDuenio;

        public Duenio()
        {
        }

        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Duenio(int identificacion, string nombre, string primerApellido, string segundaApellido, string correoElectronico, int numeroCelular, Finca fincaDuenio = null)
            : base(identificacion, nombre, primerApellido, segundaApellido)
        {

            this.correoElectronico = correoElectronico;
            this.numeroCelular = numeroCelular;
            this.FincaDuenio = fincaDuenio;
        }

        // Métodos establecer y obtener.
        public string CorreoElectronico { get => correoElectronico; set => correoElectronico = value; }
        public int NumeroCelular { get => numeroCelular; set => numeroCelular = value; }
        public Finca FincaDuenio { get => fincaDuenio; set => fincaDuenio = value; }


    }
}
