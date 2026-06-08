PhonePad (Code Name)
-----------------------
A fast-communication app that turns your smartphone into a gamepad. 

Currently supports Android devices. 

It allows your phone and connected controller to function similarly to popular game controllers such as the DualShock 4 and Xbox 360 controller.

Demostrative Video: https://www.youtube.com/watch?v=13fSelx3i2I 

## Build Android APK

### Prerequisites
- Unity `6000.0.43f1` (see `ProjectSettings/ProjectVersion.txt`)
- Android Build Support module installed in Unity Hub (SDK/NDK + OpenJDK)

### Build in Unity Editor
1. Open the project in Unity
2. `File` → `Build Settings...` → select `Android` → `Switch Platform`
3. Click `Build` and choose an output path ending in `.apk`

### Build from command line (Unity batchmode)
This repository includes a build method at `Assets/Editor/Build/BuildAndroid.cs`.

Example (adjust the Unity executable path for your OS):
`Unity -quit -batchmode -projectPath . -executeMethod PhonePad.Editor.Build.BuildAndroid.Build -outputPath Builds/Android/Phone2Pad.apk`

### Build via GitHub Actions
Run the `Build Android APK` workflow and download the `Phone2Pad-Android-APK` artifact.

Notes:
- The workflow requires a `UNITY_LICENSE` GitHub Actions secret.
- To install an *updated* APK over an existing install, it must be signed with the same keystore as the already-installed app.

## Install APK on Android
- On your phone, open the downloaded `.apk` and tap `Install`
- If prompted: `Settings` → `Apps` → (Browser / Files) → `Install unknown apps` → allow

## Technical specification

![image](https://github.com/user-attachments/assets/22369b47-ee61-48ff-9f0d-dd9c8264fe63)

### Architecture:
 - Gamepad services library (C++): https://github.com/Aileck/Gamepad_API
 - Server (Typescript, Node.js): https://github.com/Aileck/Gamepad-API-Testtool 
 - Client App (Unity C#): This repository :)
   
### Key Technical aspect
- The gamepad core services is written in C++ for better efficiency.
- Lightweight binary messaging with WebSocket + MessagePack.
- Mobile input handling via Unity Input System, with multi-input system support.

## Feature
### Done:
- An Android mobile app that can connect to a computer
- The mobile app can emulate either a DualShock 4 or Xbox 360 input system
- The mobile app can use an external controller, acting as a remote gamepad for the PC

### Next Step:
- Mobile:
  - Currently requires manual input of the PC IP address; next step is to search for local servers automatically
  - Create an decent UI
  - Customizable controller layout

- PC:
  - Develop a decent visually appealing gamepad management system on the PC side

### Want to do but need to research feasibility:
- Port the client to WebGL
- Port the client to some niche operating systems
- Enable connections beyond local network, including Bluetooth
