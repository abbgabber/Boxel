using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWater : Block
{
    public BlockWater()
        :base()
    {

    }

    public override void FlowWater(Chunk chunk, int x, int y, int z, Chunk chunkCopy) 
    {
    
        if (chunk.GetBlock(x, y - 1, z).IsWaterPassable())
        {
            // Water can pass through this block
            chunkCopy.SetBlock(x, y - 1, z, new BlockWater());
            chunkCopy.SetBlock(x, y, z, new BlockAir());
        }
        if (!chunk.GetBlock(x, y - 1, z).IsWaterPassable())
        {
            if (chunk.GetBlock(x + 1, y, z).IsWaterPassable())
            {
                chunkCopy.SetBlock(x + 1, y, z, new BlockWater());
            }
        
            if (chunk.GetBlock(x - 1, y, z).IsWaterPassable())
            {
                chunkCopy.SetBlock(x - 1, y, z, new BlockWater());
            }
        
            if (chunk.GetBlock(x, y, z + 1).IsWaterPassable())
            {
                chunkCopy.SetBlock(x, y, z + 1, new BlockWater());
            }
        
            if (chunk.GetBlock(x, y, z - 1).IsWaterPassable())
            {
                chunkCopy.SetBlock(x, y, z - 1, new BlockWater());
            }
        }
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 2;
        tile.y = 0;
        return tile;
    }

    public override bool IsSolid(Direction direction)
    {
        return false;
    }

    public override bool IsWater()
    {
        return true;
    }
}
