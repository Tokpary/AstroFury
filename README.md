# AstroFury

> *A high-performance Bullet Hell shooter built as a technical showcase for advanced architectural design patterns.*

This project is a 2D bullet hell shooter developed primarily to demonstrate the implementation of industry-standard software design patterns. The core engineering objective was to achieve strict memory optimization and minimize Garbage Collection (GC) spikes, ensuring a stable frame rate while handling hundreds of concurrent entities (projectiles, enemies, and particles) on screen.

## 📐 Architecture & Design Patterns

The codebase is structured around SOLID principles, utilizing specific design patterns to solve common performance bottlenecks in high-density gameplay:

* **Object Pooling:** Completely eliminates runtime memory allocation and deallocation overhead. High-frequency entities such as bullets and enemies are pre-instantiated at startup and recycled, neutralizing GC spikes during dense wave generation.
* **Flyweight:** Optimizes RAM consumption by decoupling intrinsic data (base stats, shared sprite references, trajectory algorithms) from extrinsic data (current position, current health). Hundreds of active projectiles reference a single shared data object rather than duplicating data.
* **State Pattern (FSM):** Implemented for managing both the global application state (Menu, Active Wave, Upgrade Phase) and the deterministic behavior of enemy AI, ensuring clean and scalable state transitions.
* **Observer:** Decouples the core gameplay loop from secondary systems. Entity destruction broadcasts events that the UI, scoring, and resource management systems listen to, preventing tight coupling between the physics/health systems and the UI.
* **Singleton:** Used restrictively for overarching global managers (e.g., GameManager, ObjectPoolManager) to provide centralized access points for core services without cluttering the dependency graph.

## ⚙️ Core Technical Features

* **High-Density Entity Management:** Custom update loops optimized to handle massive amounts of moving objects without relying on expensive physics callbacks.
* **Modular Weapon System:** Allows for seamless runtime swapping of weapon behaviors and projectile types.
* **Progression & Resource System:** An event-driven upgrade system where collected resources dynamically scale player attributes between waves.

## 🚀 Installation & Build

1. Download the latest compiled build from the [Releases page](https://tokpary.itch.io/astrofury).
2. Extract the `.zip` file into a local directory.
3. Run `AstroFury.exe`.

## 🕹️ Input Mapping

| Action | Input |
| :--- | :--- |
| **Movement** | `W A S D` |
| **Fire** | `Left Mouse Button` |
| **Aim** | `Mouse` |
| **Pause** | `Escape` |

## 🛠️ Tech Stack

* **Engine:** [Unity]
* **Language:** C# 
* **Development Focus:** Clean Architecture, Memory Management, Performance Profiling.

## 👤 Developer

**[Tokpary]** 
* [GitHub](https://github.com/tokpary)
