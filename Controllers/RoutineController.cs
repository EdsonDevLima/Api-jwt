using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers{
    public class RoutineController:ControllerBase{
        [HttpGet]
        [Route("List")]
        [Authorize]
        public async Task<IActionResult> ListUser(){
            return Ok(new {message = "rota de usuarios"});
        }
    }
}