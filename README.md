# AR Spectro-scope 🔬

**Theme:** AR/VR App | **Platform:** Android | **Engine:** Unity 2022.3 LTS

---

## 📱 Project Description

An **Augmented Reality (AR) application** developed using the Unity platform. AR Spectro-scope overlays real-time spectral and scientific data onto physical objects through the device camera, enabling users to visualize hidden spectral properties of their environment.

Using **AR Foundation** and **Google ARCore**, the app detects flat surfaces in the real world and places an interactive spectral analyzer overlay — displaying wavelength data, spectral bands, and intensity graphs in augmented space.

---

## ✨ Features

- 📡 Real-time AR plane detection using ARCore
- 🌈 Spectral data visualization with wavelength overlay (380nm–750nm)
- 📊 Live spectral intensity graph rendered in AR space
- 🎯 Tap-to-place interactive scanner in the real world
- 📱 Optimized for Android mobile devices
- 🔄 Reset and re-scan functionality
- 💡 Pulse & rotation animations on AR overlay

---

## 🛠️ Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| Unity | 2022.3 LTS | Game Engine |
| AR Foundation | 5.0.7 | Cross-platform AR API |
| ARCore (Google) | 5.0.7 | Android AR Tracking |
| C# | — | Scripting |
| TextMesh Pro | 3.0.6 | AR Text UI |
| Universal RP | 14.0.9 | Rendering Pipeline |
| Android SDK | API 26–33 | Build Target |

---

## 📁 Project Structure

```
AR-Spectro-scope/
├── Assets/
│   ├── Scripts/
│   │   ├── ARSpectroManager.cs       # Main AR session controller
│   │   ├── SpectroAnalyzer.cs        # Spectral analysis engine
│   │   ├── SpectroOverlay.cs         # AR overlay animations
│   │   ├── UIController.cs           # HUD & button management
│   │   ├── ARPlaneVisualizer.cs      # Plane detection visuals
│   │   └── CameraPermissionHandler.cs # Android camera permissions
│   ├── Scenes/                        # Unity scenes
│   ├── Prefabs/                       # AR overlay prefabs
│   ├── Materials/                     # Shaders & materials
│   ├── Textures/                      # UI & overlay textures
│   └── Plugins/
│       └── Android/
│           └── AndroidManifest.xml   # Android AR configuration
├── Packages/
│   └── manifest.json                 # Unity package dependencies
├── ProjectSettings/
│   ├── ProjectSettings.asset         # Build & player settings
│   └── XRGeneralSettings.asset       # AR/XR configuration
└── README.md
```

---

## 🚀 How to Run

### Option 1 — Direct APK Install (Recommended)
1. Download the `.apk` from the [Releases](../../releases) section
2. On your Android device: **Settings → Security → Enable Unknown Sources**
3. Transfer and install the APK
4. Open **AR Spectro-scope**
5. Allow camera permission when prompted
6. Point camera at a flat surface and tap to scan!

### Option 2 — Build from Source (Unity)
1. Install **Unity 2022.3 LTS** with:
   - Android Build Support
   - Android SDK & NDK Tools
   - OpenJDK
2. Clone this repository:
   ```bash
   git clone https://github.com/RoHITKumar3456256/AR-Spectro-scope.git
   ```
3. Open project in **Unity Hub**
4. Open the main scene: `Assets/Scenes/ARSpectroscope.unity`
5. Go to **File → Build Settings**
6. Select **Android** → Click **Switch Platform**
7. Connect Android device via USB
8. Click **Build and Run**

---

## 📋 Device Requirements

| Requirement | Minimum |
|---|---|
| Android Version | 8.0 (Oreo) |
| ARCore Support | Required ([Check device list](https://developers.google.com/ar/devices)) |
| Camera | Required |
| RAM | 3GB+ |
| GPU | OpenGL ES 3.0+ |

---

## 📸 How It Works

1. **Launch** the app — AR session initializes automatically
2. **Scan** your environment — AR detects flat surfaces (floor, table, etc.)
3. **Tap** on a detected surface — Spectro-scope overlay appears in AR
4. **Analyze** — The scanner runs a spectral analysis (2.5 seconds)
5. **View Results** — Peak wavelength, spectral band, and intensity displayed
6. **Reset** to scan again anytime

---

## 👥 Team

Developed for **Microsoft Build AI Hackathon**

---

## 📄 License

MIT License — free to use and modify.
