using Microsoft.EntityFrameworkCore;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;


namespace ShopTARpe25.ApplicationServices.Services
{


    public class SpaceshipServices : ISpaceshipServices
    {
        private readonly ShopTARpe25Context _context;

        public SpaceshipServices
            (
                ShopTARpe25Context context
            )
        {
            _context = context;
        }
        public async Task<Spaceship> Create(SpaceshipDto dto)
        {
            Spaceship domain = new();

            domain.Id = dto.Id;
            domain.Name = dto.Name;
            domain.Classification = dto.Classification;
            domain.BuiltDate = dto.BuiltDate;
            domain.Crew = dto.Crew;
            domain.EnginePower = dto.EnginePower;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = dto.ModifiedAt;

            //siia tuleb kood, mis salvestab domain
            //objekti andmebaasi
            //tuleb kasutada repository'd, mis
            //on defineeritud Core projektis
            //konstruktori kaudu tuleb injectida repository

            await _context.Spaceships.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        //siia teha meetod nimega DetailsAsync
        //see ainult p'rib andmed contextist
        public async Task<Spaceship> DetailsAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
