using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria
{
    [Serializable]

    public class Finca
    {

        // Definición de variables.
        private int numeroFinca;

        private string nombre;

        private int tamanioFinca;

        private string dirreccionFinca;

        private int telefono;

        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Finca(int numeroFinca, string nombre, int tamanioFinca, string dirreccionFinca, int telefono)
        {
            this.numeroFinca = numeroFinca;
            this.nombre = nombre;
            this.tamanioFinca = tamanioFinca;
            this.dirreccionFinca = dirreccionFinca;
            this.telefono = telefono;
        }
        // Constructor sin parámetros.
        public Finca()
        {

        }

        // Métodos establecer y obtener.
        public int NumeroFinca { get => numeroFinca; set => numeroFinca = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int TamanioFinca { get => tamanioFinca; set => tamanioFinca = value; }
        public string DirreccionFinca { get => dirreccionFinca; set => dirreccionFinca = value; }
        public int Telefono { get => telefono; set => telefono = value; }

    }
}
