using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplexNoise;

public class TerrainGen
{
    float stoneBaseHeight = -24;
    float stoneBaseNoise = 0.05f;
    float stoneBaseNoiseHeight = 4;

    float stoneMountainHeight = 48;
    float stoneMountainFrequency = 0.008f;
    float stoneMinHeight = -12;

    float sandMaxHeight = 0;

    float dirtBaseHeight = 1;
    float dirtNoise = 0.04f;
    float dirtNoiseHeight = 3;

    float caveFrequency = 0.025f;
    int caveSize = 7;

    float treeFrequency = 0.2f;
    int treeDensity = 3;
    int waterHeight = -5;
    public enum Biome { flat, hilly, mountainous, ocean, test };

    public Chunk ChunkGen(Chunk chunk)
    {
        // OVERLAP FOR TREES (probs not needed for model trees)
        for (int x = chunk.pos.x-3; x < chunk.pos.x + Chunk.chunkSize+3; x++)
        {
            for (int z = chunk.pos.z-3; z < chunk.pos.z + Chunk.chunkSize+3; z++)
            {
                chunk = ChunkColumnGen(chunk, x, z);
            }
        }
        return chunk;
    }

    public Chunk ChunkColumnGen(Chunk chunk, int x, int z)
    {
        BiomeVars(chunk); // Sets the chunk settings to match the current biome, maybe should be own class?

        int stoneHeight = Mathf.FloorToInt(stoneBaseHeight);
        stoneHeight += GetNoise(x, 0, z, stoneMountainFrequency, Mathf.FloorToInt(stoneMountainHeight));

        if (stoneHeight < stoneMinHeight)
            stoneHeight = Mathf.FloorToInt(stoneMinHeight);

        stoneHeight += GetNoise(x, 0, z, stoneBaseNoise, Mathf.FloorToInt(stoneBaseNoiseHeight));

        int dirtHeight = stoneHeight + Mathf.FloorToInt(dirtBaseHeight);
        dirtHeight += GetNoise(x, 100, z, dirtNoise, Mathf.FloorToInt(dirtNoiseHeight));

        // OVERLAP HERE TOO
        for (int y = chunk.pos.y - 8; y < chunk.pos.y + Chunk.chunkSize; y++)
        {
            //Get a value to base cave generation on
            int caveChance = GetNoise(x, y, z, caveFrequency, 100);

            if (y <= stoneHeight) //  && caveSize < caveChance
            {
                SetBlock(x, y, z, new Block(), chunk);
            }
            else if (y <= (sandMaxHeight + Random.Range(0,3)) && y <= dirtHeight)
            {
                SetBlock(x, y, z, new BlockSand(), chunk);
            }
            else if (y <= dirtHeight) //  && caveSize < caveChance
            {
                SetBlock(x, y, z, new BlockGrass(), chunk);
                // THIS IS WHERE TREES ARE PLACED
                if (y == dirtHeight && GetNoise(x, 0, z, treeFrequency, 100) < treeDensity && y > waterHeight)
                    CreateTree(x, y + 1, z, chunk);
            }
            else if (y <= waterHeight) //  && caveSize < caveChance
            {
                // WATER GEN (probs should be changed)
                SetBlock(x, y, z, new BlockWater(), chunk);
            }
            else
            {
                SetBlock(x, y, z, new BlockAir(), chunk);
            }
        }

        return chunk;
    }

    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }

    public static void SetBlock(int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks = false)
    {
        x -= chunk.pos.x;
        y -= chunk.pos.y;
        z -= chunk.pos.z;
        if (Chunk.InRange(x) && Chunk.InRange(y) && Chunk.InRange(z))
        {
            if (replaceBlocks || chunk.blocks[x, y, z] == null)
                chunk.SetBlock(x, y, z, block);
        }
    }

    // TEMP FUNCTION (will be replaced with tree models instead)
    void CreateTree(int x, int y, int z, Chunk chunk)
    {
        x -= chunk.pos.x;
        y -= chunk.pos.y;
        z -= chunk.pos.z;
        if (Chunk.InRange(x) && Chunk.InRange(y) && Chunk.InRange(z))
        {
            if (chunk.blocks[x,y,z]==null)
            {
                chunk.generateTree(x, y, z);
            }
        }

        //SetBlock(x, y, z, new BlockWood(), chunk, true);
        /* NEEDS MONOBEHAVIOUR
        GameObject newChunkObject = Instantiate(
                        chunk.world.treeTestPrefab, new Vector3(x, y, z),
                        Quaternion.Euler(Vector3.zero)
                    ) as GameObject;
        */
        /*
        //Instantiate the chunk at the coordinates using the chunk prefab
        //create leaves
        for (int xi = -2; xi <= 2; xi++)
        {
            for (int yi = 4; yi <= 8; yi++)
            {
                for (int zi = -2; zi <= 2; zi++)
                {
                    SetBlock(x + xi, y + yi, z + zi, new BlockLeaves(), chunk, true);
                }
            }
        }
        //create trunk
        for (int yt = 0; yt < 6; yt++)
        {
            SetBlock(x, y + yt, z, new BlockWood(), chunk, true);
        }
        */
    }

    // BIOME FUNCTIONS
    private void BiomeVars(Chunk chunk)
    {
        switch (chunk.biome)
        {
            case Biome.hilly:
                stoneBaseHeight = -24;
                stoneBaseNoise = 0.05f;
                stoneBaseNoiseHeight = 4;

                stoneMountainHeight = 48;
                stoneMountainFrequency = 0.008f;
                stoneMinHeight = -12;

                dirtBaseHeight = 1;
                dirtNoise = 0.04f;
                dirtNoiseHeight = 3;

                caveFrequency = 0.025f;
                caveSize = 7;

                treeFrequency = 0.2f;
                treeDensity = 3;

                return;
            case Biome.flat:
                stoneBaseHeight = -24;
                stoneBaseNoise = 0.01f;
                stoneBaseNoiseHeight = 4;

                stoneMountainHeight = 0;
                stoneMountainFrequency = 0f;
                stoneMinHeight = -12;

                dirtBaseHeight = 1;
                dirtNoise = 0.04f;
                dirtNoiseHeight = 3;

                // No caves or trees in plains
                caveFrequency = 0.0f;
                treeFrequency = 0.0f;

                return;

            case Biome.mountainous:
                stoneBaseHeight = -24;
                stoneBaseNoise = 0.01f;
                stoneBaseNoiseHeight = 4;

                stoneMountainHeight = 86;
                stoneMountainFrequency = 0.01f;
                stoneMinHeight = -24;

                dirtBaseHeight = 1;
                dirtNoise = 0.04f;
                dirtNoiseHeight = 3;

                // No caves or trees in plains
                caveFrequency = 0.0f;
                treeFrequency = 0.0f;

                return;
        }
    }
}
