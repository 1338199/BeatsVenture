using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Player;

namespace Assets.Scripts
{
    [InitializeOnLoad]
    class OnLoadFunc
    {
        static OnLoadFunc()
        {
            skillUtils.saveSkills(new bool[4] { false, false, false, false });
        }
    }
}
