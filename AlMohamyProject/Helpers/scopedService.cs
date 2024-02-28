using BL;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace AlMohamyProject.Helpers
{
    public interface ConsultingEstablishServicee
    {
        List<TbConsultingEstablish> getAll();
        bool Add(TbConsultingEstablish client);

        Task<bool> Add2(TbConsultingEstablish client);
        bool Edit(TbConsultingEstablish client);

        Task<bool> Edit3(TbConsultingEstablish client);

        TbConsultingEstablish Edit2(TbConsultingEstablish client);
        bool Delete(TbConsultingEstablish client);


    }
    public class scopedService : ConsultingEstablishServicee
    {
        AlMohamyDbContext ctx;

        public scopedService(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbConsultingEstablish> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbConsultingEstablish> lstConsultingEstablishs = ctx.TbConsultingEstablishes.ToList();

            return lstConsultingEstablishs;
        }

        public bool Add(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbConsultingEstablishes.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public async Task<bool> Add2(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbConsultingEstablishes.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public async Task<bool> Edit3(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();

                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        public TbConsultingEstablish Edit2(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();

                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                return new TbConsultingEstablish();

            }
        }

        public bool Delete(TbConsultingEstablish item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();

                ctx.Entry(item).State = EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }

}
