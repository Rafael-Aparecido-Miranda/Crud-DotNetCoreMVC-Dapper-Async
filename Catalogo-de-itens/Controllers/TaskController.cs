using Catalogo_Itens.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExemploController : ControllerBase
    {
        public readonly ITaskService _taskService;
        public ExemploController(ITaskService taskService) 
        {
            _taskService = taskService;
        }
        /// <summary>
        /// Retorna uma lista de itens.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("ListaItens")]
        [SwaggerResponse(200, "Lista de itens retornada com sucesso.")]
        public async Task<ActionResult<List<string>>> GetItens()
        {
            var tasks =  await _taskService.GetTaskAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Obtém um item por ID.
        /// </summary>
        /// <param name="id">ID do item.</param>
        [HttpGet("{id}")]
        [SwaggerOperation("ObtemItemPorId")]
        [SwaggerResponse(200, "Item encontrado.", typeof(Task))]
        [SwaggerResponse(404, "Item não encontrado.")]
        public async Task <ActionResult<Task>> GetItem(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if  (task == null) { return NotFound(); }
            return Ok(task);
        }

        /// <summary>
        /// Cria um novo item.
        /// </summary>
        /// <param name="item">Dados do novo item.</param>
        [HttpPost]
        [SwaggerOperation("CriaNovoItem")]
        [SwaggerResponse(201, "Item criado com sucesso.", typeof(Task))]
        [SwaggerResponse(400, "Dados inválidos.")]
        public async Task<ActionResult<Task>> CreateItem([FromBody] Task task)
        {
            if (task == null) { return BadRequest("Erro estranho"); }
            
            await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetItens), new { id = task.Id }, task);
        }

        /// <summary>
        /// Atualiza um item existente.
        /// </summary>
        /// <param name="id">ID do item a ser atualizado.</param>
        /// <param name="item">Novos dados do item.</param>
        [HttpPut("{id}")]
        [SwaggerOperation("AtualizaItem")]
        [SwaggerResponse(200, "Item atualizado com sucesso.", typeof(Task))]
        [SwaggerResponse(400, "Dados inválidos.")]
        [SwaggerResponse(404, "Item não encontrado.")]
        public async Task<ActionResult<Task>> UpdateItem(int id, [FromBody] Task task)
        {
            if (task == null || task.Id != id) { return BadRequest("Erro Estranho"); }
            await _taskService.UpdateTaskAsync(task);
            return Ok(task);
        }

        /// <summary>
        /// Exclui um item.
        /// </summary>
        /// <param name="id">ID do item a ser excluído.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation("ExcluiItem")]
        [SwaggerResponse(204, "Item excluído com sucesso.")]
        [SwaggerResponse(404, "Item não encontrado.")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
