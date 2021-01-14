//Jamie - This class will save and load game information like settings, upgrades and current checkpoint. Uses alot of temp values until the scripts with the data is actually created

//Jamie - 26/10/20 - First implemented
//Jann  - 04/11/20 - Saving and loading implemented as far as possible with the current dependencies
//Jann  - 08/11/20 - QA improvements
//Jann  - 20/11/20 - Hooked up the settingsmenu
//Jann  - 23/11/20 - QA improvements
//Jann  - 25/11/20 - Caching implemented
//Jann  - 28/11/20 - Added new gameplay data saving

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem_JamieG
{
     const string a = "/Settings.save";
     const string b = "/Inventory.save";
     const string c = "/Gameplay.save";
     const string d = "/Checkpoint.save";

     static SaveSystemCache e = new SaveSystemCache();

    public static void QQ()
    {
        DirectoryInfo f = new DirectoryInfo(Application.persistentDataPath);

        string[] h = new[] {a, b, c, d};
        foreach(FileInfo fi in f.GetFiles())
        {
            if (h.Contains("/" + fi.Name))
            {
                fi.Delete();   
            }
        }
    }

    // Saves the configurations of the settings menu into the Settings.save file 
    public static void QW(SettingsMenu_ElliottDesouza g)
    {
        WT l = new WT(g);
        QE(a, l);
    }

    // Saves the current items in the inventory into the Inventory.save file
    public static void QR(Inventory_JoaoBeijinho i)
    {
        WI j = new WI(i);
        QE(b, j);
    }

    // Saves the current state of the game into the Gameplay.save file
    public static void QT(int k, GameObject[] m, GameObject[] n)
    {
        QB o = new QB(k, m, n);
        QE(c, o);
    }
    
    // Saves the current checkpoint (after a boss level)
    public static void QY(int p)
    {
        WE q = new WE(p);
        QE(d, q);
    }

    // Returns information from the Settings.save file or an empty struct if the file can't be found
    public static WT QU()
    {
        if (e.QZ(a))
        {
            return (WT) e.QL(a);
        }

        object r = QI(a);
        if (r is WT s)
        {
            if (s.QO == null)
            {
                s.QO = "English";
            }

            return s;
        }

        return new WT(1f, 1f, "English");
    }

    // Returns the items from the Inventory.save file or an empty struct if the file can't be found
    public static WI QP()
    {
        if (e.QZ(b))
        {
            return (WI) e.QL(b);
        }
        
        object t = QI(b);
        if (t is WI QA)
        {
            return QA;
        }

        return default;
    }

    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static QB QS()
    {
        if (e.QZ(c))
        {
            return (QB) e.QL(c);
        }
        
        object u = QI(c);
        if (u is QB QD)
        {
            return QD;
        }

        return default;
    }
    
    // Returns the gameplay data from the Gameplay.save file or an empty struct if the file can't be found
    public static WE QF()
    {
        if (e.QZ(d))
        {
            return (WE) e.QL(d);
        }
        
        object v = QI(d);
        if (v is WE QG)
        {
            return QG;
        }
        return default;
    }



    /// <summary>
    /// Saves data as file at C:\Users\{user}\AppData\LocalLow\DefaultCompany\BingerNinja
    /// </summary>
    /// <param name="QH">Name of the file</param>
    /// <param name="QJ">Data that should be saved (struct from the end of this file)</param>
     static void QE(string QH, object QJ)
    {
        //Setup formatter
        BinaryFormatter w = new BinaryFormatter();
        string x = Application.persistentDataPath + QH;
        FileStream y = new FileStream(x, FileMode.Create);

        //Save to file
        w.Serialize(y, QJ);
        y.Close();

        e.QK(QH, QJ);
    }

    /// <summary>
    /// Loads data from a file from C:\Users\{user}\AppData\LocalLow\DefaultCompany\BingerNinja
    /// </summary>
    /// <param name="C">Name of the file that should be loaded</param>
    /// <returns>A struct (defined at the end of this file)</returns>
    public static object QI(string C)
    {
        string z = Application.persistentDataPath + C;
        if (File.Exists(z))
        {
            BinaryFormatter A = new BinaryFormatter();
            FileStream B = new FileStream(z, FileMode.Open);

            object D = A.Deserialize(B);
            B.Close();
            
            return D;
        }
        else
        {
            return null;
        }
    }
}

internal class SaveSystemCache
{
     List<QX> E = new List<QX>();

    public void QK(string F, object G)
    {
        if (QZ(F))
        {
            int H = E.IndexOf(E.Find(c => c.QC.Equals(F)));
            E[H] = new QX(F, G);
        }
        else
        {
            E.Add(new QX(F, G));
        }
    }

    public object QL(string I)
    {
        return E.Find(data => data.QC.Equals(I)).QV;
    }

    public bool QZ(string J)
    {
        return E.Find(data => data.QC.Equals(J)).QV != null;
    }

     struct QX
    {
        public string QC;
        public object QV;

        public QX(string K, object L)
        {
            this.QV = L;
            this.QC = K;
        }
    }
}


[Serializable]
public struct QB
{
    public int QN;
    public string[] QM;
    public string[] WQ;

    public QB(int M, GameObject[] N, GameObject[] O)
    {
        QN = M;

        int P = N.Count(e => e.activeInHierarchy);
        QM = new string[P];
        for (int i = 0; i < N.Length; i++)
        {
            if (N[i].activeInHierarchy)
            {
                QM[i] = N[i].name;   
            }
        }
        
        WQ = new string[O.Length];
        for (int i = 0; i < O.Length; i++)
        {
            WQ[i] = O[i].name;
        }
    }
};

[Serializable]
public struct WE
{
    public int WR;

    public WE(int P)
    {
        WR = P;
    }
};

[Serializable]
public struct WT
{
    public float WY;
    public float WU;
    public string QO;

    public WT(SettingsMenu_ElliottDesouza Q)
    {
        WY = Q.m_musicSlider.normalizedValue;
        WU = Q.m_SFXSlider.normalizedValue;
        QO = Q.m_selectedLanguage;
    }

    public WT(float R, float S, string T)
    {
        WY = R;
        WU = S;
        QO = T;
    }
};

[Serializable]
public struct WI
{
    public WP[] WO;

    public WI(Inventory_JoaoBeijinho U)
    {
        WO = new WP[U.m_inventoryItems.Count];

        int V = 0;
        foreach (KeyValuePair<ItemType, int> pair in U.m_inventoryItems)
        {
            WO[V] = new WP(pair.Key, pair.Value);
            V++;
        }
    }
};

[Serializable]
public struct WP
{
    public ItemType WA;
    public int WS;

    public WP(ItemType W, int U)
    {
        WA = W;
        WS = U;
    }
}
