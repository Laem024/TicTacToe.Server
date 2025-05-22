using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Shared.Enums;
using TicTacToe.Shared.Responses;

namespace TicTacToe.API.Common
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public readonly ILogger _logger;

        protected ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            var traceID = HttpContext.TraceIdentifier;

            switch (result.Type)
            {
                case ResultType.Success:
                {
                    return Ok(new
                    {
                        message = result.Message,
                        status = StatusCodes.Status200OK,
                    });
                }

                case ResultType.NotFound:
                {
                    _logger.LogWarning("Recurso no encontrado. TraceId: {TraceId}. Detalle: {Detail}", traceID, result.Message);

                    return NotFound(new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                        Title = "Recurso no encontrado",
                        Status = StatusCodes.Status404NotFound,
                        Detail = result.Message,
                        Instance = traceID
                    });
                }

                case ResultType.BadRequest:
                {
                    _logger.LogWarning("Solicitud incorrecta. TraceId: {TraceId}. Error: {Error}", traceID, result.Message);

                    return BadRequest(new ValidationProblemDetails(
                        new Dictionary<string, string[]>
                        {
                            { "General", new[] { result.Message ?? "La solicitud no es válida." } }
                        })
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                        Title = "Ocurrieron uno o más errores de validación.",
                        Status = StatusCodes.Status400BadRequest,
                        Instance = traceID
                    });
                }

                default:
                {
                    _logger.LogError("Error inesperado. TraceId: {TraceId}.", traceID);

                    return StatusCode(500, new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                        Title = "Error interno del servidor",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = "Ocurrió un error inesperado.",
                        Instance = traceID
                    });
                }
            };
        }
    }
}