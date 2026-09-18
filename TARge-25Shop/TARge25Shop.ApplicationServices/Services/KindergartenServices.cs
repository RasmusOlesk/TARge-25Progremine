using Microsoft.EntityFrameworkCore;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;


namespace TARge25Shop.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly TARge25ShopContext _context;

        public KindergartenServices
            (
                TARge25ShopContext context
            )
        {
            _context = context;
        }

        //see meetod on vaja controlleris esile kutsuda
        //peab lisama interface, et kutsuda see meetod välja
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            //siin peab tegema vaheinstansi dto ja domain vahel,
            //et andmed liiguvad dto-st domain objekt
            Kindergarten kindergarten = new();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            //andmete salvestamine andmebaasi
            _context.Kindergartens.Add(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        //teha update meetod, mis võtab vastu dto ja uuendab olemasolevat kosmoselaeva
        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            //siin peab tegema vaheinstansi dto ja domain vahel,
            //et andmed liiguvad dto-st domain objekt
            Kindergarten KinderGarten = new();

            KinderGarten.Id = dto.Id;
            KinderGarten.GroupName = dto.GroupName;
            KinderGarten.KindergartenName = dto.KindergartenName;
            KinderGarten.TeacherName = dto.TeacherName;
            KinderGarten.ChildrenCount = dto.ChildrenCount;
            KinderGarten.CreatedAt = dto.CreatedAt;
            KinderGarten.UpdatedAt = DateTime.Now;

            //andmete salvestamine andmebaasi
            _context.Kindergartens.Update(KinderGarten);
            await _context.SaveChangesAsync();

            return KinderGarten;
        }



        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return kindergarten;



        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }



    }
}
