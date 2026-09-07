

using TARge25SHop.Core.Domain;
using TARge25SHop.Core.Dto;

namespace TARge25SHop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceship> Create(SpaceshipDto dto);
    }
}
