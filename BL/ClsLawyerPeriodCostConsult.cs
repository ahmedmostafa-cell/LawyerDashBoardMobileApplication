using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface LawyerPeriodCostConsultService
    {
        List<TbLawyerPeriodCostConsult> getAll();
        bool Add(TbLawyerPeriodCostConsult client);
        bool Edit(TbLawyerPeriodCostConsult client);
        bool Delete(TbLawyerPeriodCostConsult client);


    }
    public class ClsLawyerPeriodCostConsult : LawyerPeriodCostConsultService
    {
        AlMohamyDbContext ctx;

        public ClsLawyerPeriodCostConsult(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbLawyerPeriodCostConsult> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbLawyerPeriodCostConsult> lstLawyerPeriodCostConsults = ctx.TbLawyerPeriodCostConsults.ToList();

            return lstLawyerPeriodCostConsults;
        }

        public bool Add(TbLawyerPeriodCostConsult item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbLawyerPeriodCostConsults.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbLawyerPeriodCostConsult item)
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

        public bool Delete(TbLawyerPeriodCostConsult item)
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
