using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Entidad;

namespace BusinessLayer
{
    public sealed class RepositorioPersona
    {
        public List<Persona> Personas { get; set; } = new List<Persona>();
        public static RepositorioPersona Instancia { get; } = new RepositorioPersona();

        private RepositorioPersona()
        {

        }


    }
}
