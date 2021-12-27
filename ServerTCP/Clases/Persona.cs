using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP.Clases
{
    class Persona
    { // Definición de variable que son los atributos de la persona
        private int identificacion;

        private string nombre;

        private string primerApellido;

        private string segundaApellido;

        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Persona(int identificacion, string nombre, string primerApellido, string segundaApellido)
        {
            this.identificacion = identificacion;
            this.nombre = nombre;
            this.primerApellido = primerApellido;
            this.segundaApellido = segundaApellido;
        }
        // Constructor vacio, inicializa variables.
        public Persona()
        {
            this.identificacion = 0;
            this.nombre = "";
            this.primerApellido = "";
            this.segundaApellido = "";
        }

        // Métodos establecer y obtener.
        public int Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string PrimerApellido { get => primerApellido; set => primerApellido = value; }
        public string SegundaApellido { get => segundaApellido; set => segundaApellido = value; }

    }
}
