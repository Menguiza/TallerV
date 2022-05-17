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

        gm.vidaINP = (sbyte)EditorGUILayout.Slider("Vida (+%)", gm.vidaINP, -10, 10);

        gm.dmgINP = (sbyte)EditorGUILayout.Slider("Daño (+%)", gm.dmgINP, -10, 10);

        gm.multConcienciaINP = (float)EditorGUILayout.Slider("Mult. Conciencia (+Value)", gm.multConcienciaINP, -10f, 10f);

        gm.tgpcINP = (sbyte)EditorGUILayout.Slider("TGPC (+Value)", gm.tgpcINP, -100, 100);

        gm.critProbINP = (sbyte)EditorGUILayout.Slider("Critical Probability (+%)", gm.critProbINP, -100, 100);

        gm.critMultINP = (float)EditorGUILayout.Slider("Mult. Critico (+Value)", gm.critMultINP, -10f, 10f);

        gm.roboDeVidaINP = (sbyte)EditorGUILayout.Slider("Robo de Vida (+%)", gm.roboDeVidaINP, -100, 100);

        gm.multVelAtaqueINP = (float)EditorGUILayout.Slider("Mult. Velocidad Ataque (+Value)", gm.multVelAtaqueINP, -10f, 10f);

        gm.speedMultINP = (float)EditorGUILayout.Slider("Mult. Velocidad (+Value)", gm.speedMultINP, -10f, 10f);

        gm.multPesadillaINP = (sbyte)EditorGUILayout.Slider("Nightmare Probability (+%)", gm.multPesadillaINP, -100, 100);

        gm.multDañoRecibidoINP = (float)EditorGUILayout.Slider("Mult. Daño Recibido (+Value)", gm.multDañoRecibidoINP, -10f, 10f);

        gm.multHechizosINP = (float)EditorGUILayout.Slider("Mult. Vel. Hechizos (+Value)", gm.multHechizosINP, -10f, 10f);

        #endregion

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Mod"))
        {
            gm.AddMod(gm.nameINP, gm.vidaINP, gm.dmgINP, gm.multConcienciaINP, gm.tgpcINP, gm.critProbINP, gm.critMultINP, gm.roboDeVidaINP, gm.multVelAtaqueINP, gm.speedMultINP, gm.multPesadillaINP, gm.multDañoRecibidoINP, gm.multHechizosINP);
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
                displayMod.Add("Nombre: " + gm.mods[i].Name + " | " + "Vida +% : " + gm.mods[i].MultVidaMax + " | " + "Daño +% : " + gm.mods[i].MultDmg
                    + " | " + "MultConciencia +X : " + gm.mods[i].MultConciencia + " | " + "MultTGPC +X : " + gm.mods[i].MultTGPC + 
                    " | " + "Crit Prob +% : " + gm.mods[i].MultCritProb + " | " + "MultCrit +X : " + gm.mods[i].MultCrit + " | " + "Robo de Vida +% : " + gm.mods[i].MultRoboPer
                    + " | " + "MultVelAtaque +X : " + gm.mods[i].MultVelAtaque + " | " + "MultSpeed +X : " + gm.mods[i].MultSpeed + " | " +"MultPesadilla +% : " + gm.mods[i].MultPesadillaPer
                    + " | " + "MultDañoRecibido +X : " + gm.mods[i].MultDañoRecibido + " | " + "MultVelHechizos +X : " + gm.mods[i].MultHechizos);
            }

            for (int i = 0; i < displayMod.Count; i++)
            {
                GUILayout.BeginHorizontal();
                displayMod[i] = EditorGUILayout.TextField(displayMod[i]);
                if (GUILayout.Button("x"))
                {
                    gm.mods.RemoveAt(i);

                    gm.CheckMods();

                    Debug.Log(gm.mods.Count);
                }
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.Space();

        gm.damageToPlayer = EditorGUILayout.IntField("Daño a aplicar:", gm.damageToPlayer);

        if (GUILayout.Button("Do Damage"))
        {
            if(gm.playerObject.GetComponent<PlayerController>().blocking)
            {
                gm.playerObject.GetComponent<PlayerController>().OnHitBlock();
                gm.DamagePlayer(gm.damageToPlayer/2, gm.damageToPlayer / 2);
            }
            else
            {
                gm.DamagePlayer(gm.damageToPlayer, gm.damageToPlayer / 2);
            }
        }

        EditorGUILayout.Space();

        gm.dmrcatcherINP = (DreamCatcher)EditorGUILayout.ObjectField("Atrapasueños:", gm.dmrcatcherINP, typeof(DreamCatcher), true);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Equipar"))
        {
            Inventory.instance.AddDreamcatcher(gm.dmrcatcherINP);
        }

        if (GUILayout.Button("Desquipar"))
        {
            Inventory.instance.Remove();
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        #region"Player Stats"

        if (gm.Player != null)
        {
            GUILayout.Label("Player Stats", EditorStyles.boldLabel);

            string maxLife, life, maxConciencia, conciencia, damage, status, tGPC, critProb, critMult, roboDeVida, multVelAtaque, speedMult, multPesadilla, multDañoRecibido, multHechizos;
            bool pesadilla;

            maxLife = EditorGUILayout.TextField("Vida Maxima: ", gm.Player.MaxLife.ToString());
            life = EditorGUILayout.TextField("Vida Actual: ", gm.Player.Life.ToString());
            maxConciencia = EditorGUILayout.TextField("Conciencia Maxima: ", gm.Player.MaxConciencia.ToString());
            conciencia = EditorGUILayout.TextField("Conciencia Actual: ", gm.Player.Conciencia.ToString());
            damage = EditorGUILayout.TextField("Daño: ", gm.Player.Damage.ToString());
            status = EditorGUILayout.TextField("Estado: ", gm.Player.Status.ToString());
            tGPC = EditorGUILayout.TextField("TGPC: ", gm.Player.TGPC.ToString());
            critProb = EditorGUILayout.TextField("Probabilidad de Critico: ", gm.Player.CritProb.ToString());
            critMult = EditorGUILayout.TextField("Mult. Critico: ", gm.Player.CritMult.ToString());
            roboDeVida = EditorGUILayout.TextField("Porcentaje Robo de Vida: ", gm.Player.RoboVida.ToString());
            multVelAtaque = EditorGUILayout.TextField("Mult. Velocidad Ataque: ", gm.Player.MultVelAtaque.ToString());
            speedMult = EditorGUILayout.TextField("Mult. Velocidad: ", gm.Player.SpeedMult.ToString());
            multPesadilla = EditorGUILayout.TextField("Mult. Pesadilla: ", gm.Player.MultPesadilla.ToString());
            pesadilla = EditorGUILayout.Toggle("Pesadilla: ", gm.Player.Pesadilla);
            multDañoRecibido = EditorGUILayout.TextField("Mult. daño recibido: ", gm.Player.MultDañoRecibido.ToString());
            multHechizos = EditorGUILayout.TextField("Mult. vel. Hechizos: ", gm.Player.MultHechizos.ToString());

        }

        #endregion

        EditorGUILayout.Space();

        if (GUILayout.Button("Reload Scene"))
        {
            gm.ReloadScene();
        }
    }
}