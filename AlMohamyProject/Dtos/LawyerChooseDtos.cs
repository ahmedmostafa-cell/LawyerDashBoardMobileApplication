using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlMohamyProject.Dtos
{
    public class LawyerChooseDtos
    {

        public string Id { get; set; }
       

        public string EvaluationNoOfStatrs { get; set; }

        public string EvaluationNumerical { get; set; }

        public string YearsOfExperience { get; set; }


       
        public string ShortDescription { get; set; }

        public string Cost { get; set; }


        

        public string Image { get; set; }



        public string FirstName { get; set; }

        public string? FamilyName { get; set; }


        public string consultationNo { get; set; }




    }
}
