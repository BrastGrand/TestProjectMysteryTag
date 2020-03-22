using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    [CreateAssetMenu(fileName = "DefaultProfile", menuName = "Data/DefaultProfile")]
    public class DefaultProfile : ScriptableObject
    {
        public GameData profileData;
    }
}
