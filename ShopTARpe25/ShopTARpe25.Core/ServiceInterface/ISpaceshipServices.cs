using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;


namespace ShopTARpe25.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceship> Create(SpaceshipDto dto);
        Task<Spaceship> DetailsAsync(Guid id);
        Task<Spaceship> Update(SpaceshipDto dto);
        Task<Spaceship> Delete(Guid id);
    }
}
