using HoaccAPI.Mappers;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccIServices.Operations;
using HoaccIServices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HoaccAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly HoaccDbContext _context;
    private readonly IOperationsService _operationsService;

    public OperationsController(HoaccDbContext context, IOperationsService operationsService)
    {
        _context = context;
        _operationsService = operationsService;
    }

    [HttpGet("{operationId:min(1)}", Name = "GetOperations")]
    public async Task<IActionResult> GetOperations(int operationId)
    {
        var operations = await _operationsService.GetOperations(operationId);
        if (operations != null)
            return Ok(OperationsToOperationsViewModelMapper.OperationsToOperationsViewModel(operations));
        return NotFound();
    }

    [ValidateModel]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOperations createOperations)
    {
        var operations = await _operationsService.CreateOperations(createOperations);
        return Created(operations.OperationId.ToString(),
            OperationsToOperationsViewModelMapper.OperationsToOperationsViewModel(operations));
    }

    [ValidateModel]
    [HttpPut("{operationId:min(1)}", Name = "EditOperations")]
    public async Task<IActionResult> EditOperations([FromBody] EditOperations editOperations, int operationId)
    {
        await _operationsService.EditOperations(operationId, editOperations);
        return NoContent();
    }

    [HttpDelete("{operationId:min(1)}", Name = "RemoveOperations")]
    public async Task<IActionResult> RemoveOperations(int operationId)
    {
        await _operationsService.RemoveOperations(operationId);
        return NoContent();
    }
}