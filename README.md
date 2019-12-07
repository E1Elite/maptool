# MapTool

This program exists to apply conversion profiles, 'scripts' of sorts to map files from Command & Conquer: Tiberian Sun and Command & Conquer: Red Alert 2 and their respective expansion packs that offer ability to alter map's theater, tile, overlay and other object data, essentially allowing user to perform operations such as cross-theater, or even cross-game map conversions.

Project Page:

* http://github.com/starkku/maptool

Downloads: 

* http://projectphantom.net/utils/maptool

## Installation

As of current, MapTool has been designed to run on Windows operating systems, with Microsoft .NET Framework 4.0 as a requirement. Installation is simple, just place all of the required program files in a directory of your choice, and run it from there. 

## Usage

Using the graphical user interface (MapTool_UI.exe) should be fairly straightforward. If the program was installed correctly, available conversion profiles (conversion profiles are loaded from subdirectory called 'Profiles' in the program directory) should be displayed in a list for user to choose from, with a description for each of the profiles displayed next to the list if available. Maps can be added to the list of files to process by using Browse button or drag & drop on the file list itself.

Instructions for the command line tool (MapTool.exe) can be found by running the program with argument -h (f.ex `MapTool.exe -h`).
MapTool_UI.exe can also be run with the argument -log, which enables writing of full debug log file (which can get fairly large), and is passed to MapTool.exe. It can also be run with the argument -logx, which enables generating extensive log content of non-zero or non-default data from IsoMapPack5, OverlayPack and OverlayDataPack. This extended logging is for analysis and is large, so be advised to use it for one map at a time. When parameter -filex is used, it generates raw data as bin files for IsoMapPack5, OverlayPack and OverlayDataPack. 

Examples:

Command line MapTool.exe (filenames can use full path or relative path from the base directory):
MapTool.exe -i sov01umd.map -o sov01umd_out.map -p Profiles\misc_test.ini -log -logx -filex 

For UI based tool (can add the parameters to the shortcut link):
MapTool_UI.exe -log -logx -filex

Documentation on the contents of conversion profile INI files and how to write them can be found [here](https://github.com/starkku/maptool/blob/master/Conversion-Profile-Documentation.md).

## Acknowledgements

MapTool uses code from the following open-source projects to make it's functionality possible.

* CnCMaps Renderer by Frank Razenberg - http://github.com/zzattack/ccmaps-net
* OpenRA by [OpenRA Authors](https://raw.github.com/OpenRA/OpenRA/master/AUTHORS) - http://github.com/OpenRA/OpenRA

Additionally thanks to [E1 Elite](https://ppmforums.com/profile.php?mode=viewprofile&u=7356) for writing several conversion profile files to use with Command & Conquer: Tiberian Sun and implementing IsoMapPack5 optimization & ice growth fix features.

## License

See [COPYING](https://github.com/starkku/maptool/blob/master/COPYING).
