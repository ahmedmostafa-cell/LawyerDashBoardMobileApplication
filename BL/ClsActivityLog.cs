using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface ActivityLogService
    {
        List<TbActivityLog> getAll();
        bool Add(TbActivityLog client);
        bool Edit(TbActivityLog client);
        bool Delete(TbActivityLog client);


    }
    public class ClsActivityLog : ActivityLogService
    {
        AlMohamyDbContext ctx;
        private readonly UserManager<ApplicationUser> userManager;
        public ClsActivityLog(AlMohamyDbContext context , UserManager<ApplicationUser> UserManager)
        {
            ctx = context;
            userManager = UserManager;
        }
        public List<TbActivityLog> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbActivityLog> lstActivityLog = ctx.TbActivityLogs.ToList();

            return lstActivityLog;
        }

        public bool Add(TbActivityLog item)
        {
            try
            {
                if(item.UserName ==null) 
                {
                    item.UserName = userManager.Users.Where(a => a.Id == item.UserId).FirstOrDefault().UserName;
                }
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbActivityLogs.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbActivityLog item)
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

        public bool Delete(TbActivityLog item)
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
