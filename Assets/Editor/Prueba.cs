using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(GameMaster))]
public class Prueba : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameMaster gm = (GameMaster)target;

        EditorGUILayout.Space();

        GUILayout.Label("Sistema de Modificadores", EditorStyles.boldLabel);

        #region"Inputs Modificadores"
        gm.nameINP = EditorGUILayout.TextField("Nombre", gm.nameINP);

        gm.vidaINP = (byte)EditorGUILayout.Slider("Vida (+%)", gm.vidaINP, 0, 10);

        gm.dmgINP = (byte)EditorGUILayout.Slider("Da�o (+%)", gm.dmgINP, 0, 10);

        gm.multConcienciaINP = (float)EditorGUILayout.Slider("Mult. Conciencia (+Value)", gm.multConcienciaINP, 0f, 10f);

        gm.tgpcINP = (byte)EditorGUILayout.Slider("TGPC (+Value)", gm.tgpcINP, 0, 100);

        gm.critProbINP = (byte)EditorGUILayout.Slider("Critical Probability (+%)", gm.critProbINP, 0, 100);

        gm.critMultINP = (float)EditorGUILayout.Slider("Mult. Critico (+Value)", gm.critMultINP, 0f, 10f);

        gm.roboDeVidaINP = (byte)EditorGUILayout.Slider("Robo de Vida (+%)", gm.roboDeVidaINP, 0, 100);

        gm.multVelAtaqueINP = (float)EditorGUILayout.Slider("Mult. Velocidad Ataque (+Value)", gm.multVelAtaqueINP, 0f, 10f);

        gm.speedMultINP = (float)EditorGUILayout.Slider("Mult. Velocidad (+Value)", gm.speedMultINP, 0f, 10f);
        #endregion

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Mod"))
        {
            gm.AddMod(gm.nameINP, gm.vidaINP, gm.dmgINP, gm.multConcienciaINP, gm.tgpcINP, gm.critProbINP, gm.critMultINP, gm.roboDeVidaINP, gm.multVelAtaqueINP, gm.speedMultINP);
            gm.nameINP = "";
        }

        if (GUILayout.Button("Reset"))
        {
            gm.mods.Clear();
            gm.ResetStats();
            Debug.Log(gm.mods.Count);
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (gm.mods.Count != 0)
        {
            GUILayout.Label("Lista de Modifcadores", EditorStyles.boldLabel);

            List<string> displayMod = new List<string>();

            for (int i = 0; i < gm.mods.Count; i++)
            {
                displayMod.Add("Nombre: " + gm.mods[i].Name + " | " + "Vida +% : " + gm.mods[i].MultVidaMax + " | " + "Da�o +% : " + gm.mods[i].MultDmg
                    + " | " + "MultConciencia +X : " + gm.mods[i].MultConciencia + " | " + "MultTGPC +X : " + gm.mods[i].MultTGPC + 
                    " | " + "Crit Prob +% : " + gm.mods[i].MultCritProb + " | " + "MultCrit +X : " + gm.mods[i].MultCrit + " | " + "Robo de Vida +% : " + gm.mods[i].MultRoboPer
                    + " | " + "MultVelAtaque +X : " + gm.mods[i].MultVelAtaque + " | " + "MultSpeed +X : " + gm.mods[i].MultSpeed) ;
            }

            for (int i = 0; i < displayMod.Count; i++)
            {
                GUILayout.BeginHorizontal();
                displayMod[i] = EditorGUILayout.TextField(displayMod[i]);
                if (GUILayout.Button("x"))
                {
                    gm.ResetStats(gm.mods.ElementAt(i));
                    Debug.Log(gm.mods.Count);
                }
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.Space();

        gm.damageToPlayer = EditorGUILayout.IntField("Da�o a aplicar:", gm.damageToPlayer);

        if (GUILayout.Button("Do Damage"))
        {
            gm.DamagePlayer(gm.damageToPlayer);
        }

        EditorGUILayout.Space();

        #region"Player Stats"

        if (gm.Player != null)
        {
            GUILayout.Label("Player Stats", EditorStyles.boldLabel);

            string maxLife, life, maxConciencia, conciencia, damage, status, tGPC, critProb, critMult, roboDeVida, multVelAtaque, speedMult;

            maxLife = EditorGUILayout.TextField("Vida Maxima: ", gm.Player.MaxLife.ToString());
            life = EditorGUILayout.TextField("Vida Actual: ", gm.Player.Life.ToString());
            maxConciencia = EditorGUILayout.TextField("Conciencia Maxima: ", gm.Player.MaxConciencia.ToString());
            conciencia = EditorGUILayout.TextField("Conciencia Actual: ", gm.Player.Conciencia.ToString());
            damage = EditorGUILayout.TextField("Da�o: ", gm.Player.Damage.ToString());
            status = EditorGUILayout.TextField("Estado: ", gm.Player.Status.ToString());
            tGPC = EditorGUILayout.TextField("TGPC: ", gm.Player.TGPC.ToString());
            critProb = EditorGUILayout.TextField("Probabilidad de Critico: ", gm.Player.CritProb.ToString());
            critMult = EditorGUILayout.TextField("Mult. Critico: ", gm.Player.CritMult.ToString());
            roboDeVida = EditorGUILayout.TextField("Porcentaje Robo de Vida: ", gm.Player.RoboVida.ToString());
            multVelAtaque = EditorGUILayout.TextField("Mult. Velocidad Ataque: ", gm.Player.MultVelAtaque.ToString());
            speedMult = EditorGUILayout.TextField("Mult. Velocidad: ", gm.Player.SpeedMult.ToString());
        }

        #endregion
    }
}