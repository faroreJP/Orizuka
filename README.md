# About
C# library for unity3d.  
It provides simple object reference registry.

# Installation
1. Open project on unity editor, then export `Assets/Orizuka` as `orizuka.unity3d`
1. Import `orizuka.unity3d` to your project.

# Usage
Get some asset  
```cs
public TextAsset GetTextAssetFromRegistry (IObjectRegistry objectRegistry) {
  return objectRegistry.Get<TextAsset>("someAsset");
}
```
