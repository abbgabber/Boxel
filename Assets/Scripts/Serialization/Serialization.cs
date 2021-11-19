using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Serialization
{
    public static string saveFolderName = "saves";
    public static string worldName = "error";
    
    public static string SaveLocation(string worldName)
    {
        string saveLocation = Application.persistentDataPath + '/' + saveFolderName + "/" + worldName + "/";
        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }
        return saveLocation;
    }
    public static string FileName(WorldPos chunkLocation)
    {
        string filename = chunkLocation.x + "," + chunkLocation.y + "," + chunkLocation.z + ".bin";
        return filename;
    }
    public static void SaveChunk(Chunk chunk)
    {
        Save save = new Save(chunk);
        if (save.blocks.Count == 0)
            return;

        string saveFile = SaveLocation(worldName); // CHANGED
        saveFile += FileName(chunk.pos);

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, save);
        stream.Close();
    }
    public static bool Load(Chunk chunk)
    {
        string saveFile = SaveLocation(worldName); // CHANGED
        saveFile += FileName(chunk.pos);

        if (!File.Exists(saveFile))
            return false;

        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFile, FileMode.Open);

        Save save = (Save)formatter.Deserialize(stream);
        foreach (var block in save.blocks)
        {
            chunk.blocks[block.Key.x, block.Key.y, block.Key.z] = block.Value;
        }
        
        stream.Close();
        return true;
    }

    public static void SaveMetaFile()
    {

    }

    public static void SaveAllChunks(Chunk[] chunks)
    {
        foreach (Chunk chunk in chunks)
        {
            SaveChunk(chunk);
        }
    }
}
