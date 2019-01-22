using System;
using System.Collections.Generic;
using System.Text;

namespace FitCenter.Models.ModelDto.User
{
    public class DeleteUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
