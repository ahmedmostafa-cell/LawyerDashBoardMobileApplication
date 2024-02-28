using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AlMohamyProject.Dtos
{
    public class ChatHistoryDtos
    {
       

        public Guid? ConsultingId { get; set; }

        public string SenderId { get; set; }

      


      


        public string SenderText { get; set; }


        public string SenderAudio { get; set; }


        public string SenderDocument { get; set; }

        public string RecieverId { get; set; }


        public IFormFile RequestDocumentt { get; set; }
       


        public IFormFile RequestAudioo { get; set; }


    }
}
