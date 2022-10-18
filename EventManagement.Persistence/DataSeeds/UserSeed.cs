using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Persistence.DataSeeds
{
    internal static class UserSeed
    {
        // Şifreler asdfghjkl1, gwendolin için asdfghjkl2
        public static object[] Objects => new object[] {
            new { Id = 1, FirstName = "Admin", LastName = "Admin", PasswordHash = "$2a$11$LTwig7Q1qvqrGCsBES3YaeUJ4/NPdyUOvgIwVu/TNyfVGuL9DPOHW", Email = "admin@admin.com" },
            new { Id = 2, FirstName = "Gwendolin", LastName = "Connely", PasswordHash = "$2a$11$GZnQc1ePrcdTGa4sSfpsFeZEIjdxMFMPtuSI5UAN89PvFxJrHv9w.", Email = "gwendolyn.connelly@ethereal.email" },
            new { Id = 3, FirstName = "John", LastName = "Doe", PasswordHash = "$2a$11$uyjFi3yqEJiFpZu4IkCxfOmjL.EKPQEu4pobskKZKM3UU3xYDkwyO", Email = "john.doe@admin.com" },
            new { Id = 4, FirstName = "Jane", LastName = "Doe", PasswordHash = "$2a$11$uyjFi3yqEJiFpZu4IkCxfOmjL.EKPQEu4pobskKZKM3UU3xYDkwyO", Email = "jane.doe@admin.com" },
        };
    }
}
