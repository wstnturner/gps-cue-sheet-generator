Meeting notes 1/14/11

Weston met with Michal, and discussed concerns that we may be biting off too much with features.

Features that are not necessary, but nice:
  * cacheing
  * arbitrary POIs

Also discussed algorithms for path finding; we can use these to improve our algorithm in version 2, perhaps.

In user documentation, need to be sure to explain that the process can take quite a while and set expectations accordingly.

Multithreading may be required to insure that the UI remains responsive even while geocoding requests are taking place.

Discussed converting to Java, then considered not doing that and instead keeping it in C#. Advantages of keeping in C#:
  * reduced time (up front, at least)
  * learning new skills


Challenges with keeping in C#:
  * learning curve for 4/5 team members may cancel out time savings from not translating to Java up front.
  * not possible to easily convert to a web app for part 2 because would require IIS.

Team members agree that we will keep it in C#.


To dos:

Kurt:
  * update project plan to reflect keeping in C#.
  * archive Java stuff in SVN repo. - DONE
  * learn about automating code documentation in C#.

Weston:
  * import existing code into SVN. - DONE

Hang: work on UI.

Jungyul: work on UI.

Ka Long: learn about unit testing in C#.

Everybody: get the code working on their own machines. Please get in touch if you have any issues checking out the project from SVN.

Thoughts for version 2, if we don't make it a webapp:
  * improve the algorithm
  * replace static map with a dynamic map in an embedded web browser.
  * create a website that could be used for sharing results