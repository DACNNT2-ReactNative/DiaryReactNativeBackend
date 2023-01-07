using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Implementations;
using DiaryReactNativeBackend.RequestModels.Device;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DiaryReactNativeBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IDeviceLogic _deviceLogic;

    public DeviceController(IConfiguration configuration, IDeviceLogic deviceLogic)
    {
        _configuration = configuration;
        _deviceLogic = deviceLogic;
    }

    [HttpPost]
    [Route("create-device")]
    public async Task<IActionResult> CreateDevice(CreateDeviceRequestModel requestModel)
    {
        try
        {
            var deviceCreated = await _deviceLogic.SaveDevice(requestModel);
            return Ok(deviceCreated);
        }
        catch
        {
            return BadRequest("Tạo thiết bị không thành công.");
        }
    }
}
