# UIStackManager_Unity

How to use?

1. Add a UIStackManager component to the object that will manage the ui.
2. Create an prefab in a folder. ```YourProject/Assets/Resources/yourPrefabPath.```
3. Check your ```UIStackManager::defaultPageCode```
4. Play.
5. Instantiate your defaultPageCode prefab.

How to change from main page to another page.

 in source code
 ```c#
 UIStackManager.instance.TryOpenPage(yourPageCode)
 ```
 
 in canvas button
 Canvas/Button add OpenPage component. write your page code.
