using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface ComplainsAndSuggestionsService
    {
        List<TbComplainsAndSuggestion> getAll();
        Guid? Add(TbComplainsAndSuggestion client);
        bool Edit(TbComplainsAndSuggestion client);
        bool Delete(TbComplainsAndSuggestion client);


    }
    public class ClsComplainsAndSuggestions : ComplainsAndSuggestionsService
    {
        AlMohamyDbContext ctx;

        public ClsComplainsAndSuggestions(AlMohamyDbContext context)
        {
            ctx = context;
        }
        public List<TbComplainsAndSuggestion> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbComplainsAndSuggestion> lstComplainsAndSuggestions = ctx.TbComplainsAndSuggestions.ToList();

            return lstComplainsAndSuggestions;
        }

        public Guid? Add(TbComplainsAndSuggestion item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.ComplaintsAndSuggestionsId = Guid.NewGuid();
                TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
                DateTimeOffset easternTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, easternTimeZone);
                item.CreatedDate = easternTime.DateTime;
                ctx.TbComplainsAndSuggestions.Add(item);
                ctx.SaveChanges();
                return item.ComplaintsAndSuggestionsId;
            }
            catch (Exception ex)
            {
                return Guid.Parse("00000000-0000-0000-0000-000000000000");

            }
        }
        public bool Edit(TbComplainsAndSuggestion item)
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

        public bool Delete(TbComplainsAndSuggestion item)
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
