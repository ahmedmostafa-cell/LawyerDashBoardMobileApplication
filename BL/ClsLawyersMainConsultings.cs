using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface LawyersMainConsultingsService
    {
        List<TbLawyersMainConsultings> getAll();
        bool Add(TbLawyersMainConsultings client);
        bool Edit(TbLawyersMainConsultings client);
        bool Delete(TbLawyersMainConsultings client);


    }
    public class ClsLawyersMainConsultings : LawyersMainConsultingsService
    {
        AlMohamyDbContext ctx;

        public ClsLawyersMainConsultings(AlMohamyDbContext context)
        {
            ctx = context;

        }
        public List<TbLawyersMainConsultings> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbLawyersMainConsultings> lstLawyersMainConsultings = ctx.TbLawyersMainConsultingss.ToList();

            return lstLawyersMainConsultings;
        }

        public bool Add(TbLawyersMainConsultings item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbLawyersMainConsultingss.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbLawyersMainConsultings item)
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

        public bool Delete(TbLawyersMainConsultings item)
        {
            try
            {

                
                foreach (var i in ctx.TbConsultingEstablishes)
                {
                    if (i.MainConsultingId == item.MainConsultingId.ToString() && i.LawyerId == item.LawyerId)
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
