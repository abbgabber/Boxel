# **Boxel**
# **Table of Contents**
- [**Boxel**](#boxel)
- [**Table of Contents**](#table-of-contents)
- [**Todo**](#todo)
  - [**World Generation**](#world-generation)
    - [**Biomes**](#biomes)
  - [**Serialization**](#serialization)
- [**Info**](#info)
  - [**World Generation**](#world-generation-1)
    - [**Chunks**](#chunks)
    - [**Blocks**](#blocks)
      - [**Block types** *(WIP)*](#block-types-wip)
    - [**Noise maps**](#noise-maps)
    - [**Biomes** *(WIP)*](#biomes-wip)
      - [**Deciding factors**](#deciding-factors)
- [**Sources**](#sources)
  - [**World Generation**](#world-generation-2)
# **Todo**
## **World Generation**
- [ ] Rivers
- [ ] Lakes
- [ ] Moisture maps
- [ ] Temperature maps (noise)
- [x] Trees
- [ ] Grass
- [ ] Caves (perlin worms)
- [ ] Seed (for world gen, used on the noise maps)
- [ ] Mountains
### **Biomes**
- [ ] Plains
- [ ] Snowy
- [ ] Mountain
- [ ] Ocean
- [ ] Desert
## **Serialization**
- [x] Modified blocks
- [ ] Trees
- [ ] Player info
# **Info**
## **World Generation**
The world consists of chunks that load in and out depending on how far the player is from it. The chunks contain all the world information in its area; blocks, trees and more. The terrain is generated using a simplex noise script.
### **Chunks**
### **Blocks**
#### **Block types** *(WIP)*
* Air
* Grass
* Stone
### **Noise maps**
### **Biomes** *(WIP)*
#### **Deciding factors**
# **Sources**
## **World Generation**
[Layered Noise by Ronja](https://www.ronja-tutorials.com/post/027-layered-noise/#layered-multidimensional-noise)  

[Book of shaders - Nosie](https://thebookofshaders.com/11/)  
[Book of shaders - Fractal Brownian Motion](https://thebookofshaders.com/13/)