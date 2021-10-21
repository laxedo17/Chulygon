using System.Collections.Generic;
using Chulygon.Models;

namespace Chulygon.Data
{
    /// <summary>
    /// Repositorio xeral para a aplicacion, paras as operacions CRUD e modificar os recursos na base de datos
    /// </summary>
    public interface IChulygonRepo
    {
        bool SaveChanges();

        IEnumerable<Comando> GetTodosComandos(); //usamos IEnumerable porque queremos obter unha lista
        Comando GetComandoPorId(int id); //devolvee un unico comando ao usuario baseado na id que nos pase
        void CrearComando(Comando cmd);//Inicio operacions CRUD, xerar datos na Base de datos.
        void ActualizarComando(Comando cmd);
        //para operacions PUT e PATCH (Put e moi ineficiente porque tes que pasar todos os atributos do obxeto. Ineficiente, e para obxetos con moitos atributos pode ser un problema)
        //Na actualidade e preferible PATCH
        void BorrarComando(Comando cmd);
    }
}