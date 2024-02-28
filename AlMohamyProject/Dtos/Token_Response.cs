using System;

namespace AlMohamyProject.Dtos
{
    public class Token_Response
    {
        public string id_token { get; set; }
       


        public bool IsSuccess()
        {
            if (String.IsNullOrWhiteSpace(id_token) )
            {
                return false;
            }

            return true;
        }
    }
}
