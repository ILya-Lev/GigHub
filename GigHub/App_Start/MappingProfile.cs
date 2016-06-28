using AutoMapper;
using GigHub.Dto;
using GigHub.Models;

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