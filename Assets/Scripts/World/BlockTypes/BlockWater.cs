using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWater : Block
{
    public BlockWater()
        :base()
    {

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
