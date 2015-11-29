# Getting Ant #

Some systems (linux, Mac OS X) include Ant out of the box. On Windows you will have to install it manually if you want to run it on a command line, as shown below. Installation instructions are probably at [Apache Ant build framework](http://ant.apache.org/).

Eclipse comes with Ant, so you should be able to use it from within Eclipse without installing it for command-line use. I haven't tested this, since it's already installed on my system (Mac OS X).

# Using Ant within Eclipse #

Once you have opened the project in Eclipse, you should see a file called build.xml. Open this file for editing (don't change anything unless you know what you are doing!) and you will some targets in the "outline" pane on the right side of the Eclipse destkop. These have a green circle next to each Ant task (compile, run, jar, dist, clean, etc). To execute an Ant task, right-click on it and select the Run As / Ant Build menu item that pops up.

# Compiling the project (command-line stuff from here forward) #

Assuming you have a directory called "pathfinder" which you got by checking out the SVN repo:

```
$ cd pathfinder
$ ant compile
```

This will compile the source files from the src directory (PathFinder.java, for example) into corresponding class files in the build directory.

# Creating a jar file #

```
$ ant jar
```

will create a jar file from the class files in build (compiling first if necessary), and put the jar file in build/jar/.

# Running #

```
$ ant run
```

This will take the jar file in build/jar/ (compiling and creating the jar file first if necessary) and execute the main method of the class specified in the build.xml main-class property (currently set to cis522/PathFinder.class).

# Cleaning #

```
$ ant clean
```

will remove the build directory and all files in it (compiled class files and jar files). It will also remove the dist directory.

# Making a distribution jar with timestamp #

```
$ ant dist
```

will create a timestamp-labeled jar file in dist/lib.