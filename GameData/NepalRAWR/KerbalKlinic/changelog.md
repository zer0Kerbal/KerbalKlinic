## Version 1.2.99.0

* 10 Jun 2022
* Kerbal Space Program 1.4.5

### Adoption

### Code

* Upgrade
  * Visual Studio
    * from VisualStudioVersion = 15.0.27130.2010
    * to VisualStudioVersion = 17.2.32526.322
  * KerbalKlinic.csproj
* Add
  * [Version.tt] 2.0.2.1
* Change
  * [KerbalKlinic.cs]
    * cleaning
    * linting
    * reorganizing
    * [Config]
      * rename "Stock price" to "StockPrice"
    * readonly
      * readonly string RelPath
      * readonly ConfigNode Konf
    * private
      * private Vector2 scrollPosition;
      * private Rect KlinicWindow = new Rect(200, 200, 100, 100);
      * private bool ButtonPress = false;
      * private int ToolbarINT = 0;
      * private ProtoCrewMember SelectedKerb;
      * private bool isVisible
    * simplify
      * private Rect KlinicWindow
        * old: new Rect(200, 200, 100, 100);
        * new: new (200, 200, 100, 100);
      * Texture2D texture
        * old: new Texture2D(36, 36, TextureFormat.RGBA32, false);
        * new: new Texture2D(36, 36, TextureFormat.RGBA32, false);


### Documentation

* Create
  * [changelog.md]
  * [KerbalKlinic.version]
  * [Readme.md]

---

## Version 1.2.0 for Kerbal Space Program 1.4.1

* Released on 2018-03-16
* compatible with KSP 1.4.1

---

## Version 1.1.0 for Kerbal Space Program 1.3.1

* Released on 2018-01-06
* *No changelog provided*

---
