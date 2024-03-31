using System.Collections.Generic;

namespace usue_online_tests.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }

    public string Group { get; set; }

    public bool IsDark { get; set; }

    public ICollection<UserExamResult> UserExamResults { get; set; }
}