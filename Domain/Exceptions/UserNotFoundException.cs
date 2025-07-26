using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UserNotFoundException(string username) : NotFoundException($"No User With This username : {username} Was Found")
    {
    }
}
