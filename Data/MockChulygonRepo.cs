using System.Collections.Generic;
using Chulygon.Models;

namespace Chulygon.Data
{
    /// <summary>
    /// Esta clase e unha implementacion da nosa interface, sen usar a base de datos para nada, o cal permite facer testing usando codigo hard coded
    /// </summary>
    public class MockChulygonRepo : IChulygonRepo
    {
        public void CrearComando(Comando cmd)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public Comando GetComandoPorId(int id)
        {
            return new Comando { Id = 0, ComoSeFai = "Escribir hola", Linea = "Teclea as letras h o l a", Plataforma = "Teclado" };
        }

        public IEnumerable<Comando> GetTodosComandos()
        {
            var comandos = new List<Comando>
            {
                new Comando { Id = 0, ComoSeFai = "Escribir hola", Linea = "Teclea as letras h o l a", Plataforma = "Teclado" },
                new Comando { Id = 1, ComoSeFai = "Pañar berzas", Linea = "Compra un carretillo", Plataforma = "Horta" },
                new Comando { Id = 2, ComoSeFai = "Fai un bolico", Linea = "Fariña auga sal e levadura para facer pan", Plataforma = "Forno" }
            };

            return comandos;
        }

        public void ActualizarComando(Comando cmd)
        {
            throw new System.NotImplementedException();
        }

        public void BorrarComando(Comando cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}