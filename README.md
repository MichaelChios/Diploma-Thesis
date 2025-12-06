# Diploma Thesis: Mixed Reality Smartphone Controllers

Expansion and evaluation of Mixed Reality interaction controllers using a mobile phone.

## Overview

This project is a **Unity-based Mixed Reality application** that explores using a smartphone as an advanced interaction controller for XR/VR environments. The system enables users to control complex spatial interactions in a mixed reality setting through intuitive mobile device gestures and touch inputs.

### Key Features

- **Smartphone-as-Controller**: Uses a mobile device to control VR/XR interactions
- **Multi-Touch Gesture Recognition**: Pinch detection, force sensing, and trajectory drawing
- **Real-time Networking**: WebSocket-based communication between HoloLens/XR headset and smartphone
- **Physics-Based Interactions**: Orbital mechanics, projectile physics, and celestial interactions
- **Visual Feedback**: Real-time trajectory visualization and UI feedback on both devices
- **Practice Modes**: Dedicated scenes for testing and practicing various interaction techniques

## Project Structure

### Core Components

#### Networking (`Assets/scripts/Networking/`)
Handles real-time communication between the XR device and smartphone:
- **WebSocket Communication**: `ClientSocketWs.cs`, `ServerSocketWs.cs`, `PeerWs.cs`
- **Message System**: `Message.cs`, `MessageFactory.cs`, `SerializablePacket.cs`
- **Protocol Handlers**: `PacketHandler.cs`, `MessageHelper.cs`
- **Serialization**: Custom binary serialization with `SerializationExtensions.cs`

Key classes:
- `BasePeer`: Base class for peer communication
- `PeerWs`: WebSocket peer implementation with message queueing and delivery options
- `PacketHandler`: Manages incoming and outgoing message packets

#### Player Controller (`Assets/scripts/Player/`)
Manages player interactions and gameplay mechanics:

**Main Control Systems**:
- `ActionsController.cs`: Orchestrates different actions (spawn, shoot, force adjustment)
- `Phone Controlls/` subdirectory containing:
  - `ShootController.cs`: Handles projectile launching
  - `SpawnController.cs`: Manages object spawning
  - `ForceController.cs`: Controls force/power input via slider
  - `RotateController.cs`: Handles rotation inputs
  - `PinchDetector.cs`: Detects pinch gestures on the smartphone
  - `NewForceController.cs`, `ThrowForcePinchSliderRemapper.cs`: Advanced force mapping

**Physics & Gameplay**:
- `projectileGravity.cs`: Applies realistic gravity to projectiles
- `OrbitDetection.cs`: Detects orbital mechanics
- `GoalDetection.cs`: Manages goal/objective detection
- `IgnoreCollisionWithCelestials.cs`: Physics collision handling
- `TimerCountdown.cs`: Game timing mechanics
- `DrawSpawnedTrajectory.cs`: Visualizes projectile paths

#### Planets System (`Assets/scripts/Planets/`)
Simulates celestial mechanics:
- `physicsOrbit.cs`: Orbital physics simulation
- `PlanetRotation.cs`: Planet rotation animations

#### Gameplay Utilities (`Assets/scripts/Player/Gameplay/`)
Additional gameplay-specific functionality

#### Practice Mode (`Assets/scripts/Player/Practice/`)
Training and testing scenarios

### Scenes (`Assets/Scenes/`)

Multiple test and gameplay scenes:
- **Primary Scenes**:
  - `SolarSystem2.1.unity` - Main solar system interaction
  - `SolarSystem2.2.unity` - Enhanced solar system variant
  - `SolarSystemWithPhoneController.unity` - Phone-controlled solar system
  - `SolarSystemWithPhoneController2.unity` - Improved version

- **Training Scenes**:
  - `PracticeScene1.unity` - Initial practice
  - `PracticeScene2.unity` - Advanced practice
  
- **Utility Scenes**:
  - `TestScene.unity` - Testing and debugging
  - `SmartphoneJoystick.unity` - Joystick interaction testing
  - `SampleScene.unity` - Basic reference scene

### Assets

- **Materials** (`Assets/materials/`): Earth and celestial object materials
- **Textures** (`Assets/textures/`): Visual assets for planets and UI
- **Prefabs** (`Assets/prefabs/`): Reusable game object templates
- **Input System** (`Assets/InputSystemCustom/`): Custom input configuration and ArUCO marker support
- **XR Integration** (`Assets/XR/`): Mixed Reality and OpenXR setup

## Technical Stack

### Unity Engine
- **Version**: 2022.3.20f1
- **Engine Features**: Physics, UI, Input System, XR support

### Mixed Reality Toolkit (MRTK)
- **Version**: 2.8.3
- **Components**: Foundation, Extensions, Standard Assets, Tools, Examples
- **Purpose**: HoloLens 2 and Windows Mixed Reality support

### XR Support
- **OpenXR**: Version 1.12.1 (cross-platform XR)
- **Mixed Reality OpenXR**: Version 1.11.2 (Windows MR specific)
- **Input System**: Unity's new InputSystem with custom actions

### Key Dependencies
- **TextMeshPro**: Version 3.0.6 (advanced text rendering)
- **Newtonsoft JSON**: Version 3.2.1 (JSON serialization)
- **WebSocket Support**: Custom WebSocket implementation for networking
- **Visual Scripting**: Version 1.9.1 (visual programming support)
- **Timeline**: Version 1.7.6 (animation and sequencing)

## Getting Started

### Prerequisites
- Unity 2022.3.20f1 or compatible
- Mixed Reality Toolkit 2.8.3
- OpenXR and Mixed Reality OpenXR plugins
- A smartphone with touchscreen capabilities
- (Optional) HoloLens 2 or compatible XR device

### Installation

1. **Clone the repository**
   ```
   git clone <repository-url>
   cd Diploma-Thesis
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Add the project folder
   - Ensure Unity 2022.3.20f1 is installed
   - Open the project

3. **Install Dependencies**
   - Dependencies are managed via `Packages/manifest.json`
   - Unity will automatically resolve packages on project load

4. **Configure Input**
   - Review `Assets/InputSystemCustom/Input Actions.inputactions`
   - Configure smartphone input mappings as needed

### Running the Project

1. **Select a Scene**
   - Open `Assets/Scenes/SolarSystemWithPhoneController.unity` for the main experience
   - Or choose a practice scene for testing

2. **Build for Target Device**
   - For HoloLens 2: Build and deploy using UWP (Universal Windows Platform)
   - For smartphone controller app: Build as standalone mobile application
   - For testing: Run in Unity Editor with mock input

3. **Network Setup**
   - Ensure both devices are on the same network
   - Configure WebSocket endpoints in network scripts
   - Both applications will auto-discover and connect

## Architecture Highlights

### Message-Based Communication
All device communication uses a serialized packet system:
- Binary serialization for efficiency
- Message factory pattern for extensibility
- Delivery methods (reliable, unreliable, etc.)

### Action-Based Input System
- Decoupled input from logic using the new Input System
- Custom action maps for smartphone gestures
- Support for multiple input devices simultaneously

### Physics Integration
- Realistic projectile physics with gravity
- Orbital mechanics simulation
- Collision detection and response

### Real-Time Synchronization
- WebSocket-based low-latency communication
- Message queueing with configurable delays
- Automatic reconnection handling

## Development Guidelines

### Adding New Features

**New Interaction Controller**:
1. Create a new script in `Assets/scripts/Player/Phone Controlls/`
2. Inherit from `MonoBehaviour`
3. Register input actions from `Input Actions.inputactions`
4. Integrate with `ActionsController.cs`

**New Networking Message**:
1. Create a message class inheriting from `Message.cs`
2. Implement serialization methods
3. Register in `MessageFactory.cs`
4. Add handler in `PacketHandler.cs`

**New Scene**:
1. Create in `Assets/Scenes/`
2. Follow naming convention: `[Purpose][Version].unity`
3. Include necessary controllers and managers

## Build & Deployment

### For HoloLens 2 / Windows Mixed Reality
```
Build Settings:
- Platform: Universal Windows Platform (UWP)
- Architecture: ARM64
- Min SDK Version: 10.0.18362.0
- Target SDK Version: Latest installed
```

### For Smartphone Controller App
```
Build Settings:
- Platform: Android or iOS
- Minimum API Level: Depends on target device
- Input System: New Input System enabled
```

## Testing

### Practice Scenes
- `PracticeScene1.unity` and `PracticeScene2.unity` provide isolated testing environments
- `TestScene.unity` for debugging specific components
- `SmartphoneJoystick.unity` for input validation

### Network Testing
- Verify WebSocket connections in console output
- Check serialization/deserialization of custom messages
- Monitor network latency and packet loss

## Project Status

This is an active diploma thesis project focused on:
- Expanding mixed reality interaction possibilities
- Evaluating smartphone as a viable MR controller
- Optimizing gesture recognition and input mapping
- Performance analysis across different devices

## Author

Created as part of a diploma thesis project on Mixed Reality interaction controllers.

---

**Last Updated**: December 2025
**Unity Version**: 2022.3.20f1
**MRTK Version**: 2.8.3
