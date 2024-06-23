using Application.MediatR;
using Domains.Data;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 

namespace ClaenArch.Controllers
{
 
    public class ActivitiesController : BaseApiController
    {
       // [Authorize(Roles = "Admin111")]
        [HttpGet]
        public async Task<IEnumerable<Activity>> GetActivities()
        {
           
            return await Mediator.Send(new GetAllQuery<Activity>());
        }
    }
}
