using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.AI;
using System.Diagnostics;
using HarmonyLib;

namespace ZonesAndStorage {
    [HarmonyPatch(typeof(Verse.Zone), "CheckContiguous")]
    static class Nuke_Zone_CheckContinguous {
        static bool Prefix() {
            return false; // Do not run CheckContiguous
        }
    }
}
