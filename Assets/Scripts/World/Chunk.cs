using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{


    public Block[, ,] blocks = new Block[chunkSize, chunkSize, chunkSize];

    public static int chunkSize = 16;
    public bool update = false;
    public bool rendered;

    MeshFilter filter;
    MeshCollider coll;

    public World world;
    public WorldPos pos;

    public TerrainGen.Biome biome = TerrainGen.Biome.flat;

    public static float waitTime = 15.0f;
    public float timer;

    void Start()
    {
        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();
    }

    //Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }

        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            UpdateWaterFlow();
            UpdateChunk();
            timer = 0.0f;
        }
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    public void SetBlock(int x, int y, int z, Block block, bool water = false)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    void reassignChunk(Chunk chunk, Chunk chunkCopy)
    {
        chunk = chunkCopy;
    }

    void UpdateWaterFlow() 
    {
        Chunk chunkCopy = this; // needs t do a deep copy and not a shallow copy
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (blocks[x, y, z].IsWater())
                    {
                        blocks[x, y, z].FlowWater(this, x, y, z, chunkCopy);
                        // This is a water block

                    }
                }
            }
        }
    
        reassignChunk(this, chunkCopy);
    }

    // Updates the chunk based on its contents
    void UpdateChunk()
    {
        rendered = true;

        MeshData meshData = new MeshData();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (blocks[x, y, z].IsWater())
                    {
                        // This is a water block
                    }
                    else if (blocks[x, y, z].IsWaterPassable())
                    {
                        // Water can pass through this block
                    }
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }

        RenderMesh(meshData);
    }

    // Sends the calculated mesh information
    // to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();

        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        coll.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        coll.sharedMesh = mesh;
    }

    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.changed = false;
        }
    }

    // TREES CURRENTLY DO NOT GET SERIALIZED
    public void generateTree(int x, int y, int z)
    {
        // Randomize number here to generate diffirent types of trees
        int rng = (int)Random.Range(0f, 3f);
        GameObject tree = world.treeTestPrefab;
        if (rng > 0)
        {
            tree = world.treeTestPrefab2;
        }
        GameObject newTreeObject = Instantiate(
                            tree, new Vector3(pos.x + x, pos.y + y - 4, pos.z + z),
                            Quaternion.Euler(new Vector3(-90, 0, 0))
                        ) as GameObject;
        newTreeObject.transform.SetParent(this.gameObject.transform);
    }

    public bool destroyTree(RaycastHit hit)
    {
        GameObject tree = hit.collider.gameObject;
        if (tree == null)
            return false;

        Destroy(tree);
        return true;
    }
}