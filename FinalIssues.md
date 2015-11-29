# Technical docs #

architecture doc - Kurt
build support: how to build: Visual Studio, Monodevelop

generate doxygen docs - Kurt/Weston

> make sure all code is commented for good docs; file headers for each file, method headers for public methods - Weston?

# User docs #

readme.txt - Hang
> include directions for Linux/Mac with links to Monodevelop

user manual - Hang

in-app help - Jungyul

# Testing #

gather collection of GPX files for testing - Ka Long

good files:

> short

> long

> really long!

bad files:

> empty

> not XML data; just a text file

> XML file but with no GPX tags

> GPX file with corruption (unclosed tags)

> GPX file with bad data (alphanumeric rather than numeric lat/long data)

list of UI elements to test ("click the File Open dialog box, select a file...") - Jungyul?

> this should be a comprehensive set of directions about how to test every feature of the program

list of operating systems to test, with notes about differences (no file open dialog on Mac, for instance) - Jungyul?

# Package up code #

shell script for launching with mono on non-windows systems? - Kurt