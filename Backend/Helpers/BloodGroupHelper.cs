using System.Reflection;
using Backend.Enums;

namespace Backend.Helpers;

public static class BloodGroupHelper
{
    public static string ToString(BloodGroup bloodGroup)
    {
        FieldInfo fieldInfo = bloodGroup.GetType().GetField(bloodGroup.ToString());
        BloodGroupAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(BloodGroupAttribute), false) as BloodGroupAttribute[];

        if (attributes.Length > 0)
        {
            return attributes[0].Name;
        }

        return bloodGroup.ToString(); // Fallback if no attribute found
    }

    public static bool TryToBloodGroup(this string bloodGroupString, out BloodGroup bloodGroup)
    {
        bloodGroup = new();
        foreach (BloodGroup blood in Enum.GetValues(typeof(BloodGroup)))
        {
            if (ToString(blood) == bloodGroupString)
            {
                bloodGroup = blood;
                return true;
            }
        }

        return false;
    }
}
