using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile ()
		{
			// todo lis: there is a difference in auto mapper 4.1 and 5.0.  investigate it!
			Mapper.Initialize(config =>
			{
				config.CreateMap<ApplicationUser, UserDto>();
				config.CreateMap<Gig, GigDto>();
				config.CreateMap<Notification, NotificationDto>();
			});
		}
	}
}