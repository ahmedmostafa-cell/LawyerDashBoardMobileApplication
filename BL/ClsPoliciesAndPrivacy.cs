using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface PoliciesAndPrivacyService
    {
        List<TbPoliciesAndPrivacy> getAll();
        bool Add(TbPoliciesAndPrivacy client);
        bool Edit(TbPoliciesAndPrivacy client);
        bool Delete(TbPoliciesAndPrivacy client);


    }
    public class ClsPoliciesAndPrivacy : PoliciesAndPrivacyService
    {
        AlMohamyDbContext ctx;

        public ClsPoliciesAndPrivacy(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbPoliciesAndPrivacy> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbPoliciesAndPrivacy> lstPoliciesAndPrivacies = ctx.TbPoliciesAndPrivacies.ToList();

            return lstPoliciesAndPrivacies;
        }

        public bool Add(TbPoliciesAndPrivacy item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbPoliciesAndPrivacies.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbPoliciesAndPrivacy item)
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

        public bool Delete(TbPoliciesAndPrivacy item)
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
