# AR Spectro-scope

**Theme:** AR/VR App  
**Platform:** Android (Unity)

## Project Description

An Augmented Reality (AR) application developed using the Unity platform. AR Spectro-scope overlays real-time spectral and scientific data onto physical objects through the device camera, enabling users to visualize hidden properties of their environment.

## Features

- Real-time AR object detection and overlay
- Spectral data visualization using AR markers
- Unity AR Foundation + ARCore integration
- Interactive UI panels in augmented space
- Android APK deployment

## Tech Stack

| Technology | Purpose |
|---|---|
| Unity 2022.3 LTS | Game Engine / AR Framework |
| AR Foundation | Cross-platform AR API |
| ARCore (Google) | Android AR tracking |
| C# | Scripting |
| Android SDK | Build target |

## How to Run

### Option 1 – Direct APK Install
1. Download the `.apk` file from the Releases section
2. Enable **Install from Unknown Sources** on your Android device
3. Install and launch the app
4. Point your camera at flat surfaces or AR markers

### Option 2 – Build from Source (Unity)
1. Install **Unity 2022.3 LTS** with Android Build Support
2. Clone this repository:
```bash
git clone https://github.com/RoHITKumar3456256/AR-Spectro-scope.git
```
3. Open the project in Unity Hub
4. Go to **File → Build Settings → Android → Switch Platform**
5. Click **Build** to generate APK

## Instructions

Run the apk file using android studio or directly install in the mobile device

## Requirements

- Android 8.0+ (ARCore supported device)
- Camera permission
- Unity 2022.3+ (for building from source)

## License

MIT License
