using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP.Clases
{
    class Raza
    {
        // Definición de variables.
        private int codigo;

        private string descripcionRaza;

        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Raza(int codigoRaza, string descripcionRaza)
        {
            this.codigo = codigoRaza;
            this.descripcionRaza = descripcionRaza;
        }

        // Métodos establecer y obtener.
        public int Codigo { get => codigo; set => codigo = value; }
        public string DescripcionRaza { get => descripcionRaza; set => descripcionRaza = value; }

        // Constructor sin parámetros.
        public Raza()
        {                       // Inicializa variables.
            codigo = 0;
            descripcionRaza = "";
        }
        // Método que devuelve los  datos guardados en la clase covertidos a string.

    }
}
