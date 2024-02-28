using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface ApprovedOfficeService
    {
        List<TbApprovedOffice> getAll();
        bool Add(TbApprovedOffice client);
        bool Edit(TbApprovedOffice client);
        bool Delete(TbApprovedOffice client);


    }
    public class ClsApprovedOffice : ApprovedOfficeService
    {
        AlMohamyDbContext ctx;

        public ClsApprovedOffice(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbApprovedOffice> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbApprovedOffice> lstApprovedOffices = ctx.TbApprovedOffices.ToList();

            return lstApprovedOffices;
        }

        public bool Add(TbApprovedOffice item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbApprovedOffices.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbApprovedOffice item)
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

        public bool Delete(TbApprovedOffice item)
        {
            try
            {
                foreach (var i in ctx.TbEvaluationApprovedOffices)
                {
                    if (i.EvaluationApprovedOfficeId == item.ApprovedOfficeId)
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
