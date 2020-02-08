using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BossTracker
{
    public class BossTracker : Mod, ITogglableMod
    {
        internal static BossTracker instance;
        internal static GameObject canvas;
        internal static GameObject text;

        public override void Initialize()
        {
            instance = this;
            bossCount = 0;
            ModHooks.Instance.SetPlayerBoolHook += CountBosses;
            canvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, new Vector2(1920, 1080));
            GameObject.DontDestroyOnLoad(canvas);
            UpdateUI();
        }

        public override string GetVersion()
        {
            return "1.0";
        }

        private static void CountBosses(string boolName, bool value)
        {
            PlayerData.instance.SetBoolInternal(boolName, value);

            if (boolList.Contains(boolName))
            {
                bossCount = 0;
                foreach (string b in boolList)
                {
                    if (PlayerData.instance.GetBool(b)) bossCount++;
                }
                instance.Log($"{bossCount} bosses have now been killed.");
                UpdateUI();
            }

        }

        private static void UpdateUI()
        {
            if (text == null)
            {
                text = CanvasUtil.CreateTextPanel(canvas, $"Bosses: {bossCount}", 24, TextAnchor.MiddleCenter,
                    new CanvasUtil.RectData(new Vector2(180, 54), new Vector2(-900, -480)));
            }
            else text.GetComponent<Text>().text = $"Bosses: {bossCount}";
        }

        public void Unload()
        {
            ModHooks.Instance.SetPlayerBoolHook -= CountBosses;
            Object.Destroy(canvas);
        }

        public static int bossCount;

        public static readonly string[] boolList =
        {
            // bosses - hunters journal bools seem like the safest, where available
            nameof(PlayerData.killedMawlek),
            nameof(PlayerData.killedFalseKnight),
            nameof(PlayerData.killedBigFly), // gruz mother
            nameof(PlayerData.killedBigBuzzer), // vengefly king
            nameof(PlayerData.killedHornet),
            nameof(PlayerData.killedMegaMossCharger),
            nameof(PlayerData.killedMantisLord),
            nameof(PlayerData.killedMageKnight),
            nameof(PlayerData.killedMageLord),
            nameof(PlayerData.killedDungDefender),
            nameof(PlayerData.killedFlukeMother),
            nameof(PlayerData.killedMegaBeamMiner),
            nameof(PlayerData.defeatedMegaBeamMiner2),
            nameof(PlayerData.killedInfectedKnight),
            nameof(PlayerData.killedMimicSpider), // nosk???
            nameof(PlayerData.killedBlackKnight),
            nameof(PlayerData.hornetOutskirtsDefeated),
            nameof(PlayerData.killedJarCollector),
            nameof(PlayerData.killedMegaJellyfish),
            nameof(PlayerData.killedTraitorLord),
            nameof(PlayerData.killedHiveKnight),
            nameof(PlayerData.killedZote),
            nameof(PlayerData.killedOblobble),
            nameof(PlayerData.killedLobsterLancer), // god tamer???
            nameof(PlayerData.killedGrimm),
            nameof(PlayerData.killedHollowKnight),

            // dream warriors
            nameof(PlayerData.killedGhostXero),
            nameof(PlayerData.killedGhostHu),
            nameof(PlayerData.killedGhostAladar), // gorb??
            nameof(PlayerData.killedGhostMarmu),
            nameof(PlayerData.killedGhostNoEyes),
            nameof(PlayerData.killedGhostGalien),
            nameof(PlayerData.killedGhostMarkoth),

            // dream bosses - no journal entries here
            nameof(PlayerData.falseKnightDreamDefeated),
            nameof(PlayerData.mageLordDreamDefeated),
            nameof(PlayerData.infectedKnightDreamDefeated),
            nameof(PlayerData.killedGreyPrince),
            nameof(PlayerData.killedWhiteDefender),
            nameof(PlayerData.killedNightmareGrimm),
            nameof(PlayerData.killedFinalBoss),

            // pantheon exclusive bosses
            nameof(PlayerData.killedNailBros),
            nameof(PlayerData.killedPaintmaster),
            nameof(PlayerData.killedNailsage),
            nameof(PlayerData.killedHollowKnightPrime),
            // do the others just not have bools??
            // winged nosk
            // sisters of battle
            // absolute radiance
        };
    }
}
