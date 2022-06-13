---
permalink: /ManualInstallation.html
title: Manual Installation
description: the flat-pack Kiea instructions, written in Kerbalese, unusally present
tags: installation,directions,page,kerbal,ksp,zer0Kerbal,zedK
---

<!-- ManualInstallation.md v1.1.7.0
KerbalKlinic (NRKK) created: 01 Oct 2019
updated: 18 Apr 2022 -->

<!-- based upon work by Lisias -->

# KerbalKlinic (NRKK)

[Home](./index.md)

`<BLURB>`

## Installation Instructions

### Using CurseForge/OverWolf app or CKAN

You should be all good! (check for latest version on CurseForge)

### If Downloaded from CurseForge/OverWolf manual download

To install, place the `NepalRAWR` folder inside your Kerbal Space Program's GameData folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**
  * Delete `<KSP_ROOT>/GameData/NepalRAWR/KerbalKlinic`
* Extract the package's `NepalRAWR/` folder into your KSP's GameData folder as follows:
  * `<PACKAGE>/NepalRAWR` --> `<KSP_ROOT>/GameData`
    * Overwrite any preexisting folder/file(s).
  * you should end up with `<KSP_ROOT>/GameData/NepalRAWR/KerbalKlinic`

### If Downloaded from SpaceDock / GitHub / other

To install, place the `GameData` folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**
  * Delete `<KSP_ROOT>/GameData/NepalRAWR/KerbalKlinic`
* Extract the package's `GameData` folder into your KSP's root folder as follows:
  * `<PACKAGE>/GameData` --> `<KSP_ROOT>`
    * Overwrite any preexisting file.
  * you should end up with `<KSP_ROOT>/GameData/NepalRAWR/KerbalKlinic`

## The following file layout must be present after installation

```markdown
<KSP_ROOT>
  + [GameData]
    + [NepalRAWR]
      + [KerbalKlinic]
        + [Agencies]
          ...
        + [Compatibility]
          ...
        + [Flags]
          ...
        + [Localization]
          ...
        + [Plugins]
          ...
        * #.#.#.#.htm
        * changelog.md
        * GPLv3.txt
        * readme.htm
        * KerbalKlinic.version
      ...
  * KSP.log
  ...
```

### Dependencies

* none
