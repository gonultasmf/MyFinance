using AutoMapper;

namespace MyFinance.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMaps();
    }

    private void CreateMaps()
    {
        CreateMap<OperationItem, OperationItemsVM>()
            .ForMember(dist => dist.Date, opt => opt.MapFrom(src => src.Date.ToString("dd.MM.yyyy HH:mm")))
            .ForMember(dist => dist.Color, opt => opt.MapFrom(src => src.Color == nameof(Green) ? Green : Red))
            .ForMember(dist => dist.Amount, opt => opt.MapFrom(src => $"{src.Amount} ₺"));
    }
}
