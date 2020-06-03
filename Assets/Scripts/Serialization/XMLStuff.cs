using System.IO;
using System;
using System.Xml;
using UnityEngine;

public class XMLStuff : ISaveStuff
{
    string SavePath = Path.Combine(Application.dataPath, "XMLStuffData.xml");
    public void Save(DroppedStuff[] _stuff, int count)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("Stuff");
        xmlDoc.AppendChild(rootNode);
        XmlElement element = xmlDoc.CreateElement("Count");
        element.SetAttribute("value", $"{count}");
        rootNode.AppendChild(element);
        for (int i = 0; i < count; i++)
        {

            element = xmlDoc.CreateElement("Name");
            element.SetAttribute("value", _stuff[i].PrefName);
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Tag");
            element.SetAttribute("value", _stuff[i].PrefTag);
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Position");
            element.SetAttribute("value", _stuff[i].PrefPos.ToString());
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Scale");
            element.SetAttribute("value", _stuff[i].PrefScale.ToString());
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("Rotation");
            element.SetAttribute("value", _stuff[i].PrefRotation.ToString());
            rootNode.AppendChild(element);
        }
        xmlDoc.Save(SavePath);
    }

    public DroppedStuff[] Load()
    {

        bool counted = false;
        int _counter = 0;

        if (!File.Exists(SavePath))
        {
            Debug.Log("File not exists....");
            return null;
           
        }

        using (XmlTextReader reader = new XmlTextReader(SavePath))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement("Count"))
                {
                    _counter = Convert.ToInt32(reader.GetAttribute("value"));
                    counted = true;
                    break;
                }
            }
        }
        using (XmlTextReader reader = new XmlTextReader(SavePath))
        {
            DroppedStuff[] result = new DroppedStuff[_counter];
            int i = 0;
            while (reader.Read())
            {
                
                if (reader.IsStartElement("Name"))
                {
                    result[i].PrefName = reader.GetAttribute("value");
                    Debug.Log(i + result[i].PrefName);
                }
                if (reader.IsStartElement("Tag"))
                {
                    result[i].PrefTag = reader.GetAttribute("value");
                    Debug.Log(i + result[i].PrefTag);
                }
                if (reader.IsStartElement("Position"))
                {
                    result[i].PrefPos = DataStructure.ToSVect(reader.GetAttribute("value"));
                    Debug.Log(i + result[i].PrefPos.ToString());
                }
                if (reader.IsStartElement("Scale"))
                {
                    result[i].PrefScale = DataStructure.ToSVect(reader.GetAttribute("value"));
                    Debug.Log(i + result[i].PrefScale.ToString());
                }
                if (reader.IsStartElement("Rotation"))
                {
                    result[i].PrefRotation = DataStructure.ToSQuater(reader.GetAttribute("value"));
                    Debug.Log(i + result[i].PrefRotation.ToString());
                    i++;
                }
                
               
            }



            return result;
        }
    }
}

