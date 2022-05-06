using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GameData
{
    #region"Saved currency"
    public static int Gems = 0;
    #endregion

    #region"Bought stances"
    public static int boughtStance0 = 1; // Siempre estará comprada la base por default
    public static int boughtStance1 = 0;
    public static int boughtStance2 = 0;
    public static int boughtStance3 = 0;
    public static int boughtStance4 = 0;
    public static int boughtStance5 = 0;
    #endregion

    #region"Bought dreamcatchers"
    public static int boughtDreamcatcher0 = 0;
    public static int boughtDreamcatcher1 = 0;
    public static int boughtDreamcatcher2 = 0;
    public static int boughtDreamcatcher3 = 0;
    public static int boughtDreamcatcher4 = 0;
    #endregion

    #region"Save Load Reset - System"
    public static void SaveGameData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/DwizardSave.dat");
            GameDataSave data = new GameDataSave();

            data.savedGems = Gems;

            data.savedBoughtStance0 = boughtStance0;
            data.savedBoughtStance1 = boughtStance1;
            data.savedBoughtStance2 = boughtStance2;
            data.savedBoughtStance3 = boughtStance3;
            data.savedBoughtStance4 = boughtStance4;
            data.savedBoughtStance5 = boughtStance5;

            data.savedBoughtDreamcatcher0 = boughtDreamcatcher0;
            data.savedBoughtDreamcatcher1 = boughtDreamcatcher1;
            data.savedBoughtDreamcatcher2 = boughtDreamcatcher2;
            data.savedBoughtDreamcatcher3 = boughtDreamcatcher3;
            data.savedBoughtDreamcatcher4 = boughtDreamcatcher4;

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("Game data saved!");
        }
        catch(Exception)
        {
            Debug.LogError("Something went wrong trying to save");
        }
    }

    public static void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat"))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DwizardSave.dat", FileMode.Open);
                GameDataSave data = (GameDataSave)bf.Deserialize(file);
                file.Close();

                Gems = data.savedGems;

                boughtStance0 = data.savedBoughtStance0;
                boughtStance1 = data.savedBoughtStance1;
                boughtStance2 = data.savedBoughtStance2;
                boughtStance3 = data.savedBoughtStance3;
                boughtStance4 = data.savedBoughtStance4;
                boughtStance5 = data.savedBoughtStance5;

                boughtDreamcatcher0 = data.savedBoughtDreamcatcher0;
                boughtDreamcatcher1 = data.savedBoughtDreamcatcher1;
                boughtDreamcatcher2 = data.savedBoughtDreamcatcher2;
                boughtDreamcatcher3 = data.savedBoughtDreamcatcher3;
                boughtDreamcatcher4 = data.savedBoughtDreamcatcher4;

                Debug.Log("Game data loaded!");
            }
            catch(Exception)
            {
                Debug.LogError("Something went wrong while loading data");
            }  
        }
        else Debug.LogError("There is no save data!");
    }

    public static void ResetSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat"))
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/DwizardSave.dat");

                Gems = 0;

                boughtStance0 = 0;
                boughtStance1 = 0;
                boughtStance2 = 0;
                boughtStance3 = 0;
                boughtStance4 = 0;
                boughtStance5 = 0;

                boughtDreamcatcher0 = 0;
                boughtDreamcatcher1 = 0;
                boughtDreamcatcher2 = 0;
                boughtDreamcatcher3 = 0;
                boughtDreamcatcher4 = 0;

                Debug.Log("Data reset complete!");
            }
            catch(Exception)
            {
                Debug.Log("Something went wrong while resetting data");
            }
        }
        else Debug.LogError("No save data to delete.");
    }
    #endregion

    #region"Retreive methods"
    public static int RetreiveGems()
    {
        int retreivedGems = Gems;
        return retreivedGems;
    }

    public static bool[] RetreiveBoughtStances()
    {
        bool[] retreivedBoughtStances = new bool[6];

        retreivedBoughtStances[0] = boughtStance0 != 0;
        retreivedBoughtStances[1] = boughtStance1 != 0;
        retreivedBoughtStances[2] = boughtStance2 != 0;
        retreivedBoughtStances[3] = boughtStance3 != 0;
        retreivedBoughtStances[4] = boughtStance4 != 0;
        retreivedBoughtStances[5] = boughtStance5 != 0;

        return retreivedBoughtStances;
    }

    public static bool[] RetreiveDreamCatchers()
    {
        bool[] retreivedBoughtDreamCatchers = new bool[5];

        retreivedBoughtDreamCatchers[0] = boughtDreamcatcher0 != 0;
        retreivedBoughtDreamCatchers[1] = boughtDreamcatcher1 != 0;
        retreivedBoughtDreamCatchers[2] = boughtDreamcatcher2 != 0;
        retreivedBoughtDreamCatchers[3] = boughtDreamcatcher3 != 0;
        retreivedBoughtDreamCatchers[4] = boughtDreamcatcher4 != 0;

        return retreivedBoughtDreamCatchers;
    }
    #endregion
}

[Serializable]
public class GameDataSave
{
    #region"Saved currency"
    public int savedCurrency;
    public int savedGems;
    #endregion

    #region"Bought stances"
    public int savedBoughtStance0;
    public int savedBoughtStance1;
    public int savedBoughtStance2;
    public int savedBoughtStance3;
    public int savedBoughtStance4;
    public int savedBoughtStance5;
    #endregion

    #region"Bought dreamcatchers"
    public int savedBoughtDreamcatcher0;
    public int savedBoughtDreamcatcher1;
    public int savedBoughtDreamcatcher2;
    public int savedBoughtDreamcatcher3;
    public int savedBoughtDreamcatcher4;
    #endregion
}
