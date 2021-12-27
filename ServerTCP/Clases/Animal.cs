using Libreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP.Clases
{
    class Animal
    {
        // Atributos del animal, definición de variables.
        private int? identificacion;

        private string nombre;

        private Raza razaAnimal;

        private Finca fincaDelAnimal;

        private DateTime fechaNacimiento;

        private int sexo;

        private Animal madre;

        private Animal padre;

        // Constructor permite acceder a guardar datos de esta clase desde otra clase.
        public Animal(int? identificacion, string nombre, Raza razaAnimal, Finca fincaDelAnimal, DateTime fechaNacimiento, int sexo, Animal madre = null, Animal padre= null)
        {
            this.identificacion = identificacion;
            this.nombre = nombre;
            this.razaAnimal = razaAnimal;
            this.fincaDelAnimal = fincaDelAnimal;
            this.fechaNacimiento = fechaNacimiento;
            this.sexo = sexo;
            this.madre = madre;
            this.padre = padre;
        }

        // Constructor sin parámetros.
        public Animal()
        {

        }

        // Métodos establecer y obtener.
        public int? Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Raza RazaAnimal { get => razaAnimal; set => razaAnimal = value; }
        public Finca FincaDelAnimal { get => fincaDelAnimal; set => fincaDelAnimal = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public int Sexo { get => sexo; set => sexo = value; }
        public Animal Madre { get => madre; set => madre = value; }
        public Animal Padre { get => padre; set => padre = value; }
    }
}
