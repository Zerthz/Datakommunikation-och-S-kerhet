using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Server.Auth
{
    public class OnlyAllowNameRequirement : AuthorizationHandler<OnlyAllowNameRequirement, HubInvocationContext>, IAuthorizationRequirement
    {
        private readonly string name;

        public OnlyAllowNameRequirement(string name)
        {
            this.name = name;
        }
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
            OnlyAllowNameRequirement requirement,
            HubInvocationContext resource)
        {
            if (context.User != null && context.User.Identity?.Name?.ToLowerInvariant() == name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
