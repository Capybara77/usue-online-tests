using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace usue_online_tests.Dto;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TestDto
{
    public string[] CheckBoxes { get; set; } = Array.Empty<string>();

    public int Hash { get; set; }

    public string Text { get; set; } = default!;

    public int TestID { get; set; }

    public IEnumerable<string> Pictures { get; set; } = Array.Empty<string>();
}