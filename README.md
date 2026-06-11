# ADOFAI Bugfixer

A mod for A Dance of Fire and Ice that fixes bugs in the game.

## Features & Bug Fixes

### 1. Tile Direction Icon Sync Fix
Fixed an issue in the level editor where rotating or flipping tiles using keyboard shortcuts (e.g., `Ctrl + ,`, `.`, `L`) would successfully update the tile's actual rotation data, but the visual direction indicator arrow would fail to update immediately, remaining at its previous angle.

* **How It Works:** By forcefully calling the UI refresh method immediately after a tile transformation is completed, the direction indicator is now instantly and accurately synchronized.

### 2. Fullscreen Mode & Settings Input Lock Fix
Fixed an issue where the game's default fullscreen toggle behaves inconsistently, and confirming the setting could accidentally switch the active settings tab due to an input processing overlap.

* **How It Works:** Overrides the default "fullscreen" behavior by implementing a custom state tracker and reliably saving the user's preference. Upon activation, it explicitly enforces the correct mode. Additionally, it forces to update to the current frame at the exact moment of confirmation, safely locking out any lingering arrow key inputs to prevent unintended tab switching.

## Installation

1. Download the latest `AdofaiBugfixer_vX.X.X.zip` from the [Releases](../../releases) page.
2. Launch Unity Mod Manager and open the **Mods** tab.
3. Drag and drop the downloaded `.zip` file into the "Drop zip files here" area at the bottom.
4. Run the game and check if the mod is successfully activated in the UMM.

## 📜 License
This project is licensed under the [MIT License](LICENSE). You are free to view, modify, and distribute the code.
