using System.Collections.Generic;
using AutoMapper;
using Chulygon.Data;
using Chulygon.Dtos;
using Chulygon.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Chulygon.Controllers
{
    /// <summary>
    /// As clases controller, por convencion usan o nome do modelo en plural (ex. ComandosController, PlatatformasController), ao contrario que a carpeta Models que usa o nome en singular (Comando, Plataforma).
    /// 
    /// ControllerBase permitenos ter un controller para unha clase sen Views -do modelo MVC, Model View Controller-, xa que non teremos unha View neste caso. Se queremos ter soporta para vistas, escribimos Controller a secas, sen o Base.
    /// 
    /// Neste caso facemolo para manter a app o mais limpa, axil e eficiente posible porque non imos usar ningunha View.
    /// </summary>
    //api/comandos seria a ruta Route. Quita a parte controller do nome da clase e queda a ruta como /comandos
    //outra opcion seria escribir [Route("api/[controller]")] pero se modificamos o nome da clase, a ruta cambiaría tamén, co cal e millor deixala hardcoded
    [Route("api/comandos")]
    [ApiController]
    public class ComandosController : ControllerBase
    {
        private readonly IChulygonRepo _repositorio;
        private readonly IMapper _mapper;

        //necesitamos crear un constructor para que a dependencia se inxecte na clase Startup usando AddScoped, sen constructor non podemos usar a Dependency Injection
        public ComandosController(IChulygonRepo repositorio, IMapper mapper)
        {
            _repositorio = repositorio; //agora indicamoslle que todo o que entre como repositorio se asigne ao campo privado _repositorio
            _mapper = mapper; //igual que antes, con Dependency Injection agregamos servicios ao controller dos Comandos
        }
        //private readonly MockChulygonRepo _repositorio = new MockChulygonRepo(); //non e a forma adecuada, pero e un bo inicio

        //GET api/comandos
        [HttpGet]
        public ActionResult<IEnumerable<ComandoLerDto>> GetTodosComandos()
        {
            var elementosComando = _repositorio.GetTodosComandos();

            return Ok(_mapper.Map<IEnumerable<ComandoLerDto>>(elementosComando));
        }

        //GET /api/comandos/{id} --poderiamos ponher unha Route tamen
        //O metodo de arriba tamen e GET pero hai unha distincion imprescindible para chamar a API e obter un comando, ou unha lista de varios --metodo de arriba
        [HttpGet("{id}", Name = "GetComandoPorId")]
        public ActionResult<ComandoLerDto> GetComandoPorId(int id)
        {
            var elementoComando = _repositorio.GetComandoPorId(id);

            //con este if e o return evitamos devolver resposta 204 se a id que nos pide o cliente da API e null ou non existe. Devolve Error 404
            if (elementoComando != null)
            {
                return Ok(_mapper.Map<ComandoLerDto>(elementoComando));
            }
            return NotFound();
        }

        //POST api/comandos
        [HttpPost]
        public ActionResult<ComandoLerDto> CrearComando(ComandoCrearDto comandoCrearDto)
        {
            var modeloComando = _mapper.Map<Comando>(comandoCrearDto);//queremos mapear un obxeto Comando, e pasámoslle un comandoCrearDto
            _repositorio.CrearComando(modeloComando);

            _repositorio.SaveChanges();

            var comandoLerDto = _mapper.Map<ComandoLerDto>(modeloComando);

            return CreatedAtRoute(nameof(GetComandoPorId), new { Id = comandoLerDto.Id }, comandoLerDto); //Agora a API devolve Status: 201 Created e en Location devolve unha Uri coa ruta do recurso

            //Aqui documentacion completa de CreatedAtRoute: https://docs.microsoft.com/en-us/dotnet/api/system.web.http.apicontroller.createdatroute 

            //return Ok(comandoLerDto);
            //para devolver unha resposta 201, que significa Created, en vez da tipica 200
            //e tamen para pasar a direccion web URI que e parte das especificacions REST
            //houbo que facer cambios
        }

        //PUT api/comandos/{id}
        [HttpPut("{id}")]
        public ActionResult ActualizarComando(int id, ComandoActualizarDto comandoActualizarDto)
        {
            var modeloComandoDoRepo = _repositorio.GetComandoPorId(id);
            if (modeloComandoDoRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(comandoActualizarDto, modeloComandoDoRepo);

            _repositorio.ActualizarComando(modeloComandoDoRepo);

            _repositorio.SaveChanges();

            return NoContent();
        }

        //PATCH api/comandos/{1}
        [HttpPatch("{id}")]
        public ActionResult ActualizacionComandoParcial(int id, JsonPatchDocument<ComandoActualizarDto> parchearComando)
        {
            var modeloComandoDoRepo = _repositorio.GetComandoPorId(id);
            if (modeloComandoDoRepo == null)
            {
                return NotFound();
            }

            var comandoParaParchear = _mapper.Map<ComandoActualizarDto>(modeloComandoDoRepo);
            parchearComando.ApplyTo(comandoParaParchear, ModelState);//ModelState asegurase de que as validacions son correctas

            if (!TryValidateModel(parchearComando))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(comandoParaParchear, modeloComandoDoRepo);

            _repositorio.ActualizarComando(modeloComandoDoRepo);

            _repositorio.SaveChanges();

            return NoContent();
        }

        //DELETE api/comandos/{1}
        [HttpDelete("{id}")]
        public ActionResult BorrarComando(int id)
        {
            var modeloComandoDoRepo = _repositorio.GetComandoPorId(id);
            if (modeloComandoDoRepo == null)
            {
                return NotFound();
            }

            _repositorio.BorrarComando(modeloComandoDoRepo);
            _repositorio.SaveChanges();

            return NoContent();
        }
    }
}