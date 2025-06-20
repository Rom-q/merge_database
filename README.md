# This application is built for very specific, narrow use cases

## Features

1. **Compare two music databases** and create a third one based on file creation date and format priority.
2. **Generate a large test database** that fits all expected conditions.
3. **Count the number of files** in any directory recursively.

To get started, download the [latest release](https://github.com/Rom-q/merge_database/releases/tag/v1.0).

---

## Conditions

- All files must have a **unique ID** in the format: `XXXX_000_000` (e.g., `MUSC_001_005`)
- Only the following formats are supported:
  - `.wav`, `.flac`, `.alac`, `.aac`, `.ogg`, `.mp3`
- Format priority (highest to lowest):
  - `.wav` > `.flac` > `.alac` > `.aac` > `.ogg` > `.mp3`

---

## Logs

After merging, logs are saved in the `logs` folder:
- `add.txt` — new files added
- `change.txt` — existing files updated
- `delete.txt` — skipped files (lower priority)

