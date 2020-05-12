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
    [StaticConstructorOnStartup]
    public class ZonesAndStorageModStartup {
        static ZonesAndStorageModStartup() {
            var harmony = new HarmonyLib.Harmony("net.littlewhitemouse.RimWorld.ZonesAndStorage");
            harmony.PatchAll();
        }
    }

    internal class Debug
    {
        [Conditional("DEBUG")]
        internal static void Log(string s)
        {
            Verse.Log.Message(s);
        }

        [Conditional("DEBUG")]
        internal static void Warning(string s)
        {
            Verse.Log.Warning(s);
        }

        [Conditional("DEBUG")]
        internal static void Error(string s)
        {
            Verse.Log.Error(s);
        }
    }
}
