using Business.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebConfigurationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        IConfigurationAppService _configurationAppService;

        public ConfigurationController(IConfigurationAppService configurationAppService)
        {
            _configurationAppService = configurationAppService;
        }

        [HttpGet("{applicationName}")]
        public ActionResult RefreshConfigurations(string applicationName)
        {
            _configurationAppService.RefreshConfigurations(applicationName);
            return Ok("Configurations refreshed successfully");
        }

        [HttpGet("getvaluebykey/{key}")]
        public IActionResult GetValueByKey(string key)
        {
            var value = _configurationAppService.GetValue<string>(key);
            var result = new { key = value };
            return new JsonResult(result);
        }


        [HttpGet("getAllConfigurations")]
        public IActionResult GetAllConfigurations()
        {
            var configurations = _configurationAppService.GetAllConfigurations();
            return new JsonResult(configurations);
        }

        [HttpPost]
        public IActionResult AddConfiguration([FromBody] ConfigurationDto configDto)
        {
            var config = new t_Configurations
            {
                ApplicationName = configDto.ApplicationName,
                Name = configDto.Name,
                Value = configDto.Value,
                Type = configDto.Type,
                IsActive = configDto.IsActive
            };

            _configurationAppService.AddConfiguration(config);
            return CreatedAtAction(nameof(RefreshConfigurations), new { applicationName = config.ApplicationName }, config);
        }

        [HttpPut]
        public IActionResult UpdateConfiguration([FromBody] t_Configurations config)
        {
            _configurationAppService.UpdateConfiguration(config);
            return Ok("Configuration updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfiguration(int id)
        {
            _configurationAppService.DeleteConfiguration(id);
            return Ok("Configuration deleted successfully");
        }
    }
}
