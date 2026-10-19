Hunter Lodge runtime assets

hunter_lodge_full_game.png is the optional construction/job icon. It is not a
single-sprite replacement for the building exterior.

Content/maps/HunterLodge_1.cmx, HunterLodge_2.cmx and HunterLodge_3.cmx supply the
three exterior layouts. They share Content/maps/HunterLodge.tmx and its 8x8 tile
footprint. The code reuses the installed game's leather_1 CMX/TMX interior.

The current imported database contains two custom props: the arrow barrel and
the hide rack. Keep imports.cdb, both referenced CDX files and their corresponding
Content/media PNG files together. Other doodads use the game's existing resources.

The TMX references Content/tilesets/*.tsx. Those tilesets reference the PNGs under
Content/media/tiles/. Keep these relative paths when distributing the example.

Content/.romestead-content-framework-id is an authoring marker used to keep the
CMX tool's content UUID stable. It remains in the source tree and is not deployed.

Unused concept art, duplicate pieces, night-painting helpers and old tileset XNBs
are not part of this example. If you add new doodads or night textures later,
include the complete CDB/CDX/PNG reference chain rather than only the source image.
