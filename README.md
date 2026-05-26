# Arcade Racing Game - Unity Project

A simple arcade-style racing game built in Unity, featuring a drivable car with realistic acceleration physics, free 3D assets, and road environments with day and night scenes.

---
 
## 📌Overview
 
This project is a Unity-based prototype of an arcade racing game. It includes a controllable car with acceleration, deceleration, and reverse, placed on procedurally shaped road meshes with day and night skyboxes.

---
 
## 🚘Car Controller
 
The car is driven by `Voitureprincipal.cs`, a MonoBehaviour script attached to the car prefab.

---

 
### ⚙️ Parameters (editable in the Inspector)
 
| Parameter | Default | Description |
|---|---|---|
| `vitesse` | `0` | Current speed (managed at runtime) |
| `acceleration` | `0.02` | Rate of speed increase when pressing forward |
| `deceleration` | `0.02` | Rate of natural speed loss / braking |
| `maxSpeed` | `0.25` | Maximum forward speed |
| `reverseSpeed` | `0.1` | Speed cap when reversing |
| `rotationSpeed` | `0.1` | Turning sensitivity |

---

### 🛣️Physics behaviour
 
- Pressing **forward** smoothly accelerates up to `maxSpeed`
- Releasing the key decelerates naturally (coasting)
- Pressing **back** while moving forward brakes; once stopped, the car reverses up to half of `maxSpeed`
- Steering rotates the car around its Y-axis
---
 
## 🚗Controls
 
| Key | Action |
|-----|--------|
| `Z` | Accelerate (forward) |
| `S` | Brake / Reverse |
| `Q` | Turn left |
| `D` | Turn right |
 
> These use the **AZERTY** keyboard layout. If you use a QWERTY keyboard, remap the `KeyCode` values in `Voitureprincipal.cs` (e.g. `KeyCode.W`, `KeyCode.A`).


---

## Gesture Controller (Python)

The `Scripts/HandDetector/` folder contains a standalone Python application that lets you **control the car with your hand** through a webcam, without touching the keyboard.

### ⚙️How it works

1. **`hand_detector.py`** captures webcam frames and runs them through Google MediaPipe's `HandLandmarker` model, extracting the 21 3D landmarks of the detected hand and drawing the skeleton overlay on screen.
2. **`gesture_classifier.py`** interprets those landmarks.

3. **`sender.py`** broadcasts the command string over **UDP** to `127.0.0.1:5052` so Unity can receive and act on it.
4. **`main.py`** ties everything together in a loop: capture → detect → classify → send → display.


---
## 👥 Authors

Project developed as part of a Unity course / racing game prototyping
