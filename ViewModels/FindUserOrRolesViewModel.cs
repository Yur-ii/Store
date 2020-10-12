using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;

namespace Store.ViewModels
{
    public class FindUserOrRolesViewModel
    {
        public User User { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
