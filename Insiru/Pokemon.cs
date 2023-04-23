using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insiru
{
    public class Pokemon
    {

        private string nombre;
        private string tipo;
        private int vida;

        public Pokemon(string nombre, string tipo, int vida)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.vida = vida;
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int Vida
        {
            get { return vida; }
            set { vida = value; }
        }

    }
}
