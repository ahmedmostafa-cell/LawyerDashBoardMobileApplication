using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface ConsultingTypeService
    {
        List<TbConsultingType> getAll();
        bool Add(TbConsultingType client);
        bool Edit(TbConsultingType client);
        bool Delete(TbConsultingType client);


    }
    public class ClsConsultingType : ConsultingTypeService
    {
        AlMohamyDbContext ctx;

        public ClsConsultingType(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbConsultingType> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbConsultingType> lstConsultingTypes = ctx.TbConsultingTypes.ToList();

            return lstConsultingTypes;
        }

        public bool Add(TbConsultingType item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbConsultingTypes.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbConsultingType item)
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

        public bool Delete(TbConsultingType item)
        {
            try
            {
                foreach (var i in ctx.TbConsultingEstablishes)
                {
                    if (i.ConsultingTypeId == item.ConsultingTypeId.ToString())
                    {
                        return false;
                    }


                }
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
