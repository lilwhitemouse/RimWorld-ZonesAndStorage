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
/* Allow non-continguous zones */
/* Remove check in designator for creating new zones instead of adding to the selected one */
namespace ZonesAndStorage {
    [HarmonyPatch(typeof(RimWorld.Designator_ZoneAdd), "DesignateMultiCell")]
    static class Patch_Desig_ZoneAdd_DesigMultiCell {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions,
                                                       ILGenerator generator) {
            /* When adding to an existing Zone, the code runs through all the new cells
             * being added, and does a check: are any of them adjacent?  If so*, add that
             * cell and repeat until there are no more adjacent cells to add.  (Then it
             * makes the zone do a check, but we address that elsewhere.)
             * To allow non-contiguous zones, we could just skip over the adjacent checks,
             * but finding all that is hard.  And the results of those checks all hinge on
             * that "If so*" up above.
             * So, we patch so that instead of loading the flag, we load true.
             * An easy transpile!  Yay!
             */
            foreach (var c in instructions) {
                if (c.opcode==OpCodes.Ldloc_S &&
                    ((LocalBuilder)c.operand).LocalIndex==9) { // "LdLoc.s 9"
                    var t=new CodeInstruction(OpCodes.Ldc_I4_1); // true
                    t.labels=c.labels; // in case anyone jumps here
                    // (hint, someone does)
                    yield return t;
                } else {
                    yield return c;
                }
            }
        }
    }
}
