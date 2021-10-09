# Boxel 
# Table of Contents
- [Boxel](#boxel)
- [Table of Contents](#table-of-contents)
- [Todo](#todo)
  - [World Generation](#world-generation)
    - [Biomes](#biomes)
  - [Serialization](#serialization)
- [Info](#info)
  - [World Generation](#world-generation-1)
    - [Chunks](#chunks)
    - [Blocks](#blocks)
      - [Block types](#block-types)
    - [Noise maps](#noise-maps)
    - [Biomes](#biomes-1)
      - [Deciding factors](#deciding-factors)
# Todo
## World Generation
- [ ] Rivers
- [ ] Lakes
- [ ] Moisture maps
- [ ] Temperature maps (noise)
- [x] Trees
- [ ] Grass
- [ ] Caves (perlin worms)
- [ ] Seed (for world gen, used on the noise maps)
- [ ] Mountains
### Biomes
- [ ] Plains
- [ ] Snowy
- [ ] Mountain
- [ ] Ocean
- [ ] Desert
## Serialization
- [x] Modified blocks
- [ ] Trees
- [ ] Player info
# Info
## World Generation
The world consists of chunks that load in and out depending on how far the player is from it. The chunks contain all the world information in its area; blocks, trees and more. The terrain is generated using a simplex noise script.
### Chunks
### Blocks
#### Block types
* Air
* Grass
* Stone
### Noise maps 
(it can generate noise in 3 dimensions versus unitys perlin noise which is only 2d).
### Biomes
#### Deciding factors