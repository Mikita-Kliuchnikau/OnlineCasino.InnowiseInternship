using AutoMapper;

namespace GamingService.Mapping.Interfaces;

public interface IMapWith<T>
{
    virtual void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}
