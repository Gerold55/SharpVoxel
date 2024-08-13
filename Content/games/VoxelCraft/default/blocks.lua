-- blocks.lua

function GetBlockNames()
    return {
        "dirt",
        "grass",
        "stone"
    }
end

function GetBlockTextureName(blockName)
    local textures = {
        dirt = "dirt_texture",
        grass = "grass_texture",
        stone = "stone_texture"
    }
    return textures[blockName] or "default_texture"
end
