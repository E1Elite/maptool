# Conversion Profile Documentation

Conversion profiles used by MapTool are INI files (text-based configuration files) which determine what sort of changes to tile & object data the tool should perform.

Users unfamiliar with structure of INI files should use one of the pre-made profile INI files as a reference.

## Available Sections

### ProfileData

#### Name

Name displayed in the GUI for this profile.

#### Description

Description displayed in the GUI for this profile.

#### IncludeFiles

A comma-separated list of filenames including file extensions to read from *same directory as the current conversion profile*. Contents of these files will be merged with the current one. This only works on one level, so trying to include files from already included files will fail.

#### ApplyMapOptimization

If set to yes / true, will ensure that on the saved map, section with name **MultiplayerDialogSettings** will be the first section, immediately followed by section with name **Basic** and finally the section with name **Digest** will be the last. This potentially allows for game to find these particular sections marginally faster.

#### ApplyMapCompress

If set to yes / true, no unnecessary white space is put on the saved map. This allows for map size to be marginally smaller.

### TheaterRules

#### ApplicableTheaters

A comma-separated list of theater ID's which must match with one declared in a map for the tool to process it. Defaults to **TEMPERATE,SNOW,URBAN,DESERT,NEWURBAN,LUNAR** the list is omitted.

#### NewTheater

A single theater ID which is assigned on any processed maps. If omitted, defaults to processed map's theater.

### IsoMapPack5

Do note that effects of both SortBy and RemoveLevel0ClearTiles are removed when a map is opened and saved in a map editor (Final Alert 2 / FinalSun).

#### SortBy

Allows for sorting of tile data in IsoMapPack5, resulting in potentially better compression.

Sorting by following values is available: **X, Y, TILEINDEX, SUBTILEINDEX, LEVEL, ICEGROWTH, X\_LEVEL\_TILEINDEX** (Sort by X then by LEVEL then by TILEINDEX - the remaining ones follow a similar pattern)**, X\_TILEINDEX\_LEVEL, LEVEL\_X\_TILEINDEX,TILEINDEX\_X\_LEVEL**.

Good compression is achieved by using either **X\_LEVEL\_TILEINDEX** or **X\_TILEINDEX\_LEVEL**.

#### RemoveLevel0ClearTiles

If set to yes / true, removes all clear tiles at lowest elevation level (0). Since game always fills cells that are missing tiles with clear tiles that have elevation level of 0, removing them from IsoMapPack5 is a way to trim down the size of a map file.

#### IceGrowthFixReset

If set to yes / true, this will disable ice growth for the entire map. This overrides IceGrowthFixUseBuilding, so omit or set to no / false if you wish to use that feature.

#### IceGrowthFixUseBuilding

A single BuildingType ID used to mark tiles which have such building placed on them on the map as tiles where ice is allowed to grow. It is recommended to use a 'dummy building' like TSTLAMP and place it on all ice tiles that should be allowed to regrow if damaged, as well as surrounding water tiles where ice should be allowed to grow / expand to.

As a note, there is no way to set or view the ice growth flag in map editor, either for individual tiles or otherwise but it is kept for existing tiles if set upon loading and / or saving a map.

### TileRules

A list of tile index conversion rules, each on it's own line with | as separator between source and destination value, as well as optional height override and sub-tile index override values.

To assist in figuring out the numbers to use, MapTool can be run with command line parameter *-l* with a game theater configuration INI (such as temperat(md).ini) as input file to extract a listing of tiles and their tile indices to a plaintext output file.

**Example #1:**
<pre>
[TileRules]
0-15|25-40
</pre>

Tiles 0-15 will get converted to tiles 25-40, respectively, respecting the range declarations.

**Example #2:**
<pre>
[TileRules]
0-15|25
</pre>

This example should produce results identical with the first one.

**Example #3:**
<pre>
[TileRules]
0-15|25-25
</pre>

Using a range declaration with identical start and end points as destination forces all matching source tiles to be converted to that specific tile.

**Example #4:**
<pre>
[TileRules]
0-15|25-40|1
</pre>

Adding a third value overrides the height of all of the applicable tiles with specific value. Only values from 0 to 14 are respected, with values lower than 0 interpreted as 0, and values higher than 14 interpreted as 14.

**Example #5:**
<pre>
[TileRules]
0-15|25-40|*|0
</pre>

Fourth value serves as an override to tile's sub-tile index, serving to determine which particular piece of that tile is used for a map cell. It might be necessary to set the override to 0 if you are converting from tiles with more than one sub-tile to a tile with just one.

Also worth noting is that if you declare sub-tile index override, you must also declare height override before it. Substituting the value with * retains the original height values in processed maps.

**Example #6:**
<pre>
[TileRules]
0-15|25~40
16|25~40
</pre>

This randomly assigns new tile index from range 25 to 45 to tiles 0-15, as well as all tile 16.

### ModifyOverlays

A list of map coordinates and its overlay index with its overlay frame index, each on its own line can be specified with a , as separator. This allows adding, replacing and removing of overlays in the map. The first two comma separated values gives the X and Y coordinates. The next two comma separated values give the Overlay Index (which is a 0-based index from [OverlayTypes] section list in rules(md).ini) and its overlay frame index in its SHP file. X and Y map coordinates can have values from 1 to 511 whereas OverlayIndex and OverlayFrameIndex can be from 0 to 255. OverlayIndex value 255 (255 is used for no overlay) will remove any overlay present at the specified coordinates. 

FinalSun and FinalAlert2 shows coordinates in reverse, that is Y / X on its status bar. For ease of use in such cases, SwitchXYToYX (false by default) can be set to true. When set it will consider the first comma separated value as Y and the second specified value as X. 

By default, overlays modification will not check if any overlay is already present on a cell or not. To check what overlay is already present on a cell and then apply the modification, RequiredList and ForbiddenList can be supplied with a list of comma (,) separated overlay index values. RequiredList allows modification only if the mentioned overlay indices are present on the considered cell. ForbiddenList prevents any modifications if the mentioned overlay indices are present on a cell. Check examples given below.

Coordinates that fall outside the map are ignored.

ModifyOverlays is applied and finishes before the OverlayRules start, so rules defined in OverlayRules will convert independently of the overlays modified here.

**Example #1:**

Format: X,Y,OverlayIndex,OverlayFrameIndex
<pre>
[ModifyOverlays]
22,40,2,0
23,40,2,1
24,40,2,2
25,40,2,3
26,40,255,0
</pre>

First line sets the overlay with index 2 (GAWALL in vanilla game) and its SHP frame to 0 at map coordinates with X at 22 and Y at 40. Last line removes any overlay present at X=26 and Y=40.

**Example #2:**

Format when SwitchXYToYX is true: Y,X,OverlayIndex,OverlayFrameIndex
<pre>
[ModifyOverlays]
SwitchXYToYX=true
40,22,2,0
40,23,2,1
40,24,2,2
40,25,2,3
</pre>

First change (2nd line in this section) sets the overlay with index 2 (GAWALL in vanilla game) and its SHP frame at 0 at map coordinates with X at 22 and Y at 40.

**Example #3:**

<pre>
[ModifyOverlays]
SwitchXYToYX=true
40,22,2,0
40,23,2,1
40,24,2,2
40,25,2,3
RequiredList=255
</pre>

Apply the modifications only if the cell has no overlay (255 is for no overlay)

**Example #4:**

<pre>
[ModifyOverlays]
SwitchXYToYX=true
40,22,2,0
40,23,2,1
40,24,2,2
40,25,2,3
ForbiddenList=2,26
</pre>

Apply the modifications only if the cell does not already have GAWALL or NAWALL (vanilla game rules(md).ini has GAWALL at overlay index 2 and NAWALL at overlay index 26).

### OverlayRules

A list of overlay and its frame conversion rules, each on it's own line with a | as a separator between source and destination value. First 2 values are used for overlay index (which is a 0-based index from [OverlayTypes] section list in rules(md).ini) and 3rd and 4th values are used for overlay frame index (the 0-based frame index in the overlay's SHP file) replacements.

Overlay values from 0 to 254 are available for regular use. Using 255 as destination value will remove overlays. Using 255 as source value is not valid for overlay and results in the conversion rule being ignored. Any ID exceeding 255 is set to 255 which means no overlay.

Overlay frame indices from 0 to 255 are available for regular use. Any value exceeding 255 is set to 0 which is the first frame of its overlay.

**Example #1:**
<pre>
[OverlayRules]
0|5
</pre>

All overlays with index 0 are converted to overlays with index 5, keeping overlay frame indices same.

**Example #2:**
<pre>
[OverlayRules]
15|20~30
</pre>

Overlays with index 15 are randomly assigned new indices from range of 20 to 30, keeping overlay frame indices same.

**Example #3:**
<pre>
[OverlayRules]
4-8|8
</pre>

Overlays with index in the range of 4 to 8 are assigned new indices in the range of 8 to 12, keeping overlay frame indices same. If multiple set of indices need to be replaced with same index, then use like 4-8|8-8.

**Example #4:**
<pre>
[OverlayRules]
16-19|20~30
</pre>

Overlays with indices in the range of 16 to 19 are randomly assigned with new indices in the range of 20 to 30, keeping overlay frame indices same.

**Example #5:**
<pre>
[OverlayRules]
4|4|0-20|50-70
</pre>

For overlays with index 4, overlay frame indices in the range of 0 to 20 are replaced with overlay frame indices in the range of 50 to 70.

**Example #6:**
<pre>
[OverlayRules]
4-5|7-8|0-10|100~110
</pre>

Overlays indices in the range of 4 to 5 are replaced overlay indices in the range 7 to 8, with overlay frames indices in the range of 0 to 10 are randomly assigned with overlay frame indices from 100 to 110.

### ObjectRules

A list of object ID conversion rules, each on it's own line with a | as a separator between source and destination value.

**Example #1:**
<pre>
[ObjectRules]
GACNST|YACNST
</pre>

Will convert any objects, be it Infantry, Building, Aircraft, Vehicle or Terrain with ID GACNST on the processed maps to an object of same type with ID YACNST.

**Example #2**
<pre>
[ObjectRules]
GACNST
</pre>

Will remove any objects, be it Infantry, Building, Aircraft, Vehicle or Terrain with ID GACNST on the processed maps.

**Example #3**
<pre>
[ObjectRules]
TREE04|TREE04,,TREE16,TREE17
</pre>

Will convert any objects, be it Infantry, Building, Aircraft, Vehicle or Terrain with ID TREE04 on the processed maps to an object of same type with random ID from TREE04, TREE16, TREE17 or random remove as one of those is empty entry. Any duplicates will get more chance on the randomness. Buildings replaced with a bigger foundation could cause problems, so care should be taken.

### SectionRules

A list of section name, keys and values conversion rules, each on it's own line with | as a separator between section name, key and value information.

**Example #1:**
<pre>
[SectionRules]
Basic|Official|no
</pre>

Sets the value for key 'Official' under section 'Basic' to 'no'.

**Example #2:**
<pre>
[SectionRules]
Basic|Official=
</pre>

Removes key 'Official' under section 'Basic'.

**Example #3:**
<pre>
[SectionRules]
Basic=
</pre>

Removes section 'Basic' altogether.

**Example #4:**
<pre>
[SectionRules]
Basic=NotSoBasic|Official=Unofficial|Yes
</pre>

Changes name of section 'Basic' to 'NotSoBasic', name of key 'Official' under said section to 'Unofficial' and it's value to 'Yes'.

**Example #5:**
<pre>
[SectionRules]
Basic|Official|$GETVAL(SpecialFlags,DestroyableBridges)
</pre>

Sets the value of key 'Official' under section 'Basic' to the value of key 'DestroyableBridges' under section 'SpecialFlags'.

**Example #6:**
<pre>
[SectionRules]
Lighting|IonAmbient|$GETVAL(Lighting,Ambient,+,0.1)
Lighting|IonRed|$GETVAL(Lighting,Red,-,0.1)
Lighting|IonGreen|$GETVAL(Lighting,Green,*,0.5)
Lighting|IonBlue|$GETVAL(Lighting,Green,/,0.25)
Lighting|IonLevel|$GETVAL(Lighting,Level,*,0.2515,false)
</pre>

Sets the value of following keys under 'Lighting' to:

'IonAmbient' to value of 'Ambient' plus 0.1.  
'IonRed' to value of 'Red' minus 0.1.  
'IonGreen' to value of 'Green' multiplied by 0.25.  
'IonBlue' to value of 'Blue' divided by 0.  
'IonLevel' to value of 'Level' multiplied by 0.2515 with fractional part of the result truncated.  

Negative values can be used for the operand. With / operator, using 0 is treated same way as 1.

### LogX

Preferences for the extended logging functionality which logs non-zero/non-default values in IsoMapPack5/OverlayPack/OverlayDataPack. This section has effect only when the MapTool.exe or MapTool_UI.exe is supplied with argument/parameter of -logx. 

FinalSun and FinalAlert2 shows coordinates in reverse, that is Y / X on its status bar. For ease of use in such cases, SwitchXYToYX (false by default) can be set to true. When set it will log coordinates of Y first and then the value of X. 

The extended log becomes huge, so IsoMapPack5 or Overlay sections can be hidden from showing in the log by setting true to HideTilesLog or HideOverlaysLog respectively. By default both the HideTilesLog and HideOverlaysLog are set to false, showing the full log for IsoMapPack5, OverlayPack and OverlayDataPack.

**Example:**
<pre>
[LogX]
SwitchXYToYX=true
HideTilesLog=true
</pre>

With this example, log will show the coordinates of Y first and X second. This will also hide tiles log, so only the overlay and overlay frames related extended log will be shown.
