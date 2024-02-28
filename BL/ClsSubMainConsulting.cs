using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface SubMainConsultingService
    {
        List<TbSubMainConsulting> getAll();
        bool Add(TbSubMainConsulting client);
        bool Edit(TbSubMainConsulting client);
        bool Delete(TbSubMainConsulting client);


    }

    public class ClsSubMainConsulting : SubMainConsultingService
    {
        AlMohamyDbContext ctx;
       
        public ClsSubMainConsulting(AlMohamyDbContext context)
        {
            ctx = context;
           
        }
        public List<TbSubMainConsulting> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbSubMainConsulting> lstSubMainConsultings = ctx.TbSubMainConsultings.ToList();

            return lstSubMainConsultings;
        }

        public bool Add(TbSubMainConsulting item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbSubMainConsultings.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbSubMainConsulting item)
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

        public bool Delete(TbSubMainConsulting item)
        {
            try
            {
                foreach (var i in ctx.TbConsultingEstablishes)
                {
                    if (i.SubConsultingId == item.SubMainConsultingId.ToString())
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
