using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;

namespace ShopTARpe25.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship domain);
        void FilesToApi(KindergartenDto dto, Kindergarten domain);

        Task<FileToApi> RemoveImageFromApi(FilesToApiDto dto);

        Task<List<FileToApi>> RemoveImagesFromApi(FilesToApiDto[] dto);
    }
}

