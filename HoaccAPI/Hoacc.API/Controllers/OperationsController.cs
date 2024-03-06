using HoaccAPI.Mappers;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccIServices.Operations;
using HoaccIServices.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet("{operationId:min(1)}", Name = "GetOperations")]
    public async Task<IActionResult> GetOperations(int operationId)
    {
        var operations = await _operationsService.GetOperations(operationId);
        if (operations != null)
            return Ok(OperationsToOperationsViewModelMapper.OperationsToOperationsViewModel(operations));
        return NotFound();
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetOperationsByUser(int userId)
    {
        // Extract user identity claim from the JWT token
        var userIdentityClaim = User.Claims.FirstOrDefault(c => c.Type == "UserIdentity")?.Value;

        // Ensure the user identity claim matches the requested user's identity
        if (userIdentityClaim != $"User:{userId}")
        {
            return Forbid(); // Return 403 Forbidden if the user is not authorized
        }

        var operations = await _operationsService.GetOperationsByUser(userId);

        if (operations == null || !operations.Any()) return NotFound($"No operations found for user ID {userId}");

        return Ok(operations);
    }

    [Authorize]
    [HttpGet("user/{userId}/filtered")]
    public async Task<IActionResult> GetFilteredOperationsByUser(
        int userId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string type = "outcome" // Default to "outcome"
    )
    {
        // Extract user identity claim from the JWT token
        var userIdentityClaim = User.Claims.FirstOrDefault(c => c.Type == "UserIdentity")?.Value;

        // Ensure the user identity claim matches the requested user's identity
        if (userIdentityClaim != $"User:{userId}")
        {
            return Forbid(); // Return 403 Forbidden if the user is not authorized
        }

        var operations = await _operationsService.GetFilteredOperationsByUser(userId, startDate, endDate, type);

        if (operations == null || !operations.Any()) return NotFound($"No operations found for user ID {userId}");

        return Ok(operations);
    }

    [Authorize]
    [HttpGet("user/{userId}/profit")]
    public async Task<IActionResult> GetProfitChartDataByUser(
        int userId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate
    )
    {
        try
        {
            // Extract user identity claim from the JWT token
            var userIdentityClaim = User.Claims.FirstOrDefault(c => c.Type == "UserIdentity")?.Value;

            // Ensure the user identity claim matches the requested user's identity
            if (userIdentityClaim != $"User:{userId}")
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }

            var profitChartData = await _operationsService.GetProfitDataForUser(userId, startDate, endDate);

            if (profitChartData == null) return NotFound($"No profit data found for user ID {userId}");

            return Ok(profitChartData);
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            return StatusCode(500, "Internal server error");
        }
    }

    [ValidateModel]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOperations createOperations)
    {
        var operations = await _operationsService.CreateOperations(createOperations);
        return Created(operations.OperationId.ToString(),
            OperationsToOperationsViewModelMapper.OperationsToOperationsViewModel(operations));
    }

    [ValidateModel]
    [Authorize]
    [HttpPut("{operationId:min(1)}", Name = "EditOperations")]
    public async Task<IActionResult> EditOperations([FromBody] EditOperations editOperations, int operationId)
    {
        await _operationsService.EditOperations(operationId, editOperations);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{operationId:min(1)}", Name = "RemoveOperations")]
    public async Task<IActionResult> RemoveOperations(int operationId)
    {
        await _operationsService.RemoveOperations(operationId);
        return NoContent();
    }
}