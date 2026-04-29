using FitCore_API.Controllers;
using FitCoreAPI.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class EquipmentController : BaseController
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
}