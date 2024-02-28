using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface MainConsultingService
    {
        List<TbMainConsulting> getAll();
        bool Add(TbMainConsulting client);
        bool Edit(TbMainConsulting client);
        bool Delete(TbMainConsulting client);


    }
    public class ClsMainConsulting : MainConsultingService
    {
        AlMohamyDbContext ctx;
        
        public ClsMainConsulting(AlMohamyDbContext context)
        {
            ctx = context;
           
        }
        public List<TbMainConsulting> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbMainConsulting> lstMainConsultings = ctx.TbMainConsultings.ToList();

            return lstMainConsultings;
        }

        public bool Add(TbMainConsulting item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbMainConsultings.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbMainConsulting item)
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

        public bool Delete(TbMainConsulting item)
        {
            try
            {

                foreach (var i in ctx.TbSubMainConsultings)
                {
                    if (i.MainConsultingId == item.MainConsultingId)
                    {
                        return false;
                    }
                    

                }
                foreach (var i in ctx.TbConsultingEstablishes)
                {
                    if (i.MainConsultingId == item.MainConsultingId.ToString())
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
