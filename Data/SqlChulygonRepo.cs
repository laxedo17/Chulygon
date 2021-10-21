using System;
using System.Collections.Generic;
using System.Linq;
using Chulygon.Models;

namespace Chulygon.Data
{
    public class SqlChulygonRepo : IChulygonRepo
    {
        private readonly ChulygonContext _context;

        /// <summary>
        /// De aqui obtemos o repositorio para recoller datos da base de datos, utilizando por suposto Dependency Injection
        /// </summary>
        /// <param name="context">Inxectamos o contexto da base de datos para facer dependency injection, que poboará o atributo context nun campo _context que sera private read-only. Mismo sistema que con Configuration na clase Startup.cs</param>
        public SqlChulygonRepo(ChulygonContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Comando GetComandoPorId(int id)
        {
            return _context.Comandos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Comando> GetTodosComandos()
        {
            return _context.Comandos.ToList();
        }

        public void CrearComando(Comando cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Comandos.Add(cmd);
        }

        public void ActualizarComando(Comando cmd)
        {
            //Non fai nada. Non e necesario para un update tocar aqui, porque diso tomara conta o DbContext
        }

        public void BorrarComando(Comando cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Comandos.Remove(cmd);
        }
    }
}