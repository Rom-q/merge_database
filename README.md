# Music Database Merger

**A flexible tool for merging two *~music~* databases with user-defined rules and format priorities.**

## Features

- Compare two music databases and create a third one based on:
  - File format priority
  - File creation date
  - Unique ID
- Fully customizable settings:
  - Supported formats (up to 6)
  - Format priority order
  - Toggle checks: ID, extension, duplicate, and date
- Generate a synthetic test database with all required conditions
- Recursively count files in any directory and size of files
- Maintain detailed logs of all actions

## Settings Overview

The application includes a graphical interface (Windows Forms) where you can:

- Define up to 6 file formats (e.g., `.flac`, `.mp3`, etc.)
- Choose the priority of each format (first is highest)
- Enable or disable:
  - ID comparison
  - Extension verification
  - Duplicate checking
  - Date-based replacement

## Requirements

- Files must contain a **unique ID** in the format: `XXXX_000_000`  *(or no)*
  
  Example: `MUSC_001_005`
- Only formats defined in the settings will be considered 
- If two files have the same ID, the one with the higher-priority format will be used *(or no)*
- If both files have the same format, the newer one (by creation date) will be selected *(or no)*

## Logs

After the merging process, log files are saved in the `logs` folder:

- `log.txt` - detailed log
- `add.txt` — files added to the merged database
- `change.txt` — files replaced due to higher priority or newer date
- `delete.txt` — skipped files (e.g., duplicates or lower priority)

## Getting Started

Download the latest version from the [Releases page](https://github.com/Rom-q/merge_database/releases/tag/v1.1)

- Command-line version: [`Program.cs`](https://github.com/Rom-q/merge_database/blob/main/music_database/Program.cs) **(OUTDATED)**
- Graphical interface version: [Forms version](https://github.com/Rom-q/merge_database/tree/main/forms_vers/test9.0)

### bye
