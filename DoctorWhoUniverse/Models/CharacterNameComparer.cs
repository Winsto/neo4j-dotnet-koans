using System;
using System.Collections.Generic;

namespace DoctorWhoUniverse.Models
{
    public class CharacterNameComparer:IEqualityComparer<Character>
    {
        public bool Equals(Character x, Character y)
        {
            return x.CharacterName.Equals(y.CharacterName, StringComparison.Ordinal);
        }

        public int GetHashCode(Character obj)
        {
            return obj.CharacterName.GetHashCode();
        }
    }
}
