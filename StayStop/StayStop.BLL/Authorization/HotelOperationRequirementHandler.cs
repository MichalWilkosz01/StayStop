﻿using Microsoft.AspNetCore.Authorization;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Authorization
{
    public class HotelOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Hotel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Hotel hotel)
        {
            if (requirement.ResourceOperation==ResourceOperation.Read || requirement.ResourceOperation==ResourceOperation.Create) 
            {
                context.Succeed(requirement);
            }
            var userId = context.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (hotel.OwnerId==int.Parse(userId)) 
            {
                context.Succeed(requirement);           
            }

            return Task.CompletedTask;
        }
    }
}
