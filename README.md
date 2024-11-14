# UnitySimDemo
Author(s): Krishna Bhatia
Rough draft, will make it sound nicer later

First install Unity Hub.

Then sign in, then install Unity Version 2022.3.52f1.
Be sure to include all Linux modules, as well as optionally Universal Windows Platform Support, and WebGL support.

Then install Microsoft Visual Studio. Be sure to add Unity support when installing.

Click Add Project. Make sure the identical Unity editor version is selected. Name the project accordingly.
Be sure to select the right directory.

Leave Unity Cloud Control on.

Make sure you have "Universal 3D" selected.

Create your project.

Once that's done, select Assets -> Install Package -> Custom Package -> Select the .unitypackage file present. Make sure all are selected, and add to your project.

Then install all packages listed below.

All packages not installed by default:
    Cinemachine

---------------------------------------------------------------

Issue Coding:

Issue area codes:

```
Environment - 1000
Rover Control - 2000
Stereo Camera - 3000
Algorithm Implementation - 4000
CAD - 5000
```

How code works:

first digit is a area of the issue. Second digit is the importance of the issue (0 - not important to 9 - urgent). Two last digits is a chronological order of the issue. 

For example:

`2353` - 2 is a "Rover Control" key digit, 3 is its importance, and 3 on the given scale is not as importnat. 53 means that is the 53rd issue from the same area.
