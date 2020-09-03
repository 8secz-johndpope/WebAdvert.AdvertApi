 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [Route("adverts/v1")]
    [ApiController]
    public class Advert : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public Advert(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponce))]
        public async Task<IActionResult> Create (AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);               
            }
            return StatusCode(201, new CreateAdvertResponce { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {

                return StatusCode(500, exception.Message);
            }
            return new OkResult();
        }
    }
}
