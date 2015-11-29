# Checkout #

You do this to checkout a fresh copy of the code from the SVN repo. Once checked out, you will make changes, add files, commit your changes, update your local directory from the repo, etc., but you shouldn't have to do another checkout unless you want to create a new working directory on your local machine. So, you should only do the checkout procedure once.

From a command prompt:

```
$ svn checkout https://gps-cue-sheet-generator.googlecode.com/svn/trunk/ pathfinder --username YOURUSERNAME
```

This will create a directory called "pathfinder" in the current directory, with the project files inside.

If it asks you for a password, do NOT input your gmail/google code password. This will not work. Instead, google code creates a special SVN password for you, which you can find at https://code.google.com/hosting/settings when logged in.

# Updating your local copy from the repository #

You do this to get the latest changes from the repository into your local working copy. From within the top-level directory of the source tree (the directory you checked out in the previous step):

```
$ svn update
```

You can also update a single file rather than the entire source directory. For instance, if you mess up a file (called myfile.cs, for instance) and want to revert to the repository's good copy, do this:

```
$ rm myfile.cs
$ svn update myfile.cs
```

# Checking in changes #

```
$ svn status
```

and

```
$ svn diff
```

are your friends. They will tell you if your local directory has changed files, and tell you the exact nature of the changes if you are unclear. Once you are happy that your local changes are ready to commit:

```
$ svn commit -m "SOME_GOOD_DESCRIPTION_OF_WHAT_YOU_DID_HERE"
```

# Adding Subversion to Eclipse #

This is a very thorough article on how to begin working with Subversion through the Eclipse IDE:

http://www.ibm.com/developerworks/opensource/library/os-ecl-subversion/