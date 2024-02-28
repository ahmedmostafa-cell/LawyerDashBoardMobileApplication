using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface PaymentGateService
    {
        List<TbPaymentGates> getAll();
        bool Add(TbPaymentGates client);
        bool Edit(TbPaymentGates client);
        bool Delete(TbPaymentGates client);


    }
    public class ClsPaymentGates : PaymentGateService
    {
        AlMohamyDbContext ctx;

        public ClsPaymentGates(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbPaymentGates> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbPaymentGates> lstMainConsultings = ctx.TbPaymentGatess.ToList();

            return lstMainConsultings;
        }

        public bool Add(TbPaymentGates item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbPaymentGatess.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbPaymentGates item)
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

        public bool Delete(TbPaymentGates item)
        {
            try
            {
                foreach (var i in ctx.TbConsultingEstablishes)
                {
                    if (i.PaymentGatesId == item.PaymentGatesId)
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
