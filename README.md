# UnitySimDemo

**Description**: A simulation demo in Unity designed to help beginners set up, run, and code for various environments and control mechanisms.

---


### 1. Install Unity Hub
1. Download **Unity Hub** from the [Unity Hub Download Page](https://unity.com/download).
2. Open Unity Hub and sign in or create an account if you don’t have one.

### 2. Install Unity Editor
1. Inside Unity Hub, go to the **Installs** tab.
2. Install **Unity Version 2022.3.52f1**.
   - **Modules to include**:
     - **Linux Build Support**
     - **Universal Windows Platform Support** (optional, for building on Windows)
     - **WebGL Build Support** (optional, for building on the web)
3. Click **Next** to begin the installation.

### 3. Install Microsoft Visual Studio
1. Download and install **Microsoft Visual Studio** from the [Visual Studio Download Page](https://visualstudio.microsoft.com/).
2. When prompted during installation, be sure to check the **Unity support** component.  
   > **Note**: This will allow Visual Studio to integrate smoothly with Unity for coding and debugging.

---

## Project Setup in Unity

1. Open **Unity Hub** and click **Add Project**.
2. Select the **Unity 2022.3.52f1** editor version.
3. Name the project as desired and choose a location on your computer.
4. Make sure **Unity Cloud Control** is enabled (recommended).
5. For **Template**, select **Universal 3D** to ensure compatibility.
6. Click **Create** to initialize the project.

---

### Import UnitySimDemo Assets

1. In Unity, go to the **Assets** menu.
2. Select **Import Package** > **Custom Package**.
3. Locate and select the `.unitypackage` file included with this project.
4. In the Import Unity Package window, make sure all items are checked, then click **Import**.

---

## Installing Required Unity Packages

1. Open the **Package Manager**: Go to **Window** > **Package Manager**.
2. Search for and install:
    - **Cinemachine** (used for camera control).  

---
   > **Note**: Additional packages may be required for advanced functionality. If any prompts for missing packages appear, follow the instructions to install them.

---

## Coding Guidelines

To make code and issue tracking simpler, we’ve assigned unique codes to various areas of the project. Use these codes to reference and organize code changes.

### Issue Area Codes

| Area                   | Code  |
|------------------------|-------|
| Environment            | 1000  |
| Rover Control          | 2000  |
| Stereo Camera          | 3000  |
| Algorithm Implementation | 4000  |
| CAD                    | 5000  |

---

### Code Numbering System

Each issue or code change should be labeled according to the following structure:

- **First Digit**: Project area (see table above)
- **Second Digit**: Priority (0 = low, 9 = high urgency)
- **Last Two Digits**: Chronological order of the issue

#### Example:
`2353` -  
- **2**: Indicates **Rover Control**  
- **3**: Priority level (mid-level priority)  
- **53**: The 53rd issue in this area  

Use these codes in comments and commit messages to keep the project organized and to make it easy to understand issue importance at a glance.

---
