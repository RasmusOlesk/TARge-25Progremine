using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;


namespace TARge25Shop.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly TARge25ShopContext _context;

        public RealEstateServices
            (
                TARge25ShopContext context
            )
        {
            _context = context;
        }

        //see meetod on vaja controlleris esile kutsuda
        //peab lisama interface, et kutsuda see meetod välja
        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            //siin peab tegema vaheinstansi dto ja domain vahel,
            //et andmed liiguvad dto-st domain objekt
            RealEstate realEstate = new();

            realEstate.Id = Guid.NewGuid();
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.ModifiedAt = DateTime.Now;
        

            //andmete salvestamine andmebaasi
            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        //teha update meetod, mis võtab vastu dto ja uuendab olemasolevat kosmoselaeva
        public async Task<RealEstate> Modify(RealEstateDto dto)
        {
            //siin peab tegema vaheinstansi dto ja domain vahel,
            //et andmed liiguvad dto-st domain objekt
            RealEstate realEstate = new();

            realEstate.Id = dto.Id;
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = dto.CreatedAt;
            realEstate.ModifiedAt = DateTime.Now;
            

            //andmete uuendamine andmebaasis
            _context.RealEstates.Update(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        public async Task<RealEstate> DetailAsync(Guid id)
        {
            var realestate = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return realestate;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.RealEstates.Remove(result);

            await _context.SaveChangesAsync();

            return result;
        }


        public Task<RealEstate> Modified(RealEstateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
