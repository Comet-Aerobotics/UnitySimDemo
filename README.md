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
     - **All Three Linux Build Support Modules**
     - **Universal Windows Platform Support** (optional, for building on Windows)
     - **WebGL Build Support** (optional, for building on the web)
3. Click **Next** to begin the installation.

### 3. Install Microsoft Visual Studio
1. Download and install **Microsoft Visual Studio** from the [Visual Studio Download Page](https://visualstudio.microsoft.com/).
2. When prompted during installation, be sure to check the **Unity support** component.  
   > **Note**: This will allow Visual Studio to integrate smoothly with Unity for coding and debugging.

---

## Project Setup in Unity

1. Open **Unity Hub** and click **Add**, the button with the dropdown arrow.
2. Select **Add project from disk** and then select the file folder containing this project.
3. The project should start compiling and Unity should open! Ensure you are using the correct version installed earlier, **Unity Version 2022.3.52f1**.

---

## Installing Required Unity Packages

1. Open the **Package Manager**: Go to **Window** > **Package Manager**.
2. Search for and install if they are not already installed:
    - **Cinemachine** (used for basic camera control).  

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
## Integrating ROS2 on Unity: 

1. Open the **Package Manager** on **Unity**: Go to **Window** < **Package Manager**.
2. Click on the **+** on the top-left.
3. Click on **Add package from git URL**.
4. Add the first git-link: https://github.com/Unity-Technologies/ROS-TCP-Connector.git?path=/com.unity.robotics.ros-tcp-connector (ROS-TCP-CONNECTOR)
5. Add the second git-link: https://github.com/Unity-Technologies/ROS-TCP-Connector.git?path=/com.unity.robotics.visualizations (Robot Visualizations)      

#### Additional Information:
Article on Ros2 integration on Unity: 
1. https://unity.com/blog/engine-platform/advance-your-robot-autonomy-with-ros-2-and-unity
2. https://github.com/Unity-Technologies/ROS-TCP-Connector

---
