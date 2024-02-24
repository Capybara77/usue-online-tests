using System.Collections.Generic;

namespace usue_online_tests.Requests;

public class GetTestResultRequest
{
    public int TestId { get; set; }
    public int Hash { get; set; }
    public Dictionary<string, string> FormData { get; set; }
}