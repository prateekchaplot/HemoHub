using System.ComponentModel.DataAnnotations;

namespace Backend.Enums;

public enum BloodGroup
{
    [BloodGroupAttribute("A+")]
    APositive,

    [BloodGroupAttribute("A-")]
    ANegative,

    [BloodGroupAttribute("B+")]
    BPositive,

    [BloodGroupAttribute("B-")]
    BNegative,

    [BloodGroupAttribute("AB+")]
    ABPositive,

    [BloodGroupAttribute("AB-")]
    ABNegative,

    [BloodGroupAttribute("O+")]
    OPositive,

    [BloodGroupAttribute("O-")]
    ONegative
}

public class BloodGroupAttribute : Attribute
{
    public string Name { get; set; }
    public BloodGroupAttribute(string name)
    {
        Name = name;
    }
}