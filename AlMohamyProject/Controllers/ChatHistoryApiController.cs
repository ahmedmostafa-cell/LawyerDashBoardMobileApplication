using AlMohamyProject.Dtos;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlMohamyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistoryApiController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        ChatService chatService;
        UserManager<ApplicationUser> Usermanager;
       
        AlMohamyDbContext ctx;
        public ChatHistoryApiController(IAccountRepository accountRepository, ChatService ChatService,UserManager<ApplicationUser> usermanager,AlMohamyDbContext context)
        {
            chatService = ChatService;
             ctx = context;
            Usermanager = usermanager;
            _accountRepository = accountRepository;

        }
        // GET: api/<ChatHistoryApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChatHistoryApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("{customerchoose}/{lawyerchoose}")]
        public IEnumerable<TbChat> Get(string customerchoose, string lawyerchoose)
        {
            List<TbChat> lstChat = new List<TbChat>();
            if (customerchoose == null && lawyerchoose == null)
            {
                lstChat = chatService.getAll().OrderBy(A => A.CreatedDate).ToList();
            }
            else if (customerchoose != null && lawyerchoose == null)
            {
                lstChat = chatService.getAll().OrderBy(A => A.CreatedDate).Where(a=> a.SenderId == customerchoose || a.RecieverId == customerchoose).ToList();
            }
            else if (customerchoose == null && lawyerchoose != null)
            {
                lstChat = chatService.getAll().OrderBy(A => A.CreatedDate).Where(a => a.SenderId == lawyerchoose || a.RecieverId == lawyerchoose).ToList();
            }
            else if (customerchoose != null && lawyerchoose != null)
            {
                lstChat = chatService.getAll().OrderBy(A => A.CreatedDate).Where(a => (a.SenderId == lawyerchoose || a.RecieverId == lawyerchoose) && (a.SenderId == customerchoose || a.RecieverId == customerchoose)).ToList();
            }
           



            return lstChat;
        }

        // POST api/<ChatHistoryApiController>
        [HttpPost("SaveChat")]
        public async Task<IActionResult> SaveChat([FromForm] ChatHistoryDtos model)
        {


            var result = await _accountRepository.SaveChat(model);

            if (result.Value.MyString == "لم يتم حفظ المحادثة")
            {
                return BadRequest("The Chat Is Not Saved");

            }
            else
            {


                return Ok(result);

            }




        }

        // PUT api/<ChatHistoryApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatHistoryApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
