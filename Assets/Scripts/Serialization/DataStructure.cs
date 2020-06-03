using System;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine;

[Serializable]
public struct SVect3
{
    public float X;
    public float Y;
    public float Z;


    public SVect3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static implicit operator SVect3(Vector3 val)
    {
        return new SVect3(val.x, val.y, val.z);
    }

    public static implicit operator Vector3(SVect3 val)
    {
        return new Vector3(val.X, val.Y, val.Z);
    }
    public override string ToString()
    {
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

        return $"({X},{Y},{Z})";
    }

}

[Serializable]
public struct SQuater
{
    public float X;
    public float Y;
    public float Z;
    public float W;


    public SQuater(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }
    public static implicit operator SQuater(Quaternion val)
    {
        return new SQuater(val.x, val.y, val.z, val.w);
    }

    public static implicit operator Quaternion(SQuater val)
    {
        return new Quaternion(val.X, val.Y, val.Z, val.W);
    }

    
    
    public override string ToString()
    {
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        return $"({X},{Y},{Z},{W})";
    }
}
[Serializable]
public struct SObject
{
    public string Name;
    public SVect3 Position;
    public SVect3 Scale;
    public SQuater Rotation;
}

[Serializable]
public struct PlayerData
{
    public string PlayerName;
    public int PlayerHealth;
    public bool PlayerDead;

    public override string ToString()
    {
        return $"Name: {PlayerName} Health: {PlayerHealth} Dead: {PlayerDead}";
    }
}
[Serializable]
public struct DroppedStuff
{
    public string PrefTag;
    public string PrefName;
    public SVect3 PrefPos;
    public SVect3 PrefScale;
    public SQuater PrefRotation;
}

public class DataStructure : MonoBehaviour
{
    public static SVect3 ToSVect(string Vector)
    {
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        if (Vector.StartsWith("(") && Vector.EndsWith(")"))
        {
            Vector = Vector.Substring(1, Vector.Length - 2);
        }
        string[] temp = Vector.Split(',');

        SVect3 result = new SVect3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
        return result;

    }
    public static SQuater ToSQuater(string Quater)
    {
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        if (Quater.StartsWith("(") && Quater.EndsWith(")"))
        {
            Quater = Quater.Substring(1, Quater.Length - 2);
        }
        string[] temp = Quater.Split(',');

        return new SQuater(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3]));
    }
}
