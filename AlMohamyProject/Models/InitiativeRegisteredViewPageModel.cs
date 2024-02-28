using System;
using System.Collections.Generic;

namespace AlMohamyProject.Models
{
    public class Names 
    {
        public string name { get; set; }

        public string birthdate { get; set; }

    }
    public class InitiativeRegisteredViewPageModel
    {

        public string Id { get; set; }

        public string InitiativeRegisteredUserName { get; set; }
        public string InitiativeRegisteredUserFamilyName { get; set; }
       
        public string InitiativeRegisteredUserAge { get; set; }
       
        public string InitiativeRegisteredUserPhoneNo { get; set; }

       


        public List<Names> Names { get; set; }
    }
}
